using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [Header("UI")]
    public GameObject LifePointsPanel;
    public GameObject UpgradesPanel;
    public HP[] LifePoints;
    public TextMeshProUGUI leafText;
    public GameObject leavesPanel;

    [Header("Games Container")]
    public GameObject platformerObject;
    public GameObject shmupObject;
    public GameObject upgradeObject;

    [Header("Sign")]
    public GameObject textBox;
    public Text signText;


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
        text = text.Replace("\\n", "\n");
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

    public void UpdateHPPlatformer(float life)
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
        upgradeObject.SetActive(true);
        platformerObject.SetActive(false);
        LifePointsPanel.SetActive(false);
        UpgradesPanel.SetActive(true);
    }

    public void SwitchToShmupUI()
    {
        upgradeObject.SetActive(false);
        shmupObject.SetActive(true);
        LifePoints[3].gameObject.SetActive(true);
        LifePoints[4].gameObject.SetActive(true);

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
