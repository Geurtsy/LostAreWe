using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IMove
{
    public enum SpeedState
    {
        STILL,
        WALK,
        RUN,
    }

    public SpeedState CurrentSpeedState { get; set; }

    public virtual void Move(Vector2 dir)
    {
    }
}
