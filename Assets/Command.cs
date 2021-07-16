using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Command 
{
    public abstract void execute();

    public abstract void undo();
}



class PlaceNode : Command
{
    Node node;
    List<Node> nodeList;
    public PlaceNode(Node node, List<Node> nodeList)
    {
        this.node = node;
        this.nodeList = nodeList;
    }


    public override void execute()
    {

    }

    public override void undo()
    {
        nodeList.Remove(node);
        Object.Destroy(node.gameObject);
    }
}





class PlaceEdge : Command
{
    Edge edge;
    public PlaceEdge(Edge edge)
    {
        this.edge = edge;
    }


    public override void execute()
    {

    }

    public override void undo()
    {
        Object.Destroy(edge.gameObject);
    }
}
