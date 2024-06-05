using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class StonePuzzleInteraction : MonoBehaviour
{
    private bool triggered;
    private bool isSolved;

    [SerializeField]
    private Camera playerCamera;
    private PlayerMovement playerMovement;
    private Vector3 startCameraPos;
    private Quaternion startCameraRot;
    private float startSpeed;


    [SerializeField]
    private Camera endCamera;
    private Vector3 endCameraPos;
    private Quaternion endCameraRot;

    [SerializeField]
    private Animator animator;

    private BoxCollider boxCollider;

    public enum PuzzleState
    {
        NORMAL,
        CAMERAMOVEFRONT,
        CAMERAMOVEBACK,
        PUZZLE,
        FAIL
    }

    public PuzzleState state;

    void Awake()
    {
        state = PuzzleState.NORMAL;
        endCameraPos = endCamera.transform.position;
        endCameraRot = endCamera.transform.rotation;

        if (playerCamera != null && playerMovement != null)
        {
            playerCamera.GetComponent<MouseLook>().enabled = true;
            playerMovement.enabled = true;
            startSpeed = playerMovement.speed;
        }

        boxCollider = GetComponent<BoxCollider>();
        isSolved = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (playerCamera != null && playerMovement != null)
        {
            CheckState();
        }

        if (Input.GetKeyDown(KeyCode.E) && state == PuzzleState.NORMAL && triggered && playerCamera != null)
        {
            startCameraPos = playerCamera.transform.position;
            startCameraRot = playerCamera.transform.rotation;
            state = PuzzleState.CAMERAMOVEFRONT;
        }
        if (state == PuzzleState.CAMERAMOVEFRONT && playerCamera != null)
        {
            MoveCameraToPosition(endCameraPos, endCameraRot);
        }
        if (state == PuzzleState.CAMERAMOVEBACK && playerCamera != null)
        {
            MoveCameraToPosition(startCameraPos, startCameraRot);
        }

        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) && state == PuzzleState.PUZZLE)
        {
            state = PuzzleState.CAMERAMOVEBACK;
        }

        Debug.Log(state);

    }

    void OnTriggerEnter(Collider other)
    {
        triggered = true;
        playerCamera = other.GetComponentInChildren<Camera>();
        playerMovement = other.GetComponent<PlayerMovement>();
        startSpeed = playerMovement.speed;
    }

    void OnTriggerExit(Collider other)
    {
        triggered = false;
    }

    void MoveCameraToPosition(Vector3 endPosition, Quaternion endRotation)
    {
        if (playerCamera.transform.position != endPosition || playerCamera.transform.rotation != endRotation)
        {
                playerCamera.transform.position = Vector3.MoveTowards(playerCamera.transform.position, endPosition, Time.deltaTime);
                playerCamera.transform.rotation = Quaternion.RotateTowards(playerCamera.transform.rotation, endRotation, Time.deltaTime * 20);
        }
            else if (playerCamera.transform.position == endPosition && playerCamera.transform.rotation == endRotation)
            {
            if (isSolved || state == PuzzleState.CAMERAMOVEBACK)
            {
                MouseVisibilityController.instance.DisableMouse();
                state = PuzzleState.NORMAL;
                return;
            }

            if (!isSolved)
            {
                MouseVisibilityController.instance.EnableMouse();
                state = PuzzleState.PUZZLE;
               
            }

            }
        
    }

    public void AnimationOnSolve(int solveCond)
    {
        if (solveCond == 1)
        {
            endCameraPos = new Vector3(endCameraPos.x, endCameraPos.y, endCameraPos.z + 1);
            animator.SetTrigger("Success");
            state = PuzzleState.CAMERAMOVEBACK;
            boxCollider.enabled = false;
            triggered = false;
            isSolved = true;
        }
        else if (solveCond == -1)
        {
            animator.SetTrigger("Fail");
            state = PuzzleState.FAIL;
            animator.SetTrigger("StoneFall");
        }
    }

    private void CheckState()
    {
        if (state == PuzzleState.NORMAL)
        {
            playerCamera.enabled = true;
            endCamera.enabled = false;
            playerCamera.GetComponent<MouseLook>().enabled = true;
            playerMovement.speed = startSpeed;
        }

        if (state == PuzzleState.PUZZLE || state == PuzzleState.CAMERAMOVEFRONT || state == PuzzleState.CAMERAMOVEBACK)
        {
            playerCamera.enabled = true;
            endCamera.enabled = false;
            playerCamera.GetComponent<MouseLook>().enabled = false;
            playerMovement.speed = 0f;
        }

        if (state == PuzzleState.FAIL)
        {
            playerCamera.enabled = false;
            endCamera.enabled = true;
            playerMovement.speed = startSpeed;
        }
    }

}