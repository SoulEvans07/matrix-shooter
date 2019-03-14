using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PowerUp {
	[SerializeField] public string name;
	[SerializeField] public float duration;
	[SerializeField] public PowerUpEvent applyAction;
	[SerializeField] public PowerUpEvent removeAction;

	public void Apply(PlayerController controller) {
		applyAction?.Invoke(controller);
	}

	public void Remove(PlayerController controller) {
		removeAction?.Invoke(controller);
	}

	[Serializable]
	public class PowerUpEvent : UnityEvent<PlayerController> {
	}
}