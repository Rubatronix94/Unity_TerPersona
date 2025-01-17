using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EscudoUp : MonoBehaviour
{

    private PlayerInputMap playerInput;
    private PlayerInputMap.OnFootActions OnFoot;//<-PRUEBA 1

   //AUTO INPUT MANaGER private InputManager inputManager; // Referencia al InputManager
    private InputAction botonXAction; // Acci�n espec�fica para el bot�n Bloqueando
    private bool botonPulsado = false; // Estado del bot�n

    private Animator animator; // Referencia al Animator
    private string animatorBoolParameter = "IsShieldUp"; // Nombre del par�metro booleano en el Animator

    void Awake()
    {
        playerInput = new PlayerInputMap();
        OnFoot = playerInput.OnFoot;

        // Obt�n la acci�n "Bloqueando" desde el InputManager
        botonXAction = OnFoot.Bloqueando; // Aseg�rate de que Bloqueando exista en el esquema
        if (botonXAction == null)
        {
            Debug.LogError("No se encontr� la acci�n 'Bloqueando' en el esquema de entrada.");
            return;
        }

        // Configura los eventos para detectar cu�ndo el bot�n es presionado o soltado
        botonXAction.performed += ctx => botonPulsado = true;
        botonXAction.canceled += ctx => botonPulsado = false;

        // Obt�n el componente Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontr� un componente Animator en este GameObject.");
        }
    }

    void Update()
    {
        if (animator != null)
        {
            // Actualiza el par�metro del Animator seg�n el estado del bot�n
            animator.SetBool(animatorBoolParameter, botonPulsado);
        }
    }
   

}
