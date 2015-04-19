using UnityEngine;
using UnityEngine.UI;

class TechUpgrader : MonoBehaviour {
  public TechnologyType tech;
  private Stockpile stockpile;
  private Technology technology;
  public UpgradeCost[] costs;
  public bool payable = false;
	int toast;

  void Start () {
    technology = GameObject.Find("/Technology").GetComponent<Technology>();
    stockpile = GameObject.Find("/Stockpile").GetComponent<Stockpile>();
    costs = GetComponents<UpgradeCost>();
//    setStatus(false);
  }

  public void upgrade() {
    foreach (UpgradeCost cost in costs) {
      stockpile.updateStockLevel(cost.toResourceAmount(), true);

      technology.Upgrade(tech);

      if (tech == TechnologyType.AlgaeFarm) {toast = 3;}
      if (tech == TechnologyType.SolarArray) {toast = 0;}
      GameObject.Find ("GameController").GetComponent<ToastNotifications>().ToastNotification (toast);
    }
  }

  public void OnClicked(Button button) {
		button.gameObject.SetActive (false);
  }

}

