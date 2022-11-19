using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetImagePrediction : MonoBehaviour
{

    // INITIALIZE CLARIFAI VARIABLES //
    private string USER_ID = "justingg";
    private string PAT = "03e4d15f3e074dd09eb2d7e5dade2814";
    private string APP_ID = "pillpal";
    private string MODEL_ID = "pills";
    private string MODEL_VERSION_ID = "d869d62602094986930528bc00f0beaa";

    // Start is called before the first frame update
    void Start()
    {

    }

    private string MakePrediction(string imageBase64)
    {
        var requestOptions = "";
        var BASE_URL = "https://api.clarifai.com/v2/models/" + MODEL_ID + "/versions/" + MODEL_VERSION_ID + "/outputs" + requestOptions;
        return "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
