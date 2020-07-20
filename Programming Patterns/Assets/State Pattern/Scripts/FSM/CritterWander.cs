using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CritterWander : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Vector3 nextLocation;
    [SerializeField]
    private float wanderDistance = 15f;
    [SerializeField]
    private GameObject deathPartciles;
    private static ObjectPool_Advanced objectPool;

    // Start is called before the first frame update
    void Start()
    {
        nextLocation = this.transform.position;
        navAgent = this.GetComponent<NavMeshAgent>();
        if(!NPCSearch.critters.Contains(this.gameObject))
            NPCSearch.critters.Add(this.gameObject);
        if (!NPCSearch_No_FSM.critters.Contains(this.gameObject))
            NPCSearch_No_FSM.critters.Add(this.gameObject);
        if (!NPCSearch_ClassBased.critters.Contains(this.gameObject))
            NPCSearch_ClassBased.critters.Add(this.gameObject);

        if (objectPool == null)
            objectPool = FindObjectOfType<ObjectPool_Advanced>();
    }

    // Update is called once per frame
    void Update()
    {
        DoWander();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
            DoDestroy();
    }

    public void DoDestroy()
    {
        if (deathPartciles != null)
            Instantiate(deathPartciles, this.transform.position, this.transform.rotation);

        //Also not a great way to do audio... but it works for the demo
        GameObject.FindGameObjectWithTag("NPC").GetComponent<AudioSource>().Play();

        NPCSearch.critters.Remove(this.gameObject);
        NPCSearch_No_FSM.critters.Remove(this.gameObject);
        NPCSearch_ClassBased.critters.Remove(this.gameObject);
        this.gameObject.SetActive(false);

    }
}
