using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject scope;
    [SerializeField] GameObject[] weapons;
    int weaponind = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.IsPaused) { /*do nothing*/ }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject newbullet = Instantiate(bullet, weapons[weaponind].transform.position, Quaternion.identity);
                newbullet.transform.LookAt(Camera.main.ViewportToWorldPoint(Camera.main.WorldToViewportPoint(scope.transform.position) + Vector3.forward * 1000 + Vector3.up * 0.05f));
                weaponind++;
                weaponind %= 2;
            }
        }
    }
}
