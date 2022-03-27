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

                // modify this to allow multiple squares if necessary, or to allow changing which square is being checked
                if (hit.collider == square.GetComponent<BoxCollider2D>() && xTileManager.activeSelf)
                {
                    infoIndex++;
                }
            }
        }

        //making x tile negative
        if (infoIndex == 2)
        {
            if (clicked)
            {
                Debug.Log("lol");
            }
        }
    }

    void TaskOnClick()
    {
        clicked = true;
    }
}
