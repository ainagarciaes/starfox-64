using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeBullets : MonoBehaviour
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
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            print("SHOULD DESTROY");
            other.gameObject.SetActive(false);
            DestroyImmediate(other.gameObject);
        }
        print("COLLISION " + other.gameObject.name);
    }
}
