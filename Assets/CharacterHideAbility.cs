using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHideAbility : MonoBehaviour
{
    public bool isVisible = true;

    bool isUsingAbility;
    [SerializeField] float timeAbilityCooldown;

    public UIProgressBar progressBar;
    public UnityEvent OnCooldownFinish;
    public UnityEvent OnAbilityUsed;
    public UnityEvent OnAbilityRunsOut;

    //private void Update()
    //{
    //    if(!isUsingAbility)
    //    {
    //        if (isCooldown)
    //        {
    //            if (currentAbilityCooldown > 0)
    //            {
    //                currentAbilityCooldown -= Time.deltaTime;
    //            }
    //            else
    //            {
    //                currentAbilityCooldown = 0;

    //                CooldownFinished();
    //            }
    //        }
    //    }
    //}

    void CooldownFinished()
    {
        OnCooldownFinish?.Invoke();
    }

    void HideAbility()
    {
        isVisible = false;
        isUsingAbility = true;
    }

    void Show()
    {
        isVisible = true;
        isUsingAbility = false;

        StartCoroutine(CooldownAbility());
    }

    IEnumerator CooldownAbility()
    {
        float maxTime = timeAbilityCooldown;
        float time = timeAbilityCooldown;

        while(time > 0)
        {
            time -= Time.deltaTime;
            progressBar.SetProgress(Mathf.Clamp01(1f - time / maxTime));
            
            yield return new WaitForEndOfFrame();
        }


        CooldownFinished();
    }

    public void ToggleAbility()
    {
        isVisible = !isVisible;

        if(isVisible)
        {
            Show();
        }
        else
        {
            HideAbility();
        }
    }
}
