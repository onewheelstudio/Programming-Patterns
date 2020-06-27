using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCSearch_ClassBased : MonoBehaviour
{
    [SerializeField]
    private string currentStateName;
    private INPCState currentState;

    public WanderState wanderState = new WanderState();
    public CollectState collectState = new CollectState();
    public AttackState attackState = new AttackState();

    public NavMeshAgent navAgent;

    public Vector3 nextLocation;
    public GameObject pickUpTarget;
    public GameObject critterTarget;

    public float wanderDistance = 10f;
    public float pickUpDistance = 25f;

    public static List<GameObject> pickUps = new List<GameObject>();
    public static List<GameObject> critters = new List<GameObject>();

    private void OnEnable()
    {
        currentState = wanderState;
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.DoState(this);
        currentStateName = currentState.ToString();
    }
}