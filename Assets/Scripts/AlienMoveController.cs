using UnityEngine;

public class AlienMoveController : MonoBehaviour
{

    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _runSpeed = 8f;

    [SerializeField] private float _turnSpeed = 100f;
    [SerializeField] private float _fastTurnSpeed = 200f;

    [SerializeField] private bool _isRootMotionned = false;
    [SerializeField] private Transform _rootCharacter;

    private AlienInputController _inputs;
    private ShootingController _shootingController;
    private CharacterController _controller;
    private Animator _animator;

    private float _angleVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputs = GetComponent<AlienInputController>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _shootingController = GetComponent<ShootingController>();

    }

    // Update is called once per frame
    void Update()
    {

        if (_isRootMotionned)
        {
            if (_inputs.Move.magnitude >= Mathf.Epsilon)
            {
                if (!_inputs.IsAiming)
                {
                    // Not aiming : Rotation
                    float targetAngle = Camera.main.transform.rotation.eulerAngles.y;
                    targetAngle += Mathf.Atan2(_inputs.Move.x, _inputs.Move.y) * Mathf.Rad2Deg;

                    float actualAngle = Mathf.SmoothDampAngle(_rootCharacter.eulerAngles.y, targetAngle, ref _angleVelocity, 0.25f);

                    _rootCharacter.rotation = Quaternion.Euler(0, actualAngle, 0);

                    float horizontalSpeed = _inputs.IsRunning ? _runSpeed : _walkSpeed;
                    _animator.SetFloat("Speed", _inputs.Move.magnitude * horizontalSpeed);
                    
                }
                else
                {
                    _animator.SetFloat("Strafe", _inputs.Move.x);
                    _animator.SetFloat("Speed", _inputs.Move.y * _walkSpeed);
                }
            }
            else
            {
                _animator.SetFloat("Strafe", 0f);
                _animator.SetFloat("Speed", 0f);
            }

            if (_inputs.IsAiming && _inputs.IsShooting && _shootingController.CanShoot)
            {
                _animator.SetBool("Shoots", true);
            }
            else
            {
                _animator.SetBool("Shoots", false);
            }
        }
        else
        {
            float turnSpeed = _inputs.IsRunning ? _fastTurnSpeed : _turnSpeed;
            transform.Rotate(Vector3.up, _inputs.Move.x * turnSpeed * Time.deltaTime);

            float horizontalSpeed = _inputs.IsRunning ? _runSpeed : _walkSpeed;
            _controller.SimpleMove(transform.forward * (_inputs.Move.y * horizontalSpeed));

            _animator.SetFloat("Speed", _controller.velocity.magnitude);
        }

    }
}
