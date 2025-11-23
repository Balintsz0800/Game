using System;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public float attackTimer;
    public float attackCooldown;
    public float attackRange;

    public EnemyHealth enemyHealth; 
    [HideInInspector] public float damage = 10;
    public float currentDamage => damage * Timer.Instance.damageMultiplier;
    public float reloadDropChace = 20f;
    private NavMeshAgent agent;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;   
        enemyHealth.UpdateHealthText(currentHealth);

        if (currentHealth <= 0)
        {
            TryGiveReload();
            Destroy(gameObject);
        }
    }

    private void TryGiveReload()
    {
        if (UnityEngine.Random.Range(0f, 100f) <= reloadDropChace)
        {
            Weapon weapon = FindObjectOfType<Weapon>();
            if(weapon != null)
            {
                weapon.AddReload(1);
            }
        }
    }
    void Start()
    {
        maxHealth = maxHealth * Timer.Instance.healthMultiplier;
        currentHealth = maxHealth;
        enemyHealth.UpdateHealthText(currentHealth);

        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange;
    }
    private void Attack()
    {
        Debug.Log("attack");
        PlayerStatics.Instance.currentHealth -= currentDamage;

        attackTimer = attackCooldown;
    }
    private void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
            
        if (!PlayerMovement.Instance) return;
            
            agent.destination = PlayerMovement.Instance.transform.position;
            
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance) 
        { 
            if(attackTimer <= 0) 
                Attack(); 
        }
    }
}