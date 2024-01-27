using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerHUD : MonoBehaviour
{
    public static UI_PlayerHUD instance;


    CanvasGroup canvasGroup;
    private void Awake()
    {
        instance = this;
        TryGetComponent(out canvasGroup);
    }

    public void HideUI()
    {
        canvasGroup.alpha = 0f;
    }

    public void ShowUI()
    {
        canvasGroup.alpha = 1f;
    }
}
