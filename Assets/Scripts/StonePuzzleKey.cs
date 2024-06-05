using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class StonePuzzleKey : MonoBehaviour
{
    public Canvas KeyFoundCanvas;

    private bool solved;
    private bool triggered;
    private CycleController cycleController;

    private void Awake()
    {
        KeyFoundCanvas.enabled = false;
        cycleController = GameObject.FindGameObjectWithTag("Cycle").GetComponent<CycleController>();
    }

    private void Update()
    {

        if (triggered && Input.GetKeyDown(KeyCode.E) && solved)
        {
            cycleController.KeyFound();
            KeyFoundCanvas.enabled = true;
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
    }

    public void CheckSolution(int solutionInd)
    {
        if (solutionInd == 1)
        {
            solved = true;
        }
    }

}
