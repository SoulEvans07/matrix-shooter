using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
	private SpriteRenderer _renderer;
	private PlayerController shooter;
	public int dmg;

	private void Awake() {
		_renderer = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate() {
		transform.position += transform.up;
	}

	public void SetColor(Color c) {
		_renderer.color = c;
	}
	
	public void SetShooter(PlayerController pc) {
		this.shooter = pc;
	}

	public PlayerController GetShooter() {
		return this.shooter;
	}
}