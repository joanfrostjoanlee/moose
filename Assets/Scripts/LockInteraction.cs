using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockInteraction : MonoBehaviour
{
    private bool triggered;

    private CycleController cycle;
    private Animator animator;

    public Canvas key;
    public Canvas noKey;
    //public GameObject end;

    private float notifTime = 3.0f;

    private void Awake()
    {
        cycle = GameObject.FindGameObjectWithTag("Cycle").GetComponent<CycleController>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("startOpen", false);
        triggered = false;
        noKey.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggered && cycle.isKeyFound())
        {
            Debug.Log("key found");
            animator.SetBool("startOpen", true);
            key.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && triggered && !cycle.isKeyFound())
        {
            noKey.enabled = true;
        }

        if (noKey.enabled && notifTime > 0)
        {
            notifTime -= Time.deltaTime;
        } else if (noKey.enabled)
        {
            noKey.enabled = false;
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
