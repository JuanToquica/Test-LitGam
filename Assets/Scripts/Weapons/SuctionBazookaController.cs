using UnityEngine;

public class SuctionBazookaController : WeaponControllerBase<SuctionBazookaData>
{
    public override void Shoot()
    {
        if (!CanShoot()) return;
        GameObject projectile = Instantiate(data.projectilePrefab, firePoint.position, firePoint.rotation);

        if (projectile.TryGetComponent(out Rigidbody projectileRb))
        {
            projectileRb.AddForce(firePoint.forward * data.launchSpeed, ForceMode.VelocityChange);
        }
        if (projectile.TryGetComponent(out SuctionBazookaProjectile script))
        {
            script.Initialize(data.launchSpeed, data.detectionRadius, data.explosionForce, data.maxLifetime, data.affectedLayer);
        }
        lastFireTime = Time.time;
    }
}
