using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton: MonoBehaviour
{
       //return to start screen
    public static void GoHome()
    {
        SceneManager.LoadScene(0);
    }

}
