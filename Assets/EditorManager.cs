using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    [SerializeField]
    Node nodePrefab;


    List<Node> nodes = new List<Node>();

    bool isPlacingNodes = false;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && isPlacingNodes)
        {
                Node node = Instantiate<Node>(nodePrefab);
                Vector3 tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                node.Setup(new Vector2(tempPos.x, tempPos.y));
                nodes.Add(node);
        }
    }




    public void SetPlacingNodes(bool placeNodes)
    {
        isPlacingNodes = placeNodes;
    }
}
