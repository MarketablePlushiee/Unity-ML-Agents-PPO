// Importamos la biblioteca de Unity necesaria para trabajar con MonoBehaviour y GUI
using UnityEngine;

// Esta clase se adjunta a un objeto en la escena para mostrar información del entrenamiento en pantalla
public class GUI_ENTRENAMIENTO : MonoBehaviour
{
    // Referencia al agente del que queremos mostrar los datos
    [SerializeField] private AgenteObs _agenteObs;

    // Estilos de texto para mostrar información en pantalla (colores y tamaños)
    private GUIStyle _defaultStyle = new GUIStyle();
    private GUIStyle _positiveStyle = new GUIStyle();
    private GUIStyle _negativeStyle = new GUIStyle();

    // Método que se llama una vez al iniciar la escena
    void Start()
    {
        // Estilo por defecto: color amarillo
        _defaultStyle.fontSize = 20;
        _defaultStyle.normal.textColor = Color.yellow;

        // Estilo para recompensa positiva: color verde
        _positiveStyle.fontSize = 20;
        _positiveStyle.normal.textColor = Color.green;

        // Estilo para recompensa negativa: color rojo
        _negativeStyle.fontSize = 20;
        _negativeStyle.normal.textColor = Color.red;
    }

    // Método que se llama automáticamente por Unity para renderizar GUI
    private void OnGUI()
    {
        // Texto con el número de episodio actual y número de pasos realizados
        string debugEpisode = "Episodio: " + _agenteObs.CurrentEpisode + " - Pasos: " + _agenteObs.StepCount;

        // Texto con la recompensa acumulada en el episodio actual
        string debugReward = "Recompensa: " + _agenteObs.CumulativeReward.ToString();

        // Elegimos el estilo según si la recompensa acumulada es positiva o negativa
        GUIStyle rewardStyle = _agenteObs.CumulativeReward < 0 ? _negativeStyle : _positiveStyle;

        // Mostramos en pantalla el texto de episodio con el estilo por defecto
        GUI.Label(new Rect(20, 20, 500, 30), debugEpisode, _defaultStyle);

        // Mostramos en pantalla la recompensa con el estilo correspondiente
        GUI.Label(new Rect(20, 60, 500, 30), debugReward, rewardStyle);
    }

    // Este método se llama en cada frame, pero en este caso está vacío
    void Update()
    {

    }
}
