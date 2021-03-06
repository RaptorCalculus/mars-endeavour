using UnityEngine;

class BuildingManager : MonoBehaviour {
  public ResourceConverter converter;
  private Technology technology;
  public float condition = 1;
  public int maxHP = 100;
  public int HP = 100;
  public int currentLevel = 0;
  public TechnologyType tech;

  public float[] techBonuses;

  void updateConverter() {
	  
    Debug.Log(currentLevel);
	
	if (techBonuses.Length > 0) {
		converter.efficiency = (1 + techBonuses[currentLevel]) * condition;
	}
  }

  void checkTech() {
    if (currentLevel != technology.techLevels[tech].current) {
      currentLevel = technology.techLevels[tech].current;
      updateConverter();
    }
  }

  void updateCondition() {
    if (HP > maxHP) {
      HP = maxHP;
    }

    if (HP <= 0) {
      HP = 0;
      // BOOM!
			GameObject.Find("GameController").GetComponent<GameController>().GameOver();
    }
    condition = ((float)HP / (float)maxHP);
	hpGUI ();
  }

  public void damage(int magnitude) {
    this.HP -= magnitude;
    updateCondition();
    updateConverter();
  }

  public void repair(int magnitude) {
    this.HP += magnitude;
    updateCondition();
    updateConverter();
  }

  void Start () {
		hpGUI ();

    if (technology == null) {
      technology = GameObject.Find("/Technology").GetComponent<Technology>();
    }

    updateCondition();
    checkTech();
  }

  void Update() {
    updateCondition();
    checkTech();
  }

	void hpGUI(){
		transform.GetChild (0).GetComponent<TextMesh> ().text = HP.ToString() + "%";
	}



}
