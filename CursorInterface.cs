using UnityEngine;

public class CursorInterface : MonoBehaviour
{
    public Animator animator;     // Referencia al Animator
    public Transform target;      // Referencia al objetivo a mirar
    public float rotationSpeed = 5f;  // Velocidad de giro

    void Start()
    {
        // Oculta el cursor
        Cursor.visible = false;
        // Bloquea el cursor al centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        // Verifica si el parámetro "istargetlock" es true y que target no sea nulo
        if (animator.GetBool("IsTargetLocked") && target != null)
        {
           //NO Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
           //VA transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

            transform.LookAt(target);
        }
    }
}
