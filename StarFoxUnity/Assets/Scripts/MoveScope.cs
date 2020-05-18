using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScope : MonoBehaviour
{
    private Vector3 viewportPos;
    private Vector3 viewportAim;
    private Vector2 distance;

    public float screenWidth = 14;
    public float screenHeight = 10;
    public float min = -0.50f;
    public float max = 0.5f;
    public float mouseX;
    public float mouseY;
    public float xyspeed = 18;
    public float rollInstant;
    private float bias;


    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rollInstant = 0;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.IsPaused) { /*do nothing*/ }
        else
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                rollInstant = 0.5f; bias = 1;
            }
            if (rollInstant <= 0) Move();
            else
            {
                viewportPos = Camera.main.WorldToViewportPoint(transform.position);
                viewportAim = Camera.main.WorldToViewportPoint(player.transform.position);
                rollInstant -= Time.deltaTime;
                if (rollInstant > 0.4f) CenterScopeLocation();
                else TweenScopeLocation();
            }
        }

    }

    private void CenterScopeLocation()
    {
        //distance = viewportAim - viewportPos;

        //transform.position = Camera.main.ViewportToWorldPoint(viewportPos + new Vector3(distance.x, distance.y + 0.1f, 0) * 25 * Time.deltaTime);

        //if (bias > 0.05f)
        //    bias *= Mathf.Pow(0.999f, Time.deltaTime);
        //else
        //{
        //    bias = 0;
        //}
    }
    private void TweenScopeLocation()
    {
        //mouseX = Input.GetAxis("Mouse X") / Screen.width;
        //mouseY = Input.GetAxis("Mouse Y") / Screen.height;
        //float newx = (Input.mousePosition.x / Screen.height) - 0.5f;
        //float newy = (Input.mousePosition.y / Screen.height) - 0.5f;
        //newx = screenWidth * Mathf.Clamp(newx, min, max);
        //newy = screenHeight * Mathf.Clamp(newy, min, max);
        //distance.x = newx - transform.localPosition.x;
        //distance.y = newy - transform.localPosition.y;
        //transform.localPosition = transform.localPosition + new Vector3(distance.x, distance.y, 0) * 0.1f;
    }

    private void Move()
    {
        mouseX = Input.GetAxis("Mouse X") / Screen.width;
        mouseY = Input.GetAxis("Mouse Y") / Screen.height;
        float newx = transform.localPosition.x / screenWidth + mouseX * 20;
        float newy = transform.localPosition.y / screenHeight + mouseY * 20;
        newx = screenWidth * Mathf.Clamp(newx, min, max);
        newy = screenHeight * Mathf.Clamp(newy, min, max);
        transform.localPosition = new Vector3(newx, newy, transform.localPosition.z);
    }
}
