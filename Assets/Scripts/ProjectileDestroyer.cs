using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Projectile") ||
		    other.gameObject.tag.Equals("Asteroid")) {
			Destroy(other.gameObject, 0f);
		}
	}
}