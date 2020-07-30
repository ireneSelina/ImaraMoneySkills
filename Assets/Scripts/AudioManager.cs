using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource GameplayAudio;//, PauseAudio //MenuAudio, 

    public AudioClip GameplayMusic, PauseMusic; //MenuMusic, 

    private Scene level;

    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {

        level = SceneManager.GetActiveScene();

        sceneName = level.name;

        GameplayAudio = this.gameObject.AddComponent<AudioSource>();

        GameplayAudio.playOnAwake = false;

        GameplayAudio.loop = true;

        //PauseAudio = this.gameObject.AddComponent<AudioSource>();

        //PauseAudio.playOnAwake = false;

    }

    public void AudioManagement() 
    {

        GameplayAudio.clip = GameplayMusic;

        if (!GameplayAudio.isPlaying)
        {
            GameplayAudio.volume = .33f;
            GameplayAudio.Play();

        }
    } 

    // Update is called once per frame
    void Update()
    {
        if (sceneName is "Level1")
        {

            AudioManagement();

        }
    }
}
