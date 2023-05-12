using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PortNumber : MonoBehaviour
{
    [System.NonSerialized]
    public static int portNumber = 9000;

    [SerializeField]
    private TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.text = portNumber.ToString();
    }

    public void SetPortNumber()
    {
        portNumber = int.Parse(inputField.text);
    }
}