using Unity.Mathematics.Geometry;
using UnityEngine;

public class AlienMoveController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 8f;
    
    
    [SerializeField] private float normalTurnSpeed = 100f;
    [SerializeField] private float fastTurnSpeed = 200f;

    [SerializeField] private bool isRootMotioned = true;
    [SerializeField] private Transform rootCharacter;
    
    
    private AlienInputController _inputs;
    private CharacterController _controller;
    private Animator _animator;
    private float _idleTargetTime = 5f;
    private float _idleTimer;


    private float _angleVelocity;
    private float _velocity;
    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputs = GetComponent<AlienInputController>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isRootMotioned)
        {
            // float turnSpeed = _inputs.IsRunning ? fastTurnSpeed : normalTurnSpeed;
            // rootCharacter.Rotate(Vector3.up, _inputs.Move.x * turnSpeed * Time.deltaTime);

            if (_inputs.Move.magnitude > Mathf.Epsilon)
            {
                float targetAngle = Camera.main.transform.rotation.eulerAngles.y;
                targetAngle += Mathf.Atan2(_inputs.Move.x, _inputs.Move.y) * Mathf.Rad2Deg;


                float actualAngle = Mathf.SmoothDampAngle(rootCharacter.localRotation.eulerAngles.y, targetAngle, ref _angleVelocity, 0.25f);
                
                rootCharacter.rotation = Quaternion.Euler(0, actualAngle, 0);

               
            }

            if (_inputs.Move.magnitude >  Mathf.Epsilon && _inputs.IsRunning)
            {
                _animator.SetFloat("Speed", Mathf.SmoothDamp(_animator.GetFloat("Speed"), 2f, ref _velocity, 0.25f));
            }
            else
            {
                _animator.SetFloat("Speed", Mathf.SmoothDamp(_animator.GetFloat("Speed"), _inputs.Move.magnitude, ref _velocity, 0.1f));
            }
            
        }
    }
}
