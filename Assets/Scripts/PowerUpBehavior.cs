using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehavior : MonoBehaviour {
	[SerializeField] private PowerUp powerup;
	private PlayerController playerController;

	private void OnTriggerEnter2D(Collider2D other) {
		bool isProjectile = other.CompareTag("Projectile");
		bool isPlayer = other.CompareTag("Player");
		if (isProjectile || isPlayer) {
			if (isProjectile) {
				this.playerController = other.GetComponent<ProjectileController>().GetShooter();
			}
			if (isPlayer) {
				this.playerController = other.GetComponent<PlayerController>();
			}

			this.ApplyPowerUp();
			Destroy(other.gameObject, 0f);
			Destroy(this.gameObject, 0f);
		}
	}

	public void SetPowerUp(PowerUp _powerup) {
		this.powerup = _powerup;
		gameObject.name = _powerup.name;
	}

	private void ApplyPowerUp() {
		this.powerup.Apply(this.playerController);
	}
}