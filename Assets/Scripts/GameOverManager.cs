using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
	public PlayerController player_one;
	public PlayerController player_two;

	public Animator[] p1_win;
	public Animator[] p2_win;
	public Animator[] game_over;

	private bool gameOver;

	private void Awake() {
		this.gameOver = false;
	}

	void FixedUpdate() {
		if (player_one.IsDead && player_two.IsDead) {
			if (player_one.healthValue > player_two.healthValue) {
				PlayWin(p1_win);
			} else if (player_one.healthValue < player_two.healthValue) {
				PlayWin(p2_win);
			} else {
				PlayGameOver();
			}
		}

		if (player_one.healthValue == 0) {
			PlayWin(p2_win);
		}

		if (player_two.healthValue == 0) {
			PlayWin(p1_win);
		}
	}

	private void Update() {
		if (this.gameOver && Input.GetButton("Restart")) {
			SceneManager.LoadScene(0);
		}
	}

	private void PlayWin(Animator[] anims) {
		Time.timeScale = 0f;
		this.gameOver = true;
		foreach (Animator anim in anims) {
			anim.SetTrigger("Start");
		}
	}

	private void PlayGameOver() {
		Time.timeScale = 0f;
		this.gameOver = true;
		foreach (Animator anim in game_over) {
			anim.SetTrigger("Start");
		}
	}
}