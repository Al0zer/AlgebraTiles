using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    //if user presses solve button, take them to the solve mode
    public void SolveButton()
    {
        SceneManager.LoadScene(1);
    }

    //substitution mode
    public void SubButton()
    {
        SceneManager.LoadScene(2);
    }

    //expand mode
    public void ExpandButton()
    {
        SceneManager.LoadScene(3);
    }


    //factor mode
    public void FactorButton()
    {
        SceneManager.LoadScene(4);
    }
}
