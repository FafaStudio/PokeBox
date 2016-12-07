using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerPoke : MonoBehaviour {

	private GameObject[] pokemons;
	private List<GameObject> pokemonsSpawned;

	public bool canSpawnPokemon(GameObject pokemon){
		return pokemonsSpawned.Contains (pokemon);
	}

	void Awake(){
		pokemons = GameObject.FindGameObjectWithTag ("PokeManager").GetComponent<PokeBoxManager> ().pokemons;
	}
	void Start(){
		pokemonsSpawned = new List<GameObject> ();
		for (int i = 0; i < 3; i++) {
			spawnPokemon ();
		}
	}

	void Update(){
		if(Input.GetKeyDown("p")){
			spawnPokemon();
		}
	}

	public void spawnPokemon(){
		GameObject toInstantiate = pokemons[Random.Range(0,pokemons.Length)];
		if (pokemonsSpawned.Count < pokemons.Length) {
			if (canSpawnPokemon (toInstantiate)) {
				spawnPokemon ();
				return;
			}
		}
		Instantiate (toInstantiate, new Vector2 (Random.Range (4f, 16f), Random.Range (6f, 13f)), Quaternion.identity);
		pokemonsSpawned.Add (toInstantiate);
	}

	public void clearPokemons(){
	}
}
