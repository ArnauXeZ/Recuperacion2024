using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance

    public GameObject backgroundObject; // Reference to the background object

    private LevelGrid levelGrid; // Reference to the level grid
    private Snake snake; // Reference to the snake

    private bool isPaused; // Flag to track if the game is paused

    private int levelWidth; // Width of the level
    private int levelHeight; // Height of the level

    private float foodTimer; // Timer for food position change
    private const float maxFoodTimer = 5f; // Maximum time before changing food position

    private void Awake()
    {
        // Singleton pattern to ensure there's only one instance of GameManager
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance");
        }

        Instance = this;
    }

    private void Start()
    {
        // Create the SoundManager GameObject
        SoundManager.CreateSoundManagerGameObject();

        // Get the selected level size
        levelWidth = PlayerPrefs.GetInt("LevelWidth", 11);
        levelHeight = PlayerPrefs.GetInt("LevelHeight", 11);

        // Create the snake head GameObject and set up its sprite
        GameObject snakeHeadGameObject = new GameObject("Snake Head");
        SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.Instance.snakeHeadSprite;
        snake = snakeHeadGameObject.AddComponent<Snake>(); // Add Snake component to the snake head GameObject

        // Set up the LevelGrid with the selected size
        levelGrid = new LevelGrid(levelWidth, levelHeight);
        snake.Setup(levelGrid); // Pass the levelGrid reference to the snake for setup
        levelGrid.Setup(snake); // Pass the snake reference to the levelGrid for setup

        // Initialize the score
        Score.InitializeStaticScore();

        isPaused = false;

        // Adjust the scale of the background to match the level size
        backgroundObject = GameObject.Find("Background"); // Find the background GameObject by name
        if (backgroundObject != null)
        {
            backgroundObject.transform.localScale = new Vector3(levelWidth, levelHeight, 1f);
        }
    }

    private void Update()
    {
        // Pause logic with the Escape key
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

        // Check if the current scene is the gameplay scene
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            // Increment the food timer
            foodTimer += Time.deltaTime;

            // Check if it's time to change the food position and if there is food in the scene
            if (foodTimer >= maxFoodTimer && levelGrid.foodGameObject != null)
            {
                levelGrid.MoveFood(); // Change the food position
                foodTimer = 0f; // Reset the timer
            }
        }
    }

    // Reset all saved values and the background scale
    public void ResetAllValues()
    {
        backgroundObject.transform.localScale = Vector3.one;
        PlayerPrefs.DeleteKey("LevelWidth");
        PlayerPrefs.DeleteKey("LevelHeight");
    }

    // Method called when the snake dies
    public void SnakeDied()
    {
        GameOverUI.Instance.Show(Score.TrySetNewHighScore());
    }

    // Method to pause the game
    public void PauseGame()
    {
        Time.timeScale = 0f; // Stop time to pause the game
        PauseUI.Instance.Show(); // Show the pause UI
        isPaused = true; // Set the paused flag to true
    }

    // Method to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume time
        PauseUI.Instance.Hide(); // Hide the pause UI
        isPaused = false; // Set the paused flag to false
    }
}



