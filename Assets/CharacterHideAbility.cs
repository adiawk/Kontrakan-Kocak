using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHideAbility : MonoBehaviour
{
    PlayerManager PlayerManager;
    public bool isVisible = true;

    [SerializeField] float timeAbilityCooldown;

    bool isCooldown;

    public UIProgressBar progressBar;
    public UnityEvent OnCooldownFinish;
    public UnityEvent OnAbilityUsed;
    public UnityEvent OnAbilityPopOut;

    private void Start()
    {
        TryGetComponent(out PlayerManager);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleAbility();
        }
    }

    void CooldownFinished()
    {
        OnCooldownFinish?.Invoke();
        isCooldown = false;
    }

    void HideAbility()
    {
        isVisible = false;

        OnAbilityUsed?.Invoke();

        PlayerManager.TogglePlayerMove(false);
    }

    void Show()
    {
        isVisible = true;

        OnAbilityPopOut?.Invoke();
        PlayerManager.TogglePlayerMove(true);

        StartCoroutine(CooldownAbility());
    }

    IEnumerator CooldownAbility()
    {
        isCooldown = true;

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
        if (isCooldown || PlayerManager.isChannelling)
            return;

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
