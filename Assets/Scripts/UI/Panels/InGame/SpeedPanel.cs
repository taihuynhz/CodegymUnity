using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedPanel : MonoBehaviour
{
    [SerializeField] protected TMP_Text speedText;

    protected void Update()
    {
        SetSpeedText(PlayerController.Instance.playerSpeed);
    }

    protected void SetSpeedText(float value)
    {
        speedText.text = value.ToString("F2");
    }
}
