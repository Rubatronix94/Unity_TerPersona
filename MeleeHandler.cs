using UnityEngine;

public class MeleeHandler : MonoBehaviour
{
    private InputManager _inputManager;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _inputManager = GetComponent<InputManager>();

        if (_inputManager == null)
        {
            Debug.LogError("InputManager not found on this GameObject.");
            enabled = false;
            return;
        }
    }

    private void OnEnable()
    {
        _inputManager.OnAttack1 += HandleAttack1;
        _inputManager.OnAttack2 += HandleAttack2;
    }

    private void OnDisable()
    {
        _inputManager.OnAttack1 -= HandleAttack1;
        _inputManager.OnAttack2 -= HandleAttack2;
    }

    private void HandleAttack1()
    {
        SetAttack(1);
    }

    private void HandleAttack2()
    {
        SetAttack(2);
    }

    private void SetAttack(int attackType)
    {
        if (_anim.GetBool("CanAttack"))
        {
            _anim.SetTrigger("Attack");
            _anim.SetInteger("AttackType", attackType);
        }
    }
}
