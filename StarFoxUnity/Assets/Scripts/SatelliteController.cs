using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    Transform player;
    [SerializeField] GameObject laserCrosshair;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject laserProjectile;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioManager audio;


    private const float shootTime = 3, cooldown = 3, maxSpeed = 30;
    private float timeCounter, speed;
    private bool alive;
    float dist;

    public float maxRange;


    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        speed = 0.5f;
        timeCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;
        if (laserCrosshair == null) return;
        dist = Vector3.Distance(transform.position, player.position + player.forward * 6);
        if (dist > 300) return;
        timeCounter += Time.deltaTime;

        if (laserCrosshair.activeSelf)
        {
            Vector3 lookDir = player.position + player.forward * 6 - transform.position;
            Ray ray = new Ray(transform.position, transform.forward);
            float distToRay = Vector3.Cross(ray.direction, player.position - ray.origin).magnitude;
            //print(distToRay + " " + dist + " " + angle);
            if (distToRay < 0.5f)
                laserCrosshair.GetComponent<LineRenderer>().SetPosition(1, Vector3.forward * (dist));
            else
                laserCrosshair.GetComponent<LineRenderer>().SetPosition(1, Vector3.forward * (300));
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            if (speed < maxSpeed) speed += speed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }

        if (!laserCrosshair.activeSelf && timeCounter > cooldown)
        {
            laserCrosshair.SetActive(true);
            speed = 0.5f;
            timeCounter = Random.Range(0, 0.5f);
            return;
        }
        if (timeCounter >= shootTime)
        {
            timeCounter = 0;
            GameObject newFlash = Instantiate(muzzle, laserCrosshair.transform.position, Quaternion.identity);
            newFlash.transform.LookAt(player.position + Camera.main.transform.forward * 7);
            Destroy(newFlash, 4);
            GameObject newbullet = Instantiate(laserProjectile, laserCrosshair.transform.position, Quaternion.identity);
            newbullet.transform.LookAt(player.position + Camera.main.transform.forward * 7);
            laserCrosshair.GetComponent<LineRenderer>().SetPosition(1, Vector3.forward * (300));
            laserCrosshair.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("EnemyBullet"))
            if (other.CompareTag("PlayerBullet"))
            {
                other.gameObject.GetComponent<ProjectileMovement>().HitnDestroy();
                LevelManager.Instance.UpdateScore(5);
                gameObject.transform.Find("SatelliteMesh").gameObject.SetActive(false);
                if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
                alive = false;
                audio.PlaySingleSound(0, 0.8f);
                Destroy(gameObject, 3);
            }
    }
}
