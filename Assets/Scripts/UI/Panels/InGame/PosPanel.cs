using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PosPanel : MonoBehaviour
{
    [SerializeField] protected TMP_Text posText;

    protected void Update()
    {
        SetSpeedText(PlayerController.Instance.currentPoint);
    }

    protected void SetSpeedText(float value)
    {
        posText.text = value.ToString();
    }
}
