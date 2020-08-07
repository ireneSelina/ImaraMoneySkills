using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using System.Linq;
using UnityEngine.SceneManagement;

public class Slider : MonoBehaviour
{

    // touch offset allows coin not to shake when it starts moving
    private GameObject draggedCoin;

    private float startPosX, draggedDistance;

    private Vector2 resetPosition;

    private string draggedCoinName;

    bool moving, finish;

    private BoxCollider2D SliderCollider;

    private MenuScript menuScript;



    // Use this for initialization
    void Start()
    {

        draggedDistance = 0;

        //Touch touch = new Touch();
        draggedCoinName = gameObject.name;

        draggedCoin = this.gameObject;

        moving = false;

        finish = false;

        resetPosition = transform.position;

        Debug.Log("Start.resetPosition of " + draggedCoinName + " is  " + resetPosition);

        SliderCollider = gameObject.GetComponent<BoxCollider2D>();

    }


    // Update is called once per frame
    void Update()
    {

        UpdateOldTouch();

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

                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.position.x - mousePosOld.x > 0 ? 0 : 180);

                transform.position =
                    new Vector3(mousePosOld.x - startPosX, transform.position.y, transform.position.z);

                draggedDistance = transform.position.x - resetPosition.x;

                Debug.Log("Update.transform.position is  " + transform.position + " and draggedDistance is  " + draggedDistance);

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

            startPosX = mousePosOld.x - transform.position.x;

            Debug.Log("OnMouseDown.startPosX is  " + startPosX);

            //play coin pickUp sound

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

        if (draggedCoin.transform.position.x - resetPosition.x >= 3 && draggedDistance > 0)
        {

            Debug.Log("OnMouseUp.if.transform.position.x is  " + transform.position.x);

            ScoreCounter.scoreValue += 500;

            StartCoroutine(WaitToDisableSlider());
        }

        else if (draggedCoin.transform.position.x - resetPosition.x <= -3 && draggedDistance < 0)

        {

            Debug.Log("OnMouseUp.else.transform.position is  " + transform.position.x);

            ScoreCounter.scoreValue -= 500;

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