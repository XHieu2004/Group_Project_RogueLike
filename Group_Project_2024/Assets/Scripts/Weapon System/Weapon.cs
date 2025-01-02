using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponState{ONHAND, ONGROUND};
    public WeaponState state;
    public RuntimeAnimatorController animatorController;

}
