using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScope : MonoBehaviour
{
    [SerializeField] GameObject lookAtObject;
    Rigidbody rb;
    Vector2 distance;

   // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAtObject.transform.position);
        distance = new Vector2(lookAtObject.transform.position.x - transform.position.x, lookAtObject.transform.position.y - transform.position.y);
    }

    private void FixedUpdate()
    {
        Vector3 newVelocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        if ((rb.velocity.x * distance.x) < 0) newVelocity.x *= 0.5f;
        if ((rb.velocity.y * distance.y) < 0) newVelocity.y *= 0.5f;
        rb.velocity = newVelocity;
        rb.AddForce(0.2f * distance * distance * distance);

    }
}
