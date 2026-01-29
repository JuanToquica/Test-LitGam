using UnityEngine;

public abstract class WeaponControllerBase<T> : MonoBehaviour, IWeapon where T : WeaponDataBase
{
    [SerializeField] protected T data;
    [SerializeField] protected Transform firePoint;
    protected Rigidbody rb;
    protected float lastFireTime;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public abstract void Shoot();

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
