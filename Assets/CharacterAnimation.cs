using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void SetWalk(bool isWalking)
    {
        anim.SetBool("IsMove", isWalking);
    }

    public void SetTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void TriggerKepleset()
    {
        anim.SetTrigger("Kepleset");
    }

    public void TriggerMarahSedang()
    {
        anim.SetTrigger("Marah1");
    }

    public void TriggerMarahBerat()
    {
        anim.SetTrigger("Marah2");
    }
}
