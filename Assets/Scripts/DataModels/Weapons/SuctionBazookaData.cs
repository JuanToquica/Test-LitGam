using UnityEngine;

[CreateAssetMenu(fileName = "AcceleratorGunData", menuName = "ScriptableObject/AcceleratorGunData")]
public class SuctionBazookaData : WeaponDataBase
{
    [Header("Accelerator Gun Settings")]
    [Min(0)] public float launchSpeed;
    public float detectionRadius;
    public float explosionForce;
    public LayerMask affectedLayer;
}
