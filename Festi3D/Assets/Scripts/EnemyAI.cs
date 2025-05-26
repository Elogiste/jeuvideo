using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    private Transform player;
    private PlayerStats playerStats;

    [Header("Stats")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackDelay;
    [SerializeField] private float damageDealt;
    [SerializeField] private float rotationSpeed;

    [Header("Wandering parameters")]
    [SerializeField] private float wanderingWaitTimeMin;
    [SerializeField] private float wanderingWaitTimeMax;
    [SerializeField] private float wanderingDistanceMin;
    [SerializeField] private float wanderingDistanceMax;

    private bool hasDestination;
    private bool isAttacking;
    private bool isDead;

    private void Awake()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = playerTransform;
        playerStats = playerTransform.GetComponent<PlayerStats>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead || !agent.isOnNavMesh) return;

        if (Vector3.Distance(player.position, transform.position) < detectionRadius && !playerStats.isDead)
        {
            agent.speed = chaseSpeed;

            Quaternion rot = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);

            if (!isAttacking)
            {
                if (Vector3.Distance(player.position, transform.position) < attackRadius)
                {
                    StartCoroutine(AttackPlayer());
                }
                else
                {
                    agent.SetDestination(player.position);
                }
            }
        }
        else
        {
            agent.speed = walkSpeed;

            if (agent.remainingDistance < 0.75f && !hasDestination)
            {
                StartCoroutine(GetNewDestination());
            }
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void TakeDammage(float damages)
    {
        if (isDead) return;

        currentHealth -= damages;

        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetTrigger("Die");
            agent.enabled = false;

            // ✅ Informe le GameManager de la mort de cet ennemi
            Gestion.instance.RegisterBearDeath();

            enabled = false;
        }
        else
        {
            animator.SetTrigger("GetHit");
        }
    }

    IEnumerator GetNewDestination()
    {
        hasDestination = true;
        yield return new WaitForSeconds(Random.Range(wanderingWaitTimeMin, wanderingWaitTimeMax));

        Vector3 nextDestination = transform.position + Random.Range(wanderingDistanceMin, wanderingDistanceMax) *
                                  new Vector3(Random.Range(-1f, 1), 0f, Random.Range(-1f, 1f)).normalized;

        if (NavMesh.SamplePosition(nextDestination, out NavMeshHit hit, wanderingDistanceMax, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }

        hasDestination = false;
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        agent.isStopped = true;

        if (audioSource != null) audioSource.Play();
        animator.SetTrigger("Attack");

        playerStats.TakeDamage(damageDealt);

        yield return new WaitForSeconds(attackDelay);

        if (agent.enabled) agent.isStopped = false;

        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
