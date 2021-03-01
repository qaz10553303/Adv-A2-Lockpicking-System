using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Gameplay : MonoBehaviour
{
    public GameObject chestCanvas;
    public GameObject pressHint;
    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)&&pressHint.activeSelf)
        {
            chestCanvas.SetActive(true);
            pressHint.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
