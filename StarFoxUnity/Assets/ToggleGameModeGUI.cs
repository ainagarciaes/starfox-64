using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameModeGUI : MonoBehaviour
{
    [SerializeField] GameObject normal;
    [SerializeField] GameObject godmode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleGodMode(bool godactive)
    {
        normal.SetActive(!godactive);
        godmode.SetActive(godactive);
    }
}
