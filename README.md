# 🧠 Autonomous Agent Navigation via Reinforcement Learning (PPO)

A Deep Reinforcement Learning (DRL) proof of concept developed with **Unity ML-Agents Toolkit** and **Python**. An autonomous agent is trained using the **Proximal Policy Optimization (PPO)** algorithm to navigate dynamic obstacle environments, optimize trajectories, and reach targets safely.

---

## 🛠️ Tech Stack & Frameworks

* **Environment & Simulation:** Unity 3D (C#)
* **AI Framework:** Unity ML-Agents Toolkit (v2.0+)
* **Machine Learning Library:** PyTorch / Python 3.9+
* **Algorithm:** Proximal Policy Optimization (PPO)
* **Telemetry & Metrics:** TensorBoard

---

## 📐 Architecture & Reward Shaping

The agent interacts with the environment using continuous vector observations and continuous action spaces.

* **Vector Observations:** Agent position, velocity, raycast distance measurements to obstacles, and direction vector toward the target.
* **Reward Function Architecture (R):**
  * +1.0 for successfully reaching the target goal.
  * -0.001 per step time penalty to encourage optimal pathfinding.
  * -0.5 heavy collision penalty when contacting obstacles or walls.

---

## 📊 Telemetry & Performance Analysis (TensorBoard)

The training progress was tracked over **1,000,000 environment steps**. The telemetry graphs illustrate clear convergence and policy optimization:

| Metric | Initial Value | Final Value | Interpretation |
| :--- | :--- | :--- | :--- |
| **Cumulative Reward** | ~ -2.0 | **+0.92** | Continuous improvement in goal acquisition and collision avoidance. |
| **Episode Length** | ~ 650 steps | **<55 steps** | Agent learned the shortest and fastest trajectory toward the goal. |
| **Value / Policy Loss** | Unstable | **Stabilized** | PPO policy convergence without gradient explosion or mode collapse. |

### 📈 Convergence Graphs

![TensorBoard Reward](tb-1.png)
*Figure 1: Cumulative Reward trajectory reaching optimization threshold around 800k steps.*

![TensorBoard Episode Length](tb-2.png)
*Figure 2: Drastic reduction in episode length as optimal navigation strategy is mastered.*

---

## 🚀 How to Run & Inference

### 1. Requirements
* Unity 2022.3 LTS or higher
* Python 3.9+
* Anaconda or venv virtual environment

### 2. Environment Setup
```bash
git clone https://github.com/MarketablePlushiee/Unity-ML-Agents-PPO.git
cd Unity-ML-Agents-PPO
pip install mlagents
```

### 3. Training
To start training the agent with the custom YAML configuration:
```bash
mlagents-learn config/OBS.yaml --run-id=PPO_Agent_Run_01
```

---

## 👨‍💻 Developer Contact

* **Developer:** Ivan Latrille Brito (ivan.latrille1@gmail.com)
* **GitHub:** @MarketablePlushiee
* **Portfolio:** marketableplushiee.github.io/Portafolio

---
---

# 🧠 Navegación de Agentes Autónomos mediante Aprendizaje por Refuerzo (PPO)

Una prueba de concepto de Aprendizaje por Refuerzo Profundo (DRL) desarrollada con **Unity ML-Agents Toolkit** y **Python**. Un agente autónomo es entrenado utilizando el algoritmo **Proximal Policy Optimization (PPO)** para navegar en entornos con obstáculos dinámicos, optimizar trayectorias y llegar a los objetivos de manera segura.

---

## 🛠️ Tecnologías y Marcos de Trabajo

* **Entorno y Simulación:** Unity 3D (C#)
* **Framework de IA:** Unity ML-Agents Toolkit (v2.0+)
* **Biblioteca de Machine Learning:** PyTorch / Python 3.9+
* **Algoritmo:** Proximal Policy Optimization (PPO)
* **Telemetría y Métricas:** TensorBoard

---

## 📐 Arquitectura y Modelado de Recompensas (Reward Shaping)

El agente interactúa con el entorno utilizando observaciones vectoriales continuas y espacios de acción continuos.

* **Observaciones Vectoriales:** Posición del agente, velocidad, mediciones de distancia por raycast hacia obstáculos y vector de dirección hacia el objetivo.
* **Arquitectura de la Función de Recompensa (R):**
  * +1.0 por alcanzar exitosamente la meta objetivo.
  * -0.001 de penalización por tiempo en cada paso para fomentar una búsqueda de ruta óptima.
  * -0.5 de penalización severa por colisión al hacer contacto con obstáculos o paredes.

---

## 📊 Análisis de Telemetría y Rendimiento (TensorBoard)

El progreso del entrenamiento fue monitoreado durante **1,000,000 de pasos en el entorno**. Los gráficos de telemetría ilustran una clara convergencia y optimización de la política:

| Métrica | Valor Inicial | Valor Final | Interpretación |
| :--- | :--- | :--- | :--- |
| **Recompensa Acumulada** | ~ -2.0 | **+0.92** | Mejora continua en la adquisición de objetivos y evasión de colisiones. |
| **Duración del Episodio** | ~ 650 pasos | **<55 pasos** | El agente aprendió la trayectoria más corta y rápida hacia la meta. |
| **Pérdida de Valor/Política** | Inestable | **Estabilizada** | Convergencia de la política PPO sin explosión de gradientes o colapso de modos. |

### 📈 Gráficos de Convergencia

![TensorBoard Reward](tb-1.png)
*Figura 1: Trayectoria de recompensa acumulada alcanzando el umbral de optimización alrededor de los 800k pasos.*

![TensorBoard Episode Length](tb-2.png)
*Figura 2: Reducción drástica en la duración del episodio a medida que se domina la estrategia de navegación óptima.*

---

## 🚀 Cómo Ejecutar e Inferir

### 1. Requisitos
* Unity 2022.3 LTS o superior
* Python 3.9+
* Entorno virtual Anaconda o venv

### 2. Configuración del Entorno
```bash
git clone https://github.com/MarketablePlushiee/Unity-ML-Agents-PPO.git
cd Unity-ML-Agents-PPO
pip install mlagents
```

### 3. Entrenamiento
Para iniciar el entrenamiento del agente con la configuración YAML personalizada:
```bash
mlagents-learn config/OBS.yaml --run-id=PPO_Agent_Run_01
```

---

## 👨‍💻 Contacto del Desarrollador

* **Desarrollador:** Ivan Latrille Brito (ivan.latrille1@gmail.com)
* **GitHub:** @MarketablePlushiee
* **Portafolio:** marketableplushiee.github.io/Portafolio
