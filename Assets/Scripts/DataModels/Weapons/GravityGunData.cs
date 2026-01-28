using UnityEngine;

[CreateAssetMenu(fileName = "GravityGunData", menuName = "ScriptableObject/GravityGunData")]
public class GravityGunData : ScriptableObject
{
    public float cooldown;
    public float launchForce;
    public GameObject projectilePrefab;
    public float detectionRadius;
    public float centripetalAcceleration;
    public float orbitalSpeed;
    public float maxLifetime;
    public LayerMask affectedLayer; 
}
