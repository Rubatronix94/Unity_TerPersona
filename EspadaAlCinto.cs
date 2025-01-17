using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class EspadaAlCinto : MonoBehaviour
{
    //We will need an "action" enum
    public enum Action { Desenvainar, Unequip };

    public Transform Weapon;
    public Transform WeaponHandle; //FUNDA
    public Transform WeaponRestPose; //MANO

    public void ResetWeapon(Action action)
    {
        if (action == Action.Desenvainar)
        {
            Weapon.SetParent(WeaponHandle);
        }
        else
        {
            Weapon.SetParent(WeaponRestPose);
        }
        Weapon.localRotation = Quaternion.identity;
        Weapon.localPosition = Vector3.zero;
    }
}
