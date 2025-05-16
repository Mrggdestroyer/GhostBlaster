using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Ghost : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public CapsuleCollider capsuleCollider;
    public float speed = 1;
    public float health = 5;
    public float damageCanTake = 1;

    public float damageCanDo = 5f;
    public float timeBetweenAttacks = 3f;

    private float distanceToPlayer;
    private float timer;
    public GameManager GameManager;

    private float agentSpeed;

    public GhostKillCounter GhostKillCounter;


    public void Start()
    {
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (!agent.enabled) return;

        Vector3 targetPosition = Camera.main.transform.position;

        agent.SetDestination(targetPosition);
        transform.LookAt(targetPosition);
        agent.speed = speed;

        distanceToPlayer = Vector3.Distance(targetPosition, agent.transform.position);


      //if (distanceToPlayer <= (agent.stoppingDistance + 0.1f) && timeBetweenAttacks <= timer) trying a diff way to check (when ghost stops moving)
        if (agent.velocity.magnitude < 0.15f && timeBetweenAttacks <= timer)
        {
            Attack();
            timer = 0;
        }

        if (health <= 0)
        {
            capsuleCollider.enabled = false;
            Kill();
        }
    }

    public void Attack()
    {
        animator.SetTrigger("TrAttack");
    }

    public void TakeDamage()
    {
        animator.SetTrigger("TrHit");
        health -= damageCanTake;
    }

    public void Kill()
    {
        agent.enabled = false;
        animator.SetTrigger("TrDie");
    }

    public void DoDamage()
    {
        GameManager.ChangeHealth(damageCanDo);
        return;
    }

    public void Destroy()
    {
        GhostKillCounter.ghostKilled += 1;
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agent.stoppingDistance);
    }

}
