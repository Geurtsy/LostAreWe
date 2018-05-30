using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMove : SimpleMove {

    [Header("Homing")]
    [SerializeField] private LayerMask _layersToSeek;
    [SerializeField] private float _range;
    [SerializeField] private float _rotationSpeed;
    private Transform _target;


    // Use this for initialization
    private void Start ()
    {
        CheckSurroundings();
	}

    // Update is called once per frame
    private void Update ()
    {
        CheckSurroundings();
        
        if(_target != null)
        {
            CurrentSpeedState = SpeedState.WALK;
            RotateToFace();
            Move(GetTargetDirection());
        }
    }

    private void CheckSurroundings()
    {
        Collider2D[] _hitColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), _range, _layersToSeek);
        float _closestHitDis = 0.0f;

        if(_hitColliders.Length > 0)
        {

            for(int index0 = 0; index0 < _hitColliders.Length; index0++)
            {
                Collider2D selHit = _hitColliders[index0];
                if(_hitColliders[index0].tag == "Player")
                {
                    float selDis = (selHit.transform.position - transform.position).magnitude;

                    if(selDis > _closestHitDis)
                    {
                        _closestHitDis = selDis;
                        _target = selHit.transform;
                    }
                }
            }
        }
        else
        {
            _target = null;
            Halt();
        }

    }

    private Vector2 GetTargetDirection()
    {
        Vector3 dir = (_target.position - transform.position).normalized;
        return new Vector2(dir.x, dir.y);
    }

    private void RotateToFace()
    {
        Vector3 vectorToTarget = _target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _rotationSpeed);
    }
}
