using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject scope;
    [SerializeField] GameObject[] weapons;
    float rate = 12f;
    float cooldown = 0.5f;
    int cooldownrate = 3;
    float next;
    int nshot;
    // Start is called before the first frame update
    void Start()
    {
        next = 0;
        nshot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.IsPaused) { /*do nothing*/ }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (next <= 0)
                {
                    nshot++;
                    GameObject newFlash = Instantiate(muzzle, weapons[0].transform.position, Quaternion.identity);
                    newFlash.transform.parent = gameObject.transform;
                    GameObject newbullet = Instantiate(bullet, weapons[0].transform.position, Quaternion.identity);
                    newbullet.transform.LookAt(Camera.main.ViewportToWorldPoint(Camera.main.WorldToViewportPoint(scope.transform.position) + Vector3.forward * 1000 + Vector3.up * 0.05f));
                    newFlash = Instantiate(muzzle, weapons[1].transform.position, Quaternion.identity);
                    newFlash.transform.parent = gameObject.transform;

                    newbullet = Instantiate(bullet, weapons[1].transform.position, Quaternion.identity);
                    newbullet.transform.LookAt(Camera.main.ViewportToWorldPoint(Camera.main.WorldToViewportPoint(scope.transform.position) + Vector3.forward * 1000 + Vector3.up * 0.05f));
                    if (nshot >= cooldownrate)
                    {
                        next = cooldown;
                        nshot = 0;
                    }
                    else next = 1.0f / rate ;
                }
                else
                {
                    next -= Time.deltaTime;
                }
            }
            else
            {
                next = 0;
                nshot = 0;
            }
        }
    }
}
