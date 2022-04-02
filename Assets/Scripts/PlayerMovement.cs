using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    [Range(0f,3f)]
    public float vertSpeed = 0f;

    public GameObject mine;
    private MineDamage mineDamage;
    private float damage;
    public float playerHealth = 1;
    public float increment= 0.5f;
    public int score = 0;
    
    private float ySpeed;
    private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");

    void Start()
    {
        InvokeRepeating(nameof(IncreaseSpeed),10f,10f);
        mineDamage = mine.GetComponent<MineDamage>();
        damage = mineDamage.damage;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 speed = new Vector2(moveSpeed, ySpeed);
        rb.velocity = speed;

        if (Input.GetKey(KeyCode.W))
        {
            ySpeed = vertSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
            ySpeed = -vertSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mine"))
        {
            playerHealth -= damage;
            animator.SetTrigger(TakeDamage);
            other.GetComponent<Animator>().SetBool("Explode",true);
            score -= 200;
        }
        else if (other.CompareTag($"Fish"))
        {
            other.gameObject.SetActive(false);
            score += 100;
        }
        else if (other.CompareTag($"BigFish"))
        {
            playerHealth -= damage;
            animator.SetTrigger(TakeDamage);
            other.gameObject.SetActive(false);
            score -= 200;
        }
    }

    private void IncreaseSpeed()
    {
        rb.velocity += new Vector2(increment, 0);
        score += 1000;
    }
    
}
