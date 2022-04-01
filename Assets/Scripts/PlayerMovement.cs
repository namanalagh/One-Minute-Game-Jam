using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    private float ySpeed;
    void Start()
    {
        mineDamage = mine.GetComponent<MineDamage>();
        damage = mineDamage.damage;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 speed = new Vector2(moveSpeed, ySpeed);
        rb.velocity = speed;
        //animator.SetFloat("Speed",moveSpeed);

        if (Input.GetKey(KeyCode.W))
        {
            ySpeed = vertSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
            ySpeed = -vertSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Mine")
        {
            playerHealth -= damage;
            Debug.Log(playerHealth);
            animator.SetTrigger("TakeDamage");
        }
    }
    
}
