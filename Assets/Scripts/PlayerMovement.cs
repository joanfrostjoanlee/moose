using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float gravity = -9.81f;
    Vector3 velocity;

    private AudioSource walkingSound;



    private void Start()
    {
        walkingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Footsteps();
        

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    private void Footsteps()
    {
        if (speed < 0.5f)
        {
            walkingSound.Stop();
            walkingSound.enabled = false;           
            return;
        }

        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            walkingSound.enabled = true;
            walkingSound.Play();
        }
        
    }

}
