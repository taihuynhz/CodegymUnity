using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LapsPanel : MonoBehaviour
{
    [SerializeField] protected TMP_Text lapsText;

    protected void Update()
    {
        SetSpeedText(PlayerController.Instance.laps);
    }

    protected void SetSpeedText(float value)
    {
        lapsText.text = value.ToString() + "/12";
    }
}
