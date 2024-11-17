using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : CharacterVisual
{
    [SerializeField] private Player player;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (player.IsDead())
        {
            DeadAnim();
        }
        else if (player.IsAttacking())
        {
            AttackAnim();
        }
        else if(player.IsThrowing())
        {
            ThrowAinm();
        }
        else if(player.IsFalling())
        {
            ChangeAnim("Fall");
        }
        else if (player.IsJumpIn() || player.IsJumpOut())
        {
            JumpAim();
        }
        else if (player.IsRunning())
        {
            RunAim();
        }
        else
        {
            IdleAim();
        }
        Flip();
    }
   
    
    private void JumpAim()
    {
        if (player.IsJumpIn()) ChangeAnim("Jump");
        else if (player.IsJumpOut() && player.IsFalling()) ChangeAnim("Fall");
    }
    private void AttackAnim()
    {
        ChangeAnim("Attack");
    }
    private void ThrowAinm()
    {
        ChangeAnim("Throw");
    }
    public void Flip()
    {
        if (player.IsFlip() && !player.IsAttacking())
        {
            transform.rotation = Quaternion.Euler(Vector2.up * 180);
        }
        else if (!player.IsFlip() && !player.IsAttacking())
        {
            transform.rotation = Quaternion.Euler(Vector2.zero);
        }
    }
}
