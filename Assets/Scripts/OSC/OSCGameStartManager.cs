using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscCore;

public class OSCGameStartManager : MonoBehaviour
{
    [System.NonSerialized]
    public bool start;

    private OscClient client = new OscClient("127.0.0.1", 9000);

    public void InputStartSignal(int inputStart)
    {
        Debug.Log(inputStart);
        start = inputStart == 1;
    }
}
