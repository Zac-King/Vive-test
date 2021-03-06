﻿using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    public AudioClip deathClip;                 // The sound to play when the enemy dies.


    Animator anim;                              // Reference to the animator.
    AudioSource enemyAudio;                     // Reference to the audio source.
    ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    bool isDead;                                // Whether the enemy is dead.
    bool isSinking;                             // Whether the enemy has started sinking through the floor.

    bool canBeHit = true;
    public float damageDelay = .5f;
    public bool canAttack = false;

    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update()
    {
        // If the enemy should be sinking...
        if (isSinking)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        // If the enemy is dead...
        if (isDead || !canBeHit)
            // ... no need to take damage so exit the function.
            return;

        StartCoroutine(HitSpacer());

        anim.SetTrigger("Hit");

        // Play the hurt sound effect.
        enemyAudio.Play();

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }

    void Death()
    {
        // The enemy is dead.
        isDead = true;

        // Tell the animator that the enemy is dead.
        anim.SetBool("Dead", true);

        // Find and disable the Nav Mesh Agent.
        GetComponent<NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        //GetComponent<Rigidbody>().isKinematic = true;

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnEnemy>().currnetNumOfUnits--;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().kills++;
    }

    public void StartSinking()
    {
        // The enemy should no sink.
        isSinking = true;

        // Increase the score by the enemy's score value.
        //ScoreManager.score += scoreValue;

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);
    }

    IEnumerator HitSpacer()
    {
        canBeHit = false;
        float timer = 0;
        while (timer <= damageDelay)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        canBeHit = true;
    }

}
