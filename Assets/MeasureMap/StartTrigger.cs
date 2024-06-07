using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    public GameSystem gm;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            Debug.Log("Player has entered the trigger zone!");
            gm.StartTime();
        }
    }
}
