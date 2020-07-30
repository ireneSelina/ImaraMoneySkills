using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using touchScr = UnityEngine.InputSystem.TouchPhase;
using System.Linq;


public class DragCoin : MonoBehaviour
{

    // touch offset allows coin not to shake when it starts moving
    float deltaX, deltaY;

    public GameObject correctJar, droppedCoinPosition;

    private GameObject draggedCoin;

    private float startPosX, startPosY;

    private Vector3 touchPosition;

    private Vector2 resetPosition;

    private string draggedCoinName;

    private DropCoin dropCoin;

    bool moving, finish;

    //public UnityEngine.InputSystem.Controls.Vector2Control position { get; }
    private Touch touch;

    // reference to Rigidbody2D component
    Rigidbody2D rb;

    // coin movement not allowed if you touches not the coin at the first time
    private bool moveAllowed = false;


    void Awake()
    {

        UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();

        foreach (var touch in Touch.activeTouches)
            Debug.Log($"{touch.touchId}: {touch.screenPosition},{touch.phase}");

    }


    // Use this for initialization
    void Start()
    {

        //Touch touch = new Touch();
        draggedCoinName = gameObject.name;

        draggedCoin = this.gameObject;

        dropCoin = correctJar.GetComponent<DropCoin>();

        moving = false;

        finish = false;

        rb = draggedCoin.GetComponent<Rigidbody2D>();

        // Add bouncy material to the coin
        PhysicsMaterial2D mat = new PhysicsMaterial2D
        {
            bounciness = 0.75f,
            friction = 0.4f
        };

        draggedCoin.GetComponent<CircleCollider2D>().sharedMaterial = mat;
        
        resetPosition = transform.position;

        Debug.Log("Start.resetPosition of " + draggedCoinName + " is  " + resetPosition); 

    }


    // Update is called once per frame
    void Update()
    {

        UpdateOldTouch();
        //UpdateNewTouch();

    }


    /****///start of UpdateOldTouch
    public void UpdateOldTouch()
    {

        if (!finish)
        {

            if (moving)
            {

                Vector3 mousePosOld;

                mousePosOld = Input.mousePosition;

                mousePosOld = Camera.main.ScreenToWorldPoint(mousePosOld);

                transform.position =
                    new Vector3(mousePosOld.x - startPosX, mousePosOld.y - startPosY, transform.position.z);

                Debug.Log("Update.transform.position is  " + transform.position);

            }

        }

    }


    void OnMouseDown()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePosOld;

            mousePosOld = Input.mousePosition;

            mousePosOld = Camera.main.ScreenToWorldPoint(mousePosOld);

            Debug.Log("OnMouseDown.mousePosOld is " + mousePosOld);

            startPosX = mousePosOld.x - transform.position.x;

            startPosY = mousePosOld.y - transform.position.y;

            Debug.Log("OnMouseDown.startPosX is  " + startPosX);

            Debug.Log("OnMouseDown.startPosY is  " + startPosY);

            //play coin pickUp sound
            dropCoin.SoundSystem(true, false, draggedCoinName);

