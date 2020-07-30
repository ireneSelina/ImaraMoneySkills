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
    private GameObject matchingJar;
    private string coinMatch;

    Dictionary<string, string> jarsForCoins = new Dictionary<string, string>()
    {
          {"Jar-forty-shilling","Forty-shillings"},
          {"Jar-fifty-cent","Fifty-Cents"},
          {"Jar-one-shilling","One-shilling"},
          {"Jar-ten-shilling","Ten-shillings"},
          {"Jar-twenty-shilling","Twenty-shillings"},
          {"Jar-five-shilling","Five-shillings"}
    };

    void Start()
    {
        matchingJar = this.gameObject;
        bgMusic = this.gameObject.AddComponent<AudioSource>();
        bgMusic.playOnAwake = false;
        sfxState = this.gameObject.AddComponent<AudioSource>();
        sfxState.playOnAwake = false;
        sfxVoice = this.gameObject.AddComponent<AudioSource>();
        sfxVoice.playOnAwake = false;
    }


    public void SoundSystem(bool picked, bool matched, string coinGameObject)
    {
        if (coinGameObject != null) 
        {
			if (!picked &&  (matched || !matched) )
		    {
		    	 coinMatch = jarsForCoins[matchingJar.name];
                 
            
			     if (!picked && !matched)
			     {
				    StartCoroutine(SoundBoom(false));
			     }
			 
			     else if (!picked && matched && coinMatch.Equals(coinGameObject))
			     {
				    StartCoroutine(SoundBoom(true));
			     }			 
		    }

		    else if (picked && !matched) 
		    {
			    sfxState.clip = coinPickUp;
			    if (!sfxState.isPlaying)
			    {
				    sfxState.Play();
			    }

			    Debug.Log("coinPickUp sound played: " + coinPickUp.name);
            }
            
            //  picked = false;
            //  matched = false;
            //  coinGameObject = null;
        }
        

        
    }

    IEnumerator SoundBoom(bool SuccessType)
    {
        if (SuccessType == true)
        {
            sfxState.clip = (AudioClip)coinDrop;

            if (!sfxState.isPlaying)
            {
                sfxState.Play();
            }
            Debug.Log("coinDrop sound played: ");
            Debug.Log("jar gameobject  is " + matchingJar.name);

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
