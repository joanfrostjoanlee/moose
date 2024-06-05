using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class JournalSigil : MonoBehaviour
{
    private Image img;
    [SerializeField]
    public int index;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void Enable()
    {
        img.enabled = true;
    }

    public void Disable()
    {
        img.enabled = false;
    }

}
