using UnityEngine;

[CreateAssetMenu(fileName = "MortarGunData", menuName = "ScriptableObject/MortarGunData")]
public class MortarGunData : WeaponDataBase
{
    [Header ("Line Renderer Settings")]
    public int linePoints;
    public float timeBetweenPoints;

    [Header("Mortar Gun Settings")]
    [Min(0)] public float launchForce;
    [Range (0, 90)] public float launchAngle;
    public LayerMask collisionMask;
}
