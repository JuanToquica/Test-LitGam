using UnityEngine;

public class GravityGunController : WeaponControllerBase<GravityGunData>
{
    public override void Shoot()
    {
        if (!CanShoot()) return;
        GameObject projectile = Instantiate(data.projectilePrefab, firePoint.position, firePoint.rotation);

        if (projectile.TryGetComponent(out Rigidbody projectileRb))
        {
            projectileRb.AddForce(firePoint.forward * data.launchSpeed, ForceMode.VelocityChange);
        }
        if (projectile.TryGetComponent(out GravityProjectile script))
        {
            script.Initialize(data.detectionRadius, data.centripetalAcceleration, data.orbitalSpeed, data.maxLifetime, data.affectedLayer);
        }
        lastFireTime = Time.time;
    }
}
