using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScope : MonoBehaviour
{
    [SerializeField] GameObject lookAtObject;
    public Vector2 distance;
    public Vector3 viewportPos;
    public Vector3 viewportAim;

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

        transform.RotateAround(transform.position, transform.forward, 50 * (viewportPos.x - viewportAim.x));
        distance = viewportAim - viewportPos;
        transform.position = Camera.main.ViewportToWorldPoint(viewportPos + new Vector3(distance.x, distance.y, 0) * 0.005f);
    }
}
