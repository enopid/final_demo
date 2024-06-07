using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class birdDebugUI : MonoBehaviour
{
    public GameObject Bird;
    public BirdMovement m_BM;
    private ValueMonitoring m_VM;
    private BirdFSM m_FSM;

    public TextMeshProUGUI Isactivated;
    public TextMeshProUGUI State;
    public TextMeshProUGUI Turnvalue;
    public TextMeshProUGUI Pitchvalue;
    public TextMeshProUGUI Flapvalue;
    public TextMeshProUGUI Speedvalue;
    public TextMeshProUGUI Heightvalue;

    public TextMeshProUGUI forward;

    // Start is called before the first frame update
    void Start()
    {
        m_VM=Bird.GetComponent<ValueMonitoring>();
        m_FSM = Bird.GetComponent<BirdFSM>();
    }

    // Update is called once per frame
    void Update()
    {
        Isactivated.text = m_FSM.isActivated.ToString();
        State.text = m_FSM.state.ToString();
        Turnvalue.text = Math.Round(m_VM.m_turn, 2).ToString();
        Flapvalue.text = Math.Round(m_VM.m_flap_intensity, 2).ToString();
        Pitchvalue.text = Math.Round(m_VM.m_pitch, 2).ToString();
        Heightvalue.text = Math.Round(m_BM.transform.position.y, 2).ToString();
        Speedvalue.text = Math.Round(m_BM.glide.speed, 2).ToString();

        forward.text = m_VM.forward.ToString();
    }
}
