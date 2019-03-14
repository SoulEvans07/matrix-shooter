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

	public void DoubleGunApplyAction(PlayerController controller) {
		controller.SetGunMode(GunMode.DOUBLE);
	}

	public void DoubleGunRemoveAction(PlayerController controller) {
		controller.SetGunMode(GunMode.SINGLE);
	}

	public void TripleGunApplyAction(PlayerController controller) {
		controller.SetGunMode(GunMode.TRIPLE);
	}

	public void TripleGunRemoveAction(PlayerController controller) {
		controller.SetGunMode(GunMode.SINGLE);
	}

	public void LaserGunApplyAction(PlayerController controller) {
		controller.SetGunMode(GunMode.LASER);
	}

	public void LaserGunRemoveAction(PlayerController controller) {
		controller.SetGunMode(GunMode.SINGLE);
	}
}