using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField]
    Node nodePrefab;
    [SerializeField]
    Edge edgePrefab;

    List<Node> nodes = new List<Node>();
    List<HashSet<int>> adj = new List<HashSet<int>>();
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(edgePrefab);
        CreateNode(new Vector3(1, 1, 0), new HashSet<int> { 3, 2 });
        CreateNode(new Vector3(1, 2, 0), new HashSet<int> { 0 });

        for (int ii = 0; ii < adj.Count; ++ii)
        {
            foreach (int jj in adj[ii])
            {
                Debug.Log("adj "+ii + ":" + jj);
            }
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(nodes[0]);
        //node.setColor();
        if(Input.GetKeyDown("space"))
        {
            nodes[0].setColor();
        }
    }




    void CreateNode(Vector3 nodePos, HashSet<int> adjNodes = null)
    {
        nodes.Add(Instantiate(nodePrefab, nodePos, Quaternion.identity));
        adj.Add(new HashSet<int>());
        if(adjNodes != null)
        {
            AddEdges(adj.Count-1, adjNodes);
        }
        
    }




    /*       Add Edges
     *       
     *  \brief: Modify the adjecency list to add a set of edges to a particular node
     *  
     *  \params: nodeInd - the node to add edges to
     *          adjNodes - the adjecent nodes to nodeInd
     */
    void AddEdges(int nodeInd, HashSet<int> adjNodes)
    {
        if(nodeInd >= adj.Count)
        {
            return;
        }


        // Add new node to adj of other nodes and add adjNode to current node
        foreach (int adjNode in adjNodes)
        {
            if (adjNode < nodeInd)
            {
                adj[adjNode].Add(nodeInd);
                adj[nodeInd].Add(adjNode);
            }
        }

        // Add adj from prev nodes to adj of new node
        for (int prevNode = 0; prevNode < nodeInd; ++prevNode)
        {
            foreach (int jj in adj[prevNode])
            {
                if (jj == nodeInd)
                {
                    adj[nodeInd].Add(prevNode);
                }
            }
        }

    }
}
