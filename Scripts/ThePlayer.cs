using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThePlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float paddingLeft;//ship not centered so clips off edge of screen so need padding
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    
    Vector2 rawInput;//stores button press, (1/-1,1/-1) for (U/D,R/L)

    Vector2 minBound;//bottom left of game window
    Vector2 maxBound;//top right of game window
    Shooter shooter;
    
    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
    }
    void Update()
    {   
        Move();
    }

    void InitBounds()//Init is short for initialisation
    {
        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0,0));//(0,0) is bottom left
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1,1));//(1,1) is top right
    }

    void Move()
    {
                                                    //*moveSpeed to alter speed or will move 1 unity movement per frame(too fast). time.deltatime makes framerate independent to make it smoother
        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;//Can use Vector 2 or 3, unity uses 3 so if use 2 then z will be 0f
        Vector2 newPosition = new Vector2();  //(what you parse in,min possible value, max possible value)
        
        
        newPosition.x = Mathf.Clamp(transform.position.x + delta.x, minBound.x + paddingLeft,maxBound.x - paddingRight);//clamp creates min and max that x value cant exceed so ship can only go o far across screen
        newPosition.y = Mathf.Clamp(transform.position.y + delta.y, minBound.y + paddingBottom,maxBound.y - paddingTop);//delta.y is y from vector of movement so position of ship and how much it will move can't go past bound of screen
        transform.position = newPosition;//move object(player) 1 or -1 in x or y, adds input vector to position vector
    }

    void OnMove(InputValue value)//automatically gives desired movement for game objects, dont need to write code from scratch
    {
        rawInput = value.Get<Vector2>();//gets vector of direction depending on key pressed

    }

    void OnFire(InputValue value)
    {
        if(shooter !=null)//if shooter exists on object
        {
            shooter.isFiring = value.isPressed;//isFiring is true if a button is pressed
        }
    }
}
