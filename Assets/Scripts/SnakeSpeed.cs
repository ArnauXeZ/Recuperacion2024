using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpeed : MonoBehaviour
{


    public float moveSpeed = 1f; // Velocidad de movimiento de la serpiente
    private const float minMoveSpeed = 0.1f; // Velocidad de movimiento m�nima
    private const float maxMoveSpeed = 2.0f; // Velocidad de movimiento m�xima
    


    private void Update()
    {
        // L�gica para cambiar la velocidad de la serpiente
        if (Input.GetKeyDown(KeyCode.Q)) // Tecla +
        {
            IncreaseSpeed();
        }
        else if (Input.GetKeyDown(KeyCode.E)) // Tecla -
        {
            DecreaseSpeed();
        }
    }

    private void IncreaseSpeed()
    {
        // Incrementar la velocidad de movimiento si no excede el m�ximo
        if (moveSpeed < maxMoveSpeed)
        {
            moveSpeed += 0.1f;
            //moveSpeed = Mathf.Min(moveSpeed + speedChangeAmount, maxMoveSpeed);
        }
    }

    private void DecreaseSpeed()
    {
        // Decrementar la velocidad de movimiento si no cae por debajo del m�nimo
        if (moveSpeed > minMoveSpeed)
        {
            moveSpeed -= 0.1f;
            //moveSpeed = Mathf.Max(moveSpeed - speedChangeAmount, minMoveSpeed);
        }
    }
}
