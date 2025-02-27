using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        // Debug.Log("Entering Chase State");
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.player != null)
        {
            enemy.navMeshAgent.destination = enemy.player.transform.position;
            
            if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) <= enemy.chaseDistance)
            {
                enemy.SwitchState(enemy.patrolState);
            }
        }
        // Debug.Log("Updating Chase State");
    }

    public void ExitState(Enemy enemy)
    {
        // Debug.Log("Exiting Chase State");
    }
}
