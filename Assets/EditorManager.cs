using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    [SerializeField]
    Node nodePrefab;
    [SerializeField]
    Edge edgePrefab;


    public List<Node> nodes = new List<Node>();

    bool isMakeEdges = false;
    Node edgeNode0 = null;
    Node edgeNode1 = null;
    Edge currentEdge;

    void Awake()
    {
        instance = this;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
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

        if(Input.GetMouseButtonDown(1))
        {
            Node node;
            if(isOnNode(Input.mousePosition, out node))
            {
                nodes.Remove(node);
                Destroy(node.gameObject);
            }
        }


        if(Input.GetMouseButtonDown(0) && isMakeEdges)
        {
            Vector3 mousePos = Input.mousePosition;
            Node node;
            
            if(isOnNode(mousePos, out node))
            {
                //selecting first node of edge, color it purple
                if (edgeNode0 == null)
                {
                    edgeNode0 = node;
                    edgeNode0.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                }
                // Can't connect node to itself, start over
                else if(edgeNode0 == node)
                {
                    edgeNode0.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    edgeNode0 = null;
                }
                // Selecting second node of edge, where it isn't same as the first node
                else if(edgeNode0 != node && !edgeNode0.AdjNodesContains(node)
                    && !node.AdjNodesContains(edgeNode0))
                {
                    edgeNode1 = node;

                    edgeNode1.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;

                    Edge edge = Instantiate<Edge>(edgePrefab);
                    edge.Setup(edgeNode0, edgeNode1);
                    edgeNode0.AddToEdgeList(edge);
                    edgeNode1.AddToEdgeList(edge);

                    edgeNode0.AddToAdjList(edgeNode1);
                    edgeNode1.AddToAdjList(edgeNode0);

                    edgeNode0.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    edgeNode1.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    edgeNode0 = null;
                    edgeNode1 = null;
                }
            }
        }

        

        
    }



    bool isOnNode(Vector3 mousePos, out Node outNode)
    {
        RaycastHit rayHit = new RaycastHit();

        if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out rayHit))
        {
            Debug.Log(rayHit.collider.gameObject);
            if (rayHit.collider.gameObject.GetComponent<Node>())
            {
                outNode = rayHit.collider.gameObject.GetComponent<Node>();
                return true;
            }
        }

        outNode = null;
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


    

    public void SetMakeEdges(bool makeEdges)
    {
        isMakeEdges = makeEdges;
    }
}
