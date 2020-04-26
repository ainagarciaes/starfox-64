using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScope : MonoBehaviour
{
    [SerializeField] GameObject lookAtObject;
    Rigidbody rb;
    public Vector2 distance;
    public Vector3 viewportPos;
    public Vector3 viewportAim;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        viewportAim = Camera.main.WorldToViewportPoint(lookAtObject.transform.position);
        transform.LookAt(lookAtObject.transform.position);

        //viewportPos = transform.position;
        //viewportAim = lookAtObject.transform.position;
        distance = viewportAim - viewportPos;
        transform.position = Camera.main.ViewportToWorldPoint(viewportPos + new Vector3(Mathf.Pow(distance.x,3),Mathf.Pow(distance.y,3),0)*0.1f);
    }
    private void FixedUpdate()
    {
        //Vector3 newVelocity = rb.velocity;
        //if ((rb.velocity.x * distance.x) < 0) newVelocity.x *= 0.5f;
        //if ((rb.velocity.y * distance.y) < 0) newVelocity.y *= 0.5f;

        //rb.AddForce(0.2f * distance * distance * distance);
        //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
    }
}
