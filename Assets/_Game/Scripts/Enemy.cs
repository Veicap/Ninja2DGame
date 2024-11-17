using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private AttackArea attackArea;


    private Rigidbody2D rb;
    private Character target;

    private IState currentState;
    private bool isRunning = false;
    private bool isRight = true;
    private bool isAttack = false;
    public bool IsRight => isRight;
    public bool IsRunning => isRunning;

    public Character Target => target;
    public bool IsDead => isDeath;
    public bool IsAttack => isAttack;   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = new PartrolState();
        maxHp = 200;
        hp = maxHp;
    }
    
    private void Update()
    {
        
        if(IsDead)
        {
            currentState.OnExit(this);
            isAttack = false;   
            isRunning = false;
            StopRun();
            Invoke(nameof(OnDespawn), 1f);
        }
        else
        {
            currentState.OnExecute(this);
        }

        if(target != null)
        {
            if(target.transform.position.x > transform.position.x)
            {
                isRight = true;
            }
            else
            {
                isRight = false;
            }
        }
    }
    public override void OnInit() {
        base.OnInit();
        ChangeState(new IdleState());
    }
    public override void OnDespawn() {
        Destroy(gameObject);
    }

    public void SetTarget(Character target)
    {
        this.target = target;
    }
    public override void Run() {
        rb.velocity = (IsRight ? Vector2.right : Vector2.left) * moveSpeed;
        ChangeDirection();
        isRunning = true;
    }
    public void ChangeDirection()
    {
        transform.rotation = isRight ? Quaternion.Euler(Vector2.zero) : Quaternion.Euler(Vector2.up * 180);
    }
    public void StopRun()
    {
        rb.velocity = Vector2.zero;
        isRunning = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("WallEnemy") && target == null)
        {
            isRight = !isRight;
        }
    }
    public bool InRangeAttack()
    {
        if(target != null && Vector2.Distance(target.transform.position, transform.position) < attackRange)
        {
            return true;
        }

        return false;
    }
    public void Attack()
    {
        isAttack = true;
        attackArea.HitTarget();
    }
    public void DeAttack()
    {
        isAttack = false;
    }
    public void ChangeState(IState newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }
    
}
