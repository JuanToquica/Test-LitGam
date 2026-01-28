using UnityEngine;

public class WeaponController : MonoBehaviour, IWeapon
{
    [SerializeField] private GravityGunData data;
    [SerializeField] private Transform firePoint;
    private Rigidbody rb;
    private float lastFireTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot()
    {
        if (Time.time < lastFireTime + data.cooldown) return;
        GameObject projectile = Instantiate(data.projectilePrefab, firePoint.position, firePoint.rotation);

        if (projectile.TryGetComponent(out Rigidbody projectileRb))
        {
            projectileRb.AddForce(firePoint.forward * data.launchForce, ForceMode.Impulse);
        }
        if (projectile.TryGetComponent(out GravityProjectile script))
        {
            script.Initialize(data.detectionRadius, data.centripetalAcceleration, data.orbitalSpeed, data.maxLifetime, data.affectedLayer);
        }
        lastFireTime = Time.time;
    }

    public void PickUp(Transform gripPoint)
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.None;

        transform.SetParent(gripPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.AddForce(transform.forward * 5, ForceMode.Impulse);
        rb.AddTorque(transform.forward * 0.05f, ForceMode.Impulse);
    }
}
