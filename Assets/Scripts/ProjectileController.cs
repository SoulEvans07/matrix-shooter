using System;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileController : MonoBehaviour {
	private SpriteRenderer _renderer;
	private PlayerController shooter;
	public int dmg;
	public bool stationary = false;
	public ProjectileEvent endAction;

	private void Awake() {
		_renderer = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate() {
		if (!stationary)
			transform.position += transform.up;
	}

	public void SetColor(Color c) {
		if (_renderer != null)
			_renderer.color = c;
	}

	public void SetShooter(PlayerController pc) {
		this.shooter = pc;
	}

	public PlayerController GetShooter() {
		return this.shooter;
	}

	public void End() {
		endAction.Invoke(shooter);
		Destroy(this.gameObject, 0f);
	}

	[Serializable]
	public class ProjectileEvent : UnityEvent<PlayerController> {
	}
}