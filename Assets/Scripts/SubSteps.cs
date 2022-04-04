using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SubSteps : MonoBehaviour
{
    private int problemStep;

    public GameObject workspace;
    public TileCreationManager creationManager;
    public TileInteraction interactionManager;

    public Toggle onesButton;
    public Toggle xButton;
    public Toggle cancelOut;

    public TextMeshProUGUI stepText1;
    public TextMeshProUGUI stepText2;
    public GameObject finalAnswer;
    public GameObject endPanel;

    public Button check1;
    public Button check2;
    public Button check3;
    public Button clearButton;
    public GameObject garbageBin;

    private SymbolicMathProblem.SubstituteTypeProblem problem;
    public TextMeshProUGUI problemText;

    private bool clicked;


    // Start is called before the first frame update
    void Start()
    {
        check1.onClick.AddListener(TaskOnClick);
        check2.onClick.AddListener(TaskOnClick);
        check3.onClick.AddListener(TaskOnClick);

        this.ResetBoard();
    }

    // Update is called once per frame
    void Update()
    {
        //step 1: model value of x
        if(problemStep == 0)
        {
            stepText2.enabled = false;
            onesButton.interactable = true;
            xButton.interactable = true;

            if (clicked)
            {
                clicked = false;
                problemStep++;
            }
        }

        //step 2: 
        if(problemStep == 1)
        {
            xButton.interactable = false;
            cancelOut.gameObject.SetActive(true);
            stepText2.enabled = true;
            check1.interactable = false;
            check2.gameObject.SetActive(true);

            if (clicked)
            {
                clicked = false;
                problemStep++;
            }
        }

        //step 3:
        if(problemStep == 2)
        {
            check2.interactable = false;
            finalAnswer.SetActive(true);
            clearButton.interactable = false;
            garbageBin.SetActive(false);
            if (clicked)
            {
                endPanel.SetActive(true);
            }
        }
    }

    public void TaskOnClick()
    {
        clicked = true;
    }

    public void SkipQuestion()
    {
        SceneManager.LoadScene(4);
    }

    public void generateNewProblem()
    {
        problem = SymbolicMathProblemGenerator.generateSubstituteProblem();
        problemText.text = problem.ToString();
    }

    public void ResetBoard()
    {
        problemStep = 0;
        garbageBin.SetActive(true);
        finalAnswer.SetActive(false);
        this.generateNewProblem();
        cancelOut.gameObject.SetActive(false);
        onesButton.gameObject.SetActive(true);
        xButton.gameObject.SetActive(true);
        onesButton.isOn = false;
        xButton.isOn = false;
        cancelOut.isOn = false;
        interactionManager.ResetCancelOut();
        interactionManager.allowSignChange = true;
    }
}
