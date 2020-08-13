using UnityEngine;
using UnityEngine.SceneManagement;
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

    Scene currentScene;


    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentScene == SceneManager.GetSceneByBuildIndex(1))
        {
            hoverStatus = false;
        }
        else 
        {
            hoverStatus = true;
        }


        hoverHeight = (maxHeight + minHeight) / 2.0f;

        hoverRange = maxHeight - minHeight;

        hoverSpeed = Random.Range(2.99f, 3.01f);

        coinPosition = transform.position;

        timeValue = Random.Range(0f, .1f);

    }

    // Update is called once per frame
    void Update()
    {
        if (hoverStatus)
        {

            transform.position = new Vector3(coinPosition.x, (coinPosition.y +
                (.1f * hoverHeight + Mathf.Cos(Time.time * hoverSpeed) * hoverRange)), coinPosition.z);

        }

        else 

        {
             
            CoinHoverHandler();

            //if (Time.deltaTime > timeValue)
            //{


            //}

        }

    }

    public void CoinHoverHandler()
    {

        if (transform.position.x == coinPosition.x)
        {

            transform.position = new Vector3(coinPosition.x, (coinPosition.y +
                (.1f * hoverHeight + Mathf.Cos(Time.time * hoverSpeed) * hoverRange)), coinPosition.z);

        }

    }


}
