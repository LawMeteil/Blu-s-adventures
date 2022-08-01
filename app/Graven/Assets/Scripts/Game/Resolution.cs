using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Switch to 640 x 480 full-screen
        Screen.SetResolution(1080, 1920, true);
    }
}
