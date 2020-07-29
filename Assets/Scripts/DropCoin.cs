using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoin : MonoBehaviour
{
    public AudioSource bgMusic, sfxState, sfxVoice;
    public AudioClip[] correct;
    public AudioClip incorrect, coinDrop, coinPickUp;
    public GameObject selectedJar;
    private GameObject hiddenJar;
    public DropCoin DropCoinSS;
    //private DragCoin dragCoin;

    Dictionary<string, string> jarsForCoins = new Dictionary<string, string>()
    {
          {"jar-forty-shilling","Forty-shillings"},
          {"jar-fifty-cent","Fifty-Cents"},
          {"jar-one-shilling","One-shilling"},
          {"jar-ten-shilling","Ten-shillings"},
          {"jar-twenty-shilling","Twenty-shillings"},
          {"jar-five-shilling","Five-shillings"}
    };

    void Start()
    {
        hiddenJar = gameObject;
        //selectedJar = GameObject.Find("");
        //dragCoin = gameObject.AddComponent<DragCoin>();
        bgMusic = gameObject.AddComponent<AudioSource>();
        bgMusic.playOnAwake = false;
        sfxState = gameObject.AddComponent<AudioSource>();
        sfxState.playOnAwake = false;
        sfxVoice = gameObject.AddComponent<AudioSource>();
        sfxVoice.playOnAwake = false;

    }

    //static DropCoin()
    //{ //bool picked, bool matched, string coinGameObject) {
    //    SoundSystem(picked, matched,coinGameObject);
    //}

    public void SoundSystem(bool picked, bool matched, string coinGameObject)
    {
        
        if (!picked)
        {
            sfxState.clip = (AudioClip)coinDrop;
            if (!sfxState.isPlaying)
            {
                sfxState.Play(); 
            }
            Debug.Log("coinDrop sound played: ");
            Debug.Log("jar gameobject  is " +  hiddenJar.name);
            string coinMatch = jarsForCoins[gameObject.name];

            if (!matched)
            {
                StartCoroutine(SoundBoom(false));
            }
            else if (matched && coinMatch.Equals(coinGameObject))
            {
                StartCoroutine(SoundBoom(true));
            }
        }
        else if (picked) 
        {
            sfxState.clip = coinPickUp;
            if (!sfxState.isPlaying)
            {
                sfxState.Play();
            }
            Debug.Log("coinPickUp sound played: " + coinPickUp.name);
        }
    }

    IEnumerator SoundBoom(bool SuccessType)
    {
        if (SuccessType == true)
        {
            yield return new WaitForSeconds(1);

            sfxVoice.clip = correct[UnityEngine.Random.Range(0, correct.Length)];
            if (!sfxVoice.isPlaying)
            {
                sfxVoice.Play();
                Debug.Log("correct sound played: ");
            }
        }
        else
        {
            sfxVoice.clip = incorrect;
            if (!sfxVoice.isPlaying)
            {
                sfxVoice.Play();
                Debug.Log("incorrect sound played: " + incorrect.name);
            }
        }
        //yield return null;

    }
}
