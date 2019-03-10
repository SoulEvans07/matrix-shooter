using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {
	public int dmg = 1;
	public Vector3 velocity;

	void FixedUpdate() {
		transform.position += velocity;
	}
}