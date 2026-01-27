using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObject/Input/InputReader")]
public class InputReader : ScriptableObject, PlayerInput.IPlayerActions
{
    public Action<Vector2> moveEvent;
    public Action<Vector2> lookEvent;
    public Action attackEvent;
    public Action interactEvent;
    public Action jumpEvent;

    private PlayerInput input;

    private void OnEnable()
    {
        if (input == null)
        {
            input = new PlayerInput();
            input.Player.SetCallbacks(this);
        }
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext ctx) => moveEvent?.Invoke(ctx.ReadValue<Vector2>());
    public void OnLook(InputAction.CallbackContext ctx) => lookEvent?.Invoke(ctx.ReadValue<Vector2>());
    public void OnAttack(InputAction.CallbackContext ctx) { if (ctx.performed) attackEvent?.Invoke(); }
    public void OnInteract(InputAction.CallbackContext ctx) { if (ctx.performed) interactEvent?.Invoke(); }
    public void OnJump(InputAction.CallbackContext ctx) { if (ctx.performed) jumpEvent?.Invoke(); }
}
