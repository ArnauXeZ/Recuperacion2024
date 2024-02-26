using UnityEngine;

public class LevelGrid
{
    // Position of the food on the grid
    private Vector2Int foodGridPosition;
    // Reference to the food GameObject
    public GameObject foodGameObject;

    // Width and height of the level grid
    public int width;
    public int height;

    // Reference to the snake object
    private Snake snake;

    // Timer for food position change
    private float foodTimer;
    // Maximum time before changing food position
    private const float maxFoodTimer = 5f;

    // Update method is called once per frame
    private void Update()
    {
        // Increment the food timer
        foodTimer += Time.deltaTime;

        // Check if it's time to change the food position and if there is food in the scene
        if (foodTimer >= maxFoodTimer && foodGameObject != null)
        {
            MoveFood(); // Change the position of the food
            foodTimer = 0f; // Reset the timer
        }
    }

    // Move the food to a new random position
    public void MoveFood()
    {
        // Generate a new position for the food that is not occupied by the snake
        Vector2Int newPosition;
        do
        {
            newPosition = new Vector2Int(
                Random.Range(-width / 2, width / 2),
                Random.Range(-height / 2, height / 2));
        } while (snake.GetFullSnakeBodyGridPosition().Contains(newPosition));

        // Move the food to the new position
        foodGridPosition = newPosition;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);

        Debug.Log("Food moved to: " + newPosition);
    }

    // Initialize the level with its size
    public void Initialize(int w, int h)
    {
        width = w;
        height = h;
    }

    // Constructor that accepts the width and height of the level
    public LevelGrid(int w, int h)
    {
        width = w;
        height = h;
    }

    // Method to get the size of the map
    public Vector2Int GetMapSize()
    {
        return new Vector2Int(width, height);
    }

    // Set up the level with the snake and the food
    public void Setup(Snake snake)
    {
        this.snake = snake;
        SpawnFood();
    }

    // Attempt to make the snake eat the food
    public bool TrySnakeEatFood(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            Score.AddScore(Score.POINTS);
            return true;
        }
        else
        {
            return false;
        }
    }

    // Spawn the food at a random position
    private void SpawnFood()
    {
        // Generate a random position for the food that is not occupied by the snake
        do
        {
            foodGridPosition = new Vector2Int(
                Random.Range(-width / 2, width / 2),
                Random.Range(-height / 2, height / 2));
        } while (snake.GetFullSnakeBodyGridPosition().Contains(foodGridPosition));

        // Create the food object and set its position
        foodGameObject = new GameObject("Food");
        SpriteRenderer foodSpriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        foodSpriteRenderer.sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);
    }

    // Validate the grid position within the map
    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {
        int w = Half(width);
        int h = Half(height);

        // Check if the position goes out from the right or top
        if (gridPosition.x > w)
        {
            gridPosition.x = -w;
        }
        if (gridPosition.y > h)
        {
            gridPosition.y = -h;
        }

        // Check if the position goes out from the left or bottom
        if (gridPosition.x < -w)
        {
            gridPosition.x = w;
        }
        if (gridPosition.y < -h)
        {
            gridPosition.y = h;
        }

        return gridPosition;
    }

    // Divide a number by 2
    private int Half(int number)
    {
        return number / 2;
    }
}

