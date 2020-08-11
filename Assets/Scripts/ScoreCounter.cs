using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{

    public static int scoreValue = 0;

    bool CoinsToJars, MoreCoins, MostCoins;

    TextMeshProUGUI scoreText;

    MenuScript menuScript;


    // Start is called before the first frame update
    private void Start()
    {

        scoreText = GetComponent<TextMeshProUGUI>();

        if (SceneManager.GetActiveScene().Equals("Which-Coins-Are-More"))
        {
            MoreCoins = true;
            CoinsToJars = false;
        }
        if (SceneManager.GetActiveScene().Equals("Match-Coins-To-Jars"))
        {
            CoinsToJars = false;
            MoreCoins = true;
        }


    }

    void Awake()
    {

        scoreText = GetComponent<TextMeshProUGUI>();


    }


    public void Match_Coins_To_Jars_Scores()
    {
        scoreText.text = "Score: " + scoreValue;

        Debug.Log(scoreText.text + " ________________");

        if (scoreValue >= 3000)
        {

            menuScript.LoadWhichCoinsAreMore();

        }

    }

    public void Which_Coins_Are_More_Scores()
    {

        scoreText.text = "Score: " + scoreValue;

        Debug.Log(scoreText.text + " ________________");

        if (scoreValue >= 500)
        {

            menuScript.LoadMainMenu();

        }

    }

    public void SliderScore(int score, bool status)
    {
        if (status)
        {
            scoreValue += score;
        }

        if (CoinsToJars)
        {
            Match_Coins_To_Jars_Scores();
        }


        if (MoreCoins)
        {
            Which_Coins_Are_More_Scores();
        }
    }

}
