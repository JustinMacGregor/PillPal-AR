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
        pillText.text = PlayerPrefs.GetInt("numPillsToTake").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
