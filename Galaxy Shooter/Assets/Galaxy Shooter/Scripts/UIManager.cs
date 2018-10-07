using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image playerLivesDisplay;
    public Text playerScoreDisplay;
    public Image titleMenuDisplay;
    public Sprite[] lifeImagesStore;
    public int score;

    public void UpdateTitle(bool gameStarted)
    {
        if (gameStarted)
        {
            playerLivesDisplay.gameObject.SetActive(true);
            playerScoreDisplay.gameObject.SetActive(true);
            titleMenuDisplay.gameObject.SetActive(false);
        }
        else
        {
            Reset();
            playerLivesDisplay.gameObject.SetActive(false);
            playerScoreDisplay.gameObject.SetActive(false);
            titleMenuDisplay.gameObject.SetActive(true);
        }
    }

    public void UpdateLives(int currentLives)
    {
        playerLivesDisplay.sprite = lifeImagesStore[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        playerScoreDisplay.text = $"Score: {score}";
    }

    private void Reset()
    {
        score = 0;
        playerScoreDisplay.text = $"Score: {score}";

        UpdateLives(0);
    }

}
