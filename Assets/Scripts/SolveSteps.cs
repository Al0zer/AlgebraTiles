using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SolveSteps : MonoBehaviour
{
    private int problemStep;

    public GameObject workspace;
    public TileCreationManager creationManager;

    public TileInteraction interactionManager;

    public GameObject finalAnswer;
    public GameObject EndPanel;
    public GameObject incorrectPanel;

    public Toggle onesButton;
    public Toggle xButton;

    public TextMeshProUGUI stepText;

    public Button checkButton;
    public Button clearButton;
    public Toggle cancelOutButton;
    public GameObject garbageBin;

    public TMP_InputField input;

    public TMP_Text errorMessage;
    private string inputAnswer = "";

    private bool clicked = false;

    SolveProblemEvaluator evaluator; 

    private SymbolicMathProblem.SolveTypeProblem problem;

    public TMP_Text problemText;

    void Start()
    {
        checkButton.onClick.AddListener(TaskOnClick);
        evaluator = FindObjectOfType<SolveProblemEvaluator>();
        this.resetBoard();
    }

    // Update is called once per frame
    void Update()
    {

        //pressing check button
        if (problemStep == 0)
        {
            onesButton.interactable = true;
            xButton.interactable = true;
            stepText.text = ("Step 1: Build your model");
            if (clicked)
            {
                // check if correct model built
                SymbolicMathProblem.SolveTypeProblem currentWorkspace =  evaluator.EvaluateWorkSpace();

                Debug.Log("Current Workspace: " + currentWorkspace.ToString() + " Problem: " + problem.ToString());
                if(currentWorkspace.Equivalent(problem)){
                    problemStep++;
                }else{ 
                    errorMessage.text = "That's not right, try again!";
                    incorrectPanel.SetActive(true);
                    StartCoroutine(RemoveIncorrectPanel());
                }
            }
        }

        //move blocks left and right to isolate x
        //check that model equals 3 - 1 = x
        else if (problemStep == 1)
        {
            stepText.text = ("Step 2: Solve for x");

            onesButton.gameObject.SetActive(false);
            xButton.gameObject.SetActive(false);
            cancelOutButton.gameObject.SetActive(true);
            garbageBin.SetActive(false);

            creationManager.DisableTileCreation();

            if (clicked)
            {
                SymbolicMathProblem.SolveTypeProblem currentWorkspace = evaluator.EvaluateWorkSpace();
                if (currentWorkspace.leftSide[SymbolicEquation.MathSymbol.X] == 0 && currentWorkspace.rightSide[SymbolicEquation.MathSymbol.ONE] == 0)
                {
                    // no X on one side and no ones on other side
                    problemStep++;
                }else if(currentWorkspace.leftSide[SymbolicEquation.MathSymbol.ONE] == 0 && currentWorkspace.rightSide[SymbolicEquation.MathSymbol.X] == 0){
                    // no ones on one side and no X on other side
                    problemStep++;
                }
                else
                {
                    if(currentWorkspace.leftSide[SymbolicEquation.MathSymbol.X] != 0 && currentWorkspace.rightSide[SymbolicEquation.MathSymbol.X] != 0){
                        errorMessage.text = "You have X on both the left and right side. Move all of them to one side!";
                        incorrectPanel.SetActive(true);
                        StartCoroutine(RemoveIncorrectPanel());
                    }else if(currentWorkspace.leftSide[SymbolicEquation.MathSymbol.ONE] != 0 && currentWorkspace.rightSide[SymbolicEquation.MathSymbol.ONE] != 0){
                        errorMessage.text = "You have ones on both the left and right side. Move all of them to one side!";
                        incorrectPanel.SetActive(true);
                        StartCoroutine(RemoveIncorrectPanel());
                    }
                }
            }
        }

        //enter the value
        else if (problemStep == 2)
        {
            stepText.text = ("Step 3: Answer");
            finalAnswer.SetActive(true);
            if (clicked)
            {
                clicked = false;
                inputAnswer = input.text;
                int correctAnswer = SymbolicMathProblemSolver.DoSolve(problem);
                int userAnswer;
                if(int.TryParse(inputAnswer, out userAnswer) && userAnswer == correctAnswer)
                {
                    // increment score
                    PlayerPrefs.SetInt("SolveCompleted", PlayerPrefs.GetInt("SolveCompleted", 0) + 1);
                    EndPanel.SetActive(true);
                }

                else
                {
                    errorMessage.text = "That's not right, try again!";
                    clicked = false;
                    incorrectPanel.SetActive(true);
                    StartCoroutine(RemoveIncorrectPanel());
                    Debug.Log("Received input: " + inputAnswer + " Correct answer: " + correctAnswer);
                }
            }
        }
        clicked = false;
    }

    public void TaskOnClick()
    {
        clicked = true;
    }

    //if user presses "skip tutorial", or "first question" after tutorial
    public void GoToSolve()
    {
        SceneManager.LoadScene(2);
    }

    IEnumerator RemoveIncorrectPanel()
    {
        yield return new WaitForSeconds(3);
        incorrectPanel.SetActive(false);
    }

    public void generateNewProblem()
    {
        problem = SymbolicMathProblemGenerator.generateSolveProblem();
        problemText.text = problem.ToString();
    }

    public void resetBoard(){
        EndPanel.SetActive(false);
        incorrectPanel.SetActive(false);
        creationManager.ClearButton();
        input.text = "";
        problemStep = 0;
        garbageBin.SetActive(true);
        finalAnswer.SetActive(false);
        this.generateNewProblem();
        cancelOutButton.gameObject.SetActive(false);
        onesButton.gameObject.SetActive(true);
        xButton.gameObject.SetActive(true);
        onesButton.isOn = false;
        xButton.isOn = false;
        cancelOutButton.isOn = false;
        interactionManager.ResetCancelOut();

    }
}
