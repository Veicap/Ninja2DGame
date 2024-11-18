using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{

    private Character character;
    private CrateObject crate;

    public Character Character => character;    

    public void HitTarget()
    {
        if(character != null)
        {
            character.OnHit(20);
        }
        if (crate != null)
        {
            crate.SpawnGift();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            character = collision.GetComponent<Character>();
        }
        if(collision.CompareTag("Crate"))
        {
            crate = collision.GetComponent<CrateObject>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            character = null;
        }
    }
}
