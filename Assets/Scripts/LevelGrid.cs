using UnityEngine;

public class LevelGrid 
{
    private Vector2Int foodGridPosition;
    //això era 0private
    public GameObject foodGameObject;

    public int width;
    public int height;

    private Snake snake;

    private float foodTimer; // Temporizador para el cambio de posición de la comida
    private const float maxFoodTimer = 5f; // Tiempo máximo antes de cambiar la posición de la comida

    private void Update()
    {
        // Incrementar el temporizador de la comida
        foodTimer += Time.deltaTime;

        // Verificar si ha pasado el tiempo límite y si hay una comida en escena
        if (foodTimer >= maxFoodTimer && foodGameObject != null)
        {
            MoveFood(); // Cambiar la posición de la comida
            foodTimer = 0f; // Reiniciar el temporizador
        }
    }

    //era privat
    public void MoveFood()
    {
        // Generar nueva posición para la comida
        Vector2Int newPosition;
        do
        {
            newPosition = new Vector2Int(
                Random.Range(-width / 2, width / 2),
                Random.Range(-height / 2, height / 2));
        } while (snake.GetFullSnakeBodyGridPosition().Contains(newPosition));

        // Mover la comida a la nueva posición
        foodGridPosition = newPosition;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);

        Debug.Log("Comida movida a: " + newPosition);
    }

    // Método para inicializar el nivel con su tamaño
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

    // Método para obtener el tamaño del mapa
    public Vector2Int GetMapSize()
    {
        return new Vector2Int(width, height);
    }

    // Método para configurar el nivel con la serpiente y la comida
    public void Setup(Snake snake)
    {
        this.snake = snake;
        SpawnFood();
    }

    // Método para intentar que la serpiente coma la comida
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

    // Método para generar la comida en una posición aleatoria
    private void SpawnFood()
    {
        // Generar una posición aleatoria para la comida que no esté ocupada por la serpiente
        do
        {
            foodGridPosition = new Vector2Int(
                Random.Range(-width / 2, width / 2),
                Random.Range(-height / 2, height / 2));
        } while (snake.GetFullSnakeBodyGridPosition().Contains(foodGridPosition));

        // Crear el objeto de comida y establecer su posición
        foodGameObject = new GameObject("Food");
        SpriteRenderer foodSpriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        foodSpriteRenderer.sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);
    }

    // Método para validar la posición dentro del mapa
    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {
        int w = Half(width);
        int h = Half(height);

        // Verificar si la posición se sale por la derecha o por arriba
        if (gridPosition.x > w)
        {
            gridPosition.x = -w;
        }
        if (gridPosition.y > h)
        {
            gridPosition.y = -h;
        }

        // Verificar si la posición se sale por la izquierda o por abajo
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

    // Método para dividir un número por 2
    private int Half(int number)
    {
        return number / 2;
    }
}
