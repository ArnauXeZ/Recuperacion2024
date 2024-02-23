using UnityEngine;

public class ResizeToMapSize : MonoBehaviour
{
    private void Start()
    {
        // Encontrar el objeto con el script LevelGrid en la escena
        LevelGrid levelGrid = FindObjectOfType<LevelGrid>();

        // Verificar si se encontró un LevelGrid
        if (levelGrid != null)
        {
            // Obtener el tamaño del mapa desde el LevelGrid
            Vector2Int mapSize = levelGrid.GetMapSize();

            // Ajustar el tamaño del sprite al tamaño del mapa
            transform.localScale = new Vector3(mapSize.x, mapSize.y, 1f);
        }
        else
        {
            Debug.LogError("No LevelGrid found in the scene.");
        }
    }
}


