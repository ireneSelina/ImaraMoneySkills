using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Linq;

public class CoinSpawner : MonoBehaviour
{

    //public GameObject oneShilling, fiveShilling, tenShilling, twentyShilling, fortyShilling, fiftyCent;
    Dictionary<string, float> CoinValues = new Dictionary<string, float>()
    {
          {"Forty_Shilling_Coin",    40f},
          {"Fifty_Cent_Coin",        .5f},
          {"One_Shilling_Coin",      1f},
          {"Ten_Shilling_Coin",      10f},
          {"Twenty_Shilling_Coin",   20f},
          {"Five_Shilling_Coin",     5f}
    };


    // Start is called before the first frame update
    void Start()
    {
        //oneShilling missileCopy = Instantiate<>();
        SpawnSystem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCoins() 
    {
        
    
   
    
    }

    public static void SpawnSystem()
    {
        GameObject LeftCoinCarrier = GameObject.Find("LeftCoins");
        Vector3 LeftCoinCarrierPosition = LeftCoinCarrier.transform.position;

        GameObject RightCoinCarrier = GameObject.Find("RightCoins");
        Vector3 RightCoinCarrierPosition = RightCoinCarrier.transform.position;

        // Create the link list.

        List<GameObject> CoinCarrierS = new List<GameObject>(GameObject.FindGameObjectsWithTag("CoinsNotDropped"));
        var count = CoinCarrierS.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = CoinCarrierS[i];
            CoinCarrierS[i] = CoinCarrierS[r];
            CoinCarrierS[r] = tmp;
        }

        //foreach (GameObject Coin in CoinCarrierS)
        //{
        //    Debug.Log(Coin.name);
        //}
        int leftCoins = CoinCarrierS.Count / 2;
        int rightCoins = CoinCarrierS.Count - leftCoins;
        for (int i = 0; i < leftCoins; i++)
        {
            int CoinRotationZ = i is 0 ? 0 : 360 / leftCoins * (i + 1);
            Quaternion CoinRotation = Quaternion.AngleAxis(CoinRotationZ, Vector3.forward);

            GameObject LeftCoinCarrierClone = new GameObject("LeftCoin" + i);
            LeftCoinCarrierClone.transform.SetParent(LeftCoinCarrier.transform);
            LeftCoinCarrierClone.transform.position = LeftCoinCarrierPosition;// * .1f;

            Instantiate(CoinCarrierS[i], LeftCoinCarrier.transform.position + new Vector3(-1, 0, 0), CoinRotation, LeftCoinCarrierClone.transform);
            LeftCoinCarrierClone.transform.rotation = Quaternion.AngleAxis(-CoinRotationZ, Vector3.forward);

            Debug.Log((i + 1) + "nth leftCoin is " + CoinCarrierS[i]);
        }
        

        for (int i = leftCoins; i < CoinCarrierS.Count; i++)
        {
            int CoinRotationZ = i is 0 ? 0 : (360 / rightCoins * (i + 1));
            Quaternion CoinRotation = Quaternion.AngleAxis(CoinRotationZ, Vector3.forward);

            GameObject RightCoinCarrierClone = new GameObject("RightCoin" + i);
            RightCoinCarrierClone.transform.SetParent(RightCoinCarrier.transform);
            RightCoinCarrierClone.transform.localPosition = RightCoinCarrierPosition * .1f;

            Instantiate(CoinCarrierS[i], RightCoinCarrier.transform.position + new Vector3(-1, 0, 0), CoinRotation, RightCoinCarrierClone.transform);
            RightCoinCarrierClone.transform.rotation = Quaternion.AngleAxis(-CoinRotationZ, Vector3.forward);

            Debug.Log((i + 1) + "nth rightCoin is " + CoinCarrierS[i]);
        }


        foreach (GameObject Coin in CoinCarrierS)
        {
            Destroy(Coin);
        }
    }
}