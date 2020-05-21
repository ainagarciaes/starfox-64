using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class garbageCollector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "TerrainCollider" && other.gameObject.tag != "Untagged")
        {

            print("destroying: " + other.gameObject.name);
            Destroy(other.gameObject);  
        }
    }
}
