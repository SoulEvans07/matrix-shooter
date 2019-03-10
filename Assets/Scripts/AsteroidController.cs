using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {
	public int dmg = 1;
	public Vector3 velocity;
	
	private float timer;
	public int speed;

	void FixedUpdate() {
		timer += Time.deltaTime;

		if (timer > 4f / speed) {
			transform.position += velocity;
		}
	}
}