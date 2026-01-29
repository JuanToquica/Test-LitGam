using UnityEngine;

public class WeaponDataBase : ScriptableObject
{
    [Header ("General")]
    public GameObject projectilePrefab;    
    public float cooldown;    
    public float maxLifetime;   
}
