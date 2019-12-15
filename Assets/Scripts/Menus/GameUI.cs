using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public VarInt Score;
    public Text ScoreText;

    public GameObject GameOverText;

    private void Start()
    {
        Score.value = 0;
        ScoreText.text = "Score: " + Score.value;

        GameOverText.SetActive(false);
    }

    public void IncreaseScore()
    {
        Score.value++;
        ScoreText.text = "Score: " + Score.value;
    }

    public void EnableGameOverUI()
    {
        GameOverText.SetActive(true);
    }

    public void ResetScore()
    {
        Score.value = 0;
    }
}
