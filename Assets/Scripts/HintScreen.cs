using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintScreen : MonoBehaviour
{
    void OnEnable()
    {
        GameManager.instance.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        GameManager.instance.gameObject.SetActive(true);
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void BackToGame()
    {
        gameObject.SetActive(false);
    }
}
