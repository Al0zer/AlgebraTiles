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
        solveSubText.text = "Completed questions: "+ PlayerPrefs.GetInt("SolveSub", 0);
        substituteSubText.text = "Completed questions: "+ PlayerPrefs.GetInt("SubstituteSub", 0);
        expandSubText.text = "Completed questions: "+ PlayerPrefs.GetInt("ExpandSub", 0);
        factorSubText.text = "Completed questions: "+ PlayerPrefs.GetInt("FactorSub", 0);
    }
}
