using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SolveTutorial : MonoBehaviour
{
    public Image[] info;
    private int infoIndex;

    public GameObject workspace;
    public TileCreationManager creationManager;

    public GameObject finalAnswer;
    public GameObject EndPanel;
    public GameObject incorrectPanel;

    public Toggle onesButton;
    public Toggle xButton;
    public Toggle x2Button;

    public TextMeshProUGUI stepText;

    public Button checkButton;
    public Button clearButton;
    public Button cancelOutButton;
    public GameObject garbageBin;

    public TMP_InputField input;
    private string inputAnswer = "";

    private bool clicked = false;

    SolveProblemEvaluator evaluator; 

    void Start()
    {
        checkButton.onClick.AddListener(TaskOnClick);
        evaluator = FindObjectOfType<SolveProblemEvaluator>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < info.Length; i++)
        {
            if (i == infoIndex)
            {
                info[i].enabled = true;
            }
            else
            {
                info[i].enabled = false;
            }
        }

        //pressing check button
        if (infoIndex == 0)
        {
            onesButton.interactable = false;
            xButton.interactable = false;
            stepText.text = ("Step 1: Build your model");
            if (clicked)
            {
                clicked = false;
                infoIndex++;
            }
        }

        //place x tile into workspace
        else if (infoIndex == 1)
        {
            xButton.interactable = true;
            if (Input.GetMouseButtonDown(0))
            {
                // ray cast from the camera to the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("WorkSpace"));
                if (hit.collider == workspace.GetComponent<BoxCollider2D>() && creationManager.XTileOn())
                {
                    infoIndex++;
                }
            }
        }

        //making x tile negative
        //but it aint working lol
        else if (infoIndex == 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
               Debug.Log("mouse pressed");
               Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
               RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("Tile"));

               //if mouse clicks on x tile
               // check the parent of the collider is a Tile
               if (hit.collider.tag=="PositiveX")
               {
                   infoIndex++;
               }
            }
        }

        //building the rest of the model
        //checks that the model is equal to -x + 3 = 1 
        else if (infoIndex == 3)
        {
            onesButton.interactable = true;

            if (clicked)
            {
                if (evaluator.problem.ToString() == "-x + 3 = 1")
                {
                    clicked = false;
                    infoIndex++;
                }

                else
                {
                    clicked = false;
                    incorrectPanel.SetActive(true);
                    StartCoroutine(RemoveIncorrectPanel());
                }
            }
        }

        //move blocks left and right to isolate x
        //check that model equals 3 - 1 = x
        else if (infoIndex == 4)
        {
            stepText.text = ("Step 2: Solve for x");

            onesButton.interactable = false;
            xButton.interactable = false;
            clearButton.interactable = false;
            garbageBin.SetActive(false);

            creationManager.DisableTileCreation();

            if (clicked)
            {
                if(evaluator.problem.ToString() == "2 = x")
                {
                    clicked = false;
                    infoIndex++;
                }

                else
                {
                    clicked = false;
                    incorrectPanel.SetActive(true);
                    StartCoroutine(RemoveIncorrectPanel());
                }
            }
        }

        //cancel out tiles
        else if (infoIndex == 5)
        {
            onesButton.gameObject.SetActive(false);
            xButton.gameObject.SetActive(false);
            x2Button.gameObject.SetActive(false);

            cancelOutButton.gameObject.SetActive(true);

            if (clicked)
            {
                clicked = false;
                infoIndex++;
            }
        }

        //enter the value
        else if (infoIndex == 6)
        {
            stepText.text = ("Step 3: Answer");
            finalAnswer.SetActive(true);

            if (clicked)
            {
                clicked = false;
                inputAnswer = input.text;
                if(inputAnswer == "2")
                {
                    EndPanel.SetActive(true);
                }

                else
                {
                    clicked = false;
                    incorrectPanel.SetActive(true);
                    StartCoroutine(RemoveIncorrectPanel());
                }
            }
        }
    }

    void TaskOnClick()
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
        yield return new WaitForSeconds(1);
        incorrectPanel.SetActive(false);
    }
}
