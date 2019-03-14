using UnityEngine;

public class GameAreaManager : MonoBehaviour {
	public Transform player_one;
	public Transform player_two;
	public Transform barrier_top;
	public Transform barrier_bottom;

	public Rect gameArea;

	public float timer;
	public float shrinkTime = 20f;

	void Update() {
		timer += Time.unscaledDeltaTime;

		GameSettings.MIN_WIDTH = this.gameArea.x + 2;
		GameSettings.MIN_HEIGHT = this.gameArea.y + 1;
		GameSettings.MAX_WIDTH = this.gameArea.x + this.gameArea.width - 2;
		GameSettings.MAX_HEIGHT = this.gameArea.y + this.gameArea.height - 1;

		if (timer > shrinkTime && Time.timeScale > 0f) {
			Shrink();
		}
	}

	void Shrink() {
		timer = 0;
		Nudge(player_one);
		Nudge(player_two);

		barrier_top.position -= barrier_top.up;
		barrier_bottom.position -= barrier_bottom.up;

		this.gameArea.y += 1;
		this.gameArea.height -= 2;

		if (this.gameArea.height.Equals(2)) {
			player_one.gameObject.GetComponent<PlayerController>().Death();
			player_two.gameObject.GetComponent<PlayerController>().Death();
		}
	}

	static void Nudge(Transform player) {
		if (player.position.y - 1 < GameSettings.MIN_HEIGHT) {
			player.position += Vector3.up;
		} else if (player.position.y + 1 > GameSettings.MAX_HEIGHT) {
			player.position -= Vector3.up;
		}
	}
}