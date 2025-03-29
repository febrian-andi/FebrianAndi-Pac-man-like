using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public List<Transform> wayPoints = new List<Transform>();

    [SerializeField]
    public float chaseDistance;

    [SerializeField]
    public Player player;

    private BaseState currentState;

    [HideInInspector]
    public PatrolState patrolState = new PatrolState();

    [HideInInspector]
    public ChaseState chaseState = new ChaseState();

    [HideInInspector]
    public RetreatState retreatState = new RetreatState();

    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    [HideInInspector]
    public Animator animator;

    public void SwitchState(BaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentState = patrolState;
        currentState.EnterState(this);
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Start()
    {
        player.OnPowerUpStart += StartRetreating;
        player.OnPowerUpStop += StopRetreating;
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
        animator.SetFloat("Velocity", navMeshAgent.velocity.magnitude);
    }

    private void StartRetreating()
    {
        SwitchState(retreatState);
    }

    private void StopRetreating()
    {
        SwitchState(patrolState);
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Dead();
        }
    }
}
