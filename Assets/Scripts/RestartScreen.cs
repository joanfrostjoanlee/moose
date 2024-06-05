using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class RestartScreen : MonoBehaviour
{
    private Canvas canvas;
    private CycleController cycle;

    [SerializeField]
    private float timer = 5f;
    [SerializeField]
    private float textTimer = 1f;
    private float textTimerPass;
    private float timerPass;

    private Image background;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        cycle = GameObject.FindGameObjectWithTag("Cycle").GetComponent<CycleController>();
        canvas.enabled = false;
        timerPass = timer;
        textTimerPass = textTimer;  
        background = GetComponentInChildren<Image>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.enabled = false;
        background.color = new Color(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (textTimerPass < 0)
        {
           textMeshPro.enabled = true;
        }

        if (timerPass > 0)
        {
            if (cycle.getIndex() > 0)
            {
                canvas.enabled = true;
                timerPass -= Time.deltaTime;
                textTimerPass -= Time.deltaTime;
                var alpha = Mathf.Lerp(0, 1, timerPass/timer);    
                background.color = new Color(0,0,0,alpha);
            }
        } else
        {
            canvas.enabled = false;
        }
        
    }
}
