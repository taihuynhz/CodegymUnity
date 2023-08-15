using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnPauseButton : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    public void HomeButtonClicked()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void ResumeButtonClicked()
    {
        this.pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RetryButtonClicked()
    {
        SceneManager.LoadScene("Lesson8");
        Time.timeScale = 1f;
    }
}
