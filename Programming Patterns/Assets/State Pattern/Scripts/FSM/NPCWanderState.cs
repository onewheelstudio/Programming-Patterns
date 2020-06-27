using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class WanderState : INPCState
{
    public INPCState DoState(NPCSearch_ClassBased npc)
    {
        if (npc.navAgent == null)
        {
            npc.nextLocation = npc.transform.position;
            npc.navAgent = npc.GetComponent<NavMeshAgent>();
        }

        DoWander(npc);

        if (CanSeeCritter(npc))
            return npc.attackState;
        else if (CanSeePickUp(npc))
            return npc.collectState;
        else
            return npc.wanderState;
    }

    private void DoWander(NPCSearch_ClassBased npc)
    {
        //if close choose next location
        if (npc.navAgent.remainingDistance < 1f)
        {
            Vector3 random = Random.insideUnitSphere * npc.wanderDistance;
            random.y = 0f;
            npc.nextLocation = npc.navAgent.transform.position + random;

            if (NavMesh.SamplePosition(npc.nextLocation, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                npc.nextLocation = hit.position;
                npc.navAgent.SetDestination(npc.nextLocation);
            }
        }
    }

    private bool CanSeePickUp(NPCSearch_ClassBased npc)
    {
        foreach (GameObject pickup in NPCSearch_ClassBased.pickUps)
        {
            float distance = (pickup.transform.position - npc.transform.position).magnitude;
            if (distance < npc.pickUpDistance)
            {
                Vector3 direction = pickup.transform.position - (npc.transform.position + Vector3.up);
                Ray ray = new Ray(npc.transform.position + Vector3.up, direction);
                Debug.DrawRay(npc.transform.position + Vector3.up, direction, Color.blue);

                if (Physics.Raycast(ray, out RaycastHit hit, npc.pickUpDistance))
                {
                    Debug.Log("I hit" + hit.collider.name);

                    if (hit.collider.gameObject == pickup)
                    {
                        npc.pickUpTarget = pickup; //a little yucky but keeps the base class clean
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private bool CanSeeCritter(NPCSearch_ClassBased npc)
    {
        foreach (GameObject critter in NPCSearch_ClassBased.critters)
        {
            float distance = (critter.transform.position - npc.transform.position).magnitude;
            if (distance < npc.pickUpDistance)
            {
                Vector3 direction = critter.transform.position - (npc.transform.position + Vector3.up);
                Ray ray = new Ray(npc.transform.position + Vector3.up, direction);
                Debug.DrawRay(npc.transform.position + Vector3.up, direction, Color.red);

                if (Physics.Raycast(ray, out RaycastHit hit, npc.pickUpDistance))
                {
                    Debug.Log("I hit" + hit.collider.name);

                    if (hit.collider.gameObject == critter)
                    {
                        npc.critterTarget = critter;
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
