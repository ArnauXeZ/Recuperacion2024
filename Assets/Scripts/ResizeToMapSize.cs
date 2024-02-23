using UnityEngine;

public class ResizeToMapSize : MonoBehaviour
{
    [SerializeField] private LevelGrid levelGrid;
    [SerializeField] private GameObject backgroundObject;

    private void Start()
    {
        if (levelGrid != null && backgroundObject != null)
        {
            Vector2Int mapSize = levelGrid.GetMapSize();
            float width = mapSize.x;
            float height = mapSize.y;

            // Obtener el transform del objeto vacío del fondo
            Transform backgroundTransform = backgroundObject.transform;

            // Ajustar la escala del objeto vacío
            Vector3 newScale = new Vector3(width, height, 1f);
            backgroundTransform.localScale = newScale;
        }
        else
        {
            Debug.LogError("LevelGrid o backgroundObject no están asignados en el inspector.");
        }
    }
}




