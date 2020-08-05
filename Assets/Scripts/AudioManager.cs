using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource GameplayAudio, SfxCoinState, SfxVoice, SfxAmbience, SfxMenu; //, PauseAudio //MenuAudio,

    public AudioClip GameplayMusic, PauseMusic, Incorrect, Correct1, Correct2, CoinDrop, CoinPickUp, AmbienceAudio, MenuMusic; //MenuMusic, 

    private Scene level;

    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {

        level = SceneManager.GetActiveScene();

        sceneName = level.name;

        SfxAmbience = this.gameObject.AddComponent<AudioSource>();

        SfxAmbience.playOnAwake = false;

        SfxAmbience.loop = true;

        GameplayAudio = this.gameObject.AddComponent<AudioSource>();

        GameplayAudio.playOnAwake = false;

        GameplayAudio.loop = true;

        SfxMenu = this.gameObject.AddComponent<AudioSource>();

        SfxMenu.playOnAwake = false;

        SfxMenu.loop = true;

        SfxCoinState = this.gameObject.AddComponent<AudioSource>();

        SfxCoinState.playOnAwake = false;

        SfxVoice = this.gameObject.AddComponent<AudioSource>();

        SfxVoice.playOnAwake = false;

        //PauseAudio = this.gameObject.AddComponent<AudioSource>();

        //PauseAudio.playOnAwake = false;

    }

    public void GamePlayAudioManager() 
    {
        switch (sceneName)
        {

            case "MainMenu": // "Match-Coins-With-Jars": 

                SfxMenu.clip = MenuMusic;

                if (!SfxMenu.isPlaying)
                {

                    SfxMenu.volume = 0.8f;

                    SfxMenu.Play();

                }

                break;

            case "Match-Coins-With-Jars11": //"Match-Coins-With-Jars":

                break;

            case "Which-Coins-Are-More11": //"Which-Coins-Are-More":

                break;

            case "Guess-The-Value11": // "Guess-The-Value":

                break;

            default:

                GameplayAudio.clip = GameplayMusic;

                if (!GameplayAudio.isPlaying)
                {

                    GameplayAudio.volume = 0.9f;

                    GameplayAudio.Play();

                }

                SfxAmbience.clip =  AmbienceAudio;

                if (!SfxAmbience.isPlaying)
                {

                    SfxAmbience.volume = 0.02f;

                    SfxAmbience.Play();

                }

                break;

        }


    } 

    // Update is called once per frame
    void Update()
    {

        GamePlayAudioManager();

    }

    public void PlaySound(string SoundName)
    {
        switch (SoundName)
        {

            case "PickedAndNotMatched": // "Match-Coins-With-Jars": 

                SfxCoinState.clip = CoinPickUp;

                if (!SfxCoinState.isPlaying)
                {

                    SfxCoinState.Play();

                }

                //Debug.Log("coinPickUp sound played: " + coinPickUp.name);

                break;

            case "AlreadyPickedAndNotMatched": //"Which-Coins-Are-More":

                StartCoroutine(SoundBoom(false));

                break;

            case "AlreadyPickedAndMatched": // "Guess-The-Value":

                StartCoroutine(SoundBoom(true));

                break;

            default:

                
                break;
        }
    }

    public IEnumerator SoundBoom(bool SuccessType)
    {

        if (SuccessType == true)
        {

            SfxCoinState.clip = CoinDrop;

            if (!SfxCoinState.isPlaying)
            {

                SfxCoinState.Play();

            }

            Debug.Log("CoinDrop sound played: "); // + CoinDrop.ToString());

            yield return new WaitForSeconds(1);

            //SfxVoice.clip = Correct[Random.Range(0, Correct.Length)];

            SfxVoice.clip = Random.Range(0, 2) is 1  ? SfxVoice.clip = Correct1 : SfxVoice.clip = Correct2;

            if (!SfxVoice.isPlaying)
            {

                SfxVoice.Play();

                Debug.Log("Correct sound played: "); // + SfxVoice.clip.ToString());

            }
        }

        else
        {

            SfxVoice.clip = Incorrect;

            if (!SfxVoice.isPlaying)
            {

                SfxVoice.Play();

                Debug.Log("Incorrect sound played: ");// + Incorrect.ToString());

            }
        }
        //yield return null;
    }

}
