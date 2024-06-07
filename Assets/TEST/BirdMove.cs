using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class BirdMove : MonoBehaviour
{
    public ValueMonitoring M_VM;
    public BirdFSM m_bm;
    public Vector3 offset;
    public SkinnedMeshRenderer bird;

    // Start is called before the first frame update
    void Start()
    {
        bird = gameObject.GetNamedChild("Flugel").GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bm.isActivated)
        {
            bird.enabled = true;
        }
        else
        {
            bird.enabled = false;
        }
        gameObject.transform.position = M_VM.m_wrist_pos+offset;

        transform.LookAt(M_VM.forward + gameObject.transform.position, M_VM.roll_up);
    }
}
