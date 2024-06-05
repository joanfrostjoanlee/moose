using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class MooseSight : MonoBehaviour
{
    private MooseBehavior behavior;
    PlayerMovement player;
    private float originalSpeed;
    private float radius;


    private void Start()
    {
        behavior = GetComponentInParent<MooseBehavior>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        radius = GetComponent<SphereCollider>().radius;
        originalSpeed = player.speed;     
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided Player");
            Vector3 dir = (transform.position - other.transform.position).normalized;
            Vector3 forward = transform.TransformDirection(Vector3.forward);

            if (Vector3.Dot(dir,forward) < 0)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, other.transform.position, out hit)) 
                {

                    Debug.Log(hit.transform.gameObject.name);
                    if (hit.transform.gameObject.CompareTag("Player"))
                    {
                        PlayerMovement player = hit.transform.GetComponent<PlayerMovement>();

                        float distance = Vector3.Distance(transform.position, hit.transform.position);

                        Debug.Log("RC player");
                        player.speed = originalSpeed * (distance / radius);
                        behavior.OnPursuit(hit.transform.position);
                        
                    }

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.speed = originalSpeed;      
        }

    }

}
