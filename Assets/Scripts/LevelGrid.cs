using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    private float foodTimer;
    private const float maxFoodTimer = 5f; // Tiempo máximo antes de cambiar la posición de la comida

    private int width;
    private int height;

    private Snake snake;
    
    private void Update()
    {
        if (foodGameObject != null)
        {
            // Incrementar el temporizador de la comida
            foodTimer += Time.deltaTime;

            // Verificar si ha pasado el tiempo límite
            if (foodTimer >= maxFoodTimer)
            {
                MoveFood(); // Cambiar la posición de la comida
                foodTimer = 0f; // Reiniciar el temporizador
            }
        }
    }

    private void MoveFood()
    {
        // Generar nueva posición para la comida
        Vector2Int newPosition;
        do
        {
            newPosition = new Vector2Int(
                Random.Range(-width / 2, width / 2),
                Random.Range(-height / 2, height / 2));
        } while (newPosition == snake.GetGridPosition());

        // Mover la comida a la nueva posición
        foodGridPosition = newPosition;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);
    }
    public void Initialize(int w, int h)
    {
        width = w;
        height = h;
    }
    // Constructor que acepta el ancho y la altura del nivel
    public LevelGrid(int w, int h)
    {
        width = w;
        height = h;
    }
    
    public Vector2Int GetMapSize()
    {
        return new Vector2Int(width, height);
    }

    public void Setup(Snake snake)
    {
        this.snake = snake;
        SpawnFood();
    }

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

    private void SpawnFood()
    {
        // while (condicion){
        // cosas
        // }

        // { cosas }
        // while (condicion)

        do
        {
            foodGridPosition = new Vector2Int(
                Random.Range(-width / 2, width / 2),
                Random.Range(-height / 2, height / 2));
        } while (snake.GetFullSnakeBodyGridPosition().IndexOf(foodGridPosition) != -1);

        foodGameObject = new GameObject("Food");
        SpriteRenderer foodSpriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        foodSpriteRenderer.sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {
        int w = Half(width);
        int h = Half(height);

        // Me salgo por la derecha
        if (gridPosition.x > w)
        {
            gridPosition.x = -w;
        }
        if (gridPosition.x < -w)
        {
            gridPosition.x = w;
        }
        if (gridPosition.y > h)
        {
            gridPosition.y = -h;
        }
        if (gridPosition.y < -h)
        {
            gridPosition.y = h;
        }

        return gridPosition;
    }

    private int Half(int number)
    {
        return number / 2;
    }
}

