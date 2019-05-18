using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public Image image;
    public Sprite half;
    public Sprite empty;

    private int state = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        state--;

        if(state == 1)
        {
            image.sprite = half;
        }
        
        if(state == 0)
        {
            image.sprite = empty;
        }
    }
}
