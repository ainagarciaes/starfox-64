using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaticObject : MonoBehaviour
{
    public LevelManager lm;
    [SerializeField] int damage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        print("COLLIDED"); 
        lm.UpdateHitPoints(damage,1);
    }
}
