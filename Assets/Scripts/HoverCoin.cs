using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class HoverCoin : MonoBehaviour
{
    public float maxHeight, minHeight;

    float hoverHeight, hoverRange, hoverSpeed;

    Vector3 coinPosition;

    public bool hoverStatus;

    public float timeValue = 0f;

    public int timeInt;

    // Start is called before the first frame update
    void Start()
    {

        hoverStatus = true;

        hoverHeight = (maxHeight + minHeight) / 2.0f; //Min and Max height are either not defined or if they are like above i get a field initialiser cannot reference the nonstatic field

        hoverRange = maxHeight - minHeight;

        hoverSpeed = Random.Range(2.99f, 3.01f);

        coinPosition = transform.position;

        timeValue = Random.Range(.0f, .02f);

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.deltaTime >= timeValue)
        {

            if (hoverStatus && transform.position.x == coinPosition.x)
            {

                transform.position = new Vector3(coinPosition.x, (coinPosition.y + 
                    (.1f * hoverHeight + Mathf.Cos(Time.time * hoverSpeed) * hoverRange)), coinPosition.z);

            }

        }

    }

    //public void CoinHoverHandler()
    //{
        

    //} 


}
