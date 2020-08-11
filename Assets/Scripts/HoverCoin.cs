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

        hoverStatus = false;

        hoverHeight = (maxHeight + minHeight) / 2.0f;

        hoverRange = maxHeight - minHeight;

        hoverSpeed = Random.Range(2.99f, 3.01f);

        coinPosition = transform.localPosition;

        timeValue = Random.Range(0f, .01f);

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.deltaTime > timeValue)
        {
            hoverStatus = true;

            CoinHoverHandler();

        }
    }

    public void CoinHoverHandler()
    {

        if (transform.localPosition.x == coinPosition.x)
        {

            transform.localPosition = new Vector3(coinPosition.x, (coinPosition.y +
                (.1f * hoverHeight + Mathf.Cos(Time.time * hoverSpeed) * hoverRange)), coinPosition.z);

        }

    }


}
