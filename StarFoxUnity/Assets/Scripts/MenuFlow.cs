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
    GameObject audioStart;

    void Start()
    {
        title = this.gameObject.transform.GetChild(0);
        menu = this.gameObject.transform.GetChild(1);
        audioMenu = GameObject.Find("AudioMenu");
        audioTitle = GameObject.Find("AudioTitle");
        audioStart = GameObject.Find("AudioStart");

        title.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);

        audioTitle.GetComponent<AudioManagerMM>().PlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            audioTitle.GetComponent<AudioManagerMM>().StopSound();
            audioMenu.GetComponent<AudioManagerMM>().PlaySound();
            audioStart.GetComponent<AudioManagerMM>().PlaySound();
            title.gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
            Cursor.visible = true;
        }
    }
}
