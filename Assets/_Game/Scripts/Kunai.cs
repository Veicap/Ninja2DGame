using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Kunai : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float kunaiSpeed;
    [SerializeField] GameObject hpEffect; 
    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * kunaiSpeed;
        Invoke(nameof(OnDespawn), 3f);
    }
    private void OnDespawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Character>().OnHit(50);
            Instantiate(hpEffect, transform.position, transform.rotation);
            OnDespawn();
        }
    }
}
