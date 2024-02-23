using UnityEngine;

public class LevelGrid 
{
    private Vector2Int foodGridPosition;
    //aix� era 0private
    public GameObject foodGameObject;

    public int width;
    public int height;

    private Snake snake;

    private float foodTimer; // Temporizador para el cambio de posici�n de la comida
    private const float maxFoodTimer = 5f; // Tiempo m�ximo antes de cambiar la posici�n de la comida

    private void Update()
    {
        // Incrementar el temporizador de la comida
        foodTimer += Time.deltaTime;

        // Verificar si ha pasado el tiempo l�mite y si hay una comida en escena
        if (foodTimer >= maxFoodTimer && foodGameObject != null)
        {
            MoveFood(); // Cambiar la posici�n de la comida
            foodTimer = 0f; // Reiniciar el temporizador
        }
    }

    //era privat
    public void MoveFood()
    {
        // Generar nueva posici�n para la comida
        Vector2Int newPosition;
        do
        {
            newPosition = new Vector2Int(
                Random.Range(-width / 2, width / 2),
                Random.Range(-height / 2, height / 2));
        } while (snake.GetFullSnakeBodyGridPosition().Contains(newPosition));

        // Mover la comida a la nueva posici�n
        foodGridPosition = newPosition;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);

        Debug.Log("Comida movida a: " + newPosition);
    }

    // M�todo para inicializar el nivel con su tama�o
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

    // M�todo para obtener el tama�o del mapa
    public Vector2Int GetMapSize()
    {
        return new Vector2Int(width, height);
    }

    // M�todo para configurar el nivel con la serpiente y la comida
    public void Setup(Snake snake)
    {
        this.snake = snake;
        SpawnFood();
    }

    // M�todo para intentar que la serpiente coma la comida
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

    // M�todo para generar la comida en una posici�n aleatoria
    private void SpawnFood()
    {
        // Generar una posici�n aleatoria para la comida que no est� ocupada por la serpiente
        do
        {
            foodGridPosition = new Vector2Int(
                Random.Range(-width / 2, width / 2),
                Random.Range(-height / 2, height / 2));
        } while (snake.GetFullSnakeBodyGridPosition().Contains(foodGridPosition));

        // Crear el objeto de comida y establecer su posici�n
        foodGameObject = new GameObject("Food");
        SpriteRenderer foodSpriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        foodSpriteRenderer.sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);
    }

    // M�todo para validar la posici�n dentro del mapa
    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {
        int w = Half(width);
        int h = Half(height);

        // Verificar si la posici�n se sale por la derecha o por arriba
        if (gridPosition.x > w)
        {
            gridPosition.x = -w;
        }
        if (gridPosition.y > h)
        {
            gridPosition.y = -h;
        }

        // Verificar si la posici�n se sale por la izquierda o por abajo
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

    // M�todo para dividir un n�mero por 2
    private int Half(int number)
    {
        return number / 2;
    }
}
