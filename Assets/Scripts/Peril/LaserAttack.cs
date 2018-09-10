using UnityEngine;
using System.Collections;

public class LaserAttack : MonoBehaviour {

    public float timeBetweenAttacks = 1.5f;
    public int attackDamage = 50;

    GameObject player;
    GameObject laser;
    PlayerHealth playerHealth;
    OnOffRenderer onOffRenderer;

    bool playerInRange;
    float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        laser = GameObject.FindGameObjectWithTag("Laser");
        playerHealth = player.GetComponent<PlayerHealth>();
        onOffRenderer = laser.GetComponent<OnOffRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter");
        if (onOffRenderer.active && other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        //Debug.Log("OnTriggerExit");
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }
		

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
