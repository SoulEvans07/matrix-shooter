using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSlider : MonoBehaviour {
	public GameObject background;
	public GameObject slider;

	private int health;
	private float length;

	private void Awake() {
		length = background.transform.localScale.y;
	}

	void UpdateBar() {
		slider.transform.localScale = new Vector3(1, 1 + Mathf.Floor(length / 5) * health, 1);
		slider.transform.position = new Vector3(
			slider.transform.position.x,
			-0.5f * Mathf.Floor(length / 5) * (5 - health),
			slider.transform.position.z
		);
	}

	public void setValue(int val) {
		this.health = val;
		UpdateBar();
	}
}