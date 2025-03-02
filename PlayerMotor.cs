using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public Transform Model;
    //We need a target object to lock to
    public Transform TargetLock;

    public Camera mainCamera;
    private Animator Anim;
    [Range(20f, 80f)] public float RotationSpeed = 20f;

    private Vector3 StickDirection;

    private bool EspadaEquipada = false;
    private bool ObjetivoFijado = false;
    void Start()
    {
        mainCamera = Camera.main;
        Anim = Model.GetComponent<Animator>();
    }

    // Update is called once per frame
       void Update()
       {
           // AQUI LEEMOS WASD input
        StickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
      //  ProcessMove(Vector2 input);

        //Handle rotation to face stick direction, relative to the camera
        if (ObjetivoFijado) HandleTargetLockedLocomotionRotation();
        else HandleStandardLocomotionRotation();

       }

       public void HandleStandardLocomotionRotation()
       {
                Vector3 rotationOffset = mainCamera.transform.TransformDirection(StickDirection);
                rotationOffset.y = 0; //solo nos preocupamos del left/right movemtn
                Model.forward += Vector3.Lerp(Model.forward, rotationOffset, Time.deltaTime * RotationSpeed);

    }
       public void HandleTargetLockedLocomotionRotation()
       {
                        //In this case the reference for rotation is the target, not the camera
                        //We´ll use another math trick--> targetposition-currentposition= vector direction from currentposition to target position
               Vector3 rotationOffset = TargetLock.transform.position - Model.position;
               rotationOffset.y = 0;
               Model.forward += Vector3.Lerp(Model.forward, rotationOffset, Time.deltaTime * RotationSpeed);
       }

    public void ProcessMove(Vector2 input)
    {
        // Conviertes el Vector2 en Vector3 para la lógica interna, si lo necesitas
        StickDirection = new Vector3(input.x, 0, input.y).normalized;

        if (ObjetivoFijado)
        {
            // Solo cuando el objetivo está fijado:
            Anim.SetFloat("Horizontal", StickDirection.x);
            Anim.SetFloat("Vertical", StickDirection.z);

            // Si quieres que Speed sea 0 en este modo, ponlo en 0
            Anim.SetFloat("Speed", 0f);
        }
        else
        {
            // Solo cuando NO hay objetivo fijado:
            Anim.SetFloat("Speed", Vector3.ClampMagnitude(StickDirection, 1).magnitude);

            // Si no necesitas Horizontal/Vertical en modo normal, ponlos en 0
            Anim.SetFloat("Horizontal", 0f);
            Anim.SetFloat("Vertical", 0f);
        }
    }

    public void EquiparEspada()
    {
        EspadaEquipada = !EspadaEquipada;
        Anim.SetBool("IsWeaponEquipped", EspadaEquipada);

        Anim.SetBool("CanAttack", EspadaEquipada);
    }

    public void FijarObjetivo()
    {
        // Alterna el estado de objetivo fijado
        ObjetivoFijado = !ObjetivoFijado;
        Anim.SetBool("IsTargetLocked", ObjetivoFijado);

        
    }
}
