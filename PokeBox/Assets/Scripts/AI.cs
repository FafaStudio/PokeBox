using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {

    private const float DRINK_TIME = 2f;

    private Pokemon thisPokemon;
    public bool isActive,
                 isEscaped,
                 wantToEscape;
    private enum states { drinking, wandering };

    private float moveSpeed;
    [SerializeField]private int currentState,
                                previousState;
    private Transform boat;
    private Vector2 ancientDirection;
    [SerializeField] private Vector2 direction;

    public AudioClip sonDeLaVictime = null;

	void Start () {
		thisPokemon = GetComponent<Pokemon>();
		GetComponent<Pokemon>().toggleAnimationWalkOn();

        isActive = true;

        moveSpeed = 0.4f;
		currentState = (int)states.wandering;

        Vector2 startDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        ancientDirection = startDirection;
        direction = startDirection;

        StartCoroutine(AnimalStates());
	}
	
	void Update () {

        if (Mathf.Sign(ancientDirection.x) == 1)
            thisPokemon.GetComponent<SpriteRenderer>().flipX = true;
        if (Mathf.Sign(ancientDirection.x) == -1)
            thisPokemon.GetComponent<SpriteRenderer>().flipX = false;


        switch (currentState) {

            case (int)states.wandering:
                moveSpeed = 0.4f;
                direction = Wander();
                transform.Translate(direction * moveSpeed * Time.deltaTime);
                break;

        }
    }

    Vector2 Wander() {
        direction.x = ancientDirection.x + Random.Range(-0.1f, 0.1f);
        direction.y = ancientDirection.y + Random.Range(-0.1f, 0.1f);

        return Normalize(direction);
    }

    Vector2 Seek(Vector2 pos) {
        Vector2 movingTo;
        movingTo.x = (pos.x - transform.position.x) * Mathf.Sign(transform.localScale.x);
        movingTo.y = pos.y - transform.position.y;
        return Normalize(movingTo);
    }

    Vector2 Normalize(Vector2 v) {
        float length = Mathf.Sqrt(Mathf.Pow(v.x, 2) + Mathf.Pow(v.y, 2));
        Vector2 temp;

        temp.x = v.x / length;
        temp.y = v.y / length;

        return temp;
    }

    float GetDistance(Vector2 other) {
        return Mathf.Sqrt(Mathf.Pow(other.x - transform.position.x, 2) + Mathf.Pow(other.y - transform.position.y, 2));
    }

    void OnCollisionEnter2D(Collision2D other) {
        /*if(other.gameObject.name == "MurGauche" || other.gameObject.name == "MurDroite")
            ancientDirection.x *= -1;
        if(other.gameObject.name == "MurHaut" || other.gameObject.name == "MurBas")
            ancientDirection.y *= -1;*/
        BoxCollider2D b = other.gameObject.GetComponent<BoxCollider2D>();

        if (b.size.x > 0.5f || (b.size.y > 0.5f && b.transform.rotation.z == 0))
            ancientDirection.x *= -1;
        if (b.size.y > 0.5f || (b.size.x > 0.5f && b.transform.rotation.z == 0))
            ancientDirection.y *= -1;
    }

    void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Wall") {
            ancientDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            //transform.parent = other.gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
    }

    IEnumerator AnimalStates() {
        while(isActive) {
            yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
            int rand = Random.Range(0, 10);

            if (rand < 5 && (currentState == (int)states.wandering || currentState != (int)states.drinking)) {
                previousState = currentState;
                currentState = (int)states.drinking;
                thisPokemon.toggleAnimationWalkOff();
                yield return new WaitForSeconds(DRINK_TIME);
                thisPokemon.toggleAnimationWalkOn();
                currentState = previousState;
            } 
        }
    }

   /* IEnumerator AnimalEat() {

        //Trouver le son a jouer
        sonDeLaVictime = prey.GetComponent<Animal>().sonAnimal;
        if (gameObject.GetComponent<Animal>().sonAnimal != null && sonDeLaVictime != null)
        {
            //Faire un random
            if(Random.Range(0, 2) < 1)
            {
                AudioSource.PlayClipAtPoint(gameObject.GetComponent<Animal>().sonAnimal, transform.position, 1);
            }
            else
            {
                AudioSource.PlayClipAtPoint(sonDeLaVictime, transform.position, 1);
            }
            //Faire jouer le son
        }
        else
        {
            if(gameObject.GetComponent<Animal>().sonAnimal != null)
            {
                AudioSource.PlayClipAtPoint(gameObject.GetComponent<Animal>().sonAnimal, transform.position, 1);
            }
            else
            {
                if(sonDeLaVictime != null)
                {
                    AudioSource.PlayClipAtPoint(sonDeLaVictime, transform.position, 1);
                }
            }
        }


        GameObject instObject = (GameObject)Instantiate(boucane, thisPokemon.transform.position, Quaternion.identity);
        instObject.transform.parent = thisPokemon.transform;
        thisPokemon.toggleAnimationWalkOff();

        

        

        Destroy(prey);
        prey = null;

        yield return new WaitForSeconds(1f);

        
        Destroy(instObject);
        thisPokemon.toggleAnimationWalkOn();
        currentState = previousState;
    }*/
}