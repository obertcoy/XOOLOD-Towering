using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine : StateMachine
{

    public NPC NPC { get; }
    
    public NPCStateMachine(NPC npc)
    {
        NPC = npc;
    }

}
