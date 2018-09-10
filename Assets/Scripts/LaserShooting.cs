using UnityEngine;
using System.Collections;

public class LaserShooting : MonoBehaviour
{
    LineRenderer gunLine;

    Ray shootRay;
    RaycastHit shootHit;

    GameObject player;
    PlayerHealth playerHealth;

    public float timeOn = 3.0f;
    public float timeOff = 1.5f;

    bool playerInRange;
    float timer;
    float timerAttack = 0.5f;
    public bool active;

    public int attackDamage = 2000;
    public float timeBetweenAttacks = 0.5f;
	public ParticleSystem impactEffect;
	public Light laserLight;

    void Awake()
    {
        gunLine = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

        // Use this for initialization
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerAttack += Time.deltaTime;

        if (active)
        {
            shootRay.origin = transform.position;
            shootRay.direction = -transform.up;

            if (Physics.Raycast(shootRay, out shootHit))
                if (shootHit.collider.gameObject.tag == "Player")
                {
                    playerInRange = true;
                    //print("HIT TARGET");
                }else
                {
                    playerInRange = false;
                }

            //if (Physics.Raycast(shootRay, out shootHit, 15))
            //{
            //    print(shootHit.collider.gameObject.name);
            //}
        }else
        {
            playerInRange = false;
        }

        if (timerAttack >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }

        if (active && timer >= timeOn)
        {
            
            DisableEffects();
            active = false;
            timer = 0f;
        }
        else if (!active && timer >= timeOff)
        {
            createLaser();
            active = true;
            timer = 0f;
        }
    }

    void Attack()
    {
        timerAttack = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
		laserLight.enabled = false;
		impactEffect.Stop();
    }

    void createLaser()
    {
        gunLine.enabled = true;
		laserLight.enabled = true;
        gunLine.SetPosition(0, transform.position);
		impactEffect.Play();

        shootRay.origin = transform.position;
        shootRay.direction = -transform.up;
        gunLine.SetPosition(1, shootRay.origin + shootRay.direction * 10);

    }

}
