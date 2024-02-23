using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private LevelGrid levelGrid;
    private Snake snake;

    private bool isPaused;

    private int levelWidth;
    private int levelHeight;

    private float foodTimer; // Temporizador para el cambio de posición de la comida
    private const float maxFoodTimer = 10f; // Tiempo máximo antes de cambiar la posición de la comida

    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance");
        }

        Instance = this;
    }

    private void Start()
    {
        SoundManager.CreateSoundManagerGameObject();

        // Obtener el tamaño del nivel seleccionado
        levelWidth = PlayerPrefs.GetInt("LevelWidth", 11);
        levelHeight = PlayerPrefs.GetInt("LevelHeight", 11);

        // Configuración de la cabeza de serpiente
        GameObject snakeHeadGameObject = new GameObject("Snake Head");
        SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.Instance.snakeHeadSprite;
        snake = snakeHeadGameObject.AddComponent<Snake>();

        // Configurar el LevelGrid con el tamaño seleccionado
        levelGrid = new LevelGrid(levelWidth, levelHeight);
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);

        // Inicializo el marcador de puntuación
        Score.InitializeStaticScore();

        isPaused = false;

        // Ajustar la escala del fondo para que coincida con el tamaño del mapa
        GameObject backgroundObject = GameObject.Find("Background"); // Nombre del objeto vacío del fondo
        if (backgroundObject != null)
        {
            backgroundObject.transform.localScale = new Vector3(levelWidth, levelHeight, 1f);
        }
    }

    private void Update()
    {
        // Lógica de pausa con la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        // Incrementar el temporizador de la comida
        foodTimer += Time.deltaTime;

        // Verificar si ha pasado el tiempo límite y si hay una comida en escena
        if (foodTimer >= maxFoodTimer && levelGrid.foodGameObject != null)
        {
            levelGrid.MoveFood(); // Cambiar la posición de la comida
            foodTimer = 0f; // Reiniciar el temporizador
        }
    }

    public void SnakeDied()
    {
        GameOverUI.Instance.Show(Score.TrySetNewHighScore());
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseUI.Instance.Show();
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseUI.Instance.Hide();
        isPaused = false;
    }
}


