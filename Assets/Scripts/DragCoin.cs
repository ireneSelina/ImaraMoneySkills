using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
//using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;
//using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
//using touchScr = UnityEngine.InputSystem.TouchPhase;

public class DragCoin : MonoBehaviour
{

    // touch offset allows coin not to shake when it starts moving
    //float deltaX, deltaY;

    public GameObject correctJar, droppedCoinPosition;

    private GameObject draggedCoin;

    private float startPosX, startPosY;

    //private Vector3 touchPosition;

    private Vector2 resetPosition;

    private string draggedCoinName;

    private DropCoin dropCoin;

    private HoverCoin hoverCoin;

    private PhysicsMaterial2D mat;

    bool moving, finish;

    private CircleCollider2D CoinCollider;

    //public UnityEngine.InputSystem.Controls.Vector2Control position { get; }
    //private Touch touch;

    // reference to Rigidbody2D component
    Rigidbody2D rb;

    // coin movement not allowed if you touches not the coin at the first time
    //private bool moveAllowed = false;

    Dictionary<string, float> coinScales = new Dictionary<string, float>()
    {
          {"Forty_Shilling", .4f},
          {"Fifty_Cent", .55f},
          {"One_Shilling", .48f},
          {"Ten_Shilling", .47f},
          {"Twenty_Shilling", .5f},
          {"Five_Shilling", .62f}
    };


    // Use this for initialization
    void Start()
    {

        //Touch touch = new Touch();
        draggedCoinName = gameObject.name;

        draggedCoin = gameObject;

        dropCoin = correctJar.GetComponent<DropCoin>();

        hoverCoin = GetComponent<HoverCoin>();

        moving = false;

        finish = false;

        rb = GetComponent<Rigidbody2D>();

        // Add bouncy material to the coin
        mat = new PhysicsMaterial2D
        {

            bounciness = 0.4f, //was .75

            friction = 0.4f

        };

        CoinCollider = GetComponent<CircleCollider2D>();

        CoinCollider.sharedMaterial = mat;

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

        OnMouseDownOffloader();

    }


    public void OnMouseDownOffloader()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePosOld;

            mousePosOld = Input.mousePosition;

            mousePosOld = Camera.main.ScreenToWorldPoint(mousePosOld);

            Debug.Log("OnMouseDown.mousePosOld is " + mousePosOld);

            //physics to allow smooth movement of coin
            rb.freezeRotation = true;

            rb.velocity = new Vector2(0, 0);

            rb.gravityScale = 0;



            CoinCollider.sharedMaterial = null;
            //physics end

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

