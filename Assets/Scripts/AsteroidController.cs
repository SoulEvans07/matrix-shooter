using UnityEngine;

public class AsteroidController : MonoBehaviour {
	private Transform _transform;

	public Vector3 offset;

	public int dmg = 1;
	public Vector3 velocity;
	private float timer;
	public float speed;

	public GameObject powerUp;

	private void Awake() {
		_transform = transform;
	}

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
		if (other.CompareTag("Projectile") || other.CompareTag("Player")) {
			if (powerUp != null) {
				DropItem();
			}

			if (other.CompareTag("Projectile")) {
				Destroy(other.gameObject, 0f);
				Destroy(this.gameObject, 0f);
			}
		}
	}

	private void DropItem() {
		powerUp.GetComponent<Collider2D>().enabled = true;
		powerUp.transform.parent = _transform.parent;
		PowerUpBehavior puContr = powerUp.GetComponent<PowerUpBehavior>();
		puContr.SetVelocity(this.velocity, this.speed / 2);
	}
}