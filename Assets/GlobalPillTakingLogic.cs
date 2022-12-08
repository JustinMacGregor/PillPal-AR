using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalPillTakingLogic : MonoBehaviour
{
    [SerializeField] private Canvas pillImageCanvas;

    public List<Texture2D> pillImgTextureList;

    public int numPillsToTake;

    public int currentPillIndex;

    public static GlobalPillTakingLogic instance;

    // Start is called before the first frame update
    void Start()
    {
        pillImgTextureList = new List<Texture2D>();
        numPillsToTake = PlayerPrefs.GetInt("numPillsToTake");
        currentPillIndex = 0;

        for (int i = 0; i< numPillsToTake; i++)
        {
            pillImgTextureList.Add(Resources.Load<Texture2D>("pillimg"));
        }


        if (instance == null)
        {
            instance = this;
        }

        LoadCurrentPillImage();

    }

    public void LoadCurrentPillImage()
    {
        GameObject imgObject = new GameObject("currentImage");


        Texture2D texture = pillImgTextureList[currentPillIndex];
        Sprite loadedSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        UnityEngine.UI.Image canvasImage = pillImageCanvas.GetComponent<UnityEngine.UI.Image>();
        canvasImage.sprite = loadedSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
