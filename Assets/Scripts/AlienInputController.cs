using UnityEngine;
using UnityEngine.InputSystem;

public class AlienInputController : MonoBehaviour
{
    
    private Vector2 _move;
    private bool _isRunning;
    private bool _isAiming;
    private bool _isShooting;

    public Vector2 Move => _move;
    public bool IsRunning => _isRunning;
    public bool IsAiming => _isAiming;
    public bool IsShooting => _isShooting;

    public void OnMove(InputAction.CallbackContext context) => _move = context.ReadValue<Vector2>();
    public void OnRun(InputAction.CallbackContext context) => _isRunning = context.ReadValueAsButton();
    public void OnAim(InputAction.CallbackContext context) => _isAiming = context.ReadValueAsButton();
    public void OnShoot(InputAction.CallbackContext context) => _isShooting = context.ReadValueAsButton();
    
}
