using UnityEngine;
using UnityEngine.AI;

public class CollectState : INPCState
{
    public INPCState DoState(NPCSearch_ClassBased npc)
    {
        if (npc.navAgent == null)
            npc.navAgent = npc.GetComponent<NavMeshAgent>();

        DoCollect(npc);
        if (!npc.pickUpTarget.activeSelf)
            return npc.wanderState;
        else
            return npc.collectState;
    }

    private void DoCollect(NPCSearch_ClassBased npc)
    {
        if (npc.navAgent.destination != npc.pickUpTarget.transform.position)
            npc.navAgent.SetDestination(npc.pickUpTarget.transform.position);
    }
}
