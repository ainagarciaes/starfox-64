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
        if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("TerrainCollider") && !other.gameObject.CompareTag("Spawner") && !other.gameObject.CompareTag("ToNextLevel") && !other.gameObject.CompareTag("DamagePerSecond"))
        {

            //print("destroying: " + other.gameObject.name);
            Destroy(other.gameObject);  
        }
    }
}
