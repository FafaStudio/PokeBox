using UnityEngine;
using System.Collections;



public class Pokemon : MonoBehaviour {

	public static int nbrAnimalSpwaned;

	[Tooltip("Le nom de la classe de l'animal. Ex: Lion.")]
	public string name;
	[Tooltip("1 si l'animal mange.")]
	protected bool isEating;
	[Tooltip("id de l'animal")]
	[SerializeField] protected int id;

	bool isWanderOn;
	bool waitForAnim=false;


	//public AudioClip sonPokemon;



	public void setName(string n) { name = n; }
	public void setIsEating(bool i) { isEating = i; }

	public void setId() { id = nbrAnimalSpwaned++; }


	public string getName() { return name; }
	public bool getIsEating() { return isEating; }
	public int getId() { return id;}

	public void toggleAnimationWalkOn() {
		gameObject.GetComponent<Animator>().SetBool("Walk", true);
	}

	public void toggleAnimationWalkOff() {
		gameObject.GetComponent<Animator>().SetBool("Walk", false);
	}
}
