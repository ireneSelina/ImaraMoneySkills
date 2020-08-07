using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{

    public static int scoreValue = 0;

    TextMeshProUGUI scoreText;

    MenuScript menuScript;

    // Start is called before the first frame update
    void Awake()
    {

        scoreText = GetComponent <TextMeshProUGUI> ();

    }

    // Update is called once per frame
    void Update ()
    {
        scoreText.text = "Score: " + scoreValue;

        if (scoreValue >= 3000 && SceneManager.GetActiveScene().Equals("Match-Coins-To-Jars"))
        {
            menuScript.LoadWhichCoinsAreMore();
        } 
        else
            if (scoreValue >= 3000 && SceneManager.GetActiveScene().Equals("Which-Coins-Are-More"))
        {
            menuScript.LoadMainMenu();
        }
        

    }

}
