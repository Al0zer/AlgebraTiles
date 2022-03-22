using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SolveBubbleHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image solveBubble;
    private bool mouse_over = false;

    // Start is called before the first frame update
    void Start()
    {
        solveBubble.enabled = false;
    }

    void Update()
    {
        if (mouse_over)
        {
            solveBubble.enabled = true;
        }

        else
        {
            solveBubble.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        Debug.Log("mouse over");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }
}
