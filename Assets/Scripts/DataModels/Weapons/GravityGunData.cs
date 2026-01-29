using UnityEngine;

[CreateAssetMenu(fileName = "GravityGunData", menuName = "ScriptableObject/GravityGunData")]
public class GravityGunData : WeaponDataBase
{
    [Header("Gravity Gun Settings")]
    [Min(0)] public float launchSpeed;
    public float detectionRadius;
    public float centripetalAcceleration;
    public float orbitalSpeed;
    public LayerMask affectedLayer;
}
