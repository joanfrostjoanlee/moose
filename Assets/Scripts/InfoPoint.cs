using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class InfoPoint : MonoBehaviour
{
    public int index;

    private Camera cam;
    private GameObject player;
    private CycleController cycle;
    
    private Canvas notification;

    [SerializeField, Min(0f)]
    private float range;

    private bool seen;
    private bool notified; 
    private float notifTime = 3.0f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cycle = GameObject.FindGameObjectWithTag("Cycle").GetComponent<CycleController>();
        cam = player.GetComponentInChildren<Camera>();
        notification = GameObject.FindGameObjectWithTag("JournalUI").GetComponent<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
        notification.enabled = false;   
        seen = false;
        notified = true;
    }

    private void FixedUpdate()
    {
        if (!seen)
        {
            if (IsInView(player, this.gameObject))
            {
                //Debug.Log("seen");
                cycle.infoFound(index);
                seen = true;
                notified = false;
            }
        }

        if (!notified)
        {
            if (notifTime > 0)
            {
                notification.enabled = true;
                notifTime -= Time.deltaTime;
            } else
            {
                notification.enabled = false;
                notified = true;
            }
        }
    }

    private bool IsInView(GameObject origin, GameObject toCheck)
    {
        Vector3 pointOnScreen = cam.WorldToScreenPoint(toCheck.GetComponentInChildren<Renderer>().bounds.center);

        //Is in front
        if (pointOnScreen.z < 0)
        {
            //Debug.Log("Behind: " + toCheck.name);
            return false;
        }

        //Is in FOV
        if ((pointOnScreen.x < 0) || (pointOnScreen.x > Screen.width) ||
                (pointOnScreen.y < 0) || (pointOnScreen.y > Screen.height))
        {
            //Debug.Log("OutOfBounds: " + toCheck.name);
            return false;
        }

        RaycastHit hit;
        Vector3 heading = toCheck.transform.position - origin.transform.position;
        Vector3 direction = heading.normalized;// / heading.magnitude;

        if (Physics.Linecast(cam.transform.position, toCheck.GetComponentInChildren<Renderer>().bounds.center, out hit))
        {
            Vector3 difference = origin.transform.position - hit.transform.position;

            if (hit.transform.name != toCheck.name)
            {
                /* -->
                Debug.DrawLine(cam.transform.position, toCheck.GetComponentInChildren<Renderer>().bounds.center, Color.red);
                Debug.LogError(toCheck.name + " occluded by " + hit.transform.name);
                */
                //Debug.Log(toCheck.name + " occluded by " + hit.transform.name);
                return false;
            }
            if (Mathf.Abs(difference.x) >= range || Mathf.Abs(difference.z) >= range)
            {
                //Debug.Log(toCheck.name + " too far");
                return false;
            }

            return true;
        }

        return false;
    }





}
