using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public float timeValue = 60f;

    public int timeInt;

    public TextMeshProUGUI timeText;

    public bool timeLeft;

    // Start is called before the first frame update
    void Awake()
    {
        timeText = GetComponent <TextMeshProUGUI> ();

        timeLeft = true;
        //StartCoroutine ( "CountDownTimer" );
    }

    // Update is called once per frame
    void Update()
    {
        if ( timeLeft ) 
        {
            CountDownTimerB();
        }
    }

    public void CountDownTimerB() 
    {
        timeValue -= Time.deltaTime;

        timeInt = ( int ) timeValue;

        timeText.text = "TIME: " + timeInt + " sec";

        if ( timeValue < 0 )
        {
            timeLeft = false ;

            Debug.Log ( "No more time left" );
            //show summary UI here
            //go to level completed/ failed menu?
        }
    }

    /*
     IEnumerator CountDownTimer ()
    {
        timeText.text = "TIME: " + timeValue + "s";
        Debug.Log ( timeText.text );
        yield return new WaitForSeconds ( 1 );
        timeValue-- ;

        if ( timeValue == 0 )
        {
            //yield return null;
        }
    }
    */

}
