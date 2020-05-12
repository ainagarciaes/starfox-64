using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform[] pathTarget;
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject playerHit;
    [SerializeField] GameObject bullet;
    private const int spray = 2;
    private const float spraySpan = 0.3f, cooldown = 0.7f;
    int current = -1, weaponIndex, currentSpray;
    float waitToShoot;

    public Vector3 viewportPos;


    bool hasChanged = false;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        weaponIndex = 0;
        currentSpray = spray;
        waitToShoot = 0;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(2 * transform.position - Camera.main.transform.position);
        transform.rotation = Quaternion.Euler(0, 180, 0);
        if (current >= 0 && (current + 1 < pathTarget.Length) && ChangeToNext())
        {
            current++;
            hasChanged = true;
        }
        if (waitToShoot > 0) waitToShoot -= Time.deltaTime;
        else if (OnScreen())
        {
            GameObject newbullet = Instantiate(bullet, weapons[weaponIndex].transform.position, Quaternion.identity);
            newbullet.transform.LookAt(Camera.main.transform.position);
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
            rb.AddForce((pathTarget[current].position - transform.position));
            if (velocity < 20)
                rb.AddForce((pathTarget[current].position - transform.position).normalized);
            else rb.velocity = rb.velocity.normalized * 15;
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
        float distA = Vector3.Distance(transform.position, Vector3.zero);
        float distB = Vector3.Distance(Vector3.zero, pathTarget[current].position);
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
                if (playerHit != null)
                    Instantiate(playerHit,other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
    }
}