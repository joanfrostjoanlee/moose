using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Sigil : MonoBehaviour
{
    [SerializeField]
    private int nr;
    private Material mat;

    public UnityEvent<int> sigilEvent = new UnityEvent<int>();

    [SerializeField]
    private StonePuzzleInteraction spi;

    [SerializeField]
    private LayerMask sigilLayer;

    private Color selected = new Color(11, 133, 88);

    private void Awake()
    {
        mat = GetComponent<MeshRenderer>().materials[0];
    }

    private void Update()
    {
        if (spi.state == StonePuzzleInteraction.PuzzleState.PUZZLE)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Selection();
            }
        }
    }


    public void ChangeAll(int state)
    {
        if (state <= 0)
        {
            Debug.Log("Reset");
            mat.SetColor("_EmissionColor", Color.white); ;
        } else if (state == 1)
        {
            mat.SetColor("_EmissionColor", selected);
        }
    }

    private void Selection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, sigilLayer)) 
        {
            Sigil sigil;
            hit.transform.TryGetComponent(out sigil);
            if (this == sigil)
            {
                sigilEvent.Invoke(nr);
                mat.SetColor("_EmissionColor", selected);
            }
           
        }
    }

}
