using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Windows.WebCam;

public class GetImagePrediction : MonoBehaviour
{
    // Variable for storing a reference to the PhotoCapture instance

    /// <summary>
    /// TAKE PICTURE FROM HOLOLENS, SEND TO 
    /// </summary>
    void Start()
    {
        
    }

    public void TakePicture()
    {
        PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
    }

    private PhotoCapture photoCaptureObject = null;
    string imageFilename = string.Format(@"CapturedImage{0}_n.jpg", Time.time);

    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCaptureObject = captureObject;

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        CameraParameters c = new CameraParameters();
        c.hologramOpacity = 0.0f;
        c.cameraResolutionWidth = cameraResolution.width;
        c.cameraResolutionHeight = cameraResolution.height;
        c.pixelFormat = CapturePixelFormat.BGRA32;

        captureObject.StartPhotoModeAsync(c, OnPhotoModeStarted);
    }

    private float time = Time.time;

    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            string filename = string.Format(@"CapturedImage{0}_n.jpg", time);
            string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);
            Debug.Log(filePath);

            photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);
        }
        else
        {
            Debug.LogError("Unable to start photo mode!");
        }
    }

    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            string filename = string.Format(@"CapturedImage{0}_n.jpg", time);
            string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);

            // Open the JPG file and read its contents
            byte[] imageBytes = File.ReadAllBytes(filePath);

            // Encode the image bytes as a base64 string
            string base64String = Convert.ToBase64String(imageBytes);

            // Save the base64 string to a variable
            var myBase64StringVariable = base64String;

            Debug.Log(myBase64StringVariable);
            MakePrediction(myBase64StringVariable);

            Debug.Log("Saved Photo to disk!");

            photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
        }
        else
        {
            Debug.Log("Failed to save Photo to disk");
        }
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }


    /// <summary>
    /// THIS IS CODE TO MAKE A WEB REQUEST, SEND THE IMAGE DATA HERE
    /// </summary>
    /// <param name="IMAGE_BYTES_STRING"></param>


    //The method that begins the prediction process
    //Takes Image as byte64 string
    public void MakePrediction(string IMAGE_BYTES_STRING)
    {
        //And we start a new co routine in Unity and wait for the response.
        StartCoroutine(WaitForRequest(IMAGE_BYTES_STRING));
    }

 
    //Wait for the www Request and get result
    IEnumerator WaitForRequest(string IMAGE_BYTES_STRING)
    {
        GameObject predictionGameObject = GameObject.FindGameObjectWithTag("Prediction");
        TextMesh predictionTextMesh = predictionGameObject.GetComponent<TextMesh>();
        predictionTextMesh.text = "Loading...";

        string USER_ID = "justingg";
        string PAT = "03e4d15f3e074dd09eb2d7e5dade2814";
        string APP_ID = "pillpal";

        string MODEL_ID = "pills";

        var BASE_URL = "https://api.clarifai.com/v2/users/" + USER_ID + "/apps/" + APP_ID + "/models/" + MODEL_ID + "/outputs";


        Image image = new Image();
        image.base64 = IMAGE_BYTES_STRING;
        Data data = new Data();
        data.image = image;
        Input input = new Input();
        input.data = data;
        List<Input> inputs = new List<Input>() { input };

        var requestData = new RequestData();

        requestData.inputs = inputs;
        requestData.inputs[0].data = data;
        requestData.inputs[0].data.image = image;
        requestData.inputs[0].data.image.base64 = IMAGE_BYTES_STRING;

        string json = JsonConvert.SerializeObject(requestData);

        var req = new UnityWebRequest(BASE_URL, "POST");

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        req.SetRequestHeader("Accept", "application/json");
        req.SetRequestHeader("Authorization", "Key " + PAT);
        req.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
        }
        else
        {
            var jsonResponse = JsonConvert.DeserializeObject<ResponseData>(req.downloadHandler.text);

            var prediction = jsonResponse.outputs[0].data.concepts[0].name;

            predictionTextMesh.text = prediction.ToString();
        }
    }
}


[Serializable]
public class Data
{
    [SerializeField] public Image image { get; set; }
    public List<Concept> concepts { get; set; }
}

[Serializable]
public class Image
{
    [SerializeField] public string base64 { get; set; }
}

[Serializable]
public class Input
{
    [SerializeField] public Data data { get; set; }
}

[Serializable]
public class RequestData
{
    [SerializeField] public List<Input> inputs { get; set; }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Concept
{
    public string id { get; set; }
    public string name { get; set; }
    public double value { get; set; }
    public string app_id { get; set; }
}

public class ImportInfo
{
}


public class InputInfo
{
}

public class Metadata
{
}

public class Model
{
    public string id { get; set; }
    public string name { get; set; }
    public DateTime created_at { get; set; }
    public DateTime modified_at { get; set; }
    public string app_id { get; set; }
    public OutputInfo output_info { get; set; }
    public ModelVersion model_version { get; set; }
    public string user_id { get; set; }
    public InputInfo input_info { get; set; }
    public TrainInfo train_info { get; set; }
    public string model_type_id { get; set; }
    public Visibility visibility { get; set; }
    public object metadata { get; set; }
    public Presets presets { get; set; }
    public List<object> languages { get; set; }
    public ImportInfo import_info { get; set; }
    public bool workflow_recommended { get; set; }
}

public class ModelVersion
{
    public string id { get; set; }
    public DateTime created_at { get; set; }
    public Status status { get; set; }
    public int total_input_count { get; set; }
    public DateTime completed_at { get; set; }
    public Visibility visibility { get; set; }
    public string app_id { get; set; }
    public string user_id { get; set; }
    public Metadata metadata { get; set; }
    public OutputInfo output_info { get; set; }
    public InputInfo input_info { get; set; }
    public TrainInfo train_info { get; set; }
    public ImportInfo import_info { get; set; }
}

public class Output
{
    public string id { get; set; }
    public Status status { get; set; }
    public string created_at { get; set; }
    public Model model { get; set; }
    public Input input { get; set; }
    public Data data { get; set; }
}

public class OutputConfig
{
    public bool concepts_mutually_exclusive { get; set; }
    public bool closed_environment { get; set; }
    public int max_concepts { get; set; }
    public int min_value { get; set; }
}

public class OutputInfo
{
    public OutputConfig output_config { get; set; }
    public string message { get; set; }
    public Params @params { get; set; }
}

public class Params
{
    public int max_concepts { get; set; }
    public int min_value { get; set; }
    public List<SelectConcept> select_concepts { get; set; }
    public string dataset_id { get; set; }
    public string dataset_version_id { get; set; }
}

public class Presets
{
}

public class ResponseData
{
    public Status status { get; set; }
    public List<Output> outputs { get; set; }
}

public class SelectConcept
{
    public string id { get; set; }
}

public class Status
{
    public int code { get; set; }
    public string description { get; set; }
    public string req_id { get; set; }
}

public class TrainInfo
{
    public Params @params { get; set; }
}

public class Visibility
{
    public int gettable { get; set; }
}

