using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "ScriptableObject/PlayerMovementConfig")]
public class PlayerMovementConfig : ScriptableObject
{
    public float moveSpeed;
    public float sensitivity;
    public float jumpHeight;
    public float gravity;
    [Range(0, 90)] public float maxViewAngle;
    public float pushPower;
}
