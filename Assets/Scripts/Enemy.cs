using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100;

    public float attackTimer;
    public float attackCooldown;
    public float attackRange;

    public EnemyHealth enemyHealth;
    public float damage = 5;
    private NavMeshAgent agent;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;   
        enemyHealth.UpdateHealthText(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        enemyHealth.UpdateHealthText(currentHealth);

        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange;
    }
    
    private void Attack()
    {
        Debug.Log("attack");
        PlayerStatics.Instance.currentHealth -= damage;

        attackTimer = attackCooldown;
    }
    
    private void Update()
    {
        if(attackTimer > 0)
            attackTimer -= Time.deltaTime;
        
        if (!PlayerMovement.Instance) return;
            
        agent.destination = PlayerMovement.Instance.transform.position;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if(attackTimer <= 0)
                Attack();
        }
    }
}