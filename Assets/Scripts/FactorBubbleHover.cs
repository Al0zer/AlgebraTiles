using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FactorBubbleHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image factBubble;
    private bool mouse_over = false;

    // Start is called before the first frame update
    void Start()
    {
        factBubble.enabled = false;
    }

    void Update()
    {
        if (mouse_over)
        {
            factBubble.enabled = true;
        }

        else
        {
            factBubble.enabled = false;
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
