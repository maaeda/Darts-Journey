using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnApplicationPause (bool pauseStatus)
    {
        if (pauseStatus) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }
}
