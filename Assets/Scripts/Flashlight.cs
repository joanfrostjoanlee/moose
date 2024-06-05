using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class Flashlight : MonoBehaviour
{
    public Light flashLight;
    //private Light flashLight;
    private bool lightOn;

    private void Awake()
    {
        flashLight = GetComponent<Light>();
        lightOn = false;
        flashLight.intensity = 0;
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }

    }

    private void ToggleFlashlight()
    {

        if (!lightOn)
        {
            flashLight.intensity = 20;
            lightOn = true;
        }
        else if (lightOn)
        {
            flashLight.intensity = 0;
            lightOn = false;
        }
    }
}
