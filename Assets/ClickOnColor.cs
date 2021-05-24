using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickOnColor : MonoBehaviour, IPointerDownHandler
{

    Image image;
    private void Start()
    {
        image = GetComponent<Image>();
    }



    public void OnPointerDown(PointerEventData pointerEventData)
    {
        GameManager.instance.setCurrentColor(image.color);
    }
}
