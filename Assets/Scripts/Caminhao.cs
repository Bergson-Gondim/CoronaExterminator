using UnityEngine;

public class Caminhao : MonoBehaviour
{
    public static Caminhao _caminhao;
    public Rigidbody _rb;
    public Transform _centerMass;
    public Transform _target;
    public Transform _motorista;
    public WheelCollider[] _wheelCollider;
    public VariableJoystick _joystick;
    public Transform[] _wheelMesh;
    public bool[] _wheelSteer;
    public bool[] _wheelTorque;
    public bool _correr;
    public int _torque;
    public int _steerAngle;
    [Header("Particle")]
    [Range(0, 100)]
    public float _tanque;
    public int _fluxo;
    public bool _pulverizar;
    public ParticleSystem _particleSystemFrontal;
    public ParticleSystem _particleSystemEsquerdo;
    public ParticleSystem _particleSystemDireito;
    [Header("Som")]
    public AudioSource _somMotor;
    // Start is called before the first frame update
    void Start()
    {
        _caminhao = this;
        _rb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().centerOfMass = _centerMass.localPosition;
        _tanque = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager._gameManager._gamePlay)
            return;
        Vector2 _inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _inputAxis = Vector2.ClampMagnitude(_inputAxis, 1);
        _inputAxis = new Vector2(_inputAxis.x + _joystick.Horizontal, _inputAxis.y + _joystick.Vertical);

        Vector3 _relativeVector = transform.InverseTransformPoint(new Vector3(transform.position.x + _inputAxis.x, transform.position.y, transform.position.z + _inputAxis.y));

        for (int i = 0; i < _wheelCollider.Length; i++)
        {
            _wheelCollider[i].GetWorldPose(out Vector3 pos, out Quaternion quat);
            _wheelMesh[i].position = pos;
            _wheelMesh[i].rotation = quat;
            //
            if (_wheelTorque[i] && GameManager._gameManager._dirigindo)
            {
                
                if (_relativeVector.z > 0)
                {
                    if (_correr)
                    {
                        if (transform.InverseTransformDirection(_rb.velocity).z < -1)
                        {
                            _wheelCollider[i].brakeTorque = _torque * _inputAxis.magnitude * 3;
                        }
                        else
                        {
                            _wheelCollider[i].brakeTorque = 0;
                            _wheelCollider[i].motorTorque = _torque * _inputAxis.magnitude * 3;
                        }
                    }
                    else
                    {
                        if (transform.InverseTransformDirection(_rb.velocity).z < -1)
                        {
                            _wheelCollider[i].brakeTorque = _torque * _inputAxis.magnitude;
                        }
                        else
                        {
                            _wheelCollider[i].brakeTorque = 0;
                            _wheelCollider[i].motorTorque = _torque * _inputAxis.magnitude;
                        }
                    }
                }
                else
                {
                    if (_correr)
                    {
                        if (transform.InverseTransformDirection(_rb.velocity).z > 1)
                        {
                            _wheelCollider[i].brakeTorque = -_torque * _inputAxis.magnitude * 3;
                        }
                        else
                        {
                            _wheelCollider[i].brakeTorque = 0;
                            _wheelCollider[i].motorTorque = -_torque * _inputAxis.magnitude * 3;
                        }
                    }
                    else
                    {
                        if (transform.InverseTransformDirection(_rb.velocity).z > 1)
                        {
                            _wheelCollider[i].brakeTorque = -_torque * _inputAxis.magnitude;
                        }
                        else
                        {
                            _wheelCollider[i].brakeTorque = 0;
                            _wheelCollider[i].motorTorque = -_torque * _inputAxis.magnitude;
                        }
                    }

                }
            }
            if (_wheelSteer[i] && GameManager._gameManager._dirigindo)
            {
                _wheelCollider[i].steerAngle = (_relativeVector.x / _relativeVector.magnitude) * _steerAngle;
            }
        }
        _somMotor.pitch = Mathf.Lerp(1, (1 + _inputAxis.magnitude * 5) + _rb.velocity.magnitude, Time.deltaTime);
        if (_pulverizar && GameManager._gameManager._dirigindo && _tanque > 0)
        {
            _tanque -= _fluxo * Time.deltaTime;
            UImanager._uimanager._caminahoTanque.value = _tanque / 100;

            if (!_particleSystemFrontal.isEmitting)
            {
                _particleSystemFrontal.Play();
                _particleSystemEsquerdo.Play();
                _particleSystemDireito.Play();
            }
        }
        else
        {
            _particleSystemFrontal.Stop();
            _particleSystemDireito.Stop();
            _particleSystemEsquerdo.Stop();
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (!GameManager._gameManager._gamePlay)
            return;
        if (other.tag == "PlataformaCaminhao")
        {
            if (_tanque < 100)
            {
                _tanque += Time.deltaTime / _fluxo;
                UImanager._uimanager._caminahoTanque.value = _tanque / 100;
            }
        }
    }
}
