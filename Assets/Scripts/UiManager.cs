using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public HP[] LifePoints;
    public TextMeshProUGUI leafText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLeaves(int leaves)
    {
        leafText.text = IntToSprite(leaves);
    }

    public void UpdateHP(float life)
    {
        if (life >= 2)
        {
            LifePoints[2].Hit();
        }
        else if (life >= 1.0)
        {
            LifePoints[1].Hit();
        }
        else
        {
            LifePoints[0].Hit();
        }
    }

    private string IntToSprite(int number)
    {
        int num = number;
        List<string> spriteTags = new List<string>();
        while(num > 0)
        {
            int digit = num % 10;
            num = num / 10;
            spriteTags.Add("<sprite index=" + digit.ToString() + ">");
        }

        spriteTags.Reverse();


        return string.Join("", spriteTags);
    }
}
