using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public float delay = 1;
    
    public void QuitWithDelay()
    {
        Invoke(nameof(QuitApp), delay);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
