using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_JebakanKena : MonoBehaviour
{
    public TextMeshProUGUI textProgress;

    private void Start()
    {
        TrapUpdated(0, RoomManager.instance.traps.Length);
    }

    public void TrapUpdated(int currentTrapDone, int trapAvailable)
    {
        textProgress.text = $"{currentTrapDone}/{trapAvailable}";
    }
}
