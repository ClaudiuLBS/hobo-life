using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
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
    public RectTransform inventoryUI;
    public Slider healthSlider, hungerSlider, thirstSlider, poopSlider, peeSlider;
    public TextMeshProUGUI moneyDisplay;
    public List<Item> inventory;

    public PlayerStats stats = new();
    private List<Disease> diseases;

    //instantiere jucator
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        foreach (PlayerMetrics pm in Enum.GetValues(typeof(PlayerMetrics)))
            stats[pm] = 50f;
    }

    private void Update()
    {
        // Actualizare UI
        //setare slidere + suma de bani
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
            stats[PlayerMetrics.health] -= 100 / cycleDuration * Time.deltaTime;

        if (stats[PlayerMetrics.hunger] < 10)
            stats[PlayerMetrics.health] -= 100 / cycleDuration * Time.deltaTime;

    }

    public bool AddItemToInventory(Item item)
    {
        //nu pot adauga daca depaseste capacitatea maxima a inventarului
        if (inventory.Sum(item => item.size) + item.size > inventoryCapacity)
            return false;
        inventory.Add(item);
        PlayerMenu.instance.UpdateInventory();
        return true;

    }
}
