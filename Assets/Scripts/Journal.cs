using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Journal : MonoBehaviour
{
    private CycleController cycle;
    
    private List<int> list = new List<int>();

    private Canvas journal;

    private JournalSigil[] sigils;

    private void Awake()
    {
        journal = GameObject.FindGameObjectWithTag("Journal").GetComponent<Canvas>();
        cycle = GameObject.FindGameObjectWithTag("Cycle").GetComponent<CycleController>();
        sigils = journal.GetComponentsInChildren<JournalSigil>();
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sigils.Length; i++)
        {
            sigils[i].Disable();
        }
        journal.enabled = false;       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && journal.enabled == false){
            showInfo();
        } else if (Input.GetKeyDown(KeyCode.J) && journal.enabled)
        {
            journal.enabled = false;
        }

    }

    void showInfo()
    {
        list = cycle.getInfo();
        
        journal.enabled = true;

        Debug.Log("sigils" + sigils.Length);
        if (list.Count > 0)
        {
            for (int i = 0; i < sigils.Length; i++)
            {
               for (int j = 0; j <  list.Count; j++)
                {
                    if (sigils[i].index == list[j])
                    {
                        sigils[i].Enable();
                    }
                }
                
            }
        }
        
    }
}
