using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPillScreenshotData : MonoBehaviour
{
    public string[] pillScreenshotStringData;

    public static GlobalPillScreenshotData instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        pillScreenshotStringData = new string[PlayerPrefs.GetInt("numPillsToTake")];
        DontDestroyOnLoad(instance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
