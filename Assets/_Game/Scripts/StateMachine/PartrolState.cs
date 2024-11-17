using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartrolState : IState
{
    private float time;
    private float randomTime;

    [System.Obsolete]
    public void OnEnter(Enemy enemy)
    {
        time = 0;
        randomTime = Random.RandomRange(5f, 7f);
    }

    public void OnExecute(Enemy enemy)
    {
        if (enemy.Target != null )
        {
            enemy.ChangeDirection();
            if ( enemy.InRangeAttack()) { enemy.ChangeState(new AttackState()); }
            else { enemy.Run(); }    
        }
        else
        {
            time += Time.deltaTime;
            if (time < randomTime)
            {
                if (enemy != null) {
                   
                    enemy.Run();
                  
                }
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
        }
        
    }

    public void OnExit(Enemy enemy)
    {
       
    }
}
