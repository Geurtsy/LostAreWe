using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    Movement.SpeedState CurrentSpeedState { get; set; }

    void Move(Vector2 dir);
}
