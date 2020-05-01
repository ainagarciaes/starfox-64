using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDirection : MonoBehaviour
{
    public Vector3 direction;
    public int speed = 300;
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        lifetime = 0;
        direction = transform.forward;
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;
        transform.position += transform.forward * speed*Time.deltaTime;
        //if (lifetime > 1) Destroy(this);
    }
}
