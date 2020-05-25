using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Movement : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shieldObj;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioManager audio;

    public Vector3 MoveTo;

    GameObject player;
    private const int maxShield = 40, life = 40;
    private const float spraySpan = 1.5f, shieldCooldown = 3f;
    private bool alive;

    int weaponIndex;
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
        alive = true;
        shield = true;
        floatingAround = 0;
        shieldHits = 0;
        hits = life;
        weaponIndex = 0;
        waitToShoot = 0;
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;
        shieldObj.SetActive(shield);

        floatingAround += Time.deltaTime;
        transform.position += Vector3.up * Mathf.Sin(floatingAround) * Time.deltaTime * 3;
        transform.position += Vector3.Normalize(MoveTo - transform.position) * Time.deltaTime * 10;
        transform.LookAt(MoveTo);

        if (waitToShoot > 0) waitToShoot -= Time.deltaTime;
        else if (OnScreen() && shield)
        {
            if (muzzle != null)
            {
                GameObject newFlash = Instantiate(muzzle, weapons[weaponIndex].transform.position, Quaternion.identity);
                newFlash.transform.parent = gameObject.transform;
                newFlash.transform.LookAt(gameObject.transform.forward);
                Destroy(newFlash, 4);
            }
            GameObject newbullet = Instantiate(bullet, weapons[weaponIndex].transform.position, Quaternion.identity);
            newbullet.transform.LookAt(player.transform.position);
            weaponIndex++;
            weaponIndex %= 2;
            waitToShoot = spraySpan;
        }
        else if (!shield)
        {
            audio.PlaySingleSound(1, 0.6f);
            shield = true;
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
                if (!shield)
                {
                    print("HIT");
                    --hits;
                    other.gameObject.GetComponent<ProjectileMovement>().HitnDestroy();
                    LevelManager.Instance.UpdateScore(1);
                    if (hits <= 0)
                    {
                        LevelManager.Instance.UpdateScore(30);
                        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
                        gameObject.transform.GetComponent<MeshCollider>().enabled = false;
                        gameObject.transform.Find("E2 FighterV2").gameObject.SetActive(false);
                        shieldObj.SetActive(false);
                        audio.PlaySingleSound(3, 0.6f);
                        alive = false;
                        Destroy(gameObject, 3);
                    }

                }
                else
                {
                    ++shieldHits;
                    other.gameObject.GetComponent<ProjectileMovement>().HitnDestroy();
                    audio.PlaySingleSound(0);
                    if (shieldHits >= maxShield)
                    {
                        shieldHits = 0;
                        audio.PlaySingleSound(2, 0.6f);
                        shield = false;
                        waitToShoot = shieldCooldown;
                    }
                }
    }
}