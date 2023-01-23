using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    int bodyCount = 3;
    bool turned = false;
    bool canInput = false;

    bool hitTopBound = false;
    bool hitBottomBound = false;
    bool hitLeftBound = false;
    bool hitRightBound = false;

    private Vector3 goUp;
    private Vector3 goDown;
    private Vector3 goRight;
    private Vector3 goLeft;
    private Vector3 storedPosition;

    private Vector3 direction;
    private Vector3 oldDirection;

    [SerializeField] private float timer;
    [SerializeField] private float restarTimer;

    public Sprite[] snakeSprite; //Up,Down,Right,Left
    public Sprite[] bodySprite; //Horizontal,Vertical,DownLeft,DownRight,UpLeft,UpRight

    public SpriteRenderer spriteRenderer;
    public AudioSource audioApple;
    public AudioSource audioMushroom;

    public GameObject snakeBody;
    [SerializeField] private List<GameObject> snakeBody_ls;

    public GameObject gameOver;
    
    void Start()
    {
        gameOver = GameObject.Find("GameOver");
        gameOver.SetActive(false);

        snakeBody_ls = new List<GameObject>();

        goUp    = new Vector3(0,64,0);
        goDown  = new Vector3(0,-64,0);
        goRight = new Vector3(64,0,0);
        goLeft  = new Vector3(-64,0,0);

        direction = goRight;
        timer = restarTimer;
    }

    void Update()
    {
        if(canInput)
        {
            GetDirection();
        }        

        timer -= 1f * Time.deltaTime;
        if(timer <= 0)
        {
            Move();
            timer = restarTimer;
            
            if(bodyCount>0)
            {
                MakeSnake();
                bodyCount -= 1;
            }
            else
            {
                MakeSnake();
                Destroy(snakeBody_ls[0]);
                snakeBody_ls.RemoveAt(0);
            }
            
            canInput = true;
        }
    }
    
    void Move()
    {
        if(hitTopBound)
        {
            storedPosition = transform.position;
            transform.position += (direction + new Vector3(0,-64*35,0));
            hitTopBound = false;
        }
        else if(hitBottomBound)
        {
            storedPosition = transform.position;
            transform.position += (direction + new Vector3(0,64*35,0));
            hitBottomBound = false;
        }
        else if(hitLeftBound)
        {
            storedPosition = transform.position;
            transform.position += (direction + new Vector3(-64*61,0,0));
            hitLeftBound = false;
        }
        else if(hitRightBound)
        {
            storedPosition = transform.position;
            transform.position += (direction + new Vector3(64*61,0,0));
            hitRightBound = false;
        }
        else
        {
            storedPosition = transform.position;
            transform.position += direction;
        }
    }

    void MakeSnake()
    {
        GameObject newBodyPart = Instantiate(snakeBody,storedPosition, Quaternion.identity);
        snakeBody_ls.Add(newBodyPart);

        SpriteRenderer newBodySprite = newBodyPart.GetComponent<SpriteRenderer>();

        //Going Up
        if(spriteRenderer.sprite == snakeSprite[0] && turned == false)
        {
            newBodySprite.sprite = bodySprite[1];
        }
        
        //Going Down
        if(spriteRenderer.sprite == snakeSprite[1] && turned == false)
        {
            newBodySprite.sprite = bodySprite[1];
        }
        

        //Going Right
        if(spriteRenderer.sprite == snakeSprite[2] && turned == false)
        {
            newBodySprite.sprite = bodySprite[0];
        }
        
        //Going Left
        if(spriteRenderer.sprite == snakeSprite[3] && turned == false)
        {
            newBodySprite.sprite = bodySprite[0];
        }

        //Turns =================================================

        //Going Up, From Left
        if(turned == true && direction == goUp && oldDirection == goLeft
        || turned == true && direction == goRight && oldDirection == goDown)
        {
            newBodySprite.sprite = bodySprite[3];
            turned = false;
        }

        //Going Up, From Right
        if(turned == true && direction == goUp && oldDirection == goRight
        || turned == true && direction == goLeft && oldDirection == goDown)
        {
            newBodySprite.sprite = bodySprite[2];
            turned = false;
        }
        
        //Going Down, From Left
        if(turned == true && direction == goDown && oldDirection == goLeft
        || turned == true && direction == goRight && oldDirection == goUp)
        {
            newBodySprite.sprite = bodySprite[5];
            turned = false;
        }

        //Going Down, From Right
        if(turned == true && direction == goDown && oldDirection == goRight
        || turned == true && direction == goLeft && oldDirection == goUp)
        {
            newBodySprite.sprite = bodySprite[4];
            turned = false;
        }
    }

    void GetDirection()
    {       
        //Up
        if(Input.GetKeyDown(KeyCode.W) && (direction == goLeft || direction == goRight))
        {
            oldDirection = direction;
            direction = goUp;
            spriteRenderer.sprite = snakeSprite[0];    
            turned = true;
            canInput = false;
        }

        //Down    
        if(Input.GetKeyDown(KeyCode.S) && (direction == goLeft || direction == goRight))
        {
            oldDirection = direction;
            direction = goDown;
            spriteRenderer.sprite = snakeSprite[1];
            turned = true;
            canInput = false;
        }

        //Right
        if(Input.GetKeyDown(KeyCode.D) && (direction == goUp || direction == goDown))
        {
            oldDirection = direction;
            direction = goRight;
            spriteRenderer.sprite = snakeSprite[2];
            turned = true;
            canInput = false;
        }

        //Left
        if(Input.GetKeyDown(KeyCode.A) && (direction == goUp || direction == goDown))
        {
            oldDirection = direction;
            direction = goLeft;
            spriteRenderer.sprite = snakeSprite[3];
            turned = true;
            canInput = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Apple")
        {
            bodyCount = 1;
            audioApple.Play();
            Destroy(other.gameObject);
        }
        
        if(other.tag == "Death")
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
       
        if(other.tag == "Mushroom")
        {
            restarTimer -= 0.03f;
            audioMushroom.Play();
            Destroy(other.gameObject);
        }
        
        if(other.tag == "TopBound")
        {
            hitTopBound = true;
        }
        
        if(other.tag == "BottomBound")
        {
            hitBottomBound = true;
        }
                if(other.tag == "LeftBound")
        {
            hitLeftBound = true;
        }
        
        if(other.tag == "RightBound")
        {
            hitRightBound = true;
        }
    }

}