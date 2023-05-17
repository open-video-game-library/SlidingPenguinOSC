using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCInputManager : MonoBehaviour
{
    [System.NonSerialized]
    public Vector2 speed;

    [System.NonSerialized]
    public int acceleration;

    public void InputMovingSpeed(Vector3 inputSpeed)
    {
        speed.x = inputSpeed.x;
        speed.y = inputSpeed.y;
    }

    public void InputAcceleration(int inputAcceleration)
    {
        acceleration = inputAcceleration;
    }
}
