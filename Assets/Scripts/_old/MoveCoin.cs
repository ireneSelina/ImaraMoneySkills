using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class MoveCoin : MonoBehaviour
{
    public GameObject CorrectForm;
    public bool moving, finish;

    public float startPosX, startPosY;

    private float width, height;

    public Vector3 resetPosition, position;

    public AudioSource bgMusic, sfx, sfxVoice;

    public AudioClip[] correct;
    public AudioClip incorrect, coinDrop, coinPickUp;


    void Awake()
    {
        width = ( float ) Screen.width / 2.0f;
        height = ( float ) Screen.height / 2.0f;
        Debug.Log ( "Awake.width is " + width );
        Debug.Log ( "Awake.height is " + height );

        // Position used for the coin.
        position = new Vector3 ( 0.0f, 0.0f, 0.0f );
    }

    void OnGUI()
    {
        // Compute a fontSize based on the size of the screen width.
        GUI.skin.label.fontSize = ( int ) ( Screen.width / 25.0f );

        GUI.Label ( new Rect(20, 20, width, height * 0.25f ),
            "x = " + position.x.ToString ( "f2" ) +
            ", y = " + position.y.ToString ( "f2" ) );
    }

    void Start ()
    {
        resetPosition = gameObject.transform.localPosition;
        Debug.Log ( "Start.resetPosition is  " + resetPosition );

        finish = false;
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateNewTouch();

        //UpdateOldTouch();

        
    }

    public void UpdateNewTouch() 
    {
        // Handle screen touches.
        if ( Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch ( 0 );

            // Move the coin if the screen has the finger moving.
            if ( touch.phase.Equals ( UnityEngine.InputSystem.TouchPhase.Moved ) )
            {
                Debug.Log ( "UpdateNewTouch.touch.phase == TouchPhase.Moved - coin drag started " );

                Vector2 pos = touch.position;
                pos.x = ( pos.x - width ) / width;
                pos.y = ( pos.y - height ) / height;
                position = new Vector3 ( -pos.x, pos.y, 0.0f );

                // Position the coin.
                transform.position = position;

                /****///start of snippet sync from UpdateOld
                position = Camera.main.ScreenToWorldPoint ( position );
                Debug.Log ( "UpdateNewTouch.position is " + position );

                startPosX = position.x - gameObject.transform.localPosition.x;
                startPosY = position.y - gameObject.transform.localPosition.y;

                Debug.Log ( "UpdateNewTouch.startPosX is  " + startPosX );
                Debug.Log ( "UpdateNewTouch.startPosY is  " + startPosY );

                sfx.clip = coinPickUp;
                sfx.Play();
                Debug.Log ( "UpdateNewTouch.sfx.clip coinPickUp played" );

                moving = true;
                /****///end of snippet sync from UpdateOld

            }

            if ( Input.touchCount == 2 )
            {
                touch = Input.GetTouch ( 1 );

                if ( touch.phase.Equals ( UnityEngine.InputSystem.TouchPhase.Began ) )
                {
                    // Enlarge the size of the coin.
                    transform.localScale = new Vector3 ( 1.2f, 1.2f, 1.2f );
                    Debug.Log ( "UpdateNewTouch.touch.phase == TouchPhase.Began : coin enlarged" );

                    /****///start of snippet sync from UpdateOld
                    if ( !finish )
                    {
                        if ( moving )
                        {
                            //Vector3 position;
                            //position = Input.mousePosition;
                            //position = Camera.main.ScreenToWorldPoint( position );

                            gameObject.transform.localPosition =
                                new Vector3 ( position.x - startPosX, position.y - startPosY, gameObject.transform.localPosition.z );

                            Debug.Log ( "UpdateNewTouch.gameObject.transform.localPosition is " + gameObject.transform.localPosition );
                        }
                    }
                    /****///end of snippet sync from UpdateOld

                }

                if ( touch.phase.Equals ( UnityEngine.InputSystem.TouchPhase.Ended ) )
                {
                    // Restore the regular size of the coin.
                    transform.localScale = new Vector3 ( 1.0f, 1.0f, 1.0f ); 
                    Debug.Log ( "UpdateNewTouch.touch.phase == TouchPhase.Ended : coin restored to normal size " );

                    /****///start of snippet sync from UpdateOld
                    moving = false;

                    if ( Mathf.Abs ( gameObject.transform.localPosition.x - CorrectForm.transform.localPosition.x ) <= .8f &&
                         Mathf.Abs ( gameObject.transform.localPosition.y - CorrectForm.transform.localPosition.y ) <= .8f )
                    {

                        Debug.Log ( "UpdateNewTouch.TouchPhase.Ended.if.gameObject.transform.localPosition.x is  " 
                            + gameObject.transform.localPosition.x );
                        Debug.Log ( "UpdateNewTouch.TouchPhase.Ended.if.gameObject.transform.localPosition.y is  " 
                            + gameObject.transform.localPosition.y );

                        gameObject.transform.position = new Vector3 ( CorrectForm.transform.position.x,
                            CorrectForm.transform.position.y, CorrectForm.transform.position.z ); 
                        Debug.Log ( "UpdateNewTouch.TouchPhase.Ended.if.gameObject.transform.localPosition: coin was dropped at: " 
                            + gameObject.transform.position );

                        ScoreCounter.scoreValue += 500;
                        Debug.Log ( "UpdateNewTouch.TouchPhase.Ended.if.ScoreCounter updated: Score is  " + ScoreCounter.scoreValue );

                        StartCoroutine ( SoundBoom( true ) );
                    }
                    else
                    {
                        gameObject.transform.localPosition = new Vector3 ( resetPosition.x, resetPosition.y, resetPosition.z );
                        Debug.Log ( "UpdateNewTouch.TouchPhase.Ended.else.gameObject.transform.localPosition is  " 
                            + gameObject.transform.localPosition );

                        StartCoroutine ( SoundBoom( false ) );
                    }
                    /****///end of snippet sync from UpdateOld
                }
            }
        }
    }

    IEnumerator SoundBoom ( bool SuccessType )
    {

        if ( SuccessType == true )
        {
            sfx.clip = coinDrop;
            sfx.Play();

            yield return new WaitForSeconds ( 1 );

            sfxVoice.clip = correct [ Random.Range ( 0, correct.Length ) ];
            sfxVoice.Play();
        }
        else
        {
            sfx.clip = incorrect;
            sfx.Play();
        }
        //yield return null;

    }



    /****///start of UpdateOldTouch
    //public void UpdateOldTouch()
    //{
    //    if ( !finish )
    //    {
    //        if ( moving )
    //        {
    //            Vector3 mousePosOld;
    //            mousePosOld = Input.mousePosition;
    //            mousePosOld = Camera.main.ScreenToWorldPoint( mousePosOld );

    //            gameObject.transform.localPosition =
    //                new Vector3( mousePosOld.x - startPosX, mousePosOld.y - startPosY, gameObject.transform.localPosition.z );

    //            Debug.Log( "Update.gameObject.transform.localPosition is  " + gameObject.transform.localPosition );
    //        }
    //    }
    //}


    //public void OnMouseDown()
    //{
    //    if ( Input.GetMouseButtonDown( 0 ) )
    //    {
    //        Vector3 mousePosOld;
    //        mousePosOld = Input.mousePosition;
    //        mousePosOld = Camera.main.ScreenToWorldPoint( mousePosOld );

    //        Debug.Log("OnMouseDown.mousePosOld is " + mousePosOld);

    //        startPosX = mousePosOld.x - gameObject.transform.localPosition.x;
    //        startPosY = mousePosOld.y - gameObject.transform.localPosition.y;

    //        Debug.Log( "OnMouseDown.startPosX is  " + startPosX );
    //        Debug.Log( "OnMouseDown.startPosY is  " + startPosY );

    //        sfx.clip = coinPickUp;
    //        sfx.Play();

    //        moving = true;
    //    }
    //}

    //public void OnMouseUp()
    //{
    //    moving = false;

    //    if (Mathf.Abs( gameObject.transform.localPosition.x - CorrectForm.transform.localPosition.x ) <= .8f &&
    //         Mathf.Abs( gameObject.transform.localPosition.y - CorrectForm.transform.localPosition.y ) <= .8f )
    //    {
    //        Debug.Log( "OnMouseUp.if.gameObject.transform.localPosition.x is  " + gameObject.transform.localPosition.x );
    //        Debug.Log( "OnMouseUp.if.gameObject.transform.localPosition.y is  " + gameObject.transform.localPosition.y );

    //        gameObject.transform.position = new Vector3(CorrectForm.transform.position.x,
    //            CorrectForm.transform.position.y, CorrectForm.transform.position.z);

    //        ScoreCounter.scoreValue += 500;
    //        StartCoroutine( SoundBoom( true ) );
    //    }
    //    else
    //    {
    //        Debug.Log( "OnMouseUp.else.gameObject.transform.localPosition is  " + gameObject.transform.localPosition );
    //        gameObject.transform.localPosition = new Vector3( resetPosition.x, resetPosition.y, resetPosition.z );

    //        StartCoroutine( SoundBoom( false ) );
    //    }
    //}
    /****///end of UpdateOldTouch


}
