using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Animator Anim;
    private Vector3 StickDirection;
    private bool SwordEquipped = false;

    void Start()
    {
        Anim = GetComponent<Animator>();
        if (Anim == null) Debug.LogError("No se encontró un componente Animator en el jugador.");
    }

    void Update()
    {
        // Rotar al jugador según la dirección de entrada
        if (StickDirection.magnitude > 0.1f) // Si hay movimiento
        {
            RotatePlayer();
        }
    }

    public void RotatePlayer()
    {
        // Calcula el ángulo hacia la dirección de entrada
        float targetAngle = Mathf.Atan2(StickDirection.x, StickDirection.z) * Mathf.Rad2Deg;

        // Aplica la rotación de manera suave
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    public void ProcessMove(Vector2 input)
    {
        // Convertir el input 2D en una dirección 3D
        StickDirection = new Vector3(input.x, 0, input.y).normalized;

        // Actualizar los parámetros del Animator
        Anim.SetFloat("Speed", StickDirection.magnitude);
    }

    public void EquiparEspada()
    {
        SwordEquipped = !SwordEquipped;
        Anim.SetBool("IsWeaponEquipped", SwordEquipped);
    }
}
