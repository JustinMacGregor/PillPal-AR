using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{
    public int numPills;
    public static GlobalState instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(instance);
    }

    public void IncNumPills()
    {
        instance.numPills += 1;
        PlayerPrefs.SetInt("numPillsToTake", instance.numPills);
    }

    public void DecNumPills()
    {
        instance.numPills -= 1;
        if (instance.numPills < 0)
        {
            instance.numPills = 0;
        }
        PlayerPrefs.SetInt("numPillsToTake", instance.numPills);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
