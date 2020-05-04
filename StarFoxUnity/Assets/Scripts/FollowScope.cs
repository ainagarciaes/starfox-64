using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScope : MonoBehaviour
{
    [SerializeField] GameObject lookAtObject;
    public Vector2 distance;
    public Vector3 viewportPos;
    public Vector3 viewportAim;

    // rotation parameters
    private float offset;
    private float offset_ini;
    private bool rotation_side; //true = right roll, false = left roll
    private bool rotating = false;
    private float rotation_step;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        viewportAim = Camera.main.WorldToViewportPoint(lookAtObject.transform.position);
        transform.LookAt(lookAtObject.transform.position);

        if (rotating)
        {
            rotate();
            transform.RotateAround(transform.position, transform.forward, offset);
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                offset_ini = 50 * (viewportPos.x - viewportAim.x);
                rotation_step = 0;
                rotating = true;
            }
            transform.RotateAround(transform.position, transform.forward, 50 * (viewportPos.x - viewportAim.x));
        }

        distance = viewportAim - viewportPos;
        transform.position = Camera.main.ViewportToWorldPoint(viewportPos + new Vector3(distance.x, distance.y - 0.1f, 0) * 0.005f);
    }

    private void rotate()
    {
        rotation_step += 1;
        float diff = Mathf.Sin(Mathf.PI * rotation_step * Time.deltaTime * 0.01f);

        if (diff <= 0)
        {
            print("stop rotation");
            rotating = false;
            offset = 0;
            return;
        }
        offset = diff * 360 + offset_ini;
    }
}
