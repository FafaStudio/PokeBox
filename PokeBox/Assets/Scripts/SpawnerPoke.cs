using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerPoke : MonoBehaviour {

	private GameObject[] pokemons;
	private GameObject[] mimiPokemons;
	private List<GameObject> pokemonsSpawned;

	[HideInInspector]public PokeBoxManager manager;

	public bool canSpawnPokemon(GameObject pokemon){
		foreach (GameObject poke in pokemonsSpawned) {
			if (poke.GetComponent<Pokemon>().name == pokemon.GetComponent<Pokemon> ().name)
				return false;
		}
		return true;
	}

	void Awake(){
		manager =GetComponent<PokeBoxManager> ();
		mimiPokemons = manager.mimikyuGamePokemons;
		pokemons =manager.pokemons;
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
		if (manager.isMiniGame)
			return;
		GameObject toInstantiate = pokemons[Random.Range(0,pokemons.Length)];
		if (pokemonsSpawned.Count < pokemons.Length) {
			if (!canSpawnPokemon (toInstantiate)) {
				spawnPokemon ();
				return;
			}
		}
		toInstantiate.GetComponent<Pokemon> ().spawner = this;
		pokemonsSpawned.Add(Instantiate (toInstantiate, new Vector2 (Random.Range (4f, 16f), Random.Range (6f, 13f)), Quaternion.identity)as GameObject);
	}

	public void spawnSpecificPokemon(GameObject pokemon){
		pokemon.GetComponent<Pokemon> ().spawner = this;

		pokemonsSpawned.Add(Instantiate (pokemon, new Vector2 (Random.Range (4f, 16f), Random.Range (6f, 13f)), Quaternion.identity)as GameObject);
	}

	public void initMimikyuGame(){
		clearPokemons ();
		manager.isMiniGame = true;
		manager.isMimikyuGame = true;
		for (int i = 0; i < 600; i++) {
			spawnSpecificPokemon (mimiPokemons [0]);
		}
		spawnSpecificPokemon (mimiPokemons [1]);
	}

	public void clearPokemons(){
		if (manager.isMiniGame) {
			manager.stopMiniGame ();
		}
		foreach (GameObject poke in pokemonsSpawned) {
			Destroy (poke.gameObject);
		}
		pokemonsSpawned.Clear ();
	}
}