            moving = true;

        }

    }


    void OnMouseUp()
    {

        moving = false;

        if (Mathf.Abs(draggedCoin.transform.position.x - droppedCoinPosition.transform.position.x) <= .5f &&
             Mathf.Abs(draggedCoin.transform.position.y - droppedCoinPosition.transform.position.y) <= .5f)
        {

            Debug.Log("OnMouseUp.if.transform.position.x is  " + transform.position.x);

            Debug.Log("OnMouseUp.if.transform.position.y is  " + transform.position.y);

            transform.position = new Vector3(droppedCoinPosition.transform.position.x,
                droppedCoinPosition.transform.position.y, droppedCoinPosition.transform.position.z);

            ScoreCounter.scoreValue += 500;

            dropCoin.SoundSystem(false, true, draggedCoinName);

            Destroy(draggedCoin);

        }

        else

        {

            Debug.Log("OnMouseUp.else.transform.position is  " + transform.position);

            transform.position = new Vector3(resetPosition.x, resetPosition.y, 0);

            dropCoin.SoundSystem(false, false, draggedCoinName);

        }

    }
    /****///end of UpdateOldTouch

    
    /****///start of UpdateNewTouch
    public void UpdateNewTouch() 
    {

        // Initiating touch event
        // if touch event takes place
        if (Touch.activeTouches.Count > 0)
        {

            // get touch position
            //Touch touch = new Touch();
            touch = Touch.activeTouches.FirstOrDefault();
            //Input.GetTouch(0);

            Vector2 touchScreenPosition = Touchscreen.current.position.ReadValue();

            // obtain touch position
            touchPosition = Camera.main.ScreenToWorldPoint(touchScreenPosition, 0f); 
            // Touchscreen.current.position.ReadValue()); // touch.screenPosition);

            Debug.Log("Update.if.Input.touchPosition => 0 .position is " + touchPosition);

            // get touch to take a deal with
            switch (touch.phase)
            {

                // if you touches the screen
                case touchScr.Began:

                    // if you touch the coin
                    if (draggedCoin.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPosition))
                    {

                        // get the offset between position you touches
                        // and the center of the game object
                        deltaX = touchPosition.x - transform.position.x;

                        deltaY = touchPosition.y - transform.position.y;

                        Debug.Log("Update.touch.phase == TouchPhase.Began.deltaX is  " + deltaX);

                        Debug.Log("Update.touch.phase == TouchPhase.Began.deltaY is  " + deltaY);

                        // if touch begins within the coin collider
                        // then it is allowed to move
                        moveAllowed = true;

                        // restrict some rigidbody properties so it moves
                        // more smoothly and correctly
                        rb.freezeRotation = true;

                        rb.velocity = new Vector2(0, 0);

                        rb.gravityScale = 0;

                        draggedCoin.GetComponent<CircleCollider2D>().sharedMaterial = null;

                        //play coin pickUp sound
                        dropCoin.SoundSystem(true, false, draggedCoinName);

                    }

                break;


                // you move your finger
                case touchScr.Moved:

                    //enlarge coin
                    //selectedGameObject.
                    transform.localScale = new Vector2(1.05f, 1.05f);
                    //Debug.Log("Update.touch.phase == TouchPhase.Moved : coin enlarged");

                    // if you touch the coin and movement is allowed, then you can move it
                    if (draggedCoin.GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(touchPosition) && moveAllowed)
                        rb.MovePosition(new Vector2(touchPosition.x - deltaX, touchPosition.y - deltaY));

                    Debug.Log("Update.touch.phase == TouchPhase.Moved -  drag started ");

                    moving = true;

                break;


                // you release your finger
                case touchScr.Ended:

                    // restore initial parameters
                    // when touch is ended
                    moving = false;

                    moveAllowed = false;

                    rb.freezeRotation = false;

                    rb.gravityScale = 2;
                    PhysicsMaterial2D mat = new PhysicsMaterial2D
                    {
                        bounciness = 0.75f,
                        friction = 0.4f
                    };

                    draggedCoin.GetComponent<CircleCollider2D>().sharedMaterial = mat;

                    // Restore the regular size of the coin.
                    //selectedGameObject.
                    transform.localScale = new Vector2(1/1.05f, 1/1.05f);
                    //Debug.Log("UpdateNewTouch.touch.phase == TouchPhase.Ended : coin restored to normal size ");

                    if (Mathf.Abs(draggedCoin.transform.position.x - droppedCoinPosition.transform.position.x) <= .5f &&
                        Mathf.Abs(draggedCoin.transform.position.y - droppedCoinPosition.transform.position.y) <= .5f)
                    {

                        Debug.Log("Update.TouchPhase.Ended.if.transform.position.x is  "
                            + transform.position.x);

                        Debug.Log("Update.TouchPhase.Ended.if.transform.position.y is  "
                            + transform.position.y);

                        //transform.position = new Vector2(droppedCoinPosition.x,   droppedCoinPosition.y);

                        Debug.Log("Update.TouchPhase.Ended.if.transform.position: coin was dropped at: "
                            + transform.position);

                        ScoreCounter.scoreValue += 500;

                        Debug.Log("Update.TouchPhase.Ended.if.ScoreCounter updated: Score is  " + ScoreCounter.scoreValue);

                        dropCoin.SoundSystem(false, true, draggedCoinName);

                        //transform.localScale = new Vector2(.01f, .01f);

                    }

                    //else if (gameObject)//check if game object isnt destroyed
                    //{
                    //
                    //    transform.position = new Vector2(resetPosition.x, resetPosition.y);
                    //
                    //    Debug.Log("Update.TouchPhase.Ended.else.transform.position is  "
                    //        + transform.position);
                    //
                    //    dropCoin.SoundSystem(false, true, draggedCoinName);dropCoin.SoundSystem(false, false, draggedCoinName);
                    //
                    //    Destroy(gameObject);
                    //}

                break;

            }
                
        }

    }
    /****///end of UpdateNewTouch


    /****///Start of Trigger && Collision events
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //
    //    if (collision.gameObject.tag is "Coins")
    //    {
    //        Destroy(draggedCoin);
    //
    //        Debug.Log(gameObject + "has Destroyed from DragCoin.OnCollisionEnter2D " + gameObject.name);
    //
    //    }
    //
    //}
    //
    //
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //
    //    if (other.gameObject.tag is "Coins")
    //    {
    //
    //        //Destroy(gameObject);
    //
    //        Debug.Log(" Destroyed from DragCoin.OnCollisionEnter2D " + gameObject.name);
    //
    //    }
    //
    //}
    //
    //
    //void OnTriggerExit2D(Collider2D other)
    //{
    //
    //    if (other.gameObject.tag is "Coins")
    //    {
    //
    //        //Destroy(gameObject);
    //
    //        Debug.Log(" Destroyed from DragCoin.OnTriggerExit2D " + gameObject.name);
    //
    //    }
    //
    //}
    /****///End of Trigger && Collision events

}