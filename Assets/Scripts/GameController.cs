using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject wall;
    public GameObject snake;
    public GameObject apple;
    public GameObject mushroom;

    private int tileSize = 64;

    private Vector2 positionTop; //This x can range from -1920 to 3776 or -30 tile to 29 tiles
    private Vector2 offsetTop;   //There are a total of 60 tiles on the Horizontal

    private Vector2 positionBottom; 
    private Vector2 offsetBottom;

    private Vector2 positionLeft; //This y can range from -1088 to 2112 or -17 tiles to 16 tiles
    private Vector2 offsetLeft;   //There are a total of 34 tiles on the Vertical

    private Vector2 positionRight;
    private Vector2 offsetRight;

    [SerializeField] private float appleTimer;
    [SerializeField] private float mushroomTimer;
    [SerializeField] private float restarTimerMin;
    [SerializeField] private float restarTimerMax;


    void Start()
    {
        PlaceWalls();
        Instantiate(snake,Vector2.zero, Quaternion.identity);

        appleTimer = Random.Range(restarTimerMin,restarTimerMax);
        mushroomTimer = 0;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }

        appleTimer -= 1f * Time.deltaTime;
        if(appleTimer <= 0)
        {
            CreateApple();
            appleTimer = Random.Range(restarTimerMin,restarTimerMax);
        }

        mushroomTimer -= 1f * Time.deltaTime;
        if(mushroomTimer <= 0)
        {
            CreateMushroom();
            mushroomTimer = Random.Range(restarTimerMin * 2, restarTimerMax * 5);
        }        
    }

    void PlaceWalls()
    {
        CreateWallTop();
        CreateWallBottom();
        CreateWallLeft();
        CreateWallRight(); 
    }

    void CreateWallTop()
    {
        int yPosition = 16;
        int xAnchor = Random.Range(-30,30); //Determines point of 1st Spawn
        positionTop = new Vector2(xAnchor * tileSize, yPosition * tileSize);
        
        int minSteps = 1;
        int maxSteps = 30 - xAnchor;
        if(maxSteps>15) {minSteps = 15;}

        int callSteps = Random.Range(minSteps, maxSteps); //Determines how many times WALL will be called
        for (int i = 0; i < callSteps; i++)
        {
            Instantiate(wall, positionTop + offsetTop, Quaternion.identity);
            offsetTop += new Vector2(64,0);
        }
    }

    void CreateWallBottom()
    {
        int yPosition = -17;
        int xAnchor = Random.Range(-30,30); //Determines point of 1st Spawn
        positionBottom = new Vector2(xAnchor * tileSize, yPosition * tileSize);
        
        int minSteps = 1;
        int maxSteps = 30 - xAnchor;
        if(maxSteps>15) {minSteps = 15;}

        int callSteps = Random.Range(minSteps, maxSteps); //Determines how many times WALL will be called
        for (int i = 0; i < callSteps; i++)
        {
        Instantiate(wall, positionBottom + offsetBottom, Quaternion.identity);
        offsetBottom += new Vector2(64,0);
        }
    }

    void CreateWallLeft()
    {
        int xPosition = -30;
        int yAnchor = Random.Range(-17,17); //Determines point of 1st Spawn
        positionLeft = new Vector2(xPosition * tileSize, yAnchor * tileSize);
        
        int minSteps = 1;
        int maxSteps = 17 - yAnchor;
        if(maxSteps>8) {minSteps = 8;}

        int callSteps = Random.Range(minSteps, maxSteps); //Determines how many times WALL will be called
        for (int i = 0; i < callSteps; i++)
        {
            Instantiate(wall, positionLeft + offsetLeft, Quaternion.identity);
            offsetLeft += new Vector2(0,64);
        }        
    }

    void CreateWallRight()
    {
        int xPosition = 29;
        int yAnchor = Random.Range(-17,17); //Determines point of 1st Spawn
        positionRight = new Vector2(xPosition * tileSize, yAnchor * tileSize);
        
        int minSteps = 1;
        int maxSteps = 17 - yAnchor;
        if(maxSteps>8) {minSteps = 8;}

        int callSteps = Random.Range(minSteps, maxSteps); //Determines how many times WALL will be called
        for (int i = 0; i < callSteps; i++)
        {
            Instantiate(wall, positionRight + offsetRight, Quaternion.identity);
            offsetRight += new Vector2(0,64);
        }
    }

    void CreateApple()
    {
        int xPosition = Random.Range(-29,29);
        int yPosition = Random.Range(-16,16);
        Vector2 position = new Vector2(xPosition * tileSize,yPosition * tileSize);

        Instantiate(apple, position ,Quaternion.identity);
    }

    void CreateMushroom()
    {
        int xPosition = Random.Range(-29,29);
        int yPosition = Random.Range(-16,16);
        Vector2 position = new Vector2(xPosition * tileSize,yPosition * tileSize);

        Instantiate(mushroom, position ,Quaternion.identity);
    }


}