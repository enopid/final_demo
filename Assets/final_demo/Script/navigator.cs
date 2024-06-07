using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class navigator : MonoBehaviour
{
    public List<GameObject> checkpoints = new List<GameObject>();
    public GameObject player;

    public GameObject destinationobj;
    public GameObject currentobj;
    public GameObject doneobj;

    public int targetradius=10;
    public int startradius = 10;

    private int currentindex;
    private int finalindex;

    private GameObject current;
    private GameObject destination;

    public bool isstart;
    private bool done;



    private void Depart()
    {
        isstart = true;
        SetDone();
    }

    private void Arrive()
    {
        isstart = false;
        if (currentindex + 1 == finalindex)
        {
            done = true;
        }
        SetNextPoint();
        SetDestination();
    }

    private void SetNextPoint()
    {
        currentindex += 1;
        current = checkpoints[currentindex];
        destination = checkpoints[currentindex + 1];
    }

    private bool IsArrive()
    {
        if (isstart)
        {
            return Vector3.Distance(destination.transform.position, player.transform.position) < targetradius;
        }
        else
        {
            return false;
        }
    }

    private bool IsDepart()
    {
        if (!isstart)
        {
            return Vector3.Distance(current.transform.position, player.transform.position) > startradius;
        }
        else
        {
            return false;
        }
    }

    private void SetDestination()
    {
        destinationobj.transform.position = destination.transform.position;
        currentobj.transform.position = current.transform.position;
        doneobj.transform.position = current.transform.position;
        doneobj.SetActive(false);
        currentobj.SetActive(true);
    }
    private void SetDone()
    {
        doneobj.SetActive(true);
        currentobj.SetActive(false);
    }

    void Start()
    {
        isstart = false;
        done = false;
        currentindex = 0;
        finalindex= checkpoints.Count-1;
        current = checkpoints[currentindex];
        destination = checkpoints[currentindex+1];
        SetDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            if (IsDepart())
            {
                Depart();
            }
            else if (IsArrive())
            {
                Arrive();
            }
        }
    }
}
