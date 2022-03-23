using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExpandBubbleHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image expBubble;
    private bool mouse_over = false;

    // Start is called before the first frame update
    void Start()
    {
        expBubble.enabled = false;
    }

    void Update()
    {
        if (mouse_over)
        {
            expBubble.enabled = true;
        }

        else
        {
            expBubble.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }
}
