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
    public int currentcheckpoint;

    public navigator navigator;

    private float time_start;
    private float time_current;
    private bool timer_on;
    
    void Start()
    {
        Reset_Timer();
        timer_on = false;
        currentcheckpoint = 1;
    }

    // Update is called once per frame
    void Update()
    {
        destination = navigator.destinationobj;
        character = navigator.player;

        if (navigator.isstart && !timer_on)
        {
            timer_on = true;
            arrival.text = "False!";
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

        if (navigator.done)
        {
            arrival.text = "all task done!";
        }

        gameObject.transform.right =  -(destination.transform.position-character.transform.position).normalized;

    }

      private void Check_Timer()
    {
        time_current = Time.time - time_start;
        time.text = time_current.ToString();
    }

    private void End_Timer()
    {
        time_current = Time.time - time_start;
        Debug.LogFormat("checkpoint{0} : {1}s", currentcheckpoint, time_current);
        currentcheckpoint += 1;
    }


    private void Reset_Timer()
    {
        time_start = Time.time;
        time_current = 0;
    }
}
