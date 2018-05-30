using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RBManualMovement : Movement, IMove {

    [Header("Movement")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    //References.
    private Rigidbody2D _rb;

    private void Start()
    {
        CurrentSpeedState = SpeedState.STILL;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    }

    public override void Move(Vector2 input)
    {
        Vector2 forceToAdd = Vector2.zero;

        switch (CurrentSpeedState)
        {
            case SpeedState.STILL:
                _rb.velocity = Vector3.zero;
                break;

            case SpeedState.WALK:
                forceToAdd = new Vector2(GetForce(_walkSpeed, input.x, _rb.velocity.x), 
                    GetForce(_walkSpeed, input.y, _rb.velocity.y)) * Time.deltaTime;
                break;

            case SpeedState.RUN:
                forceToAdd = new Vector2(GetForce(_runSpeed, input.x, _rb.velocity.x),
                    GetForce(_runSpeed, input.y, _rb.velocity.y)) * Time.deltaTime;
                break;
        }

        _rb.AddForce(forceToAdd);
    }

    private float GetForce(float speed, float input, float vel)
    {
        float step = 0.5f;

        if (input > 0.0f)
            step = _rb.velocity.x <= 0.0f ? 1.0f : 1.0f - input * vel / _walkSpeed / 2.0f;
        else if (input < 0.0f)
            step = _rb.velocity.x >= 0.0f ? 0.0f : Mathf.Abs(input * vel / _walkSpeed / 2.0f);

        return Mathf.Lerp(-speed, speed, step);
    }
}
