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
        if (instance == null)
        {
            instance = this;
        }
    }

    public void incNumPills(int numPills)
    {
        numPills += 1;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
