using UnityEngine;



[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public static Player _player;
    
    public VariableJoystick _joystick;
    [Header("Character Controller")]
    public CharacterController _characterController;
    public float WalkingSpeed = 6.0f;
    public float RunSpeed = 6.0f;
    public bool _correr = false;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    [Header("Saúde")]
    public float _vida;
    public float _resistencia;
    [Header("Pulverizar")]
    public bool _pulverizar;
    public ParticleSystem _particleSystem;
    [Header("Animação")]
    public Animator _animator;
    public float _speed;
    public bool _aim;

    private Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        _player = this;
        _particleSystem = transform.GetComponentInChildren<ParticleSystem>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("Iddle", true);
        //_particleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {   
        Vector2 _inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _inputAxis = Vector2.ClampMagnitude(_inputAxis, 1);
        _inputAxis = new Vector2(_inputAxis.x + _joystick.Horizontal, _inputAxis.y + _joystick.Vertical);
        
        if (_characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            moveDirection = new Vector3(_inputAxis.x, 0, _inputAxis.y);

            if (!_correr)
            {
                moveDirection *= WalkingSpeed;
            }
            else
            {
                moveDirection *= RunSpeed;
            }

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        //Rotate

        if (_inputAxis.magnitude != 0)
        {
            Vector3 _rotation = new Vector3(_inputAxis.x, 0, _inputAxis.y);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_rotation), .5f);
        }

        // Move the controller
        if (!_pulverizar)
        {
            _characterController.Move(moveDirection * Time.deltaTime);
            _aim = false;
        }
        else
        {
            _aim = true;
        }
        //menu ou gameplay
        if (!GameManager._gameManager._gamePlay)
        {
            _aim = true;
        }
        else
        {
            _aim = false;
        }

        //Animação
        float _verticalFloat = _inputAxis.y;
        float _horizontalFloat = _inputAxis.x;

        _animator.SetFloat("Speed", _speed);
        _animator.SetLayerWeight(1, 0);

        if (Mathf.Abs(transform.TransformDirection(_characterController.velocity).z) > 0 && !_pulverizar)
        {
            //_pulverizar = false;

            _animator.SetBool("Iddle", false);
            _animator.SetLayerWeight(1, _inputAxis.magnitude);
            if (!_correr)
            {
                if (_aim)
                {
                    _animator.SetBool("Walking", false);
                    _animator.SetBool("Aim", true);
                    _animator.SetBool("Run", false);
                }
                else
                {
                    
                    _animator.SetBool("Walking", true);
                    _animator.SetBool("Aim", false);                    
                    _animator.SetBool("Run", false);
                }
                _animator.SetBool("Run", false);
            }
            else
            {
                _animator.SetBool("Walking", false);
                _animator.SetBool("Run", true);
                _animator.SetBool("Aim", false);
                
            }
        }
        else
        {            
            if (_aim)
            {
                _animator.SetBool("Iddle", false);
                _animator.SetBool("Aim", true);
            }
            else
            {
                _animator.SetBool("Iddle", true);
                _animator.SetBool("Aim", false);
            }
            _animator.SetBool("Walking", false);
            _animator.SetBool("Run", false);
        }

        //Pulverização

        if (_pulverizar)
        {   
            _animator.SetBool("Aim", true);
            _animator.SetBool("Iddle", false);
            if (!_particleSystem.isEmitting)
            {
                _particleSystem.Play();
            }
        }
        else
        {   
            _animator.SetBool("Aim", false);
            _animator.SetBool("Iddle", true);
            _particleSystem.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Caminhao")
        {
            Caminhao._caminhao._motorista = this.transform;
            transform.position += Vector3.back;
            transform.SetParent(Caminhao._caminhao.transform);
            CameraPosition._cameraPosition._target= GameObject.FindGameObjectWithTag("Caminhao").transform;
            CameraRadar._cameraRadar._target= GameObject.FindGameObjectWithTag("Caminhao").transform;
            GameManager._gameManager._dirigindo = true;
            UImanager._uimanager._sairVeiculo.SetActive(true);
            Caminhao._caminhao._somMotor.Play();
            this.gameObject.SetActive(false);
        }
    }
}

