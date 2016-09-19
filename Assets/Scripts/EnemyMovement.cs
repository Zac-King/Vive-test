using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    Animator anim;
    bool canAttack = true;
    [SerializeField] float attackDelay = 1f;
    [SerializeField] Collider weaponCollider;
    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        nav.stoppingDistance = 1.5f;
    }

    void Update()
    {
        // If the enemy and the player have health left...
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            // ... set the destination of the nav mesh agent to the player.
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist > 7)
            {
                nav.SetDestination(transform.position);
                anim.SetBool("Walk", false);
            }
            else if(dist <= 2.5f)
            {
                anim.SetBool("Walk", false);
                Attack();
            }

            else
            {
                anim.SetBool("Walk", true);
            }

            nav.SetDestination(player.position);
        }
        // Otherwise...
        else
        {
            anim.SetBool("Walk", false);
            // ... disable the nav mesh agent.
            nav.enabled = false;
        }
    }

    void Attack()
    {
        if (!canAttack)
            return;

        canAttack = false;
        anim.SetTrigger("Attack");
        StartCoroutine(AttackDelay());

    }

    IEnumerator AttackDelay()
    {
        float timer = 0;
        while(timer < attackDelay)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        canAttack = true;
    }

    public void WeaponActive()
    {
        weaponCollider.enabled = true;
    }

    public void WeaponDeactive()
    {
        weaponCollider.enabled = false;
    }

    //void OnTriggerEnter(Collider c)
    //{
    //    if(c.gameObject.tag == "Player")
    //    {
    //        nav.SetDestination(transform.position);
    //        //anim.SetTrigger("Attack");
    //        anim.SetBool("Walk", false);
    //    }
    //}

    //void OnTriggerExit(Collider c)
    //{
    //    if (c.gameObject.tag == "Player")
    //    {
    //        nav.SetDestination(player.position);
    //        anim.SetBool("Walk", true);
    //    }
    //}
}
