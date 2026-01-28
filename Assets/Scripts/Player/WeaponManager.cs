using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform gripPoint;
    [SerializeField] private float detectionDistance;
    [SerializeField] private LayerMask weaponLayer;

    private Camera _mainCamera;
    private IWeapon currentWeapon;
    private IWeapon detectedWeapon;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        inputReader.attackEvent += Shoot;
        inputReader.interactEvent += HandleInteract;
        inputReader.dropEvent += DropWeapon;
    }

    private void OnDisable()
    {
        inputReader.attackEvent -= Shoot;
        inputReader.interactEvent -= HandleInteract;
        inputReader.dropEvent -= DropWeapon;
    }

    private void Update()
    {
        DetectWeapon();
    }

    private void DetectWeapon()
    {
        Ray ray = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, detectionDistance, weaponLayer))
        {
            if (hit.collider.TryGetComponent(out IWeapon weapon))
            {
                detectedWeapon = weapon;
                return;
            }
        }
        detectedWeapon = null;
    }

    private void HandleInteract()
    {
        if (detectedWeapon != null)
        {
            EquipWeapon(detectedWeapon);
        }
    }

    private void EquipWeapon(IWeapon newWeapon)
    {
        if (newWeapon == currentWeapon) return;

        currentWeapon?.Drop();
        currentWeapon = newWeapon;
        currentWeapon.PickUp(gripPoint);
    }

    private void DropWeapon()
    {
        currentWeapon?.Drop();
        currentWeapon = null;
    }

    private void Shoot()
    {
        currentWeapon?.Shoot();
    }
}
