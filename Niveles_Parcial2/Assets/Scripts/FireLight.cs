using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FireLight : MonoBehaviour
{
    Light _light;

    [SerializeField] Vector3 _offSet;
    [SerializeField] float _lightRate;
    [SerializeField] float _movementRate;
    [SerializeField] float _minIntensity;
    [SerializeField] float _maxIntensity;

    Vector3 _ogPos;
    Transform _transform;
    float _internalCD;
    float _internalLightCD;
    Vector3 _targetPos;
    float _targetIntensity;
    float _initialIntensity;

    public void Awake()
    {
        _light = GetComponent<Light>();
        _transform = transform;

        _internalCD = _movementRate;
        _internalLightCD = _lightRate;

        _ogPos = _transform.position;
        _targetPos = _transform.position;
        _initialIntensity = _light.intensity;
        _targetIntensity = _light.intensity;
    }

    public void Update()
    {
        _internalCD -= Time.deltaTime;
        _internalLightCD -= Time.deltaTime;

        if (_internalCD < 0 && _movementRate != 0)
        {
            _targetPos = _ogPos + GetRandomOffset();

            _internalCD = _movementRate;
        }
        Movement();

        if( _internalLightCD < 0 && _lightRate != 0)
        {
            _initialIntensity = _light.intensity;
            _targetIntensity = GetIntensity();

            _internalLightCD = _lightRate;
        }
        ChangeIntensity(_initialIntensity, _targetIntensity);
    }

    void Movement()
    {
        if (_offSet == Vector3.zero) return;

        var dir = _targetPos - _transform.position;

        _transform.position += dir.normalized * Time.deltaTime * (dir.magnitude/ _movementRate);
    }

    void ChangeIntensity(float initialInt, float targetInt)
    {
        if (_lightRate == 0) return;

        _light.intensity = Mathf.Lerp(targetInt, initialInt, _internalCD);
    }

    Vector3 GetRandomOffset() 
    {
        var x = Random.Range(-_offSet.x, _offSet.x);
        var y = Random.Range(-_offSet.y, _offSet.y);
        var z= Random.Range(-_offSet.z, _offSet.z);

        return new Vector3(x, y, z);
    }

    float GetIntensity()
    {
        return Random.Range(_minIntensity, _maxIntensity);
    }
}
