using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{

    public static int scoreValue = 0;

    TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Awake()
    {

        scoreText = GetComponent <TextMeshProUGUI> ();

    }

    // Update is called once per frame
    void Update ()
    {

        scoreText.text = "Score: " + scoreValue;

    }

}
