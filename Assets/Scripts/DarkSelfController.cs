﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DarkSelfController : MonoBehaviour
{

    public Vector3[] points;

    private Rigidbody2D rigidBody;

    public float speed;
    private int current;
    private float alpha;

    public bool finished;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        speed = 2f;
        current = points.Length - 1;
        alpha = 0.2f;
        finished = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos1 = transform.position;
        Vector3 pos2 = points[current - 1];

        Vector2 direction = new Vector2(pos2.x - pos1.x, pos2.y - pos1.y);
        //Vector2 move = new Vector2(direction.x * speed, direction.y * speed);
        //rigidBody.velocity = move;
        rigidBody.position = rigidBody.position + direction * speed * Time.fixedDeltaTime;

        if (Vector3.Distance(pos1, pos2) < alpha)
        {
            current--;
            if (current > 0)
                pos2 = points[current - 1];
            direction = new Vector2(pos2.x - pos1.x, pos2.y - pos1.y).normalized;
            //move = new Vector2(direction.x * speed, direction.y * speed);
            //rigidBody.velocity = move;
            rigidBody.position = rigidBody.position + direction * speed * Time.fixedDeltaTime;
        }

        if (current == 0)
        {
            finished = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bolder") || other.CompareTag("DoorClosed") || other.CompareTag("Lava"))
            GameManager.instance.GameOver();
            //DimensionManager.instance.EnableTrajectory();
            //SceneManager.LoadScene("NoTexturesScene");
    }
}
