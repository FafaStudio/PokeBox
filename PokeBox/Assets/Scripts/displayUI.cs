using UnityEngine;
using System.Collections;

public class displayUI : MonoBehaviour {

	public GameObject uiMenu;
	private bool isActive=false;

	void Start () {
		uiMenu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void displayMenu(){
		if (isActive) {
			uiMenu.SetActive (false);
			isActive = false;
		} else {
			uiMenu.SetActive (true);
			isActive = true;
		}
	}
}
