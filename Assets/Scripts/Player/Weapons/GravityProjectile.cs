using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GravityProjectile : ProjectileBase
{
    private float detectionRadius;
    private float centripetalAcceleration;
    private float orbitalSpeed;
    private LayerMask affectedLayer;
    private HashSet<Rigidbody> affectedObjects;
    private Rigidbody projectileRb;

    private void Awake()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

    public void Initialize(float radius, float acceleration, float orbitalSpeed, float lifetime, LayerMask layer)
    {
        detectionRadius = radius;
        centripetalAcceleration = acceleration;
        this.orbitalSpeed = orbitalSpeed;
        affectedLayer = layer;
        affectedObjects = new HashSet<Rigidbody>();

        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        AttractObjects();
    }

    private void AttractObjects()
    {
        HashSet<Rigidbody> processedObjects = new HashSet<Rigidbody>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, affectedLayer);

        foreach (Collider hit in colliders)
        {
            Rigidbody targetRb = hit.GetComponent<Rigidbody>();
            if (targetRb == null) continue;

            Vector3 direction = transform.position - hit.transform.position;
            float distance = direction.magnitude;
            float attractionFactor = Mathf.Clamp(1 - (distance / detectionRadius), 0, 1);

            if (!affectedObjects.Contains(targetRb))
            {
                ApplyOrbitalSpeed(targetRb, direction);
                affectedObjects.Add(targetRb);
            }

            // if the object is in oposite direction of the projectile, move the point of attraction forward to improve the
            // tracking of objects to the projectile
            float alignmentFactor = Vector3.Dot(transform.forward, direction.normalized);
            float attractionOffset = 0;
            if (alignmentFactor < -0.5f && projectileRb.linearVelocity.magnitude > 3)
            {                  
                attractionOffset = (detectionRadius / 4) * Mathf.Abs(alignmentFactor);
            }

            //Calculate Centripetal Acceleration 
            Vector3 adjustedDirection = (direction + transform.forward * attractionOffset).normalized; 
            Vector3 finalCentripetalAcceleration = adjustedDirection * centripetalAcceleration * attractionFactor;
            
            targetRb.AddForce(finalCentripetalAcceleration, ForceMode.Acceleration);

            processedObjects.Add(targetRb);
        }
        HashSet<Rigidbody> objectsThatExited = new HashSet<Rigidbody>(affectedObjects);
        objectsThatExited.ExceptWith(processedObjects);
        if (objectsThatExited.Count > 0)
        {
            foreach (Rigidbody rigidbody in objectsThatExited)
            {
                if (rigidbody != null)
                    rigidbody.useGravity = true;
                affectedObjects.Remove(rigidbody);
            }
        }
    }

    private void ApplyOrbitalSpeed(Rigidbody targetRb, Vector3 direction)
    {
        targetRb.useGravity = false;

        Vector3 orbitAxis = transform.forward;
        Vector3 tangentialDir = Vector3.Cross(direction.normalized, orbitAxis);

        targetRb.AddForce(tangentialDir * orbitalSpeed, ForceMode.VelocityChange);
    }

    private void OnDestroy()
    {
        foreach (Rigidbody rb in affectedObjects)
        {
            if (rb != null)
                rb.useGravity = true;
        }      
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
