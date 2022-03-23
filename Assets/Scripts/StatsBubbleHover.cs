using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatsBubbleHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image statsBubble;
    private bool mouse_over = false;

    // Start is called before the first frame update
    void Start()
    {
        statsBubble.enabled = false;
    }

    void Update()
    {
        if (mouse_over)
        {
            statsBubble.enabled = true;
        }

        else
        {
            statsBubble.enabled = false;
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
