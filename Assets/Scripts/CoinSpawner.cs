using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // Create the link list.
    public List<GameObject> CoinCarrierTemp, CoinCarrierPerma;

    GameObject LeftCoinCarrier, RightCoinCarrier;

    bool CoinsAreDestroyed = false, RightCoinsAreMore;

    float RightCoinsValue, LeftCoinsValue;

    int LevelCycle = 1;

    Dictionary<string, float> CoinValuesPerma = new Dictionary<string, float>()
    {
          {"Forty_Shilling_Coin",    40f},
          {"Fifty_Cent_Coin",        .5f},
          {"One_Shilling_Coin",      1f},
          {"Ten_Shilling_Coin",      10f},
          {"Twenty_Shilling_Coin",   20f},
          {"Five_Shilling_Coin",     5f}
    };

    Dictionary<string, float> CoinValues = new Dictionary<string, float>()
    {
          {"Forty_Shilling_Coin(Clone)",    40f},
          {"Fifty_Cent_Coin(Clone)",        .5f},
          {"One_Shilling_Coin(Clone)",      1f},
          {"Ten_Shilling_Coin(Clone)",      10f},
          {"Twenty_Shilling_Coin(Clone)",   20f},
          {"Five_Shilling_Coin(Clone)",     5f}
    };

    private void Awake()
    {

        CoinCarrierTemp = new List<GameObject>();

        CoinCarrierPerma = new List<GameObject>();

        // CoinCarrierTemp.Add(GameObject.FindGameObjectsWithTag("CoinsNotDropped"));

    }

    // Start is called before the first frame update
    void Start()
    {
        //oneShilling missileCopy = Instantiate<>();

        //CoinCarrierPerma = new List<GameObject>();

        //CoinCarrierTemp = new List<GameObject>(GameObject.FindGameObjectsWithTag("CoinsNotDropped"));

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

        if (LevelCycle < 2)//CoinCarrierPerma.Count == 0 )
        {

            if (CoinCarrierTemp.Count == 0)
            {

                foreach (GameObject Coin in GameObject.FindGameObjectsWithTag("CoinsNotDropped"))
                {

                    CoinCarrierTemp.Add(Coin);

                }

            }

            //CoinCarrierTemp = CoinCarrierTemp.Count > 2 ? CoinCarrierTemp : new List<GameObject>(GameObject.FindGameObjectsWithTag("CoinsNotDropped"));

            Debug.Log("CoinCarrierTemp run " + LevelCycle);

            var count = CoinCarrierTemp.Count;

            var last = count - 1;

            for (var i = 0; i < last; ++i)
            {

                var r = Random.Range(i, count);

                var tmp = CoinCarrierTemp[i];

                CoinCarrierTemp[i] = CoinCarrierTemp[r];

                CoinCarrierTemp[r] = tmp;
            }

            //foreach (GameObject Coin in CoinCarrierS)
            //{
            //    Debug.Log(Coin.name);
            //}
            int leftCoins = CoinCarrierTemp.Count / 2;

            int rightCoins = CoinCarrierTemp.Count - leftCoins;

            for (int i = 0; i < leftCoins; i++)
            {
                int CoinRotationZ = i is 0 ? 0 : (360 / leftCoins * (i + 1));

                Quaternion CoinRotation = Quaternion.AngleAxis(CoinRotationZ, Vector3.forward);

                GameObject LeftCoinCarrierClone = new GameObject("LeftCoin" + i);

                GameObject CoinClone = new GameObject(CoinCarrierTemp[i].name);

                LeftCoinCarrierClone.transform.SetParent(LeftCoinCarrier.transform);

                LeftCoinCarrierClone.transform.position = LeftCoinCarrierPosition;// * .1f;

                CoinClone = (GameObject)Instantiate(CoinCarrierTemp[i], LeftCoinCarrier.transform.position + new Vector3(-1, 0, 0), CoinRotation, LeftCoinCarrierClone.transform);

                CoinClone.transform.gameObject.tag = "CoinCarrierPerma";

                //CoinCarrierPerma.Add(CoinClone);

                LeftCoinCarrierClone.transform.rotation = Quaternion.AngleAxis(-CoinRotationZ, Vector3.forward);

                try
                {

                    LeftCoinsValue += CoinValues[CoinClone.name];

                    Debug.Log("LeftCoinsValue += CoinValues[CoinClone.name]; key exists");

                }

                catch (KeyNotFoundException)
                {

                    LeftCoinsValue += CoinValues[CoinClone.name];

                    Debug.Log(" LeftCoinsValue += CoinValues[CoinClone.name]; key does not exist");

                    throw;
                }

                Debug.Log("LeftCoins Value is" + LeftCoinsValue);

                Debug.Log("CoinCarrierTemp's " + (i + 1) + "nth leftCoin is " + CoinClone.name + ", Position " + CoinClone.transform.localPosition
                    + ", Rotation " + CoinClone.transform.localRotation);

            }


            for (int i = leftCoins; i < CoinCarrierTemp.Count; i++)
            {

                int CoinRotationZ = i is 0 ? 0 : (360 / rightCoins * (i + 1));

                Quaternion CoinRotation = Quaternion.AngleAxis(CoinRotationZ, Vector3.forward);

                GameObject RightCoinCarrierClone = new GameObject("RightCoin" + i);

                GameObject CoinClone = new GameObject(CoinCarrierTemp[i].name);

                RightCoinCarrierClone.transform.SetParent(RightCoinCarrier.transform);

                RightCoinCarrierClone.transform.localPosition = RightCoinCarrierPosition * .1f;

                CoinClone = (GameObject)Instantiate(CoinCarrierTemp[i], RightCoinCarrier.transform.position + new Vector3(-1, 0, 0), CoinRotation, RightCoinCarrierClone.transform);

                CoinClone.transform.gameObject.tag = "CoinCarrierPerma";

                //CoinCarrierPerma.Add(CoinClone);

                RightCoinCarrierClone.transform.rotation = Quaternion.AngleAxis(-CoinRotationZ, Vector3.forward);

                RightCoinsValue += CoinValues[CoinClone.name];

                Debug.Log("RightCoins Value is " + RightCoinsValue);

                Debug.Log("CoinCarrierTemp's " + (i + 1) + "nth rightCoin is " + CoinClone.name + ", Position " + CoinClone.transform.localPosition
                    + ", Rotation " + CoinClone.transform.localRotation);

            }

            if (RightCoinsValue > LeftCoinsValue)
            {

                SetRightCoinsAreMore(true);

            }

            else

            {

                SetRightCoinsAreMore(false);

            }

            if (!CoinsAreDestroyed)
            {
                
                Debug.Log("CoinCarrierTemp population:" + CoinCarrierTemp.Count);

                CoinCarrierPerma = CoinCarrierTemp;

                //CoinCarrierPerma = new List<GameObject>(GameObject.FindGameObjectsWithTag("CoinCarrierPerma"));
                
                //CoinCarrierTemp.Clear();
                
                foreach (GameObject Coin in CoinCarrierPerma)
                {

                    Debug.Log(Coin + " is in CoinCarrierPerma before kill CoinCarrierTemp");

                    //Coin.SetActive(false);

                }

                foreach (GameObject Coin in CoinCarrierTemp)
                {

                    Debug.Log(Coin + " is in CoinCarrierTemp");

                    Coin.SetActive(false);

                }
                
                CoinsAreDestroyed = true;

            }

        }

        else 

        if(LevelCycle <= 3)
        {

            foreach (GameObject Coin in CoinCarrierPerma)
            {

                Debug.Log(Coin + " is in CoinCarrierPerma");

                //Coin.SetActive(false);

            }

            Debug.Log("CoinCarrierPerma run " + LevelCycle);

            var count = CoinCarrierPerma.Count;

            var last = count - 1;

            for (var i = 0; i < last; ++i)
            {

                var r = UnityEngine.Random.Range(i, count);

                var tmp = CoinCarrierTemp[i];

                CoinCarrierPerma[i] = CoinCarrierPerma[r];

                CoinCarrierPerma[r] = tmp;

            }

            //foreach (GameObject Coin in CoinCarrierS)
            //{

            //    Debug.Log(Coin.name);

            //}

            int leftCoins = CoinCarrierPerma.Count / 2;

            int rightCoins = CoinCarrierPerma.Count - leftCoins;

            for (int i = 0; i < leftCoins; i++)
            {

                int CoinRotationZ = i is 0 ? 0 : (360 / leftCoins * (i + 1));

                Quaternion CoinRotation = Quaternion.AngleAxis(CoinRotationZ, Vector3.forward);

                var LeftCoinCarrierClone = CoinCarrierPerma[i].transform.parent;

                LeftCoinCarrierClone.SetParent(LeftCoinCarrier.transform);

                //CoinCarrierPerma[i].transform.parent.SetParent(LeftCoinCarrier.transform);

                CoinCarrierPerma[i].transform.rotation = CoinRotation;

                LeftCoinCarrierClone.transform.rotation = Quaternion.AngleAxis(-CoinRotationZ, Vector3.forward);

                Debug.Log("CoinCarrierPerma's " + (i + 1) + "nth Left Coin Name  is " + CoinCarrierPerma[i].name + ", Position " + CoinCarrierPerma[i].transform.localPosition
                    + ", Rotation " + CoinCarrierPerma[i].transform.localRotation);

                try
                {

                    LeftCoinsValue += CoinValuesPerma[CoinCarrierPerma[i].name];

                    Debug.Log("LeftCoinsValue += CoinValuesPerma[CoinCarrierPerma[i].name]; key exists");

                }

                catch (KeyNotFoundException)
                {

                    LeftCoinsValue += CoinValues[CoinCarrierPerma[i].name];

                    Debug.Log("LeftCoinsValue += CoinValuesPerma[CoinCarrierPerma[i].name]; key does not exist");

                    throw;
                }

                Debug.Log("LeftCoins Value is " + LeftCoinsValue);

                Debug.Log((i + 1) + "nth leftCoin is " + CoinCarrierPerma[i].name);

            }


            for (int i = leftCoins; i < CoinCarrierPerma.Count; i++)
            {

                int CoinRotationZ = i is 0 ? 0 : (360 / rightCoins * (i + 1));

                Quaternion CoinRotation = Quaternion.AngleAxis(CoinRotationZ, Vector3.forward);

                var RightCoinCarrierClone = CoinCarrierPerma[i].transform.parent;

                RightCoinCarrierClone.SetParent(RightCoinCarrier.transform);

                CoinCarrierPerma[i].transform.rotation = CoinRotation;

                RightCoinCarrierClone.transform.rotation = Quaternion.AngleAxis(-CoinRotationZ, Vector3.forward);

                Debug.Log("CoinCarrierPerma's " + (i + 1) + "nth Right Coin Name is " + CoinCarrierPerma[i].name + ", Position " + CoinCarrierPerma[i].transform.localPosition
                    + ", Rotation " + CoinCarrierPerma[i].transform.localRotation);

                try
                {

                    RightCoinsValue += CoinValuesPerma[CoinCarrierPerma[i].name];

                    Debug.Log("RightCoinsValue += CoinValuesPerma[CoinCarrierPerma[i].name]; key exists");

                }

                catch (KeyNotFoundException)
                {

                    RightCoinsValue += CoinValues[CoinCarrierPerma[i].name];

                    Debug.Log("RightCoinsValue += CoinValuesPerma[CoinCarrierPerma[i].name]; key does not exist");

                    throw;

                }

                Debug.Log("RightCoins Value is " + RightCoinsValue);

                Debug.Log((i + 1) + "nth RightCoin is " + CoinCarrierPerma[i].name);

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