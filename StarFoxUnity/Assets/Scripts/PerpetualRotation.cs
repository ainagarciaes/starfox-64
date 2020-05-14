using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerpetualRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject explosion;
    Vector3 axis;
    int dir;
    int speed;
    void Start()
    {
        axis = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
        dir = (int)axis.x % 2;
        if (dir == 0) dir = -1;
        speed = Random.Range(15, 25);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, axis, speed * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            other.gameObject.GetComponent<ProjectileMovement>().HitnDestroy();
            Explode();
        }
    }

    public void Explode()
    {
        if (explosion != null)
            Instantiate(explosion, transform.position, Quaternion.identity);
        gameObject.transform.GetComponent<MeshCollider>().enabled = false;
        gameObject.transform.GetComponent<MeshRenderer>().enabled = false;
        gameObject.transform.Find("Collisions").GetComponent<AudioManager>().PlaySound();
        Destroy(gameObject,3);
    }
}
