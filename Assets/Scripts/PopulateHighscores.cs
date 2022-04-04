using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateHighscores : MonoBehaviour
{

    public Text solveSubText;
    public Text substituteSubText;

    public Text expandSubText;

    public Text factorSubText;

    // Start is called before the first frame update
    void Start()
    {
        solveSubText.text = "Completed questions: "+ PlayerPrefs.GetInt("SolveCompleted", 0);
        substituteSubText.text = "Completed questions: "+ PlayerPrefs.GetInt("SubstituteCompleted", 0);
        expandSubText.text = "Completed questions: "+ PlayerPrefs.GetInt("ExpandCompleted", 0);
        factorSubText.text = "Completed questions: "+ PlayerPrefs.GetInt("FactorCompleted", 0);
    }
}
