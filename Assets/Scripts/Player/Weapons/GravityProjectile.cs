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
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb == null) continue;

            Vector3 direction = transform.position - hit.transform.position;

            if (!affectedObjects.Contains(rb))
            {
                ApplyOrbitalSpeed(rb, direction);
                affectedObjects.Add(rb);
            }

            //Apply Centripetal Acceleration           
            float distance = direction.magnitude;
            float attractionFactor = Mathf.Clamp(1 - (distance / detectionRadius), 0, 1);
            Vector3 finalCentripetalAcceleration = direction.normalized * centripetalAcceleration * attractionFactor;

            rb.AddForce(finalCentripetalAcceleration, ForceMode.Acceleration);
            processedObjects.Add(rb);
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

    private void ApplyOrbitalSpeed(Rigidbody rb, Vector3 direction)
    {
        rb.useGravity = false;

        Vector3 orbitAxis = transform.forward;
        Vector3 tangentialDir = Vector3.Cross(direction.normalized, orbitAxis);

        rb.AddForce(tangentialDir * orbitalSpeed, ForceMode.VelocityChange);
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
