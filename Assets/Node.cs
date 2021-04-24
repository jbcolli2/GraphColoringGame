using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Create Node");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //spriteRenderer.color = Color.red;
    }

    public void setColor()
    {
        spriteRenderer.color = Color.blue;
    }
}
