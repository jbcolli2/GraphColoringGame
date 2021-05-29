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
    int N = 14;
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
    TMP_InputField numColorsInput;

    public Color currentColor { get; private set; }
    Player currentPlayer = Player.Alice;
    public Color blankColor = Color.white;
    public int numColors;
    //[System.NonSerialized]
    public List<Color> colors = new List<Color>();
    [SerializeField]
    Image[] colorImages;
    Image currentImage;
    int numNodesColored;
    [SerializeField]
    Sprite squareColorSprite;
    [SerializeField]
    Sprite circleColorSprite;

    public static GameManager instance { get; private set; }




 


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        currentImage = colorImages[0];
        numNodesColored = 0;

        setColors();
        foreach (Color color in colors)
        {
            Debug.Log(color);
        }
        SetNumColors(3);
        
        setCurrentColor(colorImages[0]);






        //int[,] adj = new int[N, N];

        //for(int ii = 0; ii<N; ++ii)
        //{
        //    for(int jj = 0; jj < N; ++jj)
        //    {
        //        adj[ii, jj] = 0;
        //    }
        //}

        int[,] adj = CreateN11Adj();


        //adj[0, 1] = 1;
        //adj[1, 2] = 1;
        //adj[1, 6] = 1;
        //adj[1, 7] = 1;
        //adj[2, 3] = 1;
        //adj[2, 8] = 1;
        //adj[2, 9] = 1;
        //adj[3, 4] = 1;
        //adj[3, 10] = 1;
        //adj[3, 11] = 1;
        //adj[4, 5] = 1;
        //adj[4, 12] = 1;
        //adj[4, 13] = 1;

        //for(int ii = 0; ii < N; ++ii)
        //{
        //    for(int jj = ii; jj < N; ++jj)
        //    {
        //        if(adj[ii,jj] == 1)
        //        {
        //            adj[jj, ii] = 1;
        //        }
        //    }
        //}


            

        // Set up the adj matrix
        for(int ii = 0; ii < N; ++ii)
        {
            adjMatrix.Add(new List<int>());
            for(int jj = 0; jj < N; ++jj)
            {
                if(adj[ii,jj] == 1)
                {
                    adjMatrix[ii].Add(jj);
                }
            }
        }

        //adjMatrix.Add(new List<int>() { 1, 2 });
        //adjMatrix.Add(new List<int>() { 0, 2 });
        //adjMatrix.Add(new List<int>() { 0, 1,3 });
        //adjMatrix.Add(new List<int>() { 2 });

        nodes = SetupGraph(adjMatrix);
       


        SetupGameGUI();
        numColorsInput.onEndEdit.AddListener(delegate { SetNumColors(Int16.Parse(numColorsInput.text)); });

    }



    List<Node> SetupGraph(List<List<int>> adjMatrix)
    {
        List<Node> nodes = new List<Node>();

        for (int ii = adjMatrix.Count; ii < N; ++ii)
        {
            adjMatrix.Add(new List<int>());
        }


        // Create nodes
        for (int ii = 0; ii < N; ++ii)
        {
            Node node = Instantiate<Node>(nodePrefab);
            nodes.Add(node);
        }

        for (int ii = 0; ii < N; ++ii)
        {
            List<Node> adjNodes = new List<Node>();
            for (int jj = 0; jj < adjMatrix[ii].Count; ++jj)
            {
                adjNodes.Add(nodes[adjMatrix[ii][jj]]);
            }

            float theta = 2 * Mathf.PI * (ii / (float)(N));
            Vector2 nodePos = new Vector2(drawRadius * Mathf.Cos(theta), drawRadius * Mathf.Sin(theta));

            nodes[ii].Setup(nodePos, adjNodes);
        }


        return nodes;
    }


    void SetupGameGUI()
    {
        currentPlayerText.text = currentPlayer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }




    public void setCurrentColor(Image image)
    {
        currentColor = image.color;
        currentImage.sprite = squareColorSprite;
        currentImage = image;
        currentImage.sprite = circleColorSprite;
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
            SetMessageText("!!!!!!  Alice Wins  !!!!!!!", 4);
        }
        if(node.isGameOver())
        {
            SetMessageText("!!!!!!  Bob Wins  !!!!!!!", 4);
        }
         
    }



    public void ResetGame()
    {
        setCurrentColor(colorImages[0]);
        currentPlayer = Player.Alice;
        numNodesColored = 0;
        SetupGameGUI();

        foreach (Node node in nodes)
        {
            node.clearColor();
        }
    }



    public void SetMessageText(string text, float delayTime=0)
    {
        messageText.SetTemporaryText(text, delayTime);
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
            colors.Add(colorImages[ii].color);
        }
    }







    int[,] CreateZeroAdj(int N)
    {
        int[,] adj = new int[N, N];

        for (int ii = 0; ii < N; ++ii)
        {
            for (int jj = 0; jj < N; ++jj)
            {
                adj[ii, jj] = 0;
            }
        }

        return adj;
    }


    int[,] CreateN11Adj()
    {
        int[,] adj = CreateZeroAdj(N);


        for (int ii = 0; ii < N - 1; ++ii)
        {
            adj[ii, ii + 1] = 1;
        }
        adj[N - 1, 0] = 1;

        return adj;
    }


    int[,] CreateCatAdj()
    {
        N = 14;
        int[,] adj = CreateZeroAdj(N);
        



        adj[0, 1] = 1;
        adj[1, 2] = 1;
        adj[1, 6] = 1;
        adj[1, 7] = 1;
        adj[2, 3] = 1;
        adj[2, 8] = 1;
        adj[2, 9] = 1;
        adj[3, 4] = 1;
        adj[3, 10] = 1;
        adj[3, 11] = 1;
        adj[4, 5] = 1;
        adj[4, 12] = 1;
        adj[4, 13] = 1;

        for (int ii = 0; ii < N; ++ii)
        {
            for (int jj = ii; jj < N; ++jj)
            {
                if (adj[ii, jj] == 1)
                {
                    adj[jj, ii] = 1;
                }
            }
        }


        return adj;
    }
}
