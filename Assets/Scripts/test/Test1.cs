using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Slider slider = GetComponentInChildren<Slider>(true);
            if (slider != null) slider.enabled = true;
            else Debug.Log("46");
        }
    }
}
