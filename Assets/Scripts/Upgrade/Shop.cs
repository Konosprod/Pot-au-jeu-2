using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public UiManager ui;
    public PlayerPlateformerController plateformPlayer;
    public PlayerController shmupPlayer;

    [Header("Buttons")]
    public GameObject buttonHealth;
    public GameObject buttonDamages;
    public GameObject buttonFireRate;
    public GameObject buttonWeapon;

    private int[] prices = {2, 4, 6};

    private int indexHealth = 0;
    private int indexFireRate = 0;
    private int indexDamages = 0;
    private int indexWeapon = 0;

    private int leaves;

    // Start is called before the first frame update
    void Start()
    {
        leaves = plateformPlayer.leaves;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyFireRate()
    {
        leaves -= prices[indexFireRate];

        ui.UpdateLeaves(leaves);

        if (indexFireRate < 2)
        {
            indexFireRate++;
            buttonFireRate.GetComponentInChildren<Text>().text = "Fréquence de Tirs (" + prices[indexFireRate].ToString() + ")";
        }
        else
        {
            buttonFireRate.GetComponentInChildren<Text>().text = "Vendu !";
            buttonFireRate.GetComponent<Button>().interactable = false;
        }

        checkLeaves();
    }

    public void BuyHealth()
    {
        leaves -= prices[indexHealth];

        ui.UpdateLeaves(leaves);

        if (indexHealth < 2)
        {
            indexHealth++;
            buttonHealth.GetComponentInChildren<Text>().text = "Vie (" + prices[indexHealth].ToString() + ")";
        }
        else
        {
            buttonHealth.GetComponentInChildren<Text>().text = "Vendu !";
            buttonHealth.GetComponent<Button>().interactable = false;
        }

        checkLeaves();
    }

    public void BuyDamages()
    {
        leaves -= prices[indexDamages];

        ui.UpdateLeaves(leaves);

        if (indexDamages < 2)
        {
            indexDamages++;
            buttonDamages.GetComponentInChildren<Text>().text = "Dégâts (" + prices[indexDamages].ToString() + ")";
        }
        else
        {
            buttonDamages.GetComponentInChildren<Text>().text = "Vendu !";
            buttonDamages.GetComponent<Button>().interactable = false;
        }

        checkLeaves();
    }

    public void BuyWeapon()
    {
        leaves -= prices[indexWeapon];

        ui.UpdateLeaves(leaves);

        if (indexWeapon < 2)
        {
            indexWeapon++;
            buttonWeapon.GetComponentInChildren<Text>().text = "Arme (" + prices[indexWeapon].ToString() + ")";
        }
        else
        {
            buttonWeapon.GetComponentInChildren<Text>().text = "Vendu !";
            buttonWeapon.GetComponent<Button>().interactable = false;
        }

        checkLeaves();
    }

    void checkLeaves()
    {
        if(leaves < prices[indexHealth])
        {
            buttonHealth.GetComponent<Button>().interactable = false;
        }

        if(leaves < prices[indexDamages])
        {
            buttonDamages.GetComponent<Button>().interactable = false;
        }

        if (leaves < prices[indexWeapon])
        {
            buttonWeapon.GetComponent<Button>().interactable = false;
        }
        if (leaves < prices[indexFireRate])
        {
            buttonFireRate.GetComponent<Button>().interactable = false;
        }
    }

    void Done()
    {
        ui.SwitchToShmupUI();
    }
}
