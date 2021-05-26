using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField]
    Image gameOverBox;
    [SerializeField]
    TMP_InputField numColorsInput;

    public Color currentColor { get; private set; }
    Player currentPlayer = Player.Alice;
    public Color blankColor = Color.white;
    public int numColors;
    //[System.NonSerialized]
    public List<Color> colors = new List<Color>();
    [SerializeField]
    Image[] colorImages;
    int numNodesColored;

    public static GameManager instance { get; private set; }




 


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        setColors();
        foreach (Color color in colors)
        {
            Debug.Log(color);
        }
        SetNumColors(3);
        currentColor = colors[0];
        numNodesColored = 0;







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
        numColorsInput.onEndEdit.AddListener(delegate { SetNumColors(Int16.Parse(numColorsInput.text)); });

    }



    void SetupGameGUI()
    {
        currentPlayerText.text = currentPlayer.ToString();
        gameOverBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }




    public void setCurrentColor(Color color)
    {
        currentColor = color;
    }




    bool SelectNode(out Node node)
    {
        bool didSelectNode = false;
        node = null;
       
        RaycastHit rayHit = new RaycastHit();

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit))
        {
            node = rayHit.collider.gameObject.GetComponent<Node>();
            if(node)
            {
                didSelectNode = true;
            }
            
        }
        

        

        return didSelectNode;
    }


    public void PlayerMove(Node node)
    {
        node.setColor(currentColor);
        currentPlayer = currentPlayer == Player.Alice ? Player.Bob : Player.Alice;
        currentPlayerText.text = currentPlayer.ToString();
        numNodesColored += 1;

        if(numNodesColored == nodes.Count)
        {
            gameOverBox.GetComponentInChildren<TMP_Text>().text = "Alice Wins!!!";
            gameOverBox.gameObject.SetActive(true);
        }
        if(node.isGameOver())
        {
            gameOverBox.gameObject.SetActive(true);
            gameOverBox.GetComponentInChildren<TMP_Text>().text = "Bob Wins!!!";
        }
         
    }



    public void ResetGame()
    {
        gameOverBox.gameObject.SetActive(false);
        currentColor = colors[0];
        currentPlayer = Player.Alice;
        numNodesColored = 0;
        SetupGameGUI();

        foreach (Node node in nodes)
        {
            node.clearColor();
        }
    }



    public void SetMessageText(string text)
    {
        messageText.SetTemporaryText(text);
    }


    public void SetNumColors(int numColors)
    {
        this.numColors = numColors;

        for(int ii = 0; ii < colorImages.Length; ++ii)
        {
            if(ii < this.numColors)
            {
                colorImages[ii].gameObject.SetActive(true);
            }
            else
            {
                colorImages[ii].gameObject.SetActive(false);
            }
        }

        setColors();

        ResetGame();
    }


    void setColors()
    {
        colors.Clear();
        for (int ii = 0; ii < colorImages.Length; ++ii)
        {
            if (ii >= numColors)
            {
                break;
            }
            Debug.Log(numColors);
            colors.Add(colorImages[ii].color);
        }
    }
}
