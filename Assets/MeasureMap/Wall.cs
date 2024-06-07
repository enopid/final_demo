using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Color touchedColor = Color.red;
    public bool isTouched = false; 
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }
    public void SetTouched(bool value)
    {
        isTouched = value;  
        if (isTouched)
        {
            material.color = touchedColor;
        }
    }
}
