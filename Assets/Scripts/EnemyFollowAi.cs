using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyFollowAi : MonoBehaviour {

	private GameObject player;

	[SerializeField]
	private float moveSpeed = 250;
	[SerializeField]
	private float minDistance = 50.0f;
	[SerializeField]
	private float maxDistance = 100.0f;
	[SerializeField]
	private float rotationDrag = 0.75f;
	[SerializeField]
	private bool canShoot = true;
	[SerializeField]
	private float brakeForce = 5f;

	private bool isShooting = false;
	private bool m_timeShoot = true;
	private Vector3 direction;
	private float distance = 0.0f;
	private float m_enemyLife = 1;
	private float m_maxLife = 1;

	public enum CurrentState { Idle, Following, Attacking };
	public CurrentState currentState;
	public bool debugGizmo = true;
	public Rigidbody m_bullet;
	public Transform m_pivotPosition;
	public Image currentLifeBar;
    public int scoreToDestroy = 20;

    private LevelManager playerScore;

    public float DistanceToPlayer { get { return distance; } }
	public bool CanShoot { get { return canShoot; } set { canShoot = value; } }



	void Start()
	{  
		player = GameObject.FindGameObjectWithTag("Player");
		currentState = CurrentState.Idle;
		isShooting = false;

        playerScore = FindObjectOfType<LevelManager>();
    }

	void Update() 
	{
		//Find the distance to the player
		distance = Vector3.Distance(player.transform.position, this.transform.position);

		//Face the drone to the player
		direction = (player.transform.position - this.transform.position);
		direction.Normalize();

		float ratio = m_enemyLife/ m_maxLife;
		currentLifeBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
	}

	private void FixedUpdate()
	{  
		this.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(direction, Vector3.up);
		this.GetComponent<Rigidbody>().angularDrag = rotationDrag;

		//If the player is in range move towards
		if(distance > minDistance && distance < maxDistance )
		{
			currentState = CurrentState.Following;
			DroneMovesToPlayer();
		}

		//If the player is close enough shoot
		else if(distance < minDistance)
		{

			DroneStopsMoving();

			if(canShoot)
			{
				currentState = CurrentState.Attacking;
				ShootPlayer();
			}
		}

		//If the player is out of range stop moving
		else
		{
			currentState = CurrentState.Idle;
			DroneStopsMoving();
		}
	}

	private void DroneStopsMoving()
	{ 
		isShooting = false;
		this.GetComponent<Rigidbody>().drag = (brakeForce);
	}

	private void DroneMovesToPlayer()
	{
		isShooting = false;
		this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * moveSpeed);
	}

	private void ShootPlayer()
	{
		if(m_timeShoot){
			isShooting = true;
			Vector3 direction = (player.transform.position - transform.position).normalized;
			Rigidbody clone;
			clone = (Rigidbody)Instantiate(m_bullet, m_pivotPosition.position, transform.rotation);
			clone.AddForce(direction * 15.0f, ForceMode.Impulse);
			m_timeShoot  = false;
			StartCoroutine(ShootTime());
		}
	}

	private IEnumerator ShootTime(){
		yield return new WaitForSeconds(1.0f);
		m_timeShoot = true;
	}

	private void OnDrawGizmosSelected()
	{
		if (debugGizmo)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(this.transform.position, maxDistance);

			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(this.transform.position, minDistance);
		}
	}

	public void DecreaseLife(float damage){
		m_enemyLife -= damage;
		if(m_enemyLife <= 0){			
			Debug.Log("Killed");
            playerScore.sumScore(scoreToDestroy);
            Destroy(gameObject);
		}
	}
}