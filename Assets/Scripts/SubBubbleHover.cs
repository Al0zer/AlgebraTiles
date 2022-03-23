using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SubBubbleHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image subBubble;
    private bool mouse_over = false;

    // Start is called before the first frame update
    void Start()
    {
        subBubble.enabled = false;
    }

    void Update()
    {
        if (mouse_over)
        {
            subBubble.enabled = true;
        }

        else
        {
            subBubble.enabled = false;
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
