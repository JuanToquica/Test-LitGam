using UnityEngine;

public interface IWeapon
{
    public void Shoot();
    public void PickUp(Transform gripPoint);
    public void Drop();
}
