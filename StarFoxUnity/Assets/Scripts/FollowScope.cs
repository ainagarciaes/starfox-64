using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScope : MonoBehaviour
{
    [SerializeField] GameObject lookAtObject;
    public Vector2 distance;
    public Vector3 viewportPos;
    public Vector3 viewportAim;
    public int hits;
    // rotation parameters
    private float offset;
    private float offset_ini;
    private float rotation_side; //true = right roll, false = left roll
    private bool rotating = false;
    private float rotation_step;
    private float bias;

    private const float rollAcc = 10f;
    public float duration;
    private bool rollInitialized;
    private float rollingSpeed;
    private float currentRotation;
    // Start is called before the first frame update
    void Start()
    {
        duration = 0;
        hits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            rotating = true;
            rollInitialized = false;
            rotation_side = -1;
            bias = 1;
            duration = 0;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rotating = true;
            rollInitialized = false;
            rotation_side = 1;
            bias = 1;
            duration = 0;

        }
        if (rotating)
        {
            DoABarrelRoll();
            duration += Time.deltaTime;
        }
        else
        {
            print(duration);

            viewportPos = Camera.main.WorldToViewportPoint(transform.position);
            viewportAim = Camera.main.WorldToViewportPoint(lookAtObject.transform.position);
            transform.LookAt(lookAtObject.transform.position);
            transform.RotateAround(transform.position, transform.forward, 50 * (viewportPos.x - viewportAim.x));
            distance = viewportAim - viewportPos;
            transform.position = Camera.main.ViewportToWorldPoint(viewportPos + new Vector3(distance.x, distance.y - 0.1f, 0) * Time.deltaTime);
        }
    }

    private void rotate()
    {
        rotation_step += 1;
        float diff = Mathf.Sin(Mathf.PI * rotation_step * Time.deltaTime);

        if (diff <= 0)
        {
            print("stop rotation");
            rotating = false;
            offset = 0;
            return;
        }
        offset = diff * 360 + offset_ini;
    }

    private void DoABarrelRoll()
    {
        if (rollInitialized)
        {
            if (currentRotation < 180)
                rollingSpeed += Time.deltaTime * rollingSpeed * rollAcc;
            else if (currentRotation < 390 && rollingSpeed >= 1)
                rollingSpeed -= Time.deltaTime * rollingSpeed * rollAcc;
            else
            {
                print("stop rotation");
                rotating = false;
                rollingSpeed = 0;
                offset = 0;
                return;
            }
            currentRotation += rollingSpeed;
            transform.RotateAround(transform.position, transform.forward, -rotation_side * rollingSpeed);
        }
        else EnterRoll();
    }

    private void EnterRoll()
    {
        transform.LookAt(Camera.main.ViewportToWorldPoint(
                            new Vector3((1 - bias) * viewportPos.x + bias * viewportAim.x,
                                        (1 - bias) * viewportPos.y + bias * viewportAim.y, 
                                        viewportAim.z)), 
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            hits++;
            //Destroy(gameObject);
        }
    }
}
