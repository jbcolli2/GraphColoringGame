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
            Vector3 mousePos = Input.mousePosition;
            if (isInGraphArea(mousePos))
            {
                Node node = Instantiate<Node>(nodePrefab);
                Vector3 tempPos = Camera.main.ScreenToWorldPoint(mousePos);
                node.Setup(new Vector2(tempPos.x, tempPos.y));
                nodes.Add(node);
            }
        }
    }



    bool isOnNode(Vector3 mousePos)
    {
        RaycastHit rayHit = new RaycastHit();

        if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out rayHit))
        {
            if (rayHit.collider.gameObject.GetComponent<Node>())
            {
                return true;
            }
        }

        return false;
    }



    bool isInGraphArea(Vector3 mousePos)
    {
        RaycastHit rayHit = new RaycastHit();

        if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out rayHit))
        {
            if(rayHit.collider.gameObject.GetComponent<SpriteMask>())
            {
                return true;
            }
        }

        return false;
    }


    public void SetPlacingNodes(bool placeNodes)
    {
        isPlacingNodes = placeNodes;
    }
}
