using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeItExplode : MonoBehaviour
{
    [SerializeField] GameObject regular;
    [SerializeField] GameObject explosion;
    bool explode = false;

    // Start is called before the first frame update
    float explosionTime = 1;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!explode) return;
        explosionTime -= Time.deltaTime;
        if (explosionTime < 0)
        {
            regular.SetActive(false);
            GameObject inst = Instantiate(explosion, transform.parent);
            inst.transform.position = regular.transform.position;
            inst.transform.forward = regular.transform.forward;
            inst.transform.up = regular.transform.up;
            explode = false;
        }
    }

    public void Boom()
    {
        explode = true;
    }
}
