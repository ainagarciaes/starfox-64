using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCart : MonoBehaviour
{
    public bool enabled = false;
    [SerializeField] GameObject LookAtObj;
    [SerializeField] GameObject PlayerArea;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled) return;
        transform.position = 2 * PlayerArea.transform.position - LookAtObj.transform.position;
        transform.LookAt(LookAtObj.transform.position);
        transform.localPosition += Vector3.forward * -40;
    }
}
