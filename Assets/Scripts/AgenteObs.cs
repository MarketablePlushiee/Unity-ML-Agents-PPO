// Importamos las bibliotecas necesarias
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.Collections;

// Esta clase hereda de Agent, lo que la convierte en un agente de ML-Agents
public class AgenteObs : Agent
{
    // Referencia al objeto objetivo (meta) que el agente debe alcanzar
    [SerializeField] private Transform _goal;

    // Referencia al plano o piso del escenario (para cambiarle color)
    [SerializeField] private Renderer _groundRenderer;

    // Velocidad de movimiento del agente
    [SerializeField] private float _moveSpeed = 1.5f;

    // Velocidad con la que gira el agente
    [SerializeField] private float _rotationSpeed = 180f;

    // Renderer del propio agente (para cambiar su color)
    private Renderer _renderer;

    // Variables públicas para ver información del entrenamiento
    [HideInInspector] public int CurrentEpisode = 0;
    [HideInInspector] public float CumulativeReward = 0f;

    // Color por defecto del suelo (para restaurarlo luego)
    private Color _defaultGroundColor;

    // Referencia a la corrutina activa (si hay una ejecutándose)
    private Coroutine _flashGroundCoroutine;

    // Método llamado una vez al inicio del entrenamiento
    public override void Initialize()
    {
        Debug.Log("Initialize()");

        _renderer = GetComponent<Renderer>();
        CurrentEpisode = 0;
        CumulativeReward = 0f;

        // Guardamos el color original del suelo
        if (_groundRenderer != null)
        {
            _defaultGroundColor = _groundRenderer.material.color;
        }
    }

    // Método que se ejecuta al comienzo de cada episodio
    public override void OnEpisodeBegin()
    {
        Debug.Log("OnEpisodeBegin()");

        // Si hay recompensa acumulada, mostramos un color (verde si ganó, rojo si falló)
        if (_groundRenderer != null && CumulativeReward != 0f)
        {
            Color flashColor = (CumulativeReward > 0f) ? Color.green : Color.red;

            // Si ya había una corrutina ejecutándose, la detenemos
            if (_flashGroundCoroutine != null)
            {
                StopCoroutine(_flashGroundCoroutine);
            }

            // Iniciamos una nueva corrutina para mostrar el color temporalmente
            _flashGroundCoroutine = StartCoroutine(FlashGround(flashColor, 3.0f));
        }

        // Reiniciamos datos del episodio
        CurrentEpisode++;
        CumulativeReward = 0f;
        _renderer.material.color = Color.blue;

        // Colocamos al agente y la meta en nuevas posiciones
        SpawnObjects();
    }

    // Corrutina para mostrar el color del suelo y restaurarlo luego de cierto tiempo
    private IEnumerator FlashGround(Color targetColor, float duration)
    {
        float elapsedTime = 0f;

        _groundRenderer.material.color = targetColor;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _groundRenderer.material.color = Color.Lerp(targetColor, _defaultGroundColor, elapsedTime / duration);
            yield return null;
        }
    }

    // Método que posiciona al agente y al objetivo en lugares aleatorios
    private void SpawnObjects()
    {
        transform.localRotation = Quaternion.identity;
        transform.localPosition = new Vector3(0f, 0.15f, 0f);

        // Dirección aleatoria
        float randomAngle = Random.Range(0f, 360f);
        Vector3 randomDirection = Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward;

        // Distancia aleatoria
        float randomDistance = Random.Range(1f, 2.5f);

        // Posicionamos el objetivo
        Vector3 goalPosition = transform.localPosition + randomDirection * randomDistance;
        _goal.localPosition = new Vector3(goalPosition.x, 0.3f, goalPosition.z);
    }

    // Método donde el agente "observa" su entorno
    public override void CollectObservations(VectorSensor sensor)
    {
        // Normalizamos las posiciones para que estén entre -1 y 1 aprox.
        float goalPosX_normalized = _goal.localPosition.x / 5f;
        float goalPosZ_normalized = _goal.localPosition.z / 5f;
        float agenteposX_normalized = transform.localPosition.x / 5f;
        float agenteposZ_normalized = transform.localPosition.z / 5f;
        float agenteRotation_normalized = (transform.localRotation.eulerAngles.y / 360f) * 2f - 1f;

        // Agregamos las observaciones al sensor
        sensor.AddObservation(goalPosX_normalized);
        sensor.AddObservation(goalPosZ_normalized);
        sensor.AddObservation(agenteposX_normalized);
        sensor.AddObservation(agenteposZ_normalized);
        sensor.AddObservation(agenteRotation_normalized);
    }

    // Método usado para controlar manualmente el agente (modo heurístico)
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = 0; // Acción por defecto (no hacer nada)

        if (Input.GetKey(KeyCode.UpArrow))
            discreteActionsOut[0] = 1; // Avanzar

        else if (Input.GetKey(KeyCode.LeftArrow))
            discreteActionsOut[0] = 2; // Girar a la izquierda

        else if (Input.GetKey(KeyCode.RightArrow))
            discreteActionsOut[0] = 3; // Girar a la derecha
    }

    // Método que se ejecuta cuando el agente recibe acciones del modelo
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Movemos al agente según la acción recibida
        MoveAgent(actions.DiscreteActions);

        // Penalizamos ligeramente por cada paso para motivar eficiencia
        AddReward(-2f / MaxStep);

        // Actualizamos la recompensa acumulada
        CumulativeReward = GetCumulativeReward();
    }

    // Aplica el movimiento del agente según la acción
    public void MoveAgent(ActionSegment<int> act)
    {
        var action = act[0];

        switch (action)
        {
            case 1: // Avanzar
                transform.position += transform.forward * _moveSpeed * Time.deltaTime;
                break;
            case 2: // Girar a la izquierda
                transform.Rotate(0f, -_rotationSpeed * Time.deltaTime, 0f);
                break;
            case 3: // Girar a la derecha
                transform.Rotate(0f, _rotationSpeed * Time.deltaTime, 0f);
                break;
        }
    }

    // Si el agente toca la meta
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Meta"))
        {
            GoalReached();
        }
    }

    // Método que se ejecuta al alcanzar la meta
    private void GoalReached()
    {
        AddReward(1.0f); // Recompensa positiva por alcanzar el objetivo
        CumulativeReward = GetCumulativeReward();

        EndEpisode(); // Finaliza el episodio
    }

    // Si choca con una pared
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            AddReward(-0.05f); // Penalización

            if (_renderer != null)
            {
                _renderer.material.color = Color.red; // Feedback visual
            }
        }
    }

    // Si se mantiene en contacto con una pared
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            AddReward(-0.01f * Time.fixedDeltaTime); // Penalización continua
        }
    }

    // Cuando deja de tocar la pared
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
            if (_renderer != null)
            {
                _renderer.material.color = Color.blue; // Vuelve a su color original
            }
    }
}
