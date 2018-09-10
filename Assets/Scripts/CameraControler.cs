using UnityEngine;

public class CameraControler : MonoBehaviour
{

    //Camera segue o personagem com um delay suave
    private Vector2 velocity;
    public float smoothTimeX;
    public float smoothTimeY;
    public GameObject Player;
    public GameObject target;

    //Camera não passa de certos pontos do mapa
    public bool border;
    public Vector3 minCamPos;
    public Vector3 maxCamPos;

    //Tremor de camera representando dano ou movimento no mapa.
    public float quakeTimer;
    public float magnitude;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        target = Player;
    }

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);

        if (border)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minCamPos.x, maxCamPos.x),
                Mathf.Clamp(transform.position.y, minCamPos.y, maxCamPos.y),
                Mathf.Clamp(transform.position.z, minCamPos.z, maxCamPos.z)
                );
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShakeCamera(0.1f, 0.2f);
        }
        if (quakeTimer >= 0)
        {
            Vector2 quakePos = Random.insideUnitCircle * magnitude;
            transform.position = new Vector3(transform.position.x + quakePos.x, transform.position.y + quakePos.y, transform.position.z);
            quakeTimer -= Time.deltaTime;
        }
    }
    //mag é a intensidade, dur é a duração.
    public void ShakeCamera(float mag, float dur)
    {
        magnitude = mag;
        quakeTimer = dur;
    }


}