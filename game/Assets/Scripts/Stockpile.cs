using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


public class Stockpile : MonoBehaviour {
  public Dictionary<ResourceType, int> stocks;
  public Dictionary<ResourceType, int> maxima;
  public Text O2Stock;
  public Text CO2Stock;
  public Text PowerStock;
  public Text OreStock;
  public Text IceStock;

  public GameObject AlgaeUpgradeButton;
  public GameObject SolarUpgradeButton;
  public int AlgaeUpgradeThreshold;
  public int SolarUpgradeThreshold;

  Stockpile() {
    stocks = new Dictionary<ResourceType, int>();
    maxima = new Dictionary<ResourceType, int>();
  }

  private void initializeResource(ResourceType type, int stock, int max) {
    maxima.Add(type, max);
    stocks.Add(type, stock);
  }

  // Use this for initialization
  void Start () {
    //initializeResource(ResourceType.Food, 1000, 1000);
    //initializeResource(ResourceType.BioWaste, 0, 100);
    //initializeResource(ResourceType.H2O, 250, 250);
    //initializeResource(ResourceType.WasteWater, 0, 150);
    initializeResource(ResourceType.O2, 100, 200);
    initializeResource(ResourceType.CO2, 100, 200);
    //initializeResource(ResourceType.H2, 0, 500);
    initializeResource(ResourceType.Power, 500, 1500);
    initializeResource(ResourceType.Ore, 40, 200);
    initializeResource(ResourceType.Metal, 0, 100);
    initializeResource(ResourceType.Silicates, 0, 100);
    initializeResource(ResourceType.Ice, 40, 200);
  }

  void Update() {
    O2Stock.text = stocks[ResourceType.O2].ToString();
    CO2Stock.text = stocks[ResourceType.CO2].ToString();
    PowerStock.text = stocks[ResourceType.Power].ToString();
    OreStock.text = stocks[ResourceType.Ore].ToString();
    IceStock.text = stocks[ResourceType.Ice].ToString();

    if (stocks[ResourceType.Ore] >= SolarUpgradeThreshold) {
      SolarUpgradeButton.SetActive(true);
    }

    if (stocks[ResourceType.Ice] >= AlgaeUpgradeThreshold) {
      AlgaeUpgradeButton.SetActive(true);
    }

  }

  public int updateStockLevel(ResourceAmount delta, bool decrease=false) {
    int newStock = stocks[delta.type] + ((decrease ? -1 : 1) * delta.amount);

    if (newStock >= 0) {
      if (newStock <= maxima[delta.type]) {
        stocks[delta.type] = newStock;
      } else {
        stocks[delta.type] = maxima[delta.type];
      }
    }

    return newStock;
  }
}

