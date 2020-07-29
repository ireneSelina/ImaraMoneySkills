using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using System.Linq;

public class DragCoin : MonoBehaviour
{
    // touch offset allows coin not to shake when it starts moving
    float deltaX, deltaY;

    public GameObject CorrectForm;
    public GameObject correctJar;


    Vector3 touchPos; 
    public Vector2 resetPosition;

    private string coinName;

    //private GameObject selectedGameObject;
    //private SortTopCoin sortTopCoin;
    public DropCoin dropCoin;
    //private SpriteRenderer sortSpriteRenderer;

    private bool moving;
    private bool finish;

    //public UnityEngine.InputSystem.Controls.Vector2Control position { get; }
    private Touch touch;

    // reference to Rigidbody2D component
    Rigidbody2D rb;


    // coin movement not allowed if you touches not the coin at the first time
    bool moveAllowed = false;

    private void Awake()
    {
        UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();
        foreach (var touch in Touch.activeTouches)
            Debug.Log($"{touch.touchId}: {touch.screenPosition},{touch.phase}");
    }

    // Use this for initialization
    void Start()
    {
        touch = new Touch();
        coinName = gameObject.name;
        dropCoin = correctJar.GetComponent<DropCoin>();
        //selectedGameObject = gameObject;
        //sortSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //sortTopCoin = gameObject.AddComponent<SortTopCoin>();
        //dropCoin = gameObject.AddComponent<DropCoin>();


        finish = false;

        rb = GetComponent<Rigidbody2D>();

        // Add bouncy material tha coin
        PhysicsMaterial2D mat = new PhysicsMaterial2D
        {
            bounciness = 0.75f,
            friction = 0.4f
        };
        GetComponent<CircleCollider2D>().sharedMaterial = mat;
        
        resetPosition = transform.localPosition;
        Debug.Log("Start.resetPosition of " + coinName + " is  " + resetPosition); 

    }


    // Update is called once per frame
    void Update()
    {
        if (!finish)
        {
            // Initiating touch event
            // if touch event takes place
            //int[] touchSize = Touch.activeTouches.Count();
            if (Touch.activeTouches.Count > 0)
            {
           
                //if (moving)
                //{
                    // get touch position
                    touch = Touch.activeTouches.First();
                    Vector2 touchScreenPosition = Touchscreen.current.position.ReadValue();  //Input.GetTouch(0);

                    // obtain touch position
                    touchPos = Camera.main.ScreenToWorldPoint(touchScreenPosition, 0f); // Touchscreen.current.position.ReadValue()); // touch.screenPosition);

                    Debug.Log("Update.if.Input.touchPos => 0 .position is " + touchPos);

                    // get touch to take a deal with
                    switch (touch.phase)
                    {
                        // if you touches the screen
                        case UnityEngine.InputSystem.TouchPhase.Began:

                            // if you touch the coin
                            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                            {
                                //sortTopCoin.SortCoinToTop(sortSpriteRenderer);

                                // get the offset between position you touches
                                // and the center of the game object
                                deltaX = touchPos.x - transform.position.x;
                                deltaY = touchPos.y - transform.position.y;

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
                                GetComponent<CircleCollider2D>().sharedMaterial = null;

                                //play coin pickUp sound
                                dropCoin.SoundSystem(true, false, coinName);

                            }
                            break;


                        // you move your finger
                        case UnityEngine.InputSystem.TouchPhase.Moved:

                            //enlarge coin
                            //selectedGameObject.
                            //transform.localScale = new Vector2(1.2f, 1.2f);
                            //Debug.Log("Update.touch.phase == TouchPhase.Moved : coin enlarged");

                            // if you touch the coin and movement is allowed, then you can move it
                            if (GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(touchPos) && moveAllowed)
                                rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));

                            Debug.Log("Update.touch.phase == TouchPhase.Moved -  drag started ");

                            moving = true;
                            break;


                        // you release your finger
                        case UnityEngine.InputSystem.TouchPhase.Ended:

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
                            GetComponent<CircleCollider2D>().sharedMaterial = mat;

                            // Restore the regular size of the coin.
                            //selectedGameObject.
                            //transform.localScale = new Vector2(1/1.2f, 1/1.2f);
                            //Debug.Log("UpdateNewTouch.touch.phase == TouchPhase.Ended : coin restored to normal size ");

                            if (Mathf.Abs(transform.localPosition.x - CorrectForm.transform.localPosition.x) <= .8f &&
                                 Mathf.Abs(transform.localPosition.y - CorrectForm.transform.localPosition.y) <= .8f)
                            {

                                Debug.Log("Update.TouchPhase.Ended.if.gameObject.transform.localPosition.x is  "
                                    + transform.localPosition.x);
                                Debug.Log("Update.TouchPhase.Ended.if.gameObject.transform.localPosition.y is  "
                                    + transform.localPosition.y);

                                transform.position = new Vector2(CorrectForm.transform.position.x,
                                    CorrectForm.transform.position.y);
                                Debug.Log("Update.TouchPhase.Ended.if.gameObject.transform.localPosition: coin was dropped at: "
                                    + transform.position);

                                ScoreCounter.scoreValue += 500;
                                Debug.Log("Update.TouchPhase.Ended.if.ScoreCounter updated: Score is  " + ScoreCounter.scoreValue);

                                dropCoin.SoundSystem(false, true, coinName);
                            }
                            else
                            {
                                transform.localPosition = new Vector2(resetPosition.x, resetPosition.y);
                                Debug.Log("Update.TouchPhase.Ended.else.gameObject.transform.localPosition is  "
                                    + transform.localPosition);

                                dropCoin.SoundSystem(false, false, coinName);
                            }

                            break;
                    }
                //}
            }
               
        }
    }

    //private void FixedUpdate()
    //{
        
    //}

}