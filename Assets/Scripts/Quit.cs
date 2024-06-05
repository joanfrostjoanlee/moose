using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public Canvas canvas;
    
    private void Start()
    {
        canvas.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !canvas.enabled)
        {            
                canvas.enabled = true;
                MouseVisibilityController.instance.EnableMouse();          

        } 
        else if (Input.GetKeyDown(KeyCode.Escape) && canvas.enabled)
        {
            canvas.enabled = false;
            MouseVisibilityController.instance.DisableMouse();
        }

    }
}
