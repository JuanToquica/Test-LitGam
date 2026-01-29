using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SuctionBazookaProjectile : ProjectileBase 
{   
    private float detectionRadius;
    private float explosionForce;
    private float projectileSpeed;
    private float projectileLifetime;
    private LayerMask affectedLayer;

    private HashSet<Rigidbody> affectedObjects = new HashSet<Rigidbody>();
    private HashSet<Rigidbody> processedObjects = new HashSet<Rigidbody>();

    public void Initialize(float speed, float radius, float explosionForce, float lifetime, LayerMask layer)
    {  
        this.explosionForce = explosionForce;
        projectileSpeed = speed;
        detectionRadius = radius;
        affectedLayer = layer;
        projectileLifetime = lifetime;

        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        AccelerateAffectedObjects();
    }

    private void AccelerateAffectedObjects()
    {
        Vector3 targetPoint = GetTargetPoint();
        
        processedObjects.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, affectedLayer);

        foreach (Collider hit in colliders)
        {
            Rigidbody targetRb = hit.attachedRigidbody;
            if (targetRb == null || targetRb.isKinematic) continue;
            
            if (!affectedObjects.Contains(targetRb))
            {
                targetRb.useGravity = false;
                affectedObjects.Add(targetRb);
            }

            ApplySuctionVelocity(targetRb, targetPoint);
            processedObjects.Add(targetRb);
        }
        RestoreExitedObjects();       
    }

    private Vector3 GetTargetPoint()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, projectileSpeed * projectileLifetime))
            return hit.point;

        return transform.position + transform.forward * projectileSpeed * projectileLifetime;
    }

    private void ApplySuctionVelocity(Rigidbody targetRb, Vector3 targetPoint)
    {
        Vector3 targetDirection = (targetPoint - targetRb.position).normalized;
        Vector3 targetVelocity = targetDirection * projectileSpeed;

        float accelerationStep = (projectileSpeed / 0.5f) * Time.fixedDeltaTime;

        targetRb.linearVelocity = Vector3.MoveTowards(targetRb.linearVelocity, targetVelocity, accelerationStep);
    }

    private void RestoreExitedObjects()
    {
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

    private void OnDestroy()
    {
        Explode();
    }

    private void Explode()
    {
        foreach (Rigidbody rb in affectedObjects)
        {
            if (rb == null) continue;

            Vector3 direction = (rb.position - transform.position).normalized;
            float distance = Vector3.Distance(rb.position, transform.position);
            float explosionIntensity = Mathf.Clamp01(1 - (distance / detectionRadius));

            rb.useGravity = true;
            rb.AddForce(direction * explosionForce * explosionIntensity, ForceMode.Impulse);           
        }
    }
}
