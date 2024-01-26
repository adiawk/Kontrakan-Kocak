using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{
    [SerializeField] Image imgProgressAmount;

    public void SetProgress(float progress)
    {
        imgProgressAmount.fillAmount = progress;
    }
}
