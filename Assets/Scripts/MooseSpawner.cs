using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MooseSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Moose;

    [SerializeField]
    private Transform[] spawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnLocations != null)
        {
            Instantiate(Moose, spawnLocations[Random.Range(0, spawnLocations.Length)]);
        } 
    }


    
}
