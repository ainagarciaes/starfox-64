using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFlow : MonoBehaviour
{
    // Start is called before the first frame update
    Transform menu;
    Transform title;
    GameObject audioMenu;
    GameObject audioTitle;

    void Start()
    {
        title = this.gameObject.transform.GetChild(0);
        menu = this.gameObject.transform.GetChild(1);
        audioMenu = GameObject.Find("AudioMenu");
        audioTitle = GameObject.Find("AudioTitle");
        title.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);

        audioTitle.GetComponent<AudioManager>().PlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            audioTitle.GetComponent<AudioManager>().StopSound();
            audioMenu.GetComponent<AudioManager>().PlaySound();

            title.gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
            Cursor.visible = true;
        }
    }
}
