using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    [SerializeField]
    Edge edgePrefab;

    SpriteRenderer spriteRenderer;


    List<Node> adjNodes = new List<Node>();
    List<Edge> edges = new List<Edge>();


    Color currentColor = Color.white;


    static bool isNodeMoving = false;
    static Node movingNode = null;


    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;
    }



    public void Setup(Vector2 pos, List<Node> adjNodes)
    {
        // Place vert
        this.adjNodes = adjNodes;
        setPosition(pos);
        setColor(Color.white);

        // Create edges
        for(int ii = 0; ii < adjNodes.Count; ++ii)
        {
            Instantiate<Edge>(edgePrefab).Setup(this, adjNodes[ii]);
        }
    }



    public bool isProperColoring(Color newColor)
    {
        bool isProperColor = true;
        foreach(Node adjNode in adjNodes)
        {
            // If an adjecent node has same color as potential coloring
            if(newColor == adjNode.currentColor)
            {
                return false;
            }
        }


        return true;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit = new RaycastHit();

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit))
            {
                if(rayHit.collider.gameObject.GetComponent<Node>())
                {
                    isNodeMoving = true;
                    movingNode = rayHit.collider.gameObject.GetComponent<Node>();
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            isNodeMoving = false;
            movingNode = null;
        }


        if(isNodeMoving && movingNode == this)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(newPos.x, newPos.y);
        }
    }

    public void setColor(Color color)
    {
        spriteRenderer.color = color;
        currentColor = color;
    }

    public void setPosition(Vector2 nodePos)
    {
        transform.position = new Vector3(nodePos.x, nodePos.y);
    }
}
