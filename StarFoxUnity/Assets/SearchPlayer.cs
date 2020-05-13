using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour
{
    Transform player;
    [SerializeField] Transform partToRotate;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    // Update is called once per frame
    void Update()
    {
        //var lookPos = player.transform.position - transform.position;
        ////lookPos.y = 0;
        //var rotation = Quaternion.LookRotation(lookPos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 120);

        Vector3 lookDir = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookDir);
        //Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime).eulerAngles;
        //partToRotate.rotation = Quaternion.Euler(rotation.x, 0, 0);
        //rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime*0.5f);
    }
}
