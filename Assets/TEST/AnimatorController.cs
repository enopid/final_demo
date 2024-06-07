using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator m_am;
    public BirdFSM m_FSM;
    
// Start is called before the first frame update
void Start()
    {
        m_am = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_am.SetBool("Glide", false);
        m_am.SetBool("Flap", false);
        m_am.SetBool("Descent", false);
        m_am.SetBool("Decelation", false);
        m_am.SetBool("Turn", false);
        switch (m_FSM.state)
        {
            case BirdFSM.State.Glide:
                m_am.SetBool("Glide", true);
                break;
            case BirdFSM.State.Flap:
                m_am.SetBool("Flap", true);
                break;
            case BirdFSM.State.Descent:
                m_am.SetBool("Descent", true);
                break;
            case BirdFSM.State.Decelation:
                m_am.SetBool("Decelation", true);
                break;
            case BirdFSM.State.Turn:
                m_am.SetBool("Turn", true);
                break;
        }
    }
}
