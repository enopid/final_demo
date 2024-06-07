using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem: MonoBehaviour
{
    float elapsedTime;
    bool measureTime = false;
    bool measureEnd = false;
    public Camera vrCam;
    public Camera[] cameras;
    private int currentCameraIndex = 0;

    public void StartTime()
    {
        measureTime = true;
    }

    public void EndTime()
    {
        measureTime = false;
        measureEnd = true;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        Debug.Log(string.Format("{0:00}:{1:00}", minutes, seconds));
        vrCam.gameObject.SetActive(false);
        cameras[currentCameraIndex].gameObject.SetActive(true);
        StartCoroutine(SwitchCamera());


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(measureTime)
            elapsedTime += Time.deltaTime;

        

    }

    IEnumerator SwitchCamera()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f); 

            cameras[currentCameraIndex].gameObject.SetActive(false);
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}
