using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Transform aPoint;
    [SerializeField] private float speed;
    [SerializeField] private Transform bPoint;

    private void Awake()
    {
        target = aPoint;
        transform.position = bPoint.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        if(Vector2.Distance(transform.position, aPoint.position) < 0.1f)
        {
            target = bPoint;
        }
        else if(Vector2.Distance(transform.position, bPoint.position) < 0.1f)
        {
            target = aPoint;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
