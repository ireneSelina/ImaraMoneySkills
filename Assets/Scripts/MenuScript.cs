using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private Scene SceneMainMenu; 

    //private AssetBundle myLoadedAssetBundle;

    //private string[] scenePaths;

    public static bool resizableWindow;

    // Start is called before the first frame update
    void Start()
    {
        resizableWindow = true;
        SceneMainMenu = SceneManager.GetSceneByBuildIndex(0);
        //myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/scenes");
        //scenePaths = myLoadedAssetBundle.GetAllScenePaths();
    }

    // Update is called once per frame

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 100, 30), "Change Scene"))
    //    {
    //        Debug.Log("Scene2 loading: " + scenePaths[0]);
    //        SceneManager.LoadScene(scenePaths[0], LoadSceneMode.Single);
    //    }
    //}

    public void LoadMatchCoinsWithJars() 
    {

         ChooseSceneIndex(1);
        //ChooseScene("Match-Coins-To-Jars");

    }


    public void LoadWhichCoinsAreMore()
    {

         ChooseSceneIndex(2);
        //ChooseScene("Which-Coins-Are-More");

    }

    public void LoadMainMenu()
    {

         ChooseSceneIndex(0);
        //ChooseScene("Which-Coins-Are-More");
    }


    public void PauseGame()
    {

        Time.timeScale = 0f;

        AudioListener.pause = true;

    }

    public void ResumeGame()
    {

        Time.timeScale = 1;

        AudioListener.pause = false;

    }




    public void LoadExitGame()
    {

#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        PauseGame();
        UnityEditor.EditorApplication.isPlaying = false;
#else
            PauseGame();
            Application.Quit();
#endif

        PauseGame();
        Application.Quit();

    }




    public void ChooseScene(string SceneName) 
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName(SceneName).name.Length > 0 ? SceneName : SceneMainMenu.name);
        //SceneManager.LoadSceneAsync(SceneManager.GetSceneByName(SceneName).name.Length > 0 ? SceneName : SceneMainMenu.name);

    }

    public void ChooseSceneIndex(int SceneIndex) 
    {
        SceneManager.LoadScene(SceneIndex);
        //SceneManager.LoadSceneAsync(SceneManager.GetSceneByBuildIndex(SceneIndex).name);

    }
}
