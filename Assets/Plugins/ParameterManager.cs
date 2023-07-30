using UnityEngine;

public class ParameterManager : MonoBehaviour
{
    // game parameters
    public static float sensitivity = 3.0f;
    public static int limitedTime = 120;
    public static float maximumSpeed = 15.0f;
    public static float acceleration = 0.050f;
    public static float friction = 0.9990f;

    // experiment parameters
    public static bool gameEffect = true;
    public static bool respawn = true;
    public static bool playConsecutively = true;
    public static float waitTimeNext = 5.0f;
    public static bool shareHaptic = false;
}
