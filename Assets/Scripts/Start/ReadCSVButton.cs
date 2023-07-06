using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadCSVButton : MonoBehaviour
{
    [SerializeField]
    private ParameterReader parameterReader;

    private Button readCSVButton;

    // Start is called before the first frame update
    void Start()
    {
        readCSVButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        readCSVButton.interactable = !ParameterManager.playConsecutively;
    }

    public void OnClickButton()
    {
        parameterReader.SetParameters(1);
    }
}
