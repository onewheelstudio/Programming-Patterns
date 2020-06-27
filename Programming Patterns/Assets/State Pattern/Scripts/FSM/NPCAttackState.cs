using UnityEngine;
using UnityEngine.AI;

public class AttackState : INPCState
{
    public INPCState DoState(NPCSearch_ClassBased npc)
    {
        if (npc.navAgent == null)
            npc.navAgent = npc.GetComponent<NavMeshAgent>();

        MoveToCritter(npc);

        if (!npc.critterTarget.activeSelf)
            return npc.wanderState;
        else
            return npc.attackState;
    }

    private void MoveToCritter(NPCSearch_ClassBased npc)
    {
        if (npc.navAgent.destination != npc.critterTarget.transform.position)
            npc.navAgent.SetDestination(npc.critterTarget.transform.position);
    }
}
