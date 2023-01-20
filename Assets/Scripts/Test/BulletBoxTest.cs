using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Combustion;

public class BulletBoxTest : MonoBehaviour
{
    public Button changeArenaButton;
    public TMP_InputField xInput, yInput, widthInput, heightInput;

	public float bulletBoxTime;

	private void Start() {
		changeArenaButton.onClick.AddListener(() =>
		{
			Vector2 position = new Vector2(float.Parse(xInput.text), float.Parse(yInput.text));
			Vector2 size = new Vector2(float.Parse(widthInput.text), float.Parse(heightInput.text));

			BulletBox.instance.SetShape(position, size, bulletBoxTime);
		});
	}
}