using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private int speed;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform kunaiPoint;
    [SerializeField] private GameObject kunaiPrefab;
    [SerializeField] private Transform attackArea;
    [SerializeField] float timeToThrow;
    [SerializeField] float timeToAttack;
    [SerializeField] float timeToRevise;
    [SerializeField] float climbSpeed;
    [SerializeField] HeathBarCanvas heathBarCanvas;
    

    private Rigidbody2D rb;
    private float horizontal;
    private int totalCoin = 0;


    private bool isGround = true;
    private bool isRunning;
    private bool isJumpIn = false;
    private bool isJumOut = false;
    private bool isFlip = false;
    private bool isAttack = false;
    private bool isThrow = false;
    private bool canDoubleThrow = false;
    private bool canClimb = false;
    private bool climbing = false;  

    private float counterTimeAttack;
    private float counterTimeThrow;
    private float counterTimeRevise;
    private Vector3 savePoint;
   

    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isRunning = false;
        counterTimeAttack = timeToAttack;
        counterTimeRevise = timeToRevise;
        totalCoin = PlayerPrefs.GetInt("coin", 0);
        UIManager.Instance.SetCoinText(totalCoin.ToString());
    }

    private void Update()
    {
        //horizontal = Input.GetAxisRaw("Horizontal");
        isGround = IsGround();

        if (isGround)
        {
            isJumpIn = false;
            isJumOut = false;
        }
        else if (rb.velocity.y > 0.1f)
        {
            isJumpIn = true;
            isJumOut = false;
        }
        else if (rb.velocity.y < -0.1f)
        {
            isJumpIn = false;
            isJumOut = true;
        }
       
        UpdateChildTransformObject();
        if(isDeath)
        {
            rb.velocity = Vector2.zero;
            counterTimeRevise -= Time.deltaTime;
            if(counterTimeRevise <= 0)
            {
                counterTimeRevise = timeToRevise;
                OnInit();
            }
        }
        else 
        {
            Run();
            if (isAttack)
            {

                counterTimeAttack -= Time.deltaTime;
                if (counterTimeAttack < 0)
                {
                    isAttack = false;
                }
            }
            if (isThrow)
            {
                counterTimeThrow -= Time.deltaTime;
                if (counterTimeThrow < 0)
                {
                    isThrow = false;
                }
            }
        }
    }

    private void Start()
    {
        SavePoint();
        maxHp = 120;
        hp = maxHp;
    }
    public override void OnInit()
    {
        base.OnInit();  
        hp = maxHp;
        isAttack = false;
        isRunning = false;
        isThrow = false;
        isJumOut = false;
        isJumpIn = false;
        isDeath = false;
        transform.position = savePoint;
        heathBarCanvas.Show();
    }
    
    public bool IsRunning()
    {
        if(Mathf.Abs(rb.velocity.x) > 0.1f) isRunning = true;
        else isRunning = false;
        return isRunning;
    }
    public bool IsJumping()
    {
        if(!isGround) return true;
        return false;
    }
    public bool IsJumpIn() { return isJumpIn; }
    public bool IsJumpOut() {  return isJumOut; }
    public bool IsAttacking() { return isAttack; }
    public bool IsThrowing() { return isThrow; }
    public bool IsFalling() {return rb.velocity.y < -0.1f && !isGround;}
    public bool IsDead()
    {
        return isDeath;
    }
    public override bool IsFlip()
    {
        if (horizontal < 0) isFlip = true;
        else if (horizontal > 0) isFlip = false;
        return isFlip;
    }
    private bool IsGround()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.2f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, layerMask);
        return hit.collider != null;
    }

    public void Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            attackArea.GetComponent<AttackArea>().HitTarget();
            
            counterTimeAttack = timeToAttack;
        }
    }
    public void Throw()
    {
        if (!isThrow)
        {
            isThrow = true;
            Instantiate(kunaiPrefab, kunaiPoint.position, kunaiPoint.rotation);
            if (canDoubleThrow)
            {
                Debug.Log("Double throw");
                Vector3 newKunaiPosition  = kunaiPoint.position + new Vector3(1f,0f,0);
                Instantiate(kunaiPrefab, newKunaiPosition, kunaiPoint.rotation);
            }
            counterTimeThrow = timeToThrow;
        }
       
    }
   
    public override void Run()
    {
        if (Mathf.Abs(horizontal) > 0.1f && !IsAttacking() && !IsThrowing())
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
    }
    public void Jump()
    {
        if(isGround)
        {
            isJumpIn = true;
            rb.AddForce(jumpForce * Vector2.up);
        }
    }
    public void Climb()
    {
        if(CanClimb())
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            climbing = true;
            rb.gravityScale = 0f;
        }
    }
    public bool Climbing()
    {

        if (climbing) return climbing;
        else if(!canClimb) return false;
        return false;
    }
    public bool CanClimb()
    {
        return canClimb;
    }
    public void SavePoint()
    {
        savePoint = transform.position;
    }

    public void UpdateChildTransformObject()
    {
        if (isFlip)
        {
            kunaiPoint.localPosition = new Vector3(-Mathf.Abs(kunaiPoint.localPosition.x), kunaiPoint.localPosition.y, kunaiPoint.localPosition.z);
            kunaiPoint.rotation = Quaternion.Euler(Vector2.up * 180);
            attackArea.localPosition = new Vector3(-Mathf.Abs(attackArea.localPosition.x), attackArea.localPosition.y, attackArea.localPosition.z);
            attackArea.rotation = Quaternion.Euler(Vector2.up * 180);
        }
        else
        {
            kunaiPoint.localPosition = new Vector3(Mathf.Abs(kunaiPoint.localPosition.x), kunaiPoint.localPosition.y, kunaiPoint.localPosition.z);
            kunaiPoint.rotation = Quaternion.Euler(Vector2.zero);
            attackArea.localPosition = new Vector3(Mathf.Abs(attackArea.localPosition.x), attackArea.localPosition.y, attackArea.localPosition.z);
            attackArea.rotation = Quaternion.Euler(Vector2.zero);
        }
    }
    public void SetHorizontal(float horizontal)
    {
        this.horizontal = horizontal;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            totalCoin++;
            PlayerPrefs.SetInt("coin", totalCoin);
            UIManager.Instance.SetCoinText(totalCoin.ToString());
            Destroy(collision.gameObject);
            
        }
        if(collision.CompareTag("DeathZone"))
        {
            isDeath = true;
            
        }
        if(collision.CompareTag("KunaiGift"))
        {
            canDoubleThrow = true;
            Destroy(collision.gameObject);
           
        }
        if(collision.CompareTag("HiddenMap"))
        {
            collision.GetComponent<HiddenMap>().Hide();
            Debug.Log("Player interact hidden map");
        }
        if(collision.CompareTag("Rope"))
        {
            canClimb = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HiddenMap"))
        {
            collision.GetComponent<HiddenMap>().Show();
        }
        if (collision.CompareTag("Rope"))
        {
            canClimb = false;
            rb.gravityScale = 1;
        }
    }
    
}
