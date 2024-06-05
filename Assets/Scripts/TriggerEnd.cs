 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    public Canvas theEnd;
    private PlayerMovement player;
    private GameObject playerCam;
    [SerializeField]
    private GameObject endCamera;
    [SerializeField]
    private Animator endAnimator;
    [SerializeField]
    private GameObject endGameObject;

    // Start is called before the first frame update
    void Start()
    {
        theEnd.enabled = false;
        endGameObject.SetActive(false);
        endCamera.SetActive(false);
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        if (endCamera != null)
        {
            endAnimator = endCamera.GetComponent<Animator>();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        theEnd.enabled = true;
        //player.enabled = false;
        playerCam.SetActive(false);
        endCamera.SetActive(true);
        endGameObject.SetActive(true);
        endAnimator.SetTrigger("Animation");
    }
}