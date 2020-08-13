using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // Create the link list.
    List<GameObject> CoinCarrier;

    GameObject LeftCoinCarrier, RightCoinCarrier;

    bool RightCoinsAreMore;

    float RightCoinsValue, LeftCoinsValue;


    int LevelCycle = 1;

    Dictionary<string, float> CoinValues = new Dictionary<string, float>()
    {
          {"Forty_Shilling_Coin",    40f},
          {"Fifty_Cent_Coin",        .5f},
          {"One_Shilling_Coin",      1f},
          {"Ten_Shilling_Coin",      10f},
          {"Twenty_Shilling_Coin",   20f},
          {"Five_Shilling_Coin",     5f}
    };

    private void Awake()
    {

        CoinCarrier = new List<GameObject>();

        // CoinCarrier.Add(GameObject.FindGameObjectsWithTag("CoinsNotDropped"));

    }

    // Start is called before the first frame update
    void Start()
    {

        //oneShilling missileCopy = Instantiate<>();

        //CoinCarrier = new List<GameObject>();

        //CoinCarrier = new List<GameObject>(GameObject.FindGameObjectsWithTag("CoinsNotDropped"));

        SpawnSystem();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetRightCoinsAreMore(bool value)
    {
         RightCoinsAreMore = value;
    }


    public bool GetRightCoinsAreMore()
    {

        return RightCoinsAreMore;

    }


    //public Dictionary<string, Vector3> GetCoinPositions() 
    //{
    //    return CoinPositions;
    //}


    public bool SpawnSystem()
    {
        LeftCoinsValue = RightCoinsValue = 0;

        //foreach (Transform ChildCoin in LeftCoinCarrier.transform)
        //{

        //    Destroy(ChildCoin);

        //}

        //foreach (Transform ChildCoin in RightCoinCarrier.transform)
        //{

        //    Destroy(ChildCoin);

        //}

            LeftCoinCarrier = GameObject.Find("LeftCoins");

            RightCoinCarrier = GameObject.Find("RightCoins");

            Vector3 LeftCoinCarrierPosition = LeftCoinCarrier.transform.position;

            Vector3 RightCoinCarrierPosition = RightCoinCarrier.transform.position;

        if (LevelCycle < 2)//CoinCarrier.Count == 0 )
        {

            if (CoinCarrier.Count == 0)
            {

                foreach (GameObject Coin in GameObject.FindGameObjectsWithTag("CoinsNotDropped"))
                {

                    CoinCarrier.Add(Coin);

                }

            }

            //CoinCarrier = CoinCarrier.Count > 2 ? CoinCarrier : new List<GameObject>(GameObject.FindGameObjectsWithTag("CoinsNotDropped"));

            Debug.Log("CoinCarrier run " + LevelCycle);

            var count = CoinCarrier.Count;

            Debug.Log("CoinCarrier.Count is " + CoinCarrier.Count);

            var last = count - 1;

            for (var i = 0; i < last; ++i)
            {

                var r = Random.Range(i, count);

                var tmp = CoinCarrier[i];

                CoinCarrier[i] = CoinCarrier[r];

                CoinCarrier[r] = tmp;
            }

            //foreach (GameObject Coin in CoinCarrierS)
            //{
            //    Debug.Log(Coin.name);
            //}
            int leftCoins = CoinCarrier.Count / 2;

            int rightCoins = CoinCarrier.Count - leftCoins;

            for (int i = 0; i < leftCoins; i++)
            {

                int CoinRotationZ  = ( 360 / leftCoins ) *  i ;

                Quaternion CoinRotation = Quaternion.AngleAxis(CoinRotationZ, Vector3.forward);

                GameObject LeftCoinCarrierClone = new GameObject("LeftCoin" + i);


                LeftCoinCarrierClone.transform.SetParent(LeftCoinCarrier.transform);

                LeftCoinCarrierClone.transform.localPosition = Vector3.left;
                                
                LeftCoinCarrierClone.transform.rotation = Quaternion.AngleAxis(-CoinRotationZ, Vector3.forward);





                CoinCarrier[i].transform.SetParent(LeftCoinCarrierClone.transform);

                CoinCarrier[i].transform.localPosition = Vector3.left; //coinPosition;

                CoinCarrier[i].transform.rotation = CoinRotation;

                var coinName = CoinCarrier[i].name;



                    LeftCoinsValue += CoinValues[coinName];

                    Debug.Log("LeftCoinsValue += CoinValues[coinName]; key exists");

               

                Debug.Log("LeftCoins Value is" + LeftCoinsValue);

                Debug.Log("CoinCarrier's " + (i + 1) + "nth leftCoin is " + coinName + ", Position " + CoinCarrier[i].transform.localPosition
                    + ", Rotation " + CoinRotation);

            }


            for (int i = leftCoins; i < CoinCarrier.Count; i++)
            {

                int CoinRotationZ = (360 / rightCoins) * i;

                Quaternion CoinRotation = Quaternion.AngleAxis(CoinRotationZ, Vector3.forward);

                GameObject RightCoinCarrierClone = new GameObject("RightCoin" + i);



                RightCoinCarrierClone.transform.SetParent(RightCoinCarrier.transform);

                RightCoinCarrierClone.transform.localPosition = Vector3.left;

                RightCoinCarrierClone.transform.rotation = Quaternion.AngleAxis(-CoinRotationZ, Vector3.forward);


                    
                CoinCarrier[i].transform.SetParent(RightCoinCarrierClone.transform);

                CoinCarrier[i].transform.localPosition = Vector3.left;

                CoinCarrier[i].transform.rotation = CoinRotation;

                var coinName = CoinCarrier[i].name;


                    RightCoinsValue += CoinValues[coinName];

                    Debug.Log("RightCoinsValue += CoinValuesPerma[coinName]; key exists");


                Debug.Log("RightCoins Value is " + RightCoinsValue);

                Debug.Log("CoinCarrier's " + (i + 1) + "nth rightCoin is " + coinName + ", Position " + CoinCarrier[i].transform.localPosition
                    + ", Rotation " + CoinRotation);

            }

            if (RightCoinsValue > LeftCoinsValue)
            {

                SetRightCoinsAreMore(true);

            }

            else

            {

                SetRightCoinsAreMore(false);

            }

            //if (!CoinsAreDestroyed)
            //{
                
            //    Debug.Log("CoinCarrier population:" + CoinCarrier.Count);

            //    foreach (GameObject Coin in CoinCarrier)
            //    {

            //        Debug.Log(Coin + " is in CoinCarrier");

            //        //Coin.SetActive(false);

            //    }
                
            //    CoinsAreDestroyed = true;

            //}

        }

        else 

        if(LevelCycle <= 3)
        {

            //foreach (GameObject Coin in CoinCarrier)
            //{

            //    Debug.Log(Coin + " is in CoinCarrier");

            //    //Coin.SetActive(false);

            //}

            //Debug.Log("CoinCarrier run " + LevelCycle);

            var count = CoinCarrier.Count;

            var last = count - 1;

            for (var i = 0; i < last; ++i)
            {

                var r = UnityEngine.Random.Range(i, count);

                var tmp = CoinCarrier[i];

                CoinCarrier[i] = CoinCarrier[r];

                CoinCarrier[r] = tmp;

            }

            //foreach (GameObject Coin in CoinCarrierS)
            //{

            //    Debug.Log(Coin.name);

            //}

            int leftCoins = CoinCarrier.Count / 2;

            int rightCoins = CoinCarrier.Count - leftCoins;

            for (int i = 0; i < leftCoins; i++)
            {

                int CoinRotationZ = (360 / rightCoins) * i;

                Quaternion CoinRotation = Quaternion.AngleAxis(CoinRotationZ, Vector3.forward);


                var LeftCoinCarrierClone = CoinCarrier[i].transform.parent;

                LeftCoinCarrierClone.SetParent(LeftCoinCarrier.transform);

                LeftCoinCarrierClone.transform.localPosition = Vector3.left;

                LeftCoinCarrierClone.transform.localRotation = Quaternion.AngleAxis(-CoinRotationZ, Vector3.forward);


                var coinName = CoinCarrier[i].name;

                CoinCarrier[i].transform.localPosition = Vector3.left;

                CoinCarrier[i].transform.localRotation = CoinRotation;


                Debug.Log("CoinCarrier's " + (i + 1) + "nth Left Coin Name  is " + coinName + ", Position " + CoinCarrier[i].transform.localPosition
                    + ", Rotation " + CoinRotation);

                


                    LeftCoinsValue += CoinValues[coinName];

                    Debug.Log("LeftCoinsValue += CoinValuesPerma[coinName]; key exists");


                Debug.Log("LeftCoins Value is " + LeftCoinsValue);

                Debug.Log((i + 1) + "nth leftCoin is " + coinName);

            }


            for (int i = leftCoins; i < CoinCarrier.Count; i++)
            {

                int CoinRotationZ = (360 / rightCoins) * i;

                Quaternion CoinRotation = Quaternion.AngleAxis(CoinRotationZ, Vector3.forward);


                var RightCoinCarrierClone = CoinCarrier[i].transform.parent;

                RightCoinCarrierClone.SetParent(RightCoinCarrier.transform);

                RightCoinCarrierClone.transform.localPosition = Vector3.left;

                RightCoinCarrierClone.transform.localRotation = Quaternion.AngleAxis(-CoinRotationZ, Vector3.forward);


                var coinName = CoinCarrier[i].name;

                CoinCarrier[i].transform.localPosition = Vector3.left;

                CoinCarrier[i].transform.localRotation = CoinRotation;



                Debug.Log("CoinCarrier's " + (i + 1) + "nth Right Coin Name  is " + coinName + ", Position " + CoinCarrier[i].transform.localPosition
                    + ", Rotation " + CoinRotation);


                    RightCoinsValue += CoinValues[coinName];

                    Debug.Log("RightCoinsValue += CoinValuesPerma[coinName]; key exists");

               

                Debug.Log("RightCoins Value is " + RightCoinsValue);

                Debug.Log((i + 1) + "nth leftCoin is " + coinName);


            }

            if (RightCoinsValue > LeftCoinsValue)
            {

                SetRightCoinsAreMore(true);

            }

            else

            {

                SetRightCoinsAreMore(false);

            }

        }


        LevelCycle++;

        return RightCoinsAreMore;

    }

}