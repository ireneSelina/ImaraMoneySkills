using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Slider : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    // touch offset allows coin not to shake when it starts moving
    private GameObject draggedCoin;

    private Vector2 resetPosition;

    private string draggedCoinName;

    bool moving, finish;

    private BoxCollider2D SliderCollider;

    private MenuScript menuScript;

    ///startfromdragger
    Camera mainCamera;
    float draggedDistance;
    //Vector3 clickOffset = Vector3.zero;
    ///endfromdragger

    // Use this for initialization
    void Start()
    {
        menuScript = gameObject.AddComponent<MenuScript>();

        draggedDistance = 0;

        //Touch touch = new Touch();
        draggedCoinName = gameObject.name;

        draggedCoin = gameObject;

        moving = false;

        finish = false;

        resetPosition = transform.position;

        Debug.Log("Start.resetPosition of " + draggedCoinName + " is  " + resetPosition);

        SliderCollider = gameObject.GetComponent<BoxCollider2D>();

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

    //startfromdragger
    public void OnBeginDrag(PointerEventData eventData)
    {

        moving = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Use Offset To Prevent Sprite from Jumping to where the finger is
        Vector3 tempVec = mainCamera.ScreenToWorldPoint(eventData.position);// + clickOffset;

        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.position.x - tempVec.x > 0 ? 0 : 180);

        transform.position = new Vector3(tempVec.x, transform.position.y, transform.position.z); //initially was tempVec;

        if (!finish && moving)
        {
            draggedDistance = transform.position.x - resetPosition.x;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
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


    public void OnMouseUpOffloader() 
    {

        moving = false;

        if (draggedCoin.transform.position.x - resetPosition.x >= 3 && draggedDistance > 0)
        {

            Debug.Log("OnMouseUp.if.transform.position.x is  " + transform.position.x);

            ScoreCounter.scoreValue += 500;

            StartCoroutine(WaitToDisableSlider());
        }

        else if (draggedCoin.transform.position.x - resetPosition.x <= -3 && draggedDistance < 0)

        {

            Debug.Log("OnMouseUp.else.transform.position is  " + transform.position.x);

            //ScoreCounter.scoreValue -= 500;

        }

    }

    /****///end of UpdateOldTouch


    public void DisableSlider()
    {

        //this.gameObject.SetActive(false);

        //this.enabled = false;

        Slider.Destroy(SliderCollider, 0f);

        menuScript.LoadMainMenu();

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