using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Trapped : MonoBehaviour
{
    NPC_AI npc;

    CharacterAnimation anim;

    private void Start()
    {
        TryGetComponent(out anim);
        TryGetComponent(out npc);
    }

    public void Trapped(string trapAction, float stunTime)
    {
        anim.SetTrigger(trapAction);

        npc.StunDuration = stunTime;
        npc.SwitchState(NPC_State.STUNNED);
    }
}
