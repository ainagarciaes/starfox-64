using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform[] pathTarget;
    int current = -1;
    bool hasChanged = false;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
        //transform.up = Vector3.down;

        transform.rotation = Quaternion.Euler(0, 180,0);
        if (current >= 0 && (current + 1 < pathTarget.Length) && changeToNext())
        {
            current++;
            hasChanged = true;
        }

    }
    private void FixedUpdate()
    {
        if (hasChanged)
        {
            //rb.velocity = 0.7f * rb.velocity;
            rb.AddForce((pathTarget[current].position - transform.position) * 2f); 
            transform.LookAt(2 * transform.position - Camera.main.transform.position);
            //transform.LookAt(2 * transform.position - pathTarget[current].position);
            hasChanged = false;
        }
        rb.AddForce((pathTarget[current].position - transform.position) * 2f);
        float velocity = Vector3.Distance(Vector3.zero, rb.velocity);
        if (current >= 0)
        {
            if (velocity < 45)
                rb.AddForce((pathTarget[current].position - transform.position).normalized * 2f);
            else rb.velocity = rb.velocity.normalized * 40;
        }
    }

    private bool changeToNext()
    {
        float distA = Vector3.Distance(transform.position, Vector3.zero);
        float distB = Vector3.Distance(Vector3.zero, pathTarget[current].position);
        if (distB > distA) return true;
        return false;
    }
    public void SetPathTarget(Transform[] newPath)
    {
        current = 0;
        hasChanged = true;
        pathTarget = newPath;
    }
}