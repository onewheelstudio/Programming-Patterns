using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCSearch_No_FSM_ExtraState : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Vector3 nextLocation;
    [SerializeField]
    private float wanderDistance = 10f;

    public static List<GameObject> pickUps = new List<GameObject>();
    private GameObject pickUpTarget;
    [SerializeField]
    private float pickUpDistance = 25f;
    private GameObject pickUpObject;

    public static List<GameObject> critters = new List<GameObject>();
    private GameObject critterTarget;

    // Start is called before the first frame update
    void Start()
    {
        nextLocation = this.transform.position;
        navAgent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeeCritter())
        {
            if (pickUpObject != null) //yuck
                DropObject();
            MoveToCritter();
        }
        else if (pickUpObject != null) //more if's??? This is getting ugly
        {
            ScoreObject();
        }
        else if (CanSeePickUp())
        {
            DoCollect();
        }
        else
        {
            DoWander();
        }
    }

    private void DoWander()
    {
        //if close choose next location
        if (navAgent.remainingDistance < 1f)
        {
            Vector3 random = Random.insideUnitSphere * wanderDistance;
            random.y = 0f;
            nextLocation = this.transform.position + random;

            if (NavMesh.SamplePosition(nextLocation, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                nextLocation = hit.position;
                navAgent.SetDestination(nextLocation);
            }
        }
    }

    private bool CanSeePickUp()
    {
        foreach (GameObject pickup in pickUps)
        {
            float distance = (pickup.transform.position - this.transform.position).magnitude;
            if (distance < pickUpDistance)
            {
                Vector3 direction = pickup.transform.position - (this.transform.position + Vector3.up);
                Ray ray = new Ray(this.transform.position + Vector3.up, direction);
                Debug.DrawRay(this.transform.position + Vector3.up, direction, Color.blue);

                if (Physics.Raycast(ray, out RaycastHit hit, pickUpDistance))
                {
                    if (hit.collider.gameObject == pickup)
                    {
                        pickUpTarget = pickup;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private void DoCollect()
    {
        if (navAgent.destination != pickUpTarget.transform.position)
            navAgent.SetDestination(pickUpTarget.transform.position);
    }

    private void MoveToCritter()
    {
        if (navAgent.destination != critterTarget.transform.position)
            navAgent.SetDestination(critterTarget.transform.position);
    }

    private bool CanSeeCritter()
    {
        foreach (GameObject critter in critters)
        {
            float distance = (critter.transform.position - this.transform.position).magnitude;
            if (distance < pickUpDistance)
            {
                Vector3 direction = critter.transform.position - (this.transform.position + Vector3.up);
                Ray ray = new Ray(this.transform.position + Vector3.up, direction);
                Debug.DrawRay(this.transform.position + Vector3.up, direction, Color.red);

                if (Physics.Raycast(ray, out RaycastHit hit, pickUpDistance))
                {
                    Debug.Log("I hit" + hit.collider.name);

                    if (hit.collider.gameObject == critter)
                    {
                        critterTarget = critter;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private void DropObject()
    {
        //nothing to see here
    }

    private void ScoreObject()
    {
        //yep. still nothing.
    }
}
