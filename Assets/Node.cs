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


    Color currentColor;
    bool isColored;


    Vector3 firstClickOffset;


    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;
    }



    public void Setup(Vector2 pos, List<Node> adjNodes = null)
    {

        if(adjNodes == null)
        {
            this.adjNodes = new List<Node>();
        }
        else
        {
            this.adjNodes = adjNodes;
        }

        // Place vert
        setPosition(pos);
        //clearColor();

        // Create edges
        for(int ii = 0; ii < this.adjNodes.Count; ++ii)
        {
            Instantiate<Edge>(edgePrefab).Setup(this, this.adjNodes[ii]);
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

        
    }

    private void OnMouseDown()
    {
        bool illegalMove = false;
        if(!isProperColoring(GameManager.instance.currentColor))
        {
            illegalMove = true;
            GameManager.instance.SetMessageText("That is not a proper coloring");
        }
        if(isColored)
        {
            illegalMove = true;
            GameManager.instance.SetMessageText("That node is already colored");
        }
        if(!illegalMove)
        {
            GameManager.instance.PlayerMove(this);
        }



        //Store offset for where first clicked on node
        firstClickOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        firstClickOffset.z = 0;

    }

    private void OnMouseDrag()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPos.x, newPos.y) + firstClickOffset;
    }

    private void OnMouseUp()
    {
        firstClickOffset = new Vector3(0,0,0);
    }






    // Game Logic Methods
    public bool isNodeColorable()
    {

        if(adjNodes.Count < GameManager.instance.numColors)
        {
            return true;
        }

        List<Color> availableColors = new List<Color>(GameManager.instance.colors.ToArray());

        
        foreach(Node node in adjNodes)
        {
            if(availableColors.Contains(node.currentColor))
            {
                availableColors.Remove(node.currentColor);
            }
        }


        if (availableColors.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }



    public bool isGameOver()
    {
        foreach(Node node in adjNodes)
        {
            if(!node.isNodeColorable())
            {
                return true;
            }
        }

        return false;
    }














    public void setColor(Color color)
    {
        spriteRenderer.color = color;
        currentColor = color;
        isColored = true;
    }

    public void clearColor()
    {
        spriteRenderer.color = GameManager.instance.blankColor;
        currentColor = GameManager.instance.blankColor;
        isColored = false;
    }

    public void setPosition(Vector2 nodePos)
    {
        transform.position = new Vector3(nodePos.x, nodePos.y);
    }
}
