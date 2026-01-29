using UnityEngine;

public class MortarGunController : WeaponControllerBase<MortarGunData>
{
    [SerializeField] private LineRenderer lineRenderer;
    private Vector3[] points;
    private Vector3 launchDirection;

    private void LateUpdate()
    {
        if (transform.parent != null)
        {
            DrawTrajectory();
        }
        else
        {
            if (lineRenderer.enabled) lineRenderer.enabled = false;
        }
    }

    private void DrawTrajectory()
    {
        if (!lineRenderer.enabled) lineRenderer.enabled = true;

        if (points == null || points.Length != data.linePoints)
        {
            points = new Vector3[data.linePoints];
        }
        lineRenderer.positionCount = data.linePoints;

        launchDirection = Quaternion.AngleAxis(-data.launchAngle, firePoint.right) * firePoint.forward;
        Vector3 startPosition = firePoint.position;
        Vector3 startVelocity = launchDirection * data.launchForce;

        Vector3 lastPosition = startPosition;
        for (int i = 0; i < data.linePoints; i++)
        {
            float time = i * data.timeBetweenPoints;
            Vector3 currentPoint = startPosition + startVelocity * time + 0.5f * Physics.gravity * time * time;
            
            //Detect obstacles
            Vector3 direction = currentPoint - lastPosition;
            float distance = direction.magnitude;

            if (distance > 0 && Physics.Raycast(lastPosition, direction.normalized, out RaycastHit hit, distance, data.collisionMask))
            {
                points[i] = hit.point;
                lineRenderer.positionCount = i + 1;
                break;
            }

            points[i] = currentPoint;
            lastPosition = currentPoint;
        }
        lineRenderer.SetPositions(points);
    }

    public override void Shoot()
    {
        if (!CanShoot()) return;
        GameObject projectile = Instantiate(data.projectilePrefab, firePoint.position, firePoint.rotation);

        if (projectile.TryGetComponent(out Rigidbody projectileRb))
        {
            projectileRb.AddForce(launchDirection * data.launchForce, ForceMode.Impulse);
        }

        lastFireTime = Time.time;
    }
}
