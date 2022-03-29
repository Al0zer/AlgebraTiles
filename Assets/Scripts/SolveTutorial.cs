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

    public GameObject square;
    public TileCreationManager creationManager;

    public GameObject finalAnswer;
    public GameObject EndPanel;

    public Toggle onesButton;
    public Toggle xButton;
    public Toggle x2Button;

    public TextMeshProUGUI stepText;

    public Button checkButton;
    public Button clearButton;
    public Button garbageButton;
    public Button cancelOutButton;

    private bool clicked = false;

    void Start()
    {
        checkButton.onClick.AddListener(TaskOnClick);
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
            onesButton.interactable = false;

            if (Input.GetMouseButtonDown(0))
            {
                // ray cast from the camera to the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("WorkSpace"));
                if (hit.collider == square.GetComponent<BoxCollider2D>() && creationManager.XTileOn())
                {
                    infoIndex++;
                }
            }
        }

        //making x tile negative
        //should check if the user clicked on the x tile, then moves onto the next step
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
        else if (infoIndex == 3)
        {
            onesButton.interactable = true;
            if (clicked)
            {
                clicked = false;
                infoIndex++;
            }
        }

        //move blocks left and right to isolate x
        //I wanna temporarily disable the tile's sign switching script, so the user can drag 
        //tiles without accidentally changing their signs
        else if (infoIndex == 4)
        {
            stepText.text = ("Step 2: Solve for x");

            onesButton.interactable = false;
            xButton.interactable = false;
            clearButton.interactable = false;
            garbageButton.interactable = false;

            creationManager.DisableTileCreation();

            //GameObject.FindWithTag("Tile").GetComponent<SwitchTileSign>().enabled = false;

            if (clicked)
            {
                clicked = false;
                infoIndex++;
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
                EndPanel.SetActive(true);
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
}
