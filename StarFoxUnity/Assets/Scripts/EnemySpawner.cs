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
    bool readyToSpawn;
    void Start()
    {
        readyToSpawn = false;
        nSpwaned = 0;
        timeCounter = timeSpan;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToSpawn)
            if (nSpwaned < totalEnemies)
            {
                if (timeCounter < 0)
                {
                    nSpwaned++;
                    GameObject Enemy = Instantiate(EnemyObject, transform.position, Quaternion.identity);
                    if (Enemy.GetComponent<EnemyMovement>() != null)
                    {
                        Enemy.transform.LookAt(pathTarget[0].position);
                        //Enemy.GetComponent<Rigidbody>().AddForce((pathTarget[0].position- transform.position).normalized * +300);
                        Enemy.GetComponent<EnemyMovement>().SetPathTarget(pathTarget);
                    }
                    else if (Enemy.GetComponent<Enemy2Movement>() != null)
                    {
                        Enemy.GetComponent<Enemy2Movement>().MoveTo = pathTarget[0].position;
                    }
                    else {     
                        //Enemy.transform.LookAt(); 
                    }
                    timeCounter = timeSpan;
                }
                else timeCounter -= Time.deltaTime;
            }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) readyToSpawn = true;
    }
}