using UnityEngine;
using System.Collections;

public class ComportementSpawn : MonoBehaviour {

    bool isMoving = false;
    Vector2 seekPosition;
	Vector2 animPosition;
	float posY=0f;

	private SpriteRenderer spriteRender;

	void Start(){
		spriteRender = GetComponent<SpriteRenderer> ();
	}

    public void setIsMoving(bool i, Vector2 p)
    {
        seekPosition = p;
        isMoving = i;
		GetComponent<Pokemon>().toggleAnimationWalkOn();
    }

    public bool getIsMoving(){ return isMoving; }
	
	// Update is called once per frame
	void Update () {
		spriteRender.sortingOrder = (int)((1/transform.position.y) * 1000);
		if (isMoving){
			transform.position = Vector2.MoveTowards(transform.position, seekPosition, 5f * Time.deltaTime);
        }
        if(transform.position.x == seekPosition.x && transform.position.y == seekPosition.y)
        {
            isMoving = false;
			GetComponent<Pokemon>().toggleAnimationWalkOff();
        }
    }

}
