using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform[] pathTarget;
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioManager audio;

    GameObject player;
    private const int spray = 2;
    private const float spraySpan = 0.3f, cooldown = 0.7f;
    private bool alive;
    int current = -1, weaponIndex, currentSpray;
    float waitToShoot;

    public Vector3 viewportPos;


    bool hasChanged = false;
    AudioManager AudioMngr;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        weaponIndex = 0;
        currentSpray = spray;
        waitToShoot = 0;
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        AudioMngr = gameObject.transform.Find("E1 Fighter").transform.Find("LaserSound").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!alive) return;

        if (current >= 0 && (current + 1 < pathTarget.Length) && ChangeToNext())
        {
            current++;
            hasChanged = true;
        }
        if (waitToShoot > 0) waitToShoot -= Time.deltaTime;
        else if (OnScreen() && current > 1)
        {
            AudioMngr.PlaySound();
            GameObject newFlash = Instantiate(muzzle, weapons[weaponIndex].transform.position, Quaternion.identity);
            newFlash.transform.parent = gameObject.transform;
            newFlash.transform.LookAt(gameObject.transform.forward);
            Destroy(newFlash, 4);
            GameObject newbullet = Instantiate(bullet, weapons[weaponIndex].transform.position, Quaternion.identity);
            newbullet.transform.LookAt(player.transform.position+player.transform.parent.transform.forward*10);
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

    private void FixedUpdate()
    {
        if (!alive) return;


        if (hasChanged)
        {
            //rb.velocity = 0.7f * rb.velocity;
            rb.AddForce((pathTarget[current].position - transform.position));
            //transform.LookAt(2 * transform.position - Camera.main.transform.position);
            hasChanged = false;
        }
        float velocity = Vector3.Distance(Vector3.zero, rb.velocity);
        if (current >= 0)
        {
            Vector3 lookDir = rb.velocity;
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            //transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 1f);
            rb.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 1f);
            rb.AddForce((pathTarget[current].position - transform.position));
            if (velocity < 30)
                rb.AddForce((pathTarget[current].position - transform.position).normalized);
            else rb.velocity = rb.velocity.normalized * 25;
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

    private bool ChangeToNext()
    {
        float distA = Vector3.Distance(transform.position,player.transform.position);
        float distB = Vector3.Distance(player.transform.position, pathTarget[current].position);
        if (distB > distA) return true;
        return false;
    }


    public void SetPathTarget(Transform[] newPath)
    {
        current = 0;
        hasChanged = true;
        pathTarget = newPath;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("EnemyBullet"))
            if (other.CompareTag("PlayerBullet"))
            {
                other.gameObject.GetComponent<ProjectileMovement>().HitnDestroy();
                LevelManager.Instance.UpdateScore(5);
                gameObject.transform.GetComponent<MeshCollider>().enabled = false;
                gameObject.transform.Find("E1 Fighter").gameObject.SetActive(false);
                if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
                alive = false;
                audio.PlaySingleSound();
                Destroy(gameObject, 3);
            }
    }

}