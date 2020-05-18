using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Movement : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shieldObj;
    GameObject player;
    private const int spray = 2;
    private const float spraySpan = 0.5f, cooldown = 5f;
    int weaponIndex, currentSpray;
    float waitToShoot;
    int hits;
    public Vector3 viewportPos;
    float floatingAround;
    int shieldHits;
    public bool shield;
    bool hasChanged = false;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        shield = false;
        floatingAround = 0;
        shieldHits = 10;
        hits = 15;
        weaponIndex = 0;
        currentSpray = spray;
        waitToShoot = 0;
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        shieldObj.SetActive(shield);
        
        floatingAround += Time.deltaTime;
        transform.position += Vector3.up * Mathf.Sin(floatingAround) * Time.deltaTime*3;
        transform.position += transform.forward * Time.deltaTime * 20;


        if (waitToShoot > 0) waitToShoot -= Time.deltaTime;
        else if (OnScreen())
        {
            if (muzzle != null)
            {
                GameObject newFlash = Instantiate(muzzle, weapons[weaponIndex].transform.position, Quaternion.identity);
                newFlash.transform.parent = gameObject.transform;
                newFlash.transform.LookAt(gameObject.transform.forward);
            }
            GameObject newbullet = Instantiate(bullet, weapons[weaponIndex].transform.position, Quaternion.identity);
            newbullet.transform.LookAt(player.transform.position);
            currentSpray--;
            weaponIndex++;
            weaponIndex %= 2;
            if (currentSpray == 0)
            {
                waitToShoot = cooldown;
                currentSpray = spray;
            }
            else waitToShoot = spraySpan;
        }

    }

    private bool OnScreen()
    {
        viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x < 0) return false;
        if (viewportPos.y < 0) return false;
        if (viewportPos.x > 1) return false;
        if (viewportPos.y > 1) return false;
        return true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("EnemyBullet"))
            if (other.CompareTag("PlayerBullet"))
            {
                other.gameObject.GetComponent<ProjectileMovement>().HitnDestroy();
                --hits;
                LevelManager.Instance.UpdateScore(1);
                if (hits <= 0)
                {
                    LevelManager.Instance.UpdateScore(30);
                    Destroy(gameObject);
                }
            }
    }
}