using UnityEngine;

[CreateAssetMenu(fileName = "MortarGunData", menuName = "ScriptableObject/MortarGunData")]
public class MortarGunData : WeaponDataBase
{
    [Header ("Line Renderer Settings")]
    public int linePoints;
    public float timeBetweenPoints;

    [Header("Mortar Gun Settings")]
    [Range (0, 50)] public float launchAngle;
    [Min (0)] public float launchForce;    
    public LayerMask collisionMask;
}
