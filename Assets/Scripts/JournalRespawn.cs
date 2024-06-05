using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalRespawn : MonoBehaviour
{
    CycleController cycle;

    private bool triggered;
    
    void Start()
    {
        triggered = false;
        cycle = GameObject.FindGameObjectWithTag("Cycle").GetComponent<CycleController>();
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggered && this.gameObject.activeSelf)
        {
            List<int> info = cycle.getLastInfo();
            Debug.Log("info" + info.Count);
            for (int i = 0; i < info.Count; i++)
            {
                cycle.infoFound(info[i]);
            }
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
    }

}
