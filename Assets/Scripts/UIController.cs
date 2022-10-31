using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject scoresPanel;

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
}
