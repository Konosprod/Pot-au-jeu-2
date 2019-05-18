using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Cams")]
    public GameObject camPlatformer;
    public GameObject camShmup;
    public GameObject camUpgrade;

    [Header("UI")]
    public GameObject LifePointsPanel;
    public GameObject UpgradesPanel;

    [Header("Sign")]
    public GameObject textBox;
    public Text signText;

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

    public void ShowText(string text)
    {
        signText.text = text;
        textBox.gameObject.SetActive(true);
    }

    public void HideText()
    {
        textBox.gameObject.SetActive(false);
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

    public void SwitchToUpgradeUI()
    {
        camPlatformer.SetActive(false);
        camUpgrade.SetActive(true);
        LifePointsPanel.SetActive(false);
        UpgradesPanel.SetActive(true);
    }

    public void SwitchToShmupUI()
    {
        camUpgrade.SetActive(false);
        camShmup.SetActive(true);
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
