using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadNumPills : MonoBehaviour
{
    [SerializeField] private TextMeshPro pillText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pillText.text = "Current pill number: " + (GlobalPillTakingLogic.instance.currentPillIndex + 1).ToString() + " Out of: " + PlayerPrefs.GetInt("numPillsToTake").ToString() + " pills.";
    }
}
