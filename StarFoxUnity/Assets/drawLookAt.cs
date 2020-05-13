using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLookAt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position+transform.forward*1000);
    }
}
