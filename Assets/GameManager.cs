﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public enum Player
{
    Alice,
    Bob
}


public class GameManager : MonoBehaviour
{
    const int N = 4;
    List<List<int>> adjMatrix = new List<List<int>>();
    List<Node> nodes = new List<Node>();
    float drawRadius = 3.0f;


    [SerializeField]
    Node nodePrefab;
    [SerializeField]
    TemporaryText messageText;
    [SerializeField]
    TMP_Text currentPlayerText;

    static Color currentColor = Color.red;
    Player currentPlayer = Player.Alice;



    // Start is called before the first frame update
    void Awake()
    {
        // Set up the adj matrix
        adjMatrix.Add(new List<int>() { 1, 2 });
        adjMatrix.Add(new List<int>() { 0, 2 });
        adjMatrix.Add(new List<int>() { 0, 1,3 });
        adjMatrix.Add(new List<int>() { 2 });
        for(int ii = adjMatrix.Count; ii < N; ++ii)
        {
            adjMatrix.Add(new List<int>());
        }


        // Create nodes
        for(int ii = 0; ii < N; ++ii)
        {
            Node node = Instantiate<Node>(nodePrefab);
            nodes.Add(node);
        }

        for(int ii = 0; ii < N; ++ii)
        {
            List<Node> adjNodes = new List<Node>();
            for (int jj = 0; jj < adjMatrix[ii].Count; ++jj)
            {
                adjNodes.Add(nodes[adjMatrix[ii][jj]]);
            }

            float theta = 2 * Mathf.PI * (ii / (float)(N));
            Vector2 nodePos = new Vector2(drawRadius*Mathf.Cos(theta), drawRadius*Mathf.Sin(theta));

            nodes[ii].Setup(nodePos, adjNodes);
        }


        SetupGameGUI();

    }



    void SetupGameGUI()
    {
        currentPlayerText.text = currentPlayer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AttemptPlayerMove();

        }
    }




    public void setCurrentColor(Color color)
    {
        currentColor = color;
    }




    bool AttemptPlayerMove()
    {
        bool didColor = false;
       
        RaycastHit rayHit = new RaycastHit();

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit))
        {
            Node node = rayHit.collider.gameObject.GetComponent<Node>();
            if (node.isProperColoring(currentColor))
            {
                ExePlayerMove(node);
                didColor = true;
            }
            else
            {
                messageText.SetTemporaryText("That is not a proper coloring");
            }
        }

        

        return didColor;
    }


    void ExePlayerMove(Node node)
    {
        node.setColor(currentColor);
        currentPlayer = currentPlayer == Player.Alice ? Player.Bob : Player.Alice;
        currentPlayerText.text = currentPlayer.ToString();
    }
}