using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartProjectileMovement : MonoBehaviour
{
    [SerializeField] GameObject hit;
    public Vector3 direction;
    public int speed = 300;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.forward;
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void HitnDestroy()
    {
        if (hit != null)
            Instantiate(hit, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public int GetDamage()
    {
        return damage;
    }
}
