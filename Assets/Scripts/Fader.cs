using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public void FadeOver()
    {
        LevelManager.instance.ChangeScene();
    }
}
