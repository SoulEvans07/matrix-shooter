using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {
	public Vector3 offset;
	
	public int dmg = 1;
	public Vector3 velocity;
	
	private float timer;
	public float speed;

	public void SetVelocity(Vector3 _velocity, float _speed) {
		this.velocity = _velocity;
		this.speed = _speed;
	}

	void Update() {
		timer += Time.deltaTime;

		if (timer > 1f / speed) {
			transform.position += velocity;
			timer = 0;
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Projectile")) {
			Destroy(other.gameObject, 0f);
			Destroy(this.gameObject, 0f);
		}
	}
}