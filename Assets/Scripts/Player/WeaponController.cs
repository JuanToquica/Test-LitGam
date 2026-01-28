using UnityEngine;

public class WeaponController : MonoBehaviour, IWeapon
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot()
    {
        Debug.Log("Disparando");
    }

    public void PickUp(Transform gripPoint)
    {
        rb.isKinematic = true;
        rb.useGravity = false;

        transform.SetParent(gripPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(transform.forward * 5, ForceMode.Impulse);
    }
}
