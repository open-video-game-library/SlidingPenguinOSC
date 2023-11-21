using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // �q�I�u�W�F�N�g�̐����擾
        int childCount = transform.childCount;

        // �q�I�u�W�F�N�g�����Ɏ擾����
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;

            childObject.SetActive(ParameterManager.fish);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
