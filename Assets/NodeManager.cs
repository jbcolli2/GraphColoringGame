using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField]
    Node nodePrefab;

    List<Node> nodes = new List<Node>();
    List<List<int>> adj = new List<List<int>>();
    // Start is called before the first frame update
    void Start()
    {
        CreateNode(new Vector3(1, 1, 0));
        CreateNode(new Vector3(1, 2, 0), new List<int> { 0 });

        adj[0].Add(1);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(nodes[0]);
        //node.setColor();
        if(Input.GetKeyDown("space"))
        {
            nodes[0].setColor();
            Debug.Log(adj[0][0]);
            Debug.Log(adj[1][0]);
        }
    }




    void CreateNode(Vector3 nodePos, List<int> adjNodes = null)
    {
        nodes.Add(Instantiate(nodePrefab, nodePos, Quaternion.identity));
        if(adjNodes == null)
        {
            adj.Add(new List<int>());
        }
        else
        {
            adj.Add(adjNodes);
        }
        
    }
}
