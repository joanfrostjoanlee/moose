using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class MooseBehavior : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    private Animator animator;
   
    private Vector3[] goals = { new Vector3(64.4000015f, 1.80999994f, 75.6999969f), new Vector3(68.0999985f, 1.66999996f, 51.4000015f),
                                new Vector3(47.5999985f,1.38900006f,20.7999992f)};

    private int lastGoal;
    private bool settingGoal;
   
    [SerializeField]
    private float idleTime = 5;
    private float idleTimeSpent = 5;
    private new Collider collider;
    
    [SerializeField]
    private GameObject mooseSight;

    [SerializeField]
    Material distortionM;
    private float time;
    private float totalTime = 15f;
    private CycleController cycle;

    private PlayerMovement player;


    public enum MooseState
    {
        IDLE,
        WALKING,
        PURSUIT,
        CAUGHT
    }

    private MooseState mooseState;

    void Start()
    { 
        mooseSight.SetActive(true);
        mooseState = MooseState.IDLE;
        lastGoal = -1;

        agent = GetComponent("NavMeshAgent") as NavMeshAgent;
        settingGoal = false;

        distortionM.SetFloat("_LerpValue", 0f);
        time = totalTime;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player.enabled = true;
        cycle = GameObject.FindGameObjectWithTag("Cycle").GetComponent<CycleController>();
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(mooseState);
        switch (mooseState)
        {
            case MooseState.IDLE:
                agent.speed = 1.5f;
                agent.isStopped = true;
                Idle();
                break;
            case MooseState.WALKING:
                agent.speed = 1.5f;
                if (!settingGoal)
                {
                    agent.isStopped = false;
                    CheckArrival();
                }
                break;
            case MooseState.PURSUIT:
                agent.speed = 2.5f;
                if (!settingGoal)
                {
                    agent.isStopped = false;
                    CheckArrival();
                }
                break;
            case MooseState.CAUGHT:
                mooseSight.SetActive(false);
                player.speed = 0f;
                Debug.Log("Stopped");
                agent.isStopped = true;
                RotateTowards(collider.transform.position);
                End();
                animator.SetTrigger("Stop");
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collided moose b " + other.tag);
        if (other.CompareTag("Player")){
            mooseState = MooseState.CAUGHT;
            collider = other;
        }
    }

    private void RotateTowards(Vector3 position)
    {
        var direction = (position - transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 50 * Time.deltaTime);
    }

    private void Idle()
    {
        if (idleTimeSpent > 0)
        {
            idleTimeSpent -= Time.deltaTime;
        }
        else
        {
            if (goals.Length != 0)
            {
                mooseState = MooseState.WALKING;
                agent.isStopped = false;
                animator.SetTrigger("WalkOn");
                settingGoal = true;
            
            }
            else
                idleTimeSpent = 100;
        }
    }

    private void SetPosition()
    {
        int goalInd = Random.Range(0, goals.Length);
        while (goalInd == lastGoal)
        {
            goalInd = Random.Range(0, goals.Length);
        }

        agent.destination = goals[goalInd];
        lastGoal = goalInd;
        settingGoal = false;
        agent.isStopped = false;

    }

    private void CheckArrival()
    {
        if (!agent.pathPending && !agent.hasPath)
        {
            mooseState = MooseState.IDLE;
            animator.SetTrigger("WalkOff");           
            idleTimeSpent = idleTime;
        }
    }

    public void OnPursuit(Vector3 playerPosition)
    {
        Debug.Log("On Pursuit");
        mooseState = MooseState.PURSUIT;
        agent.destination = playerPosition;
    }

    private void End()
    {
        if (time > 0)
        {
            distortionM.SetFloat("_LerpValue", 1 / (time * 10));
            time -= Time.deltaTime * 2;
        }
        if (time <= 1)
        {
            cycle.NewCycle();
        }
    }

}
