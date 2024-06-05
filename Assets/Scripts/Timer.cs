using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private float m_Time;
    private float holdTime = 2.0f;
    private bool timerEnded = false;
    [SerializeField] private UnityEvent m_TimerEnd;

    public Light directionalLight;
    public GameObject head;
    public GameObject musicController;

    private Vector3 headGoal;

    private CycleController cycle;

    [SerializeField]
    private AudioSource ambient;

    public Material skybox;

    [SerializeField]
    private Material distortionM;

    private void Awake()
    {
        cycle = GameObject.FindGameObjectWithTag("Cycle").GetComponent<CycleController>();       
    }

    private void Start()
    {
        head.SetActive(false);
        headGoal = new Vector3(head.transform.position.x,head.transform.position.y + 40, head.transform.position.z);
        m_Time = cycle.cycleLenght;
        Debug.Log(m_Time);
   
    }

    void Update()
    {
        //Debug.Log(m_Time.ToString());

        if (!timerEnded)
        {
            TimePass();
        }

        if (timerEnded) {

            if (head.transform.position.y < headGoal.y) { MooseAppear(); }
            else if (holdTime > 0) { holdTime -= Time.deltaTime; }

            else { m_TimerEnd.Invoke(); }

        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space))
            TimerEnd();

    }

    private void TimerEnd()
    {
        Debug.Log("Timer end");
        RenderSettings.ambientIntensity = 0;
        RenderSettings.fog = false;
        directionalLight.enabled = false;
        head.SetActive(true);
        Instantiate(musicController);
        timerEnded = true;

    }

    private void MooseAppear()
    {
            head.transform.position = head.transform.position + Vector3.up * Time.deltaTime * 5f;
            distortionM.SetFloat("_LerpValue", 1/(headGoal.y - head.transform.position.y)*0.1f);       
    }

    private void TimePass()
    {
        if (m_Time > 0)
        {
            m_Time -= Time.deltaTime;
            var startColor = directionalLight.color;
            var startIntensity = directionalLight.intensity;
            directionalLight.color = Color.Lerp(startColor, Color.red, Time.deltaTime/cycle.cycleLenght);
            directionalLight.intensity = Mathf.Lerp(startIntensity, 0f, Time.deltaTime/cycle.cycleLenght);

            var startAudioVolume = ambient.volume;
            ambient.volume = Mathf.Lerp(startAudioVolume, 0, Time.deltaTime/cycle.cycleLenght);
        } 
        else
        {
            TimerEnd();
        }
    }
}
