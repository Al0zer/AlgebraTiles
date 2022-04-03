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
        SceneManager.LoadScene(3);
    }

    //expand mode
    public void ExpandButton()
    {
        SceneManager.LoadScene(5);
    }


    //factor mode
    public void FactorButton()
    {
        SceneManager.LoadScene(6);
    }

    public void PlayerStatsButton()
    {
        SceneManager.LoadScene(7);
    }
}
