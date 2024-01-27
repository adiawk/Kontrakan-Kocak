using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_BaseOnClick : MonoBehaviour
{
    Button btn;
    [SerializeField] MMFeedbacks MMFeedbacks;

    private void Awake()
    {
        TryGetComponent(out btn);
    }

    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(OnClickButton);
    }

    void OnClickButton()
    {
        MMFeedbacks.PlayFeedbacks();
    }
}
