using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class BirdFSM : MonoBehaviour
{
    public enum State
    {
        Glide,
        Flap,
        Turn,
        Descent,
        Decelation
    }
    public ValueMonitoring m_VM;
    public float wrist_distance_offset;
    public float wrist_lost_distance_offset;
    public float turn_offset;
    public float flap_offset;
    public float descent_offset;
    public float Decelation_offset;
    public bool isActivated;
    public State state;
    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_VM.wrist_distance< wrist_distance_offset && m_VM.m_left_glide && m_VM.m_right_glide)
        {
            isActivated = true;
        }
        if (m_VM.wrist_distance >= wrist_lost_distance_offset)
        {
            isActivated = false;
        }

        if (isActivated)
        {
            if (m_VM.m_flap_intensity > flap_offset && Mathf.Abs(m_VM.m_turn) < turn_offset-0.15f)
            {
                state = State.Flap;
            }
            else if (Mathf.Abs(m_VM.m_turn)> turn_offset)
            {
                state = State.Turn;
            }
            else if (m_VM.m_pitch > descent_offset)
            {
                state = State.Descent;
            }
            else if (m_VM.m_pitch < Decelation_offset)
            {
                state = State.Decelation;
            }
            else
            {
                state = State.Glide;
            }
        }
    }
}
