using UnityEngine;

public class PushRigidbody : MonoBehaviour
{
    [SerializeField] private PlayerMovementConfig config;
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb == null || rb.isKinematic) return;
        if (hit.moveDirection.y < -0.3f) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        rb.AddForce(pushDir * config.pushPower, ForceMode.Impulse);
    }
}
