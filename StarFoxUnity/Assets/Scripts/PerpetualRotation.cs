﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerpetualRotation : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 axis;
    int dir;
    int speed;
    void Start()
    {
        axis = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
        dir = (int)axis.x % 2;
        if (dir == 0) dir = -1;
        speed = Random.Range(15, 25);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, axis, speed * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}