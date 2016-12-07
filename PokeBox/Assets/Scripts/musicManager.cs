using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class musicManager : MonoBehaviour {

	private AudioSource audioDisplay;
	public AudioClip[] musics;

	private int indexMusic;

	private bool isMute=false;

	public Text musicName;


	void Start () {
		audioDisplay = GetComponent<AudioSource> ();
		indexMusic = (int)Random.Range (0f, musics.Length);
		launchMusic ();
	}

	void Update(){
		if (Input.GetKeyDown ("m")) {
			switchVolume ();
		}
		if (!audioDisplay.isPlaying) {
			nextMusic ();
		}
	}

	public void launchMusic(){
		musicName.text = musics[indexMusic].name;
		audioDisplay.clip = musics[indexMusic];
		audioDisplay.Play ();
	}

	public void nextMusic(){
		indexMusic++;
		if (indexMusic >= musics.Length)
			indexMusic = 0;
		launchMusic ();
	}

	public void previousMusic(){
		indexMusic--;
		if (indexMusic < 0)
			indexMusic = musics.Length - 1;
		launchMusic ();
	}

	public void switchVolume(){
		if (isMute) {
			audioDisplay.volume = 1f;
			isMute = false;
		} else {
			audioDisplay.volume = 0f;
			isMute = true;
		}
	}
}
