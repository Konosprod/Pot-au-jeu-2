using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public UiManager ui;
    public PlayerPlateformerController plateformPlayer;
    public PlayerController shmupPlayer;
    private Health shmupHealth;

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
        shmupHealth = shmupPlayer.GetComponent<Health>();
        SoundManager._instance.PlayMusic(SoundType.Upgrade);
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
            shmupPlayer.mainShot.fireRate += 3f;
            shmupPlayer.multiShot.fireRate += 0.5f;
        }
        else
        {
            buttonFireRate.GetComponentInChildren<Text>().text = "Vendu !";
            buttonFireRate.GetComponent<Button>().interactable = false;
            shmupPlayer.mainShot.fireRate += 6f;
            shmupPlayer.multiShot.fireRate += 1f;
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
            shmupHealth.hp += 4;
        }
        else
        {
            buttonHealth.GetComponentInChildren<Text>().text = "Vendu !";
            buttonHealth.GetComponent<Button>().interactable = false;
            shmupHealth.hp += 4;
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
            shmupPlayer.mainShot.damage += 0.5f;
            shmupPlayer.multiShot.damage += 0.5f;
        }
        else
        {
            buttonDamages.GetComponentInChildren<Text>().text = "Vendu !";
            buttonDamages.GetComponent<Button>().interactable = false;
            shmupPlayer.mainShot.damage += 1f;
            shmupPlayer.multiShot.damage += 1f;
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
            shmupPlayer.multiShot.nbProj += 1;
        }
        else
        {
            buttonWeapon.GetComponentInChildren<Text>().text = "Vendu !";
            buttonWeapon.GetComponent<Button>().interactable = false;
            shmupPlayer.multiShot.nbProj += 2;
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

    public void Done()
    {
        ui.SwitchToShmupUI();
    }
}
