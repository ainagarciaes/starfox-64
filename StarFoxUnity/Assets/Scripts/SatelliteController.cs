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
        timeCounter += Time.deltaTime;
        if (laserCrosshair.activeSelf)
        {
            Vector3 lookDir = player.position + player.forward * 6 - transform.position;
            float angle = Vector3.Angle(lookDir, transform.forward);
            float dist = Vector3.Distance(lookDir, Vector3.zero);
            Ray ray = new Ray(transform.position, transform.forward);
            float distToRay = Vector3.Cross(ray.direction, player.position - ray.origin).magnitude;
            //print(distToRay + " " + dist + " " + angle);
            if (distToRay < 2)
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
            timeCounter = Random.Range(0,0.5f);
            return;
        }
        if (timeCounter >= shootTime)
        {
            timeCounter = 0;
            GameObject newFlash = Instantiate(muzzle, laserCrosshair.transform.position, Quaternion.identity);
            newFlash.transform.LookAt(player.position + Camera.main.transform.forward*7);
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
