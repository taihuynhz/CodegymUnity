using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameButton : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    public void GasButtonClicked()
    {

    }

    public void PauseButtonClicked()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
