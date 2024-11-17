using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{

    private Character character;

    public Character Character => character;    
    public void HitTarget()
    {
        if(character != null)
        {
            character.OnHit(20);
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
            collision.GetComponent<CrateObject>().SpawnGift();
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
