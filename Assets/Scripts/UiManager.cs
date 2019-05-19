using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager _instance;

    [Header("UI")]
    public GameObject LifePointsPanel;
    public GameObject UpgradesPanel;
    public GameObject hpPrefab;
    public TextMeshProUGUI leafText;
    public GameObject leavesPanel;
    public GameObject gameOverPanel;
    public GameObject gameOverPanel2;
    private List<HP> hp;

    [Header("Games Container")]
    public GameObject platformerObject;
    public GameObject shmupObject;
    public GameObject upgradeObject;

    [Header("Sign")]
    public GameObject textBox;
    public Text signText;


    void Awake()
    {
        hp = new List<HP>();

        _instance = this;
    }
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

    public void SetHP(float life)
    {
        Debug.Log("SetHP : " + life);

        for(int i = 0; i < hp.Count; i++)
        {
            GameObject o = hp[i].gameObject;
            hp.RemoveAt(i);
            Destroy(o);
        }

        float nbCoeur = Mathf.Ceil(life / 2);

        for(float f = 0; f < nbCoeur; f++)
        {
            GameObject o = Instantiate(hpPrefab, LifePointsPanel.transform);
            hp.Add(o.GetComponent<HP>());
        }
    }

    public void UpdateHP(float life)
    {
        hp[(int)life / 2].Hit();
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
        UpgradesPanel.SetActive(false);
        upgradeObject.SetActive(false);
        leavesPanel.SetActive(false);
        shmupObject.SetActive(true);
        LifePointsPanel.SetActive(true);
    }

    public void GameOver(bool trueEnd)
    {
        if (trueEnd)
        {
            gameOverPanel.SetActive(true);
            LifePointsPanel.SetActive(false);
            leavesPanel.SetActive(false);
        }
        else
        {
            gameOverPanel2.SetActive(true);
            LifePointsPanel.SetActive(false);
            leavesPanel.SetActive(false);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main");
    }

    private string IntToSprite(int number)
    {
        int num = number;
        List<string> spriteTags = new List<string>();

        if(num == 0)
        {
            return "<sprite index=0>";
        }

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
