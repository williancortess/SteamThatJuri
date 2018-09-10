using UnityEngine;
using System.Collections;

public class PendalumAttack : MonoBehaviour {
    public float timeBetweenAttacks = 1.5f;
    public int attackDamage = 50;

    GameObject player;

    bool playerInRange;
    float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        timer = 0f;

		if (player.GetComponent<PlayerHealth>().currentHealth > 0)
        {
			player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
}
