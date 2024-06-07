using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Hands;

public class ValueMonitoring : MonoBehaviour
{
    public GameObject r_wrist;
    public GameObject l_wrist;
    public GameObject head;
    private Transform r_palm;
    private Transform l_palm;

    public float wrist_distance;
    public bool m_left_glide;
    public bool m_right_glide;
    public float m_turn;
    public float m_pitch;
    private float m_flap;
    private float m_exflap;
    public float m_flap_intensity;

    public Vector3 forward;
    public Vector3 roll_up;


    public Vector3 m_wrist_pos;



    public bool left_glide
    {
        get => m_left_glide;
        set => m_left_glide = value;
    }
    public bool right_glide
    {
        get => m_right_glide;
        set => m_right_glide = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        r_palm = r_wrist.transform.Find("R_Palm");
        l_palm = l_wrist.transform.Find("L_Palm");
    }

    // Update is called once per frame
    void Update()
    {
        wrist_distance = (r_wrist.transform.position - l_wrist.transform.position).magnitude;
        m_wrist_pos = 0.5f * (r_wrist.transform.position + l_wrist.transform.position);

        Vector3 temp = (m_wrist_pos - head.transform.position);
        temp.y = 0;
        temp.Normalize();
        forward = temp;

        Vector3 up = (r_palm.transform.up + l_palm.transform.up);
        up.Normalize();

        Vector3 right = Vector3.Cross(Vector3.up, forward);
        right.Normalize();

        temp = up - Vector3.Dot(up, forward) * forward;
        temp.Normalize();
        roll_up = temp;

        Vector3 pitch_up = up - Vector3.Dot(up, right) * right;
        pitch_up.Normalize();


        m_turn=Vector3.Dot(roll_up, right);

        m_pitch = Vector3.Dot(pitch_up, forward);
    }
    void FixedUpdate()
    {
        m_exflap = m_flap;
        m_flap = Vector3.Dot(r_palm.transform.up, l_palm.transform.up);
        m_flap_intensity = 0.5f*m_flap_intensity + 0.5f*Mathf.Abs(m_flap- m_exflap)/ Time.deltaTime;
    }
}
