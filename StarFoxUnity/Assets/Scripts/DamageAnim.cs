using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageAnim : MonoBehaviour
{
    float timeleft = 0;
    int count = 0;
    float velocity = 200;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (timeleft > 0)
        {
            timeleft -= Time.deltaTime;
            count++;
            Color c = image.color;
            c.a = Mathf.Sin(((count%velocity) / velocity)*2*Mathf.PI)/2 + 0.5f;
            image.color = c;
        }
        else
        {
            Color c = image.color;
            c.a = 0;
            image.color = c;
            count = 0;
        }
    }

    public void StartDamageAnimation()
    {
        timeleft = 2;
    }
}
