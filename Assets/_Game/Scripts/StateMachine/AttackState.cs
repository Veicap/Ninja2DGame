using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private float time;
    public void OnEnter(Enemy enemy)
    {
        if(enemy.Target != null)
        {
            enemy.StopRun();
            enemy.Attack();
            time = 0.6f;
        }
        
    }

    public void OnExecute(Enemy enemy)
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            enemy.DeAttack();
            enemy.ChangeState(new PartrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }  
}
