using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, IA_Input.IPlayerActions
{
    public IA_Input IA_Input { get; private set; }
    public Vector2 mouseInput { get; private set; }
    public Vector2 moveInput { get; private set; }

    public void OnEnable()
    {
        if(IA_Input == null)
        {
            IA_Input = new IA_Input();
            IA_Input.Player.Enable();

            IA_Input.Player.SetCallbacks(this);
        }
    }

    public void OnDisable()
    {
        IA_Input.Player.Disable();
        IA_Input.Player.RemoveCallbacks(this);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
