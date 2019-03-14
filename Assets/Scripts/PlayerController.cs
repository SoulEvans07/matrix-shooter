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
	public GameObject left_gun;
	public GameObject middle_gun;
	public GameObject right_gun;
	public GunMode gunMode = GunMode.SINGLE;
	public Sprite single_gun;
	public Sprite double_gun;
	public Sprite tripple_gun;
	public Sprite laser_gun;

	private float timer = 1f;

	public PixelSlider healthSlider;
	private const int maxHealth = 26;
	public int healthValue;
	private bool isDead;
	public bool IsDead => isDead;

	public Color projectileColor;

	private float x;
	private float y;
	private bool shoot;

	private PowerUp activePowerup;
	public float powerupTimer;
	public PixelSlider powerupSlider;
	public float powerTimeSlice;

 	private void Start() {
		_rigidbody = GetComponent<Rigidbody2D>();
		this.isDead = false;
		this.healthValue = maxHealth;
		this.healthSlider.SetMaxValue(maxHealth);
		this.healthSlider.SetValue(this.healthValue);
		this.shootSpeed = defaultShootSpeed;
		
		this.activePowerup = null;
		this.powerupSlider.SetMaxValue(26);
		this.powerupSlider.SetValue(0);
		
		SetGunMode(GunMode.TRIPLE);
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

	public void SetGunMode(GunMode mode) {
		this.gunMode = mode;
		SpriteRenderer spr_rendr = this.gunDisplay.GetComponent<SpriteRenderer>();
		switch (gunMode) {
			case GunMode.SINGLE:
				spr_rendr.sprite = single_gun;
				break;
			case GunMode.DOUBLE:
				spr_rendr.sprite = double_gun;
				break;
			case GunMode.TRIPLE:
				spr_rendr.sprite = tripple_gun;
				break;
			case GunMode.LASER:
				spr_rendr.sprite = laser_gun;
				break;
		}
	}

	void Shoot() {
		switch (gunMode) {
			case GunMode.SINGLE:
				SpawnProjectile(middle_gun.transform);
				break;
			case GunMode.DOUBLE:
				SpawnProjectile(left_gun.transform);
				SpawnProjectile(right_gun.transform);
				break;
			case GunMode.TRIPLE:
				SpawnProjectile(left_gun.transform);
				SpawnProjectile(middle_gun.transform);
				SpawnProjectile(right_gun.transform);
				break;
			case GunMode.LASER: break;
		}
		
		gunDisplay.SetActive(false);
		timer = 0f;
	}

	void SpawnProjectile(Transform gun) {
		GameObject proj = Instantiate(projectile, gun.position + transform.up, transform.rotation);
		ProjectileController projContr = proj.GetComponent<ProjectileController>();
		projContr.SetShooter(this);
		projContr.SetColor(projectileColor);
		proj.gameObject.layer = this.gameObject.layer;
	}

	private void TakeDamage(int dmg) {
		this.healthValue -= dmg;
		if (this.healthValue < 0) {
			this.healthValue = 0;
		}

		healthSlider.SetValue(this.healthValue);

		if (this.healthValue == 0) {
			Death();
		}
	}

	public void ActivatePowerUp(PowerUp powerup) {
		activePowerup?.Remove(this);
		this.activePowerup = powerup;
		this.powerupTimer = 0;
		this.powerTimeSlice = powerup.duration / 26f;
		this.powerupSlider.SetValue(powerup.duration.Equals(0) ? 0 : 26);
		powerup.Apply(this);
	}

	private void HandleActivePowerUp() {
		if (this.activePowerup != null) {
			this.powerupTimer += Time.deltaTime;
			if (this.powerupTimer > this.activePowerup.duration) {
				this.activePowerup.Remove(this);
				this.powerupTimer = 0f;
				this.activePowerup = null;
			} else {
				this.powerupSlider.SetValue(26 - (int) Math.Ceiling(this.powerupTimer / powerTimeSlice));
			}
		}
	}

	public void Heal(int amount) {
		this.healthValue += amount;
		if (this.healthValue > maxHealth)
			this.healthValue = maxHealth;
		healthSlider.SetValue(this.healthValue);
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