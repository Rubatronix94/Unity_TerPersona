using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EscudoUp : MonoBehaviour
{
    private InputAction botonXAction;
    private bool botonPulsado = false;

    private Animator animator;
    private string animatorBoolParameter = "IsShieldUp";

    // Aseg�rate de tener una referencia al PlayerInputMap
    public PlayerInputMap playerInput;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("No se encontr� un componente Animator en el GameObject.");
        }

        // Aqu� inicializas el PlayerInputMap
        playerInput = new PlayerInputMap();
        botonXAction = playerInput.OnFoot.Bloqueando; // Usa la acci�n directamente

        if (botonXAction == null)
        {
            Debug.LogError("No se encontr� la acci�n 'Bloqueando' en el esquema de entrada.");
            return;
        }
        botonXAction.performed += ctx => { botonPulsado = true; ActivarEscudo(); Debug.Log("Boton presionado."); };
        botonXAction.canceled += ctx => { botonPulsado = false; ActivarEscudo(); Debug.Log("Boton soltado."); };

        // Recuerda habilitar el mapa de acciones
        playerInput.OnFoot.Enable();
    }

    // M�todo p�blico para activar o desactivar el escudo
    public void ActivarEscudo()
    {
        if (animator != null)
        {
            Debug.Log("Activando escudo con valor: " + botonPulsado);
            animator.SetBool(animatorBoolParameter, botonPulsado);
        }
    }

    void OnDestroy()
    {
        // No olvides deshabilitar el mapa de acciones cuando ya no se necesite
        playerInput.OnFoot.Disable();
    }
}
