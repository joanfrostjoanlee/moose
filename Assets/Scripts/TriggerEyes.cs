using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TriggerEyes : MonoBehaviour
{
    [SerializeField]
    string eyeTag = "EyeLight";

    [SerializeField]
    Material eyeBlack;
    [SerializeField]
    Material eyeRed;

    GameObject[] eyes;
    Light[] lights;

    // Start is called before the first frame update
    void Awake()
    {
        eyes = GameObject.FindGameObjectsWithTag(eyeTag);

        for (int i =0; i < eyes.Length; i++)
        {
            eyes[i].GetComponent<MeshRenderer>().material = eyeBlack;
        }

        lights = new Light[eyes.Length];
        for (int i = 0; i < eyes.Length; i++)
        {
            lights[i] = eyes[i].GetComponent<Light>();
        }

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = false;
        }

    }

    
    void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < eyes.Length; i++)
            {
                eyes[i].GetComponent<MeshRenderer>().material = eyeRed;
            }
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].enabled = enabled;
            }

        }

    }
}
