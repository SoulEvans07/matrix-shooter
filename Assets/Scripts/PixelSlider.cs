using UnityEditor.AnimatedValues;
using UnityEngine;

public class PixelSlider : MonoBehaviour {
	public GameObject background;
	public GameObject slider;

	[SerializeField] private int value;
	[SerializeField] private int maxValue = 1;
	[SerializeField] private int baseValue;
	private float length;

	private void Awake() {
		length = background.transform.localScale.y;
	}

	void UpdateBar() {
		slider.transform.localScale = new Vector3(1, baseValue + Mathf.Floor((length-baseValue)/maxValue  * value), 1);
		slider.transform.position = new Vector3(
			slider.transform.position.x,
			-0.5f * Mathf.Floor(length / maxValue) * (maxValue - value),
			slider.transform.position.z
		);
	}

	public void SetMaxValue(int val) {
		this.maxValue = val;
		this.baseValue = (int)length - (int)Mathf.Floor(length/maxValue)*maxValue;
	}
	
	public void SetValue(int val) {
		this.value = val;
		UpdateBar();
	}
}