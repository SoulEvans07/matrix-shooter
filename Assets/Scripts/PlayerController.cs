﻿using System;
using UnityEngine;

[Serializable]
public class PlayerController : MonoBehaviour {
	public string AXIS_X = "Horizontal";
	public string AXIS_Y = "Vertical";
	public string SHOOT = "Fire1";

	private Rigidbody2D _rigidbody;
	public GameObject gunDisplay;
	public GameObject projectile;
	public float defaultShootSpeed = 0.12f;
	public float shootSpeed;

	private float timer = 1f;

	public HealthSlider healthSlider;
	private const int maxHealth = 5;
	public int healthValue;
	private bool isDead;
	public bool IsDead => isDead;

	public Color projectileColor;

	private float x;
	private float y;
	private bool shoot;

	public PowerUp activePowerup;
	public float powerupTimer;

	private void Start() {
		_rigidbody = GetComponent<Rigidbody2D>();
		this.isDead = false;
		this.healthValue = maxHealth;
		this.healthSlider.setValue(this.healthValue);
		this.shootSpeed = defaultShootSpeed;
	}

	private void Update() {
		if (isDead) return;

		x = Input.GetAxisRaw(AXIS_X);
		y = Input.GetAxisRaw(AXIS_Y);

		shoot = Input.GetButton(SHOOT);
	}

	void FixedUpdate() {
		if (isDead) return;

		timer += Time.deltaTime;

		if (!gunDisplay.activeSelf && timer >= shootSpeed) {
			gunDisplay.SetActive(true);
		}

		if (shoot && timer >= shootSpeed) {
			Shoot();
		}

		HandleActivePowerUp();

		Move(x, y);
	}

	void Move(float x, float y) {
		Vector2 nextPos = _rigidbody.position + new Vector2(x, y);
		if (nextPos.x < GameSettings.MIN_WIDTH || nextPos.x > GameSettings.MAX_WIDTH) nextPos.x = _rigidbody.position.x;
		if (nextPos.y < GameSettings.MIN_HEIGHT || nextPos.y > GameSettings.MAX_HEIGHT)
			nextPos.y = _rigidbody.position.y;

		_rigidbody.MovePosition(nextPos);
	}

	void Shoot() {
		GameObject proj = Instantiate(projectile, transform.position + (transform.up * 1.5f), transform.rotation);
		ProjectileController projContr = proj.GetComponent<ProjectileController>();
		projContr.SetShooter(this);
		projContr.SetColor(projectileColor);
		proj.gameObject.layer = this.gameObject.layer;
		gunDisplay.SetActive(false);
		timer = 0f;
	}

	private void TakeDamage(int dmg) {
		this.healthValue -= dmg;
		if (this.healthValue < 0) {
			this.healthValue = 0;
		}

		healthSlider.setValue(this.healthValue);

		if (this.healthValue == 0) {
			Death();
		}
	}

	public void ActivatePowerUp(PowerUp powerup) {
		activePowerup?.Remove(this);
		this.activePowerup = powerup;
		powerup.Apply(this);
	}

	public void HandleActivePowerUp() {
		if (this.activePowerup != null) {
			this.powerupTimer += Time.deltaTime;
			if (this.powerupTimer > this.activePowerup.duration) {
				this.activePowerup.Remove(this);
				this.powerupTimer = 0f;
				this.activePowerup = null;
			}
		}
	}

	public void Heal(int amount) {
		this.healthValue += amount;
		if (this.healthValue > maxHealth)
			this.healthValue = maxHealth;
		healthSlider.setValue(this.healthValue);
	}

	public void Death() {
		this.isDead = true;
		Debug.Log(this.name + " is dead!");
	}

	private void OnCollisionEnter2D(Collision2D other) {
//		Debug.Log("coll: " + other.gameObject.name);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Projectile")) {
			TakeDamage(other.gameObject.GetComponent<ProjectileController>().dmg);
			Destroy(other.gameObject, 0f);
		}

		if (other.gameObject.tag.Equals("Asteroid")) {
			TakeDamage(other.gameObject.GetComponent<AsteroidController>().dmg);
			Destroy(other.gameObject, 0f);
		}
	}
}