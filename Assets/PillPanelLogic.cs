using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PillPanelLogic : MonoBehaviour
{
    [SerializeField] private TextMeshPro pillText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void updateText(int amount)
    {
        pillText.text = GlobalState.instance.numPills.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
