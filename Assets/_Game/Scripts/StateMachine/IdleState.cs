using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private float time;
    private float randomTime;

    [System.Obsolete]
    public void OnEnter(Enemy enemy)
    {
        time = 0;
        randomTime = Random.RandomRange(1.5f, 2f);
        enemy.StopRun();
    }

    public void OnExecute(Enemy enemy)
    {
        time += Time.deltaTime;
        if(time >= randomTime)
        {
            enemy.ChangeState(new PartrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
