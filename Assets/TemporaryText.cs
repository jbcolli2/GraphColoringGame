using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TemporaryText : MonoBehaviour
{

    float timer;
    [SerializeField]
    float delayTime = 1;
    TMP_Text textbox;


    public void SetTemporaryText(string text, float delayTime=0)
    {
        timer = 0;

        if (delayTime != 0)
        {
            timer = this.delayTime - delayTime;
        }
        textbox.text = text;

    }




    // Start is called before the first frame update
    void Start()
    {
        textbox = GetComponent<TMP_Text>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log(timer);
        if(timer > delayTime)
        {
            textbox.text = "";
        }
    }
}
