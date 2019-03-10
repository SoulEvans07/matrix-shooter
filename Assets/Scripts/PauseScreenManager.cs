using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreenManager : MonoBehaviour {
	public GameObject pauseScreen;
	private bool paused;

	public float buttonTimer = 0.3f;
	private float timer;
	
	void Awake() {
		paused = false;
	}

	void Update() {
		timer += Time.unscaledDeltaTime;
		
		bool reset = Input.GetButton("Restart");
		bool submit = Input.GetButton("Accept");
		
		if (timer > buttonTimer && Time.timeScale > 0f) {
			if (paused) {
				if (submit) {
					Quit();
				} else if (reset) {
					Resume();
				}
			} else {
				if (reset) {
					Pause();
				}
			}
		}
	}

	void Quit() {
		timer = 0;
		Application.Quit();
	}

	void Resume() {
		Time.timeScale = 0.3f;
		timer = 0;
		paused = false;
		pauseScreen.SetActive(false);
	}

	void Pause() {
		Time.timeScale = 0f;
		timer = 0;
		paused = true;
		pauseScreen.SetActive(true);
	}
}