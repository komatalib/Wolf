using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{

    private Rigidbody2D myBody;
    private float speed = 5f;


    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
        
    void FixedUpdate()
    {
        myBody.velocity = new Vector2(0, speed);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Top")
        {
            Destroy(gameObject);
        }

        string[] name = target.name.Split();

        if (name.Length>1)
        {
            if (name[1] == "Ball")
            {
                Destroy(gameObject);
            }
        }

    }
}
