using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGameUI : MonoBehaviour
{

    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    EditorManager editorManager;
    [SerializeField]
    GameObject gameUI;
    [SerializeField]
    GameObject editorUI;



    public void GameUIActive(int dropdown)
    {
        if(dropdown == 0)
        {
            editorManager.gameObject.SetActive(true);
            editorUI.gameObject.SetActive(true);

            gameManager.gameObject.SetActive(false);
            gameUI.gameObject.SetActive(false);

            EditorManager.instance.nodes = GameManager.instance.nodes;
        }
        else
        {
            editorManager.gameObject.SetActive(false);
            editorUI.gameObject.SetActive(false);

            gameManager.gameObject.SetActive(true);
            gameUI.gameObject.SetActive(true);

            GameManager.instance.nodes = EditorManager.instance.nodes;
        }
    }
}
