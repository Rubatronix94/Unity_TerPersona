using System.Collections; using System.Collections.Generic; 
using UnityEngine; using UnityEngine.InputSystem; 

public class InputManager : MonoBehaviour
{
    private PlayerInputMap playerInput;
    private PlayerInputMap.OnFootActions onFoot;
    private PlayerMotor motor;

    void Awake()
    {
        playerInput = new PlayerInputMap();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();

        // Registra eventos
     onFoot.EquiparEspada.performed += ctx => motor.EquiparEspada();
        
    }

    void FixedUpdate()
    {
       motor.ProcessMove(onFoot.Movimiento.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}