using UnityEngine;

public class PowerUpBehavior : MonoBehaviour {
	private Transform _transform;
	[SerializeField] private PowerUp powerup;
	private PlayerController playerController;

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
		bool isProjectile = other.CompareTag("Projectile");
		bool isPlayer = other.CompareTag("Player");
		if (isProjectile || isPlayer) {
			if (isProjectile) {
				this.playerController = other.GetComponent<ProjectileController>().GetShooter();
				Destroy(other.gameObject, 0f);
			}

			if (isPlayer) {
				this.playerController = other.GetComponent<PlayerController>();
			}

			this.ApplyPowerUp();
			Destroy(this.gameObject, 0f);
		}
	}

	public void SetPowerUp(PowerUp _powerup) {
		this.powerup = _powerup;
		gameObject.name = _powerup.name;
	}

	private void ApplyPowerUp() {
		this.playerController.ActivatePowerUp(this.powerup);
	}
}