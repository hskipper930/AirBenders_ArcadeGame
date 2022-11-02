using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject scoresPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private GameObject newHighScorePanel;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameManager gm;

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void OnHelpButtonClick()
    {
        helpPanel.SetActive(true);
    }

    public void OnScoresButtonClick()
    {
        scoresPanel.SetActive(true);
    }

    public void OnCloseButtonClick(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateHPText(int health)
    {
        hpText.text = "Health: " + health.ToString();
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("Frontend");
    }

    public void ShowDeathPanel(bool isHighScore)
    {
        gameOverPanel.SetActive(true);
        if(isHighScore)
        {
            newHighScorePanel.SetActive(true);
        }
    }

    public void OnDoneButtonClick()
    {
        gm.AddNewScore(nameInputField.text);
    }
}
