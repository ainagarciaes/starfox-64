using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScope : MonoBehaviour
{
    [SerializeField] GameObject lookAtObject;
    [SerializeField] GameObject enemyHit;

    public Vector2 distance;
    public Vector3 viewportPos;
    public Vector3 viewportAim;
    public float finalBarrelX;

    public int hits;
    // rotation parameters
    private float offset;
    private float offset_ini;
    private float rotation_side; //true = right roll, false = left roll
    private bool rotating = false;
    private float rotation_step;
    private float bias;

    private const float rollAcc = 10f;
    float cooldown;
    public float duration;
    private bool rollInitialized;
    private float rollingSpeed;
    private float currentRotation;
    // Start is called before the first frame update
    void Start()
    {
        duration = 0;
        hits = 0;
        cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (LevelManager.IsPaused) { /*do nothing*/ }
        else
        {
            LevelManager.Instance.SetRotation(rotating);

            if (rotating)
            {
                DoABarrelRoll();
                duration += Time.deltaTime;
                if (duration > 0.15f) DoABarrelRollLatMovement();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    currentRotation = 0;
                    rollingSpeed = 1;
                    rotating = true;
                    rollInitialized = false;
                    rotation_side = -1;
                    bias = 1;
                    duration = 0;
                    finalBarrelX = Mathf.Clamp(viewportPos.x - 0.3f, 0.1f, 0.9f);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    currentRotation = 0;
                    rollingSpeed = 1;
                    rotating = true;
                    rollInitialized = false;
                    rotation_side = 1;
                    bias = 1;
                    duration = 0;
                    finalBarrelX = Mathf.Clamp(viewportPos.x + 0.3f, 0.1f, 0.9f);

                }
                viewportPos = Camera.main.WorldToViewportPoint(transform.position);
                viewportAim = Camera.main.WorldToViewportPoint(lookAtObject.transform.position);
                transform.LookAt(lookAtObject.transform.position);
                transform.RotateAround(transform.position, transform.forward, 50 * (1 - cooldown) * (1 - cooldown) * (viewportPos.x - viewportAim.x));
                distance = viewportAim - viewportPos;
                viewportPos += new Vector3(distance.x, distance.y - 0.1f, 0) * Mathf.Pow(1 - cooldown, 3) * Time.deltaTime;
                viewportPos.x = Mathf.Clamp(viewportPos.x, 0.1f, 0.9f);
                viewportPos.y = Mathf.Clamp(viewportPos.y, 0.1f, 0.9f);
                //if (Vector2.Distance(Vector2.zero, new Vector2(distance.x, distance.y)) > 0.1f)
                transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
                if (cooldown > 0)
                    cooldown -= Time.deltaTime;
                else cooldown = 0;
            }
        }
    }

    private bool OnScreen()
    {
        viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x < 0.1f) return false;
        if (viewportPos.y < 0.1f) return false;
        if (viewportPos.x > 0.9f) return false;
        if (viewportPos.y > 0.9f) return false;
        return true;
    }
    private void DoABarrelRollLatMovement()
    {
        viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        float latMov = finalBarrelX - viewportPos.x;
        if (OnScreen())
            transform.position = Camera.main.ViewportToWorldPoint(viewportPos + Vector3.right * 5 * latMov * Time.deltaTime);
    }
    private void DoABarrelRoll()
    {

        if (currentRotation < 180)
            rollingSpeed += Time.deltaTime * rollingSpeed * rollAcc;
        else if (currentRotation < 360 && rollingSpeed >= 1)
            rollingSpeed -= Time.deltaTime * rollingSpeed / 2 * rollAcc;
        else
        {
            rotating = false;
            rollingSpeed = 0;
            offset = 0;
            cooldown = 0.7f;
            return;
        }
        currentRotation += rollingSpeed;
        transform.RotateAround(transform.position, transform.forward, -rotation_side * rollingSpeed);

    }

    private void EnterRoll()
    {
        viewportAim = Camera.main.WorldToViewportPoint(lookAtObject.transform.position);

        transform.LookAt(Camera.main.ViewportToWorldPoint(
                            new Vector3((1 - bias) * viewportPos.x + bias * viewportAim.x,
                                        (1 - bias) * viewportPos.y + bias * viewportAim.y,
                                        -viewportAim.z)),
                        transform.up);
        if (bias > 0.05f)
            bias *= Mathf.Pow(0.9f, 500 * Time.deltaTime);
        else
        {
            bias = 0;
            Vector3 diff2 = transform.parent.transform.up - transform.up;
            float dist = Vector3.Distance(Vector3.zero, diff2);
            if (dist > 0.2f) transform.up += 20f * Time.deltaTime * (diff2);
            else
                transform.up = transform.parent.transform.up;
            if (dist <= 0.2f)
            {
                currentRotation = 0;
                rollingSpeed = 1;
                rollInitialized = true;
            }
        }
        currentRotation = 0;
        rollingSpeed = 1;
        rollInitialized = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            hits++;
            int damage = 1;

            if (other.gameObject.GetComponent<ProjectileMovement>() != null)
            {
                other.gameObject.GetComponent<ProjectileMovement>().HitnDestroy();
                damage = other.gameObject.GetComponent<ProjectileMovement>().GetDamage();
            }
            else if (other.gameObject.GetComponent<SmartProjectileMovement>() != null)
            {
                other.gameObject.GetComponent<SmartProjectileMovement>().HitnDestroy();
                damage = other.gameObject.GetComponent<SmartProjectileMovement>().GetDamage();
            }
            LevelManager.Instance.UpdateHitPoints(damage, 0);
            print(damage);

        }

        if (other.CompareTag("CollidableEnemy"))
        {
            print(other.gameObject.name);
            LevelManager.Instance.UpdateHitPoints(20, 1);
            other.enabled = false;
            other.gameObject.transform.GetComponent<PerpetualRotation>().Explode();
        }

        if (other.CompareTag("DamagePerSecond"))
        {
            LevelManager.Instance.SetBurning(true);
            other.gameObject.transform.parent.transform.Find("Audio").GetComponent<AudioManager>().RiseVolume();
        }
        if (other.CompareTag("ToNextLevel"))
        {
            print("COLLIDES");
            LevelManager.Instance.GameWin();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DamagePerSecond"))
        {
            LevelManager.Instance.SetBurning(false);
            other.gameObject.transform.parent.transform.Find("Audio").GetComponent<AudioManager>().ResetVolume();
        }
    }
}
