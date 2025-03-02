using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputMap playerInput;
    private PlayerInputMap.OnFootActions onFoot;

    private PlayerMotor motor;
    private EscudoUp escudo; // Referencia al script EscudoUp
    private MeleeHandler meleeHandler;

    // ATAQUES
    public event System.Action OnAttack1;
    public event System.Action OnAttack2;

    private void Awake()
    {
        playerInput = new PlayerInputMap();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        escudo = GetComponent<EscudoUp>();
        meleeHandler = GetComponent<MeleeHandler>();

        if (motor == null || escudo == null || meleeHandler == null)
        {
            Debug.LogError("Missing required component(s) on the GameObject.");
            enabled = false;
            return;
        }

        // Configurar eventos de las acciones
        onFoot.EquiparEspada.performed += ctx => motor.EquiparEspada();
        onFoot.Bloqueando.performed += ctx => escudo.ActivarEscudo();
        onFoot.Attack1.performed += ctx => OnAttack1?.Invoke();
        onFoot.Attack2.performed += ctx => OnAttack2?.Invoke();


        onFoot.FijarObjetivo.performed += ctx => motor.FijarObjetivo();

    }

    private void FixedUpdate()
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
