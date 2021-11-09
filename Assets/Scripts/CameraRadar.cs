using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRadar : MonoBehaviour
{
    public static CameraRadar _cameraRadar;
    public Transform _target;
    public Vector3 _startPos;
    public float _sizeDots;
    public List<Transform> _radarDots;
    // Start is called before the first frame update
    void Start()
    {
        _cameraRadar = this;
        _startPos = transform.position;
        _target = GameObject.FindGameObjectWithTag("Player").transform;


    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _target.position+_startPos;
        for (int i = 0; i < _radarDots.Count; i++)
        {
            _radarDots[i].localScale = new Vector3(_sizeDots,_sizeDots,_sizeDots);
        }
    }
}
