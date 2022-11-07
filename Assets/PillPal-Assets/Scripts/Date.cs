using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Date : MonoBehaviour
{

    public TextMeshPro largeText;

    // Start is called before the first frame update
    void Start()
    {
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("d/M/yyyy hh:mmtt");
        print(time);
        largeText.text = "Date and Time: " + time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
