using UnityEngine;

public class PowerUpActions : MonoBehaviour {
	public void FasterShootingApplyAction(PlayerController controller) {
		controller.shootSpeed /= 2;
	}

	public void FasterShootingRemoveAction(PlayerController controller) {
		controller.shootSpeed = controller.defaultShootSpeed;
	}


	public void HealApplyAction(PlayerController controller) {
		controller.Heal(1);
	}
	
	
}