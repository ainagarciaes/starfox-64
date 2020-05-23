using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeItExplode : MonoBehaviour
{
    [SerializeField] GameObject regular;
    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    float explosionTime = 8;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        explosionTime -= Time.deltaTime;
        if (explosionTime < 0)
        {
            regular.SetActive(false);
            explosion.SetActive(true);
        }
    }
}
