using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Character : MonoBehaviour
{

    [SerializeField] private CombatText combatTextPrefab;
    public virtual bool IsFlip() {  return true; }
    protected float hp;
    protected float maxHp;
    protected bool isDeath = false;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit() { hp = maxHp; }
    public virtual void OnDespawn() { }

    public bool IsDeath => isDeath;
    public void OnDeadth() { 
        isDeath = true; 
    }

    public virtual void Run() { }

    public void OnHit(int damage) { 
        if(hp > 0)
        {
            hp -= damage;
            Instantiate(combatTextPrefab, transform.position, Quaternion.identity).OnInit(damage);
            if (hp <= 0)
            {
                OnDeadth();
            }
        }
        else
        {

            OnDeadth();
        }
    }

    public float HP => hp;
    public float MaxHP => maxHp;   
}
