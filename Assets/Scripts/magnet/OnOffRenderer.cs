using UnityEngine;
using System.Collections;

public class OnOffRenderer : MonoBehaviour {

    public float timeOn = 3.0f;
    public float timeOff = 1.5f;

    float timer;
    public bool active;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    // Toggle the Object's visibility each second.
    void Update()
    {

        timer += Time.deltaTime;

        if (active && timer >= timeOn)
        {
            rend.enabled = false;
            active = false;
            GetComponent<BoxCollider>().enabled = false;
            timer = 0f;
        }
        else if(!active && timer >= timeOff)
        {
            rend.enabled = true;
            active = true;
            GetComponent<BoxCollider>().enabled = true;
            timer = 0f;
        }
    }

    public void SetAllCollidersStatus(bool active)
    {
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = active;
        }
    }
}