        OnMouseUpOffloader();

    }


    public void OnMouseUpOffloader()
    {

        moving = false;

        if (Mathf.Abs(draggedCoin.transform.position.x - droppedCoinPosition.transform.position.x) <= 1f &&
             Mathf.Abs(draggedCoin.transform.position.y - droppedCoinPosition.transform.position.y) <= 1f)
        {

            ScoreCounter.scoreValue += 500;

            dropCoin.SoundSystem(false, true, draggedCoinName);

            StartCoroutine(BounceCoin());

        }

        else

        {

            Debug.Log("OnMouseUp.else.transform.position is  " + transform.position);

            transform.position = new Vector3(resetPosition.x, resetPosition.y, 0);

            dropCoin.SoundSystem(false, false, draggedCoinName);

        }

    }
    /****///end of UpdateOldTouch


    public void DisableCoin()
    {

        //this.gameObject.SetActive(false);

        //this.enabled = false;


        Destroy(CoinCollider, 0f);

        Destroy(rb, 0f);

        Destroy(hoverCoin, 0f);

        Destroy(this, 0f);



        //if (this.enabled)
        //{

        //    Debug.Log("Object still exists");

        //    this.enabled = false;

        //}
        //else
        //{

        //    Debug.Log("Object was destroyed");

        //}

        Destroy(GetComponent<DragCoin>());
    }


    public IEnumerator BounceCoin()
    {


        // scale to fit coin in jar;
        float draggedCoinScale = coinScales[draggedCoinName] * .8f;

        transform.position = new Vector3(droppedCoinPosition.transform.position.x,
            transform.position.y, transform.position.z);

        transform.localScale = new Vector2(draggedCoinScale, draggedCoinScale);

        //Change body type to allow free fall bounce coin;
        //rb.bodyType.Equals(RigidbodyType2D.Dynamic);

        //rb.collisionDetectionMode.Equals(CollisionDetectionMode.ContinuousDynamic);

        //physics to allow coin to drop & bounce
        rb.freezeRotation = false;

        rb.sharedMaterial = mat;

        //rb.AddForce(new Vector2(0f, -1f) );

        rb.gravityScale = 2f; //was 2

        //wait 1 sec for bounce to finish
        yield return new WaitForSeconds(2);

        //freeze Transform Position to stick coin in jar
        transform.position = new Vector3(droppedCoinPosition.transform.position.x,
            droppedCoinPosition.transform.position.y - .8f, droppedCoinPosition.transform.position.z);

        //Destroy(draggedCoin) 

        yield return new WaitForSeconds(.01f);

        DisableCoin();

    }


    /****///start of UpdateNewTouch
          //public void UpdateNewTouch() 
          //{

    //    // Initiating touch event
    //    // if touch event takes place
    //    if (Touch.activeTouches.Count > 0)
    //    {

    //        // get touch position
    //        //Touch touch = new Touch();
    //        touch = Touch.activeTouches.FirstOrDefault();
    //        //Input.GetTouch(0);

    //        Vector2 touchScreenPosition = Touchscreen.current.position.ReadValue();

    //        // obtain touch position
    //        touchPosition = Camera.main.ScreenToWorldPoint(touchScreenPosition, 0f); 
    //        // Touchscreen.current.position.ReadValue()); // touch.screenPosition);

    //        Debug.Log("Update.if.Input.touchPosition => 0 .position is " + touchPosition);

    //        // get touch to take a deal with
    //        switch (touch.phase)
    //        {

    //            // if you touches the screen
    //            case touchScr.Began:

    //                // if you touch the coin
    //                if (draggedCoin.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPosition))
    //                {

    //                    // get the offset between position you touches
    //                    // and the center of the game object
    //                    deltaX = touchPosition.x - transform.position.x;

    //                    deltaY = touchPosition.y - transform.position.y;

    //                    Debug.Log("Update.touch.phase == TouchPhase.Began.deltaX is  " + deltaX);

    //                    Debug.Log("Update.touch.phase == TouchPhase.Began.deltaY is  " + deltaY);

    //                    // if touch begins within the coin collider
    //                    // then it is allowed to move
    //                    moveAllowed = true;

    //                    // restrict some rigidbody properties so it moves
    //                    // more smoothly and correctly
    //                    rb.freezeRotation = true;

    //                    rb.velocity = new Vector2(0, 0);

    //                    rb.gravityScale = 0;

    //                    CoinCollider.sharedMaterial = null;

    //                    //play coin pickUp sound
    //                    dropCoin.SoundSystem(true, false, draggedCoinName);

    //                }

    //            break;


    //            // you move your finger
    //            case touchScr.Moved:

    //                //enlarge coin
    //                //selectedGameObject.
    //                transform.localScale = new Vector2(1.05f, 1.05f);
    //                //Debug.Log("Update.touch.phase == TouchPhase.Moved : coin enlarged");

    //                // if you touch the coin and movement is allowed, then you can move it
    //                if (CoinCollider == Physics2D.OverlapPoint(touchPosition) && moveAllowed)
    //                    rb.MovePosition(new Vector2(touchPosition.x - deltaX, touchPosition.y - deltaY));

    //                Debug.Log("Update.touch.phase == TouchPhase.Moved -  drag started ");

    //                moving = true;

    //            break;


    //            // you release your finger
    //            case touchScr.Ended:

    //                // restore initial parameters
    //                // when touch is ended
    //                moving = false;

    //                moveAllowed = false;

    //                rb.freezeRotation = false;

    //                rb.gravityScale = 2;
    //                PhysicsMaterial2D mat = new PhysicsMaterial2D
    //                {
    //                    bounciness = 0.75f,
    //                    friction = 0.4f
    //                };

    //                CoinCollider.sharedMaterial = mat;

    //                // Restore the regular size of the coin.
    //                //selectedGameObject.
    //                transform.localScale = new Vector2(1/1.05f, 1/1.05f);
    //                //Debug.Log("UpdateNewTouch.touch.phase == TouchPhase.Ended : coin restored to normal size ");

    //                if (Mathf.Abs(draggedCoin.transform.position.x - droppedCoinPosition.transform.position.x) <= 1f &&
    //                    Mathf.Abs(draggedCoin.transform.position.y - droppedCoinPosition.transform.position.y) <= 1f)
    //                {

    //                    Debug.Log("Update.TouchPhase.Ended.if.transform.position.x is  "
    //                        + transform.position.x);

    //                    Debug.Log("Update.TouchPhase.Ended.if.transform.position.y is  "
    //                        + transform.position.y);

    //                    //transform.position = new Vector2(droppedCoinPosition.x,   droppedCoinPosition.y);

    //                    Debug.Log("Update.TouchPhase.Ended.if.transform.position: coin was dropped at: "
    //                        + transform.position);

    //                    ScoreCounter.scoreValue += 500;

    //                    Debug.Log("Update.TouchPhase.Ended.if.ScoreCounter updated: Score is  " + ScoreCounter.scoreValue);

    //                    dropCoin.SoundSystem(false, true, draggedCoinName);

    //                    //transform.localScale = new Vector2(.01f, .01f);

    //                }

    //                //else if (gameObject)//check if game object isnt destroyed
    //                //{
    //                //
    //                //    transform.position = new Vector2(resetPosition.x, resetPosition.y);
    //                //
    //                //    Debug.Log("Update.TouchPhase.Ended.else.transform.position is  "
    //                //        + transform.position);
    //                //
    //                //    dropCoin.SoundSystem(false, true, draggedCoinName);dropCoin.SoundSystem(false, false, draggedCoinName);
    //                //
    //                //    Destroy(gameObject);
    //                //}

    //            break;

    //        }

    //    }

    //}

    /****///end of UpdateNewTouch


    /****///Start of Trigger && Collision events
          //void OnCollisionEnter2D(Collision2D collision)
          //{

    //    if (collision.gameObject.tag is "CoinsNotDropped")
    //    {
    //        Physics2D.IgnoreCollision(collision.collider, CoinCollider);

    //        Destroy(draggedCoin);

    //        Debug.Log(gameObject + "has Destroyed from DragCoin.OnCollisionEnter2D " + gameObject.name);

    //    }

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