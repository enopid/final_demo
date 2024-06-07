using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class directionUI : MonoBehaviour
{

    public TextMeshProUGUI arrival;
    public TextMeshProUGUI time;
    private GameObject character;
    private GameObject destination;

    public navigator navigator;

    private float time_start;
    private float time_current;
    private bool timer_on;
    
    void Start()
    {
        Reset_Timer();
        timer_on = false;
    }

    // Update is called once per frame
    void Update()
    {
        destination = navigator.destinationobj;
        character = navigator.player;

        if (navigator.isstart && !timer_on)
        {
            timer_on = true;
            Reset_Timer();
        }

        if (timer_on)
        {
            Check_Timer();
        }

        if(!navigator.isstart)
        {
            timer_on = false;
            arrival.text="True!";
            End_Timer();
        }

        gameObject.transform.right =  -(destination.transform.position-character.transform.position).normalized;

    }

      private void Check_Timer()
    {
        time_current = Time.time - time_start;
        
            Debug.Log(time_current);
            time.text = time_current.ToString();
        
    }

    private void End_Timer()
    {
        Debug.Log("End");
    }


    private void Reset_Timer()
    {
        time_start = Time.time;
        time_current = 0;
        time.text = time_current.ToString();
        Debug.Log("Start");
    }
}
