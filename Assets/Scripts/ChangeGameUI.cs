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

    enum UIState : int
    {
        EDITOR = 1,
        GAME = 0
    };

    public void GameUIActive(int dropdown)
    {
        if((UIState)dropdown == UIState.EDITOR)
        {
            editorManager.gameObject.SetActive(true);
            editorUI.gameObject.SetActive(true);

            gameManager.gameObject.SetActive(false);
            gameUI.gameObject.SetActive(false);

            EditorManager.instance.nodes = GameManager.instance.nodes;
        }
        else if((UIState)dropdown == UIState.GAME)
        {
            editorManager.gameObject.SetActive(false);
            editorUI.gameObject.SetActive(false);

            gameManager.gameObject.SetActive(true);
            gameUI.gameObject.SetActive(true);

            GameManager.instance.nodes = EditorManager.instance.nodes;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }




}
