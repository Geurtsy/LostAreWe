using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour {

    public List<Transform> _targets;

    public Vector3 _offset;
    public float _smoothTime;

    public float _minZoom;
    public float _maxZoom;
    public float _zoomLimiter;

    private Vector3 _velocity;
    private Camera _cam;

    private void Start()
    {
        _targets = new List<Transform>();
        _cam = GetComponent<Camera>();

        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            _targets.Add(player.transform);
        }
    }

    private void LateUpdate()
    {
        if(_targets.Count == 0)
            return;

        Move();
        Zoom();
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        transform.position = centerPoint + _offset;
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(_maxZoom, _minZoom, GetGreatestDistance() / _zoomLimiter);
        _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, newZoom, Time.deltaTime);
    }

    private float GetGreatestDistance()
    {
        if(_targets.Count == 1)
        {
            return 0.0f;
        }

        var bounds = new Bounds(_targets[0].position, Vector3.zero);
        for(int index = 0; index < _targets.Count; index++)
        {
            bounds.Encapsulate(_targets[index].position);
        }

        return bounds.size.x;
    }

    private Vector3 GetCenterPoint()
    {
        if(_targets.Count == 1)
        {
            return _targets[0].position;
        }

        var bounds = new Bounds(_targets[0].position, Vector3.zero);
        for(int index = 0; index < _targets.Count; index++)
        {
            bounds.Encapsulate(_targets[index].position);
        }

        return bounds.center;
    }
}
