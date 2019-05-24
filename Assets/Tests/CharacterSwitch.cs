using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    Character[] characters;
    int         index = 0;
    CameraCtrl ctrl;

    // Start is called before the first frame update
    void Start()
    {
        characters = FindObjectsOfType<Character>();
        ctrl = FindObjectOfType<CameraCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            index = (index + 1) % characters.Length;

            if (ctrl)
            {
                ctrl.target = characters[index].transform;
            }
        }
    }
}
