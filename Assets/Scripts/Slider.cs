using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slider : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    // touch offset allows coin not to shake when it starts moving
    private GameObject draggedCoin;

    private Vector2 resetPosition;

    private string draggedCoinName;

    //bool moving, finish;

    private BoxCollider2D SliderCollider;

    //private MenuScript menuScript;

    private ScoreCounter scoreCounter;

    private AudioManager audioManager;

    CoinSpawner coinSpawner;

    bool RightCoinsAreMore;


    int cycleNumber;

    ///startfromdragger
    Camera mainCamera;
    float draggedDistance;
    //Vector3 clickOffset = Vector3.zero;
    ///endfromdragger

    //public UnityEvent pointerDown;  // appears in Editor as "OnClick()"
    //public UnityEvent pointerEnter;  //                    -//-
    //public UnityEvent pointerUp;     //                    -//-
    //public UnityEvent pointerExit;


    // Use this for initialization
    void Start()
    {
        coinSpawner = gameObject.AddComponent<CoinSpawner>();


        audioManager = gameObject.AddComponent<AudioManager>();

        scoreCounter = gameObject.AddComponent<ScoreCounter>();

        //menuScript = gameObject.AddComponent<MenuScript>();

        draggedDistance = 0;

        //Touch touch = new Touch();
        draggedCoinName = gameObject.name;

        draggedCoin = gameObject;

        //moving = false;

        //finish = false;

        resetPosition = transform.position;

        Debug.Log("Start.resetPosition of " + draggedCoinName + " is  " + resetPosition);

        SliderCollider = gameObject.GetComponent<BoxCollider2D>();

        cycleNumber = 0;

        ///startfromdragger
        mainCamera = Camera.main;
        if (mainCamera.GetComponent<Physics2DRaycaster>() == null)
            mainCamera.gameObject.AddComponent<Physics2DRaycaster>();

        ///endfromdragger

        ///startfromdragger
        ///endfromdragger

    }

    private void Update()
    {

        Vector3 mousePosOld = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.position.x - mousePosOld.x > 0 ? 0 : 180);

    }

    void GetRightCoinsValue() 
    {
        RightCoinsAreMore = coinSpawner.GetRightCoinsAreMore();

        Debug.Log("RightCoinsAreMore is " + RightCoinsAreMore);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }
    //startfromdragger
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag Slider started ................................" + eventData.pointerCurrentRaycast.worldPosition);
        //moving = true;

    }

    public void OnDrag(PointerEventData eventData)
    {

        Debug.Log("OnDrag Slider started ................................" + eventData.pointerCurrentRaycast.worldPosition);
        //Use Offset To Prevent Sprite from Jumping to where the finger is
        Vector3 tempVec = mainCamera.ScreenToWorldPoint(eventData.position);// + clickOffset;

        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.position.x - tempVec.x > 0 ? 0 : 180);

        //transform.Translate(eventData.delta);
        transform.position = new Vector3(tempVec.x, transform.position.y, transform.position.z); //initially was tempVec;

        //if (!finish && moving)
        //{
        //    draggedDistance = transform.position.x - resetPosition.x;
        //}
        draggedDistance = transform.position.x - resetPosition.x;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        Debug.Log("OnEndDrag Slider started ................................" + eventData.pointerCurrentRaycast.worldPosition);
        OnMouseUpOffloader();

    }



    //Add Event System to the Camera
    void AddEventSystem()
    {
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }

    ///endfromdragger
    void SuccessScore()
    {

        Debug.Log("OnMouseUp.if.transform.position.x is  " + transform.position.x);

        int score = Random.Range(501, 599);

        scoreCounter.SliderScore(score, true);

        audioManager.PlaySound("AlreadyPickedAndMatched");

        transform.position = resetPosition;

        cycleNumber++;

        if (cycleNumber == 3)
        {

            StartCoroutine(WaitToDisableSlider());
        }
        else 
        {
            coinSpawner.SpawnSystem();
        }

    }


    void FailedScore()
    {

        audioManager.PlaySound("PickedAndNotMatched");

        Debug.Log("OnMouseUp.else.transform.position is  " + transform.position.x);

        cycleNumber++;

        if (cycleNumber == 3)
        {

            StartCoroutine(WaitToDisableSlider());
        }
        else
        {
            coinSpawner.SpawnSystem();
        }

    }

    public void OnMouseUpOffloader()
    {

        //moving = false;
        GetRightCoinsValue();

        if (draggedCoin.transform.position.x - resetPosition.x >= 3 && draggedDistance > 0)
        {

            if (RightCoinsAreMore)
            {
                SuccessScore();
            }
            else 
            {
                FailedScore();
            }

        }

        //else 
        if (draggedCoin.transform.position.x - resetPosition.x <= -3 && draggedDistance < 0)

        {

            if (!RightCoinsAreMore)
            {
                SuccessScore();
            }
            else
            {
                FailedScore();
            }
            //ScoreCounter.scoreValue -= 500;

        }

    }

    /****///end of UpdateOldTouch


    public void DisableSlider()
    {

        //this.gameObject.SetActive(false);

        //this.enabled = false;

        
        Destroy(SliderCollider, 0f);

        Destroy(this, 0f);

        //menuScript.LoadMainMenu();

        //if (this.enabled)
        //{

        //    Debug.Log("Object still exists");

        //    this.enabled = false;

        //}
        //else
        //{

        //    Debug.Log("Object was destroyed");

        //}

        Destroy(GetComponent<Slider>());
    }


    public IEnumerator WaitToDisableSlider()
    {
        yield return new WaitForSeconds(.01f);

        DisableSlider();

    }


    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    pointerDown.Invoke();
    //}
    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    pointerUp.Invoke();
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    pointerDown.Invoke();
    //}
    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    pointerUp.Invoke();
    //}


    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    pointerEnter.Invoke();
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    pointerExit.Invoke();
    //}

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