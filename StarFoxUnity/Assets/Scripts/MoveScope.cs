using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScope : MonoBehaviour
{
    public float screenWidth = 14;
    public float screenHeight = 10;
    public float min = -0.50f;
    public float max = 0.5f;
    public float mouseX;
    public float mouseY;
    public float xyspeed = 18;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") / Screen.width;
        mouseY = Input.GetAxis("Mouse Y") / Screen.height;
        float newx = (Input.mousePosition.x / Screen.height) - 0.5f;
        float newy = (Input.mousePosition.y / Screen.height) - 0.5f;
        newx = screenWidth * Mathf.Clamp(newx, min, max);
        newy = screenHeight * Mathf.Clamp(newy, min, max);
        transform.localPosition = new Vector3(newx, newy, transform.localPosition.z);
    }
}
