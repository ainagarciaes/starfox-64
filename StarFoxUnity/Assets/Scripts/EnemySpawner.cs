using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] pathTarget;
    [SerializeField] GameObject EnemyObject;
    public float timeSpan = 0.5f;
    public int totalEnemies = 5;
    float timeCounter;
    int nSpwaned;
    void Start()
    {
        nSpwaned = 0;
        timeCounter = timeSpan;
    }

    // Update is called once per frame
    void Update()
    {
        if (nSpwaned < totalEnemies)
        {
            if (timeCounter < 0)
            {
                nSpwaned++;
                GameObject Enemy = Instantiate(EnemyObject, transform.position, Quaternion.identity);
                Enemy.transform.LookAt(pathTarget[0].position);
                //Enemy.GetComponent<Rigidbody>().AddForce((pathTarget[0].position- transform.position).normalized * +300);
                Enemy.GetComponent<EnemyMovement>().SetPathTarget(pathTarget);
                timeCounter = timeSpan;
            }
            else timeCounter -= Time.deltaTime;
        }
    }
}