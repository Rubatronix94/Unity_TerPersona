using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EscudoUp : MonoBehaviour
{

    private PlayerInputMap playerInput;
    private PlayerInputMap.OnFootActions OnFoot;//<-PRUEBA 1

   //AUTO INPUT MANaGER private InputManager inputManager; // Referencia al InputManager
    private InputAction botonXAction; // Acción específica para el botón Bloqueando
    private bool botonPulsado = false; // Estado del botón

    private Animator animator; // Referencia al Animator
    private string animatorBoolParameter = "IsShieldUp"; // Nombre del parámetro booleano en el Animator

    void Awake()
    {
        playerInput = new PlayerInputMap();
        OnFoot = playerInput.OnFoot;

        // Obtén la acción "Bloqueando" desde el InputManager
        botonXAction = OnFoot.Bloqueando; // Asegúrate de que Bloqueando exista en el esquema
        if (botonXAction == null)
        {
            Debug.LogError("No se encontró la acción 'Bloqueando' en el esquema de entrada.");
            return;
        }

        // Configura los eventos para detectar cuándo el botón es presionado o soltado
        botonXAction.performed += ctx => botonPulsado = true;
        botonXAction.canceled += ctx => botonPulsado = false;

        // Obtén el componente Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró un componente Animator en este GameObject.");
        }
    }

    void Update()
    {
        if (animator != null)
        {
            // Actualiza el parámetro del Animator según el estado del botón
            animator.SetBool(animatorBoolParameter, botonPulsado);
        }
    }
   

}
