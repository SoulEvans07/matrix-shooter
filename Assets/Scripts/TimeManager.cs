using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
	public float timeScale = 0.3f;

	private void Awake() {
		Time.timeScale = this.timeScale;
	}

	void FixedUpdate() {
		if(Time.timeScale > 0)
			Time.timeScale = this.timeScale;
	}

}