using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PlayerMetrics { hunger, thirst, needsToPoop, needsToPee, happiness, health, stamina, mass }

public class PlayerStats : Dictionary<PlayerMetrics, float>
{
    public new float this[PlayerMetrics key]
    {
        get { return base[key]; }
        set { base[key] = Mathf.Clamp(value, 0f, 100f); }
    }
}


public class PlayerMechanics : MonoBehaviour
{
    public static PlayerMechanics instance;
    public float money = 0;
    public float inventoryCapacity = 30;
    public bool isPeeing = false;

    public RectTransform inventoryUI;
    public Slider healthSlider, hungerSlider, thirstSlider, poopSlider, peeSlider;
    public TextMeshProUGUI moneyDisplay;
    public GameObject idCardPanel;

    public List<Item> inventory;
    public PlayerStats stats = new();
    public IdCard idCard = null;
    
    private List<Disease> diseases;
    private ParticleSystem peeParticles;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        peeParticles = transform.Find("Pee").GetComponent<ParticleSystem>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        foreach (PlayerMetrics pm in Enum.GetValues(typeof(PlayerMetrics)))
            stats[pm] = 50f;
        stats[PlayerMetrics.needsToPee] = 100;
    }

    private void Update()
    {
        // Actualizare UI
        healthSlider.value = stats[PlayerMetrics.health];
        hungerSlider.value = stats[PlayerMetrics.hunger];
        thirstSlider.value = stats[PlayerMetrics.thirst];
        poopSlider.value = stats[PlayerMetrics.needsToPoop];
        peeSlider.value = stats[PlayerMetrics.needsToPee];
        moneyDisplay.text = $"{Math.Round(money, 2)}$";

        float cycleDuration = DayNightCycle.instance.cycleDuration;

        // 100 (valoarea maxima) / (x zile * cycleDuration) => valoare se goleste in x zile
        stats[PlayerMetrics.hunger] -= 100 / (12 * cycleDuration) * Time.deltaTime;
        stats[PlayerMetrics.thirst] -= 100 / (3 * cycleDuration) * Time.deltaTime;

        // daca nivelul de apa sau hrana e scazut, mai are maxim 24 ore de trait
        if (stats[PlayerMetrics.thirst] < 10)
        {
            stats[PlayerMetrics.health] -= 100 / cycleDuration * Time.deltaTime;
            InfoHandler.instance.SetInfo("Dying of thirst!");
        }

        if (stats[PlayerMetrics.hunger] < 10)
        {
            stats[PlayerMetrics.health] -= 100 / cycleDuration * Time.deltaTime;
            InfoHandler.instance.SetInfo("Dying of Hunger!");
        }

        if (isPeeing)
        {
            if (stats[PlayerMetrics.needsToPee] <= 0)
            {
                stats[PlayerMetrics.needsToPee] = 0;
                isPeeing = false;
                peeParticles.Stop();
            } 
            else
            {
                stats[PlayerMetrics.needsToPee] -= Time.deltaTime * 10;
                peeParticles.gravityModifier = -0.08f * stats[PlayerMetrics.needsToPee] + 11.08f;
            }
        }

        if (Input.GetKey(KeyCode.P)) { 
            if (stats[PlayerMetrics.needsToPee] > 10)
            {
                isPeeing = true;
                peeParticles.Play();
            }
            else
            {
                InfoHandler.instance.SetInfo("Can't pee right now!");
            }
        }
    }

    public bool AddItemToInventory(Item item)
    {
        if (inventory.Sum(item => item.size) + item.size > inventoryCapacity)
            return false;
        inventory.Add(item);
        PlayerMenu.instance.UpdateInventory();
        return true;

    }

    public void SetIdCard(string firstName, string lastName, string birthDate, string cnp, string citizenship, string gender)
    {
        idCard = new IdCard(firstName, lastName, birthDate, cnp, citizenship, gender);
        idCardPanel.SetActive(true);
        idCardPanel.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = firstName + " " +  lastName;
        idCardPanel.transform.Find("Citizenship").GetComponent<TextMeshProUGUI>().text = citizenship + ", gen " + gender;
        idCardPanel.transform.Find("BirthDate").GetComponent<TextMeshProUGUI>().text = "birth date: " + birthDate;
        idCardPanel.transform.Find("CNP").GetComponent<TextMeshProUGUI>().text = "cnp: " + cnp;
    }

    public void AddMoney(float value)
    {
        money += value;     
        moneyDisplay.text = $"{Math.Round(money, 2)}$";
    }
}
