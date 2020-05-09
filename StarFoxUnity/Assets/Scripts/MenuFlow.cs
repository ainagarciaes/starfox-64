using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFlow : MonoBehaviour
{
    // Start is called before the first frame update
    Transform menu;
    Transform title; 

    void Start()
    {
        title = this.gameObject.transform.GetChild(0);
        menu = this.gameObject.transform.GetChild(1);

        title.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            title.gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
        }
    }
}
