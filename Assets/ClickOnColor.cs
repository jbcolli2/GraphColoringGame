using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickOnColor : MonoBehaviour, IPointerDownHandler
{
    public GameManager gameManager;

    Image image;
    private void Start()
    {
        image = GetComponent<Image>();
    }



    public void OnPointerDown(PointerEventData pointerEventData)
    {
        gameManager.setCurrentColor(image.color);
    }
}
