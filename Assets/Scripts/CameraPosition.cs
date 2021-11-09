using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraPosition : MonoBehaviour
{
    public static CameraPosition _cameraPosition;
    public int _speed;
    public Transform _target;    
    [Header("Menu")]    
    public Vector3 _positionOffsetMenu;
    public Vector3 _rotationMenu;
    [Header("GamePlay")]
    public Vector3 _positionOffset;
    public Vector3 _rotationGamePlay;
    // Start is called before the first frame update
    void Start()
    {
        _cameraPosition = this;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        //_target = GameObject.FindGameObjectWithTag("Caminhao").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager._gameManager._gamePlay)
        {
            CameraGamePlay();
        }
        else
        {
            CameraMenu();
        }
    }

    void CameraMenu()
    {
        if (_target != null)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position + _positionOffsetMenu, _speed * Time.deltaTime);
            Quaternion _rotate = Quaternion.Euler(_rotationMenu);
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotate, _speed * Time.deltaTime);
        }
    }
    void CameraGamePlay()
    {
        if (_target != null)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position + _positionOffset, _speed * Time.deltaTime);
            Quaternion _rotate = Quaternion.Euler(_rotationGamePlay);
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotate, _speed * Time.deltaTime);
        }
    }
}
