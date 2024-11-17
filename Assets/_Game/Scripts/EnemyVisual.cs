using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : CharacterVisual
{
    [SerializeField] private Enemy m_Enemy;
    private void Update()
    {
        if (m_Enemy.IsRunning)
        {
            RunAim();
        }
        else if(m_Enemy.IsAttack)
        {
            AttackAnim();
        }
        else if(m_Enemy.IsDead)
        {
            DeadAnim();
        }
        else
        {
            IdleAim();
        }
    }
    private void AttackAnim()
    {
        ChangeAnim("Attack");
    }
}
