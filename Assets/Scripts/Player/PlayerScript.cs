﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerScript : MonoBehaviour
{

    [SerializeField]
    private GameObject rocket;

    [SerializeField]
    private AudioClip shootSound;

    private float speed = 8f;
    private float maxVelocity = 4f;

    private Rigidbody2D myBody;
    private Animator anim;

    private bool canShoot;
    private bool canWalk;

    void Awake()
    {
        InitializeVariables();
    }


    void Update()
    {
        Shoot();
    }

    private void FixedUpdate()
    {
        PlayerWalk();
    }

    void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (canShoot)
            {
                canShoot= false;
                StartCoroutine(ShootTheRocket());
            }
        }
    }

    IEnumerator ShootTheRocket()
    {
        canWalk = false;
        anim.Play("Shoot");

        Vector3 temp = transform.position;
        temp.y += 1.5f;

        Instantiate(rocket, temp, Quaternion.identity);

        //AudioSource.PlayClipAtPoint(shootSound, transform.position);

        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Shoot", false);
        canWalk = true;

        yield return new WaitForSeconds(0.3f);
        canShoot = true;
    }

    void InitializeVariables()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canShoot = true;
        canWalk = true;
    }

    void PlayerWalk()
    {
        var force = 0f;
        var velocity = Mathf.Abs(myBody.velocity.x);

        float h = CrossPlatformInputManager.GetAxis("Horizontal");

        if (canWalk)
        {
            if (h > 0)
            {
                if (velocity < maxVelocity)
                {
                    force = speed;

                    Vector3 scale = transform.localScale;
                    scale.x = 1;
                    transform.localScale = scale;

                    anim.SetBool("Walk", true);
                }
            }
            else if (h < 0)
            {
                if (velocity < maxVelocity)
                {
                    force = -speed;

                    Vector3 scale = transform.localScale;
                    scale.x = -1;
                    transform.localScale = scale;

                    anim.SetBool("Walk", true);
                }
            }
            else if (h == 0)
            {
                anim.SetBool("Walk", false);
            }
        }

        myBody.AddForce(new Vector2(force, 0));
    }

    IEnumerator KillThePlayerAndRestartThegame()
    {
        transform.position = new Vector3(200, 200, 0);
        yield return new WaitForSeconds(1.5f);

        Application.LoadLevel(Application.loadedLevelName);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        string[] name = target.name.Split();
        if (name.Length>1)
        {
            if (name[1] =="Ball")
            {
                StartCoroutine(KillThePlayerAndRestartThegame());
            }
        }
    }

    
}


