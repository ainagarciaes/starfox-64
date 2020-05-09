using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public bool destroyable;
    public int givenScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyObjectFunc()
    {
        if (destroyable)
        {
            LevelManager.Instance.UpdateScore(givenScore);
            Destroy(transform.gameObject);
        }
    }
}
