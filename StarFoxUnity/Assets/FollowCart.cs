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
        transform.position = PlayerArea.transform.position +(PlayerArea.transform.position - LookAtObj.transform.position).normalized*40;
        transform.LookAt(LookAtObj.transform.position);
    }
}
