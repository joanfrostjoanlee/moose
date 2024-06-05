using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class SigilTop : MonoBehaviour
{
    private MeshRenderer[] topFigures;
    private int ind;

    [SerializeField]
    private List<int> correctOrder = new List<int> { 14, 6, 2, 8 };
    private List<int> guessOrder;

    [SerializeField]
    private UnityEvent<int> complete;

    private void Awake()
    {
        topFigures = transform.GetComponentsInChildren<MeshRenderer>();
        guessOrder = new List<int>();
        ind = 0;
        for (int i = 0; i < topFigures.Length; i++)
        {
            topFigures[i].enabled = false;
        }
    }

    public void AddSelection(int nr)
    {
        if (guessOrder.Count <= 3) 
        { 
            guessOrder.Add(nr);
        }

        if (guessOrder.Count == 4)
        {
            Invoke("CheckOrder",0.1f);
        }

    }

    private void CheckOrder()
    {
        correctOrder.Sort();
        guessOrder.Sort();

        if (correctOrder.All(guessOrder.Contains))
        {
            for (int i = 0; i < topFigures.Length; i++)
            {
                topFigures[i].enabled = true;
                Material mat = topFigures[i].GetComponent<MeshRenderer>().materials[0];
                mat.EnableKeyword("_SOLVED");
            }
            complete.Invoke(1);
            return;
        }

        else if (ind <= 3)
        {
            topFigures[ind].enabled = true;
            Material mat = topFigures[ind].materials[0];
            mat.DisableKeyword("_SOLVED");
            ind++;
            complete.Invoke(0);
            guessOrder.Clear();
            return;
        }
        else if (ind == 4)
        {
            Debug.Log("Game end");
            complete.Invoke(-1);
            return;
        }
    }

}
