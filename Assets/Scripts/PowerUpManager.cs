using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
	public void FasterShootingApplyAction(PlayerController controller) {
		controller.shootSpeed /= 2;
	}
	
	public void FasterShootingRemoveAction(PlayerController controller) {
		controller.shootSpeed = controller.defaultShootSpeed;
	}
	

	public void HealApplyAction() {
	}

	public void HealRemoveAction() {
	}
}