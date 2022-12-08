using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadPillTakenStatus : MonoBehaviour
{
    public int pillsTakenTodayInt;

    [SerializeField] private TextMeshPro pillsTakenText;

    // Start is called before the first frame update
    void Start()
    {
        pillsTakenTodayInt = PlayerPrefs.GetInt("pillsTakenTodayBool");
        if (pillsTakenTodayInt == 0)
        {
            pillsTakenText.text = "Pills Status: Not Taken";
        }
        else
        {
            pillsTakenText.text = "Pills Status: Taken";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
