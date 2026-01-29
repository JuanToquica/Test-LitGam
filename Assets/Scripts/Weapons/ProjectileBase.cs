using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public virtual void Initialize(float lifetime)
    {
        Destroy(gameObject, lifetime);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
