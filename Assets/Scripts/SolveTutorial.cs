using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SolveTutorial : MonoBehaviour
{
    public Image[] info;
    private int infoIndex;

    public GameObject square;
    public GameObject xTileManager;
    public GameObject tile;

    public Toggle onesButton;

    public TextMeshProUGUI stepText;

    public Button checkButton;
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
        if (infoIndex == 1)
        {
            onesButton.interactable = false;

            if (Input.GetMouseButtonDown(0))
            {
                // ray cast from the camera to the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

                if (hit.collider == square.GetComponent<BoxCollider2D>() && xTileManager.activeSelf)
                {
                    infoIndex++;
                }
            }
        }

        //making x tile negative
        //should check if the user clicked on the x tile, then moves onto the next step
        //but it aint working lol
        if (infoIndex == 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

                //if mouse clicks on x tile
                if (hit.collider == tile.GetComponent<BoxCollider2D>())
                {
                    Debug.Log("nice");
                    infoIndex++;
                }
            }
        }
    }

    void TaskOnClick()
    {
        clicked = true;
    }
}
