﻿using UnityEngine;
using System.Collections;

public class PokeBoxManager : MonoBehaviour {

	public GameObject[] pokemons;

	public GameObject[] decors;
	private GameObject currentDecors;
	private int indexDecors = 0;

	void Awake(){
		Application.targetFrameRate =60;
	}
	void Start () {
		initDecors ();
	}

	public void launchDecors(){
		if (currentDecors != null) {
			currentDecors.SetActive (false);
		}
		currentDecors = decors [indexDecors];
		currentDecors.SetActive (true);
	}

	public void chooseNextDecors(){
		indexDecors++;
		if (indexDecors >= decors.Length)
			indexDecors = 0;
		launchDecors ();
	}

	public void initDecors(){
		foreach (GameObject bidule in decors) {
			bidule.SetActive (false);
		}
		launchDecors ();
	}


}
