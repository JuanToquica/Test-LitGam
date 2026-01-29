using UnityEngine;

public abstract class WeaponControllerBase : MonoBehaviour
{
    public abstract void Shoot();
    protected abstract bool CanShoot();
    public abstract void PickUp(Transform gripPoint);
    public abstract void Drop();
}

public abstract class WeaponControllerBase<T> : WeaponControllerBase where T : WeaponDataBase
{
    [SerializeField] protected T data;
    [SerializeField] protected Transform firePoint;
    protected Rigidbody rb;
    protected float lastFireTime;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override abstract void Shoot();

    protected override bool CanShoot()
    {
        if (Time.time < lastFireTime + data.cooldown) return false;

        Vector3 origin = Camera.main.transform.position;
        Vector3 target = firePoint.position;
        Vector3 direction = target - origin;
        float distance = direction.magnitude;

        return !Physics.Raycast(origin, direction, distance, 1 << 12); //Environment layer
    }

    public override void PickUp(Transform gripPoint)
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.None;

        transform.SetParent(gripPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public override void Drop()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.AddForce(transform.forward * 5, ForceMode.Impulse);
        rb.AddTorque(transform.forward * 0.05f, ForceMode.Impulse);
    }
}
