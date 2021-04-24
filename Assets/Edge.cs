using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.widthMultiplier = 0.1f;
        line.positionCount = 2;

        line.SetPosition(0, new Vector3(1, 1, 0));
        line.SetPosition(1, new Vector3(1, 2, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
