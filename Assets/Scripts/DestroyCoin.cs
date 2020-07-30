using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCoin : MonoBehaviour
{
    public GameObject correctCoin, correctJar;

    // Start is called before the first frame update
    void Start()
    {
        //dropCoin = correctCoin.GetComponent<DropCoin>();
    }

    // Update is called once per frame
    
    void Update()
    {

    }
    
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag is "Coins" && gameObject.name.Equals(coinMatch))
    //    {
    //        Destroy(gameObject);
    //        Debug.Log(" Destroyed from DropCoin.OnCollisionEnter2D " + gameObject.name);
    //    }
    //}
    
    void OnTriggerEnter2D(Collider2D correctCoin)
    {
        if (correctCoin.gameObject.tag is "CoinsNotDropped"  && correctJar.gameObject.tag is "Jars")
        {
            Destroy(correctCoin);
            Debug.Log(gameObject.name + "has Destroyed from DropCoin.OnTriggerEnter2D " + correctCoin.name);
        }
    }
    
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.tag is "Coins")
    //    {
    //        //Destroy(gameObject);
    //        Debug.Log(" Destroyed from DropCoin.OnTriggerExit2D " + gameObject.name);
    //    }
    //}
}
