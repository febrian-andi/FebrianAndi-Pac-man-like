using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private bool isMoving;
    private Vector3 destination;

    public void EnterState(Enemy enemy)
    {
        isMoving = false;
        enemy.animator.SetTrigger("PatrolState");
        // Debug.Log("Entering Patrol State");
    }

    public void UpdateState(Enemy enemy)
    {
        if (
            Vector3.Distance(enemy.transform.position, enemy.player.transform.position)
            <= enemy.chaseDistance
        )
        {
            enemy.SwitchState(enemy.chaseState);
        }

        if (!isMoving)
        {
            isMoving = true;
            int index = UnityEngine.Random.Range(0, enemy.wayPoints.Count);
            destination = enemy.wayPoints[index].position;
            enemy.navMeshAgent.destination = destination;
        }
        else
        {
            if (Vector3.Distance(destination, enemy.transform.position) <= 1.0)
            {
                isMoving = false;
            }
        }
        // Debug.Log("Updating Patrol State");
    }

    public void ExitState(Enemy enemy)
    {
        // Debug.Log("Exiting Patrol State");
    }
}
