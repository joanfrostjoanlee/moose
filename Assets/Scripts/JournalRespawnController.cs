using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalRespawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject journal;

    [SerializeField]
    private Vector3 size = new Vector3(16f, 0f, 16f);

    private CycleController cycle;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }


    private void Awake()
    {
        cycle = GameObject.FindGameObjectWithTag("Cycle").GetComponent<CycleController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (cycle.getIndex() > 0 && cycle.getLastInfo().Count > 0)
        {
           SpawnJournal();
        }
    }

    // Update is called once per frame
    public void SpawnJournal()
    {
        Instantiate(journal, GetRandomPosition(), journal.transform.rotation, gameObject.transform);
    }

    private Vector3 GetRandomPosition()
    {
        var volumePosition = new Vector3(
            Random.Range(0, size.x),
            Random.Range(0, size.y),
            Random.Range(0, size.z));

        return transform.position + volumePosition - size / 2;
    }


}
