using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
   private CycleController cycleController;

    private void Awake()
    {
        cycleController = GameObject.FindGameObjectWithTag("Cycle").GetComponentInChildren<CycleController>();
    }

    public void GameEnd()
    {
        cycleController.NewCycle();
    }
}
