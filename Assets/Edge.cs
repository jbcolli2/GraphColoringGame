using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    Node node0, node1;
    LineRenderer line;
    // Start is called before the first frame update
    void Awake()
    {
        //gameObject.active = false;
        line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.widthMultiplier = 0.1f;
        line.positionCount = 2;

        line.startColor = Color.black;
        line.endColor = Color.black;
    }


    public void Setup(Node node0, Node node1)
    {
        this.node0 = node0;
        this.node1 = node1;
    }


    //public void setVerts(int vert1, int vert2)
    //{
    //    gameObject.active = true;

    //    verts = new HashSet<int>();
    //    verts.Add(vert1);
    //    verts.Add(vert2);

    //    line.SetPosition(0, NodeManager.nodes[vert1].transform.position);
    //    line.SetPosition(1, NodeManager.nodes[vert2].transform.position);
    //}




    // Update is called once per frame
    void Update()
    {
        if(node0 != null)
        {
            line.SetPosition(0, node0.transform.position);
        }
        if(node1 != null)
        {
            line.SetPosition(1, node1.transform.position);
        }
        
    }

    public void SetNode0(Node node)
    {
        node0 = node;
    }

    public void SetNode1(Node node)
    {
        node1 = node;
    }

    public Node GetNode0()
    {
        return node0;
    }
}
