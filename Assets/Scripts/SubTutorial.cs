using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SubTutorial : MonoBehaviour
{
    public GameObject[] info;
    private int infoIndex;

    public GameObject workspace;
    public TileCreationManager creationManager;

    public GameObject finalAnswer;
    public GameObject EndPanel;
    public GameObject incorrectPanel;

    public Button checkButton1;
    public Button checkButton2;
    public Button checkButton3;

    public Button cancelOutButton;
    public Button clearButton;
    public GameObject garbageBin;

    public Toggle onesButton;
    public Toggle xButton;
    public Toggle x2Button;

    public TextMeshProUGUI step2;
    public TextMeshProUGUI step3;

    private bool clicked = false;

    public TMP_InputField input;
    private string inputAnswer = "";

    // Start is called before the first frame update
    void Start()
    {
        checkButton1.onClick.AddListener(TaskOnClick);
        checkButton2.onClick.AddListener(TaskOnClick);
        checkButton3.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < info.Length; i++)
        {
            if (i == infoIndex)
            {
                info[i].SetActive(true);
            }
            else
            {
                info[i].SetActive(false);
            }
        }

        //pressing check button
        if(infoIndex == 0)
        {
            step2.enabled = false;
            step3.enabled = false;
            onesButton.interactable = false;
            xButton.interactable = false;

            if (clicked)
            {
                clicked = false;
                infoIndex++;
            }
        }

        //step 1: build your model
        //placing x tile on workspace
        if(infoIndex == 1)
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

        //placing ones tile on workspace
        if (infoIndex == 2)
        {
            xButton.interactable = false;
            onesButton.interactable = true;

            if (Input.GetMouseButtonDown(0))
            {
                // ray cast from the camera to the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("WorkSpace"));
                if (hit.collider == workspace.GetComponent<BoxCollider2D>() && creationManager.OnesTileOn())
                {
                    infoIndex++;
                }
            }
        }

        //making ones tile negative
        //need to actually check it's negative
        if (infoIndex == 3)
        {
            if (clicked)
            {
                clicked = false;
                infoIndex++;
            }
        }

        //step 2: model the expression
        //placing blocks to model the equation, replacing x with ones values
        if (infoIndex == 4)
        {
            step2.enabled = true;
            checkButton1.interactable = false;

            if (clicked)
            {
                clicked = false;
                infoIndex++;
            }
        }

        //using the cancel out button
        if(infoIndex == 5)
        {
            creationManager.DisableTileCreation();

            onesButton.gameObject.SetActive(false);
            xButton.gameObject.SetActive(false);
            x2Button.gameObject.SetActive(false);

            cancelOutButton.gameObject.SetActive(true);

            clearButton.interactable = false;
            garbageBin.SetActive(false);

            if (clicked)
            {
                clicked = false;
                infoIndex++;
            }
        }

        //step 3: answer
        if(infoIndex == 6)
        {
            step3.enabled = true;
            xButton.interactable = false;
            onesButton.interactable = false;
            checkButton1.interactable = false;
            checkButton2.interactable = false;

            finalAnswer.SetActive(true);

            if (clicked)
            {
                clicked = false;
                inputAnswer = input.text;
                if (inputAnswer == "3")
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
    public void GoToSub()
    {
        SceneManager.LoadScene(4);
    }

    IEnumerator RemoveIncorrectPanel()
    {
        yield return new WaitForSeconds(1);
        incorrectPanel.SetActive(false);
    }
}
