using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleMove : Movement, IMove
{
    public float _speed;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
    }

    public override void Move(Vector2 dir)
    {
        switch(CurrentSpeedState)
        {
            case SpeedState.STILL:
                _rb.velocity = Vector2.zero;
                break;

            case SpeedState.WALK:
                _rb.velocity = transform.right * _speed * Time.deltaTime;
                break;

            default:
                _rb.velocity = Vector2.zero;
                break;
        }
    }

    public void Halt()
    {
        CurrentSpeedState = SpeedState.STILL;
        _rb.velocity = Vector2.zero;
    }
}
