using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoin : MonoBehaviour
{

    private GameObject matchingJar;

    private string coinMatch;

    private AudioManager audioManager;


    Dictionary<string, string> jarsForCoins = new Dictionary<string, string>()
    {
          {"Jar_Forty_Shilling","Forty_Shilling"},
          {"Jar_Fifty_Cent","Fifty_Cent"},
          {"Jar_One_Shilling","One_Shilling"},
          {"Jar_Ten_Shilling","Ten_Shilling"},
          {"Jar_Twenty_Shilling","Twenty_Shilling"},
          {"Jar_Five_Shilling","Five_Shilling"}
    };

    void Start()
    {

        matchingJar = this.gameObject;

        audioManager = gameObject.AddComponent<AudioManager>();

    }

    
    public void SoundSystem(bool picked, bool matched, string coinGameObject)
    {

        if (coinGameObject != null)
        {

            if (!picked && (matched || !matched))
            {

                coinMatch = jarsForCoins[matchingJar.name];

                if (!picked && !matched)
                {

                    audioManager.PlaySound("AlreadyPickedAndNotMatched");

                }

                else if (!picked && matched && coinMatch.Equals(coinGameObject))
                {

                    audioManager.PlaySound("AlreadyPickedAndMatched");

                }
            }

            else if (picked && !matched)
            {

                audioManager.PlaySound("PickedAndNotMatched");

            }

            //  picked = false;
            //  matched = false;
            //  coinGameObject = null;
        }



    }



}
