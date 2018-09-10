using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private bool m_player = false;
    bool _shoot = false;
    float _time = 0.0f;
    float _shootForce = 20.0f;

    public Rigidbody m_projectile;
    public float _raycastLeftSize = 1.5f;
    public float _raycastRightSize = 1.5f;
    public float timeBetweenShoot = 1.0f;
    public float _raycastUpDown = 5.0f;
    public Transform m_pivot;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
        if (_shoot)
        {
            _time += Time.deltaTime;
            if (_time >= timeBetweenShoot)
            {
                _shoot = false;
            }
        }
    }

    bool CheckPlayer()
    {
        //Debug.Log("CHECKED");
        /*function Update() {
         var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
         var hit : RaycastHit;
         if (Physics.Raycast (ray, hit, 100)) {
              if (hit.collider.gameObject.find("wormyguy")) {
                     Debug.Log("Success");
                 }
         }
     }*/

        RaycastHit hit;
        if (!_shoot && (Physics.Raycast(transform.position, Vector3.right, out hit, transform.localScale.y + _raycastRightSize)) && hit.collider.gameObject.tag == "Player")
        {
            Rigidbody clone;
            clone = (Rigidbody)Instantiate(m_projectile, transform.position + Vector3.up + Vector3.right, m_pivot.rotation);
            //clone.AddForce(transform.TransformDirection(Vector3.forward) * 1600);
            clone.AddForce(Vector3.right * _shootForce, ForceMode.Impulse);

            _shoot = true;
            _time = 0.0f;
            m_player = true;
        }
        else if (!_shoot && (Physics.Raycast(transform.position, Vector3.left, out hit, transform.localScale.y + _raycastLeftSize)) && hit.collider.gameObject.tag == "Player")
        {
            Rigidbody clone;
            clone = (Rigidbody)Instantiate(m_projectile, transform.position + Vector3.up + Vector3.left, m_pivot.rotation);
            clone.AddForce(Vector3.right * -_shootForce, ForceMode.Impulse);
            _shoot = true;
            _time = 0.0f;
            //Debug.Log("CHECKED PLAYER LEFT");
            m_player = true;
        }


        else if (!_shoot && (Physics.Raycast(transform.position, Vector3.down, out hit, transform.localScale.y + _raycastUpDown)) && hit.collider.gameObject.tag == "Player")
        {
            Rigidbody clone;
            clone = (Rigidbody)Instantiate(m_projectile, transform.position + Vector3.up + Vector3.forward, m_pivot.rotation);
            clone.AddForce(Vector3.down * _shootForce, ForceMode.Impulse);
            _shoot = true;
            _time = 0.0f;
            m_player = true;
        }

        else if (!_shoot && (Physics.Raycast(transform.position, Vector3.up, out hit, transform.localScale.y + _raycastUpDown)) && hit.collider.gameObject.tag == "Player")
        {
            Rigidbody clone;
            clone = (Rigidbody)Instantiate(m_projectile, transform.position + Vector3.up + Vector3.forward, m_pivot.rotation);
            clone.AddForce(Vector3.down * -_shootForce, ForceMode.Impulse);
            _shoot = true;
            _time = 0.0f;
            m_player = true;
        }

        else
        {
            m_player = false;
        }
        return m_player;
    }
}
