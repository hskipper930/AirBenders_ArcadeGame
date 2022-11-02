using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject scoresPanel;
    [SerializeField] private TMP_Text scoreText;

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
}
