using UnityEngine;
using UnityEngine.InputSystem;

namespace Combustion.Player
{
	using Battle;

	public class PlayerController : MonoBehaviour
	{
		public static PlayerController Instance { get; private set; }

		private Rigidbody2D rb;

		private float x, y;

		private PlayerInput playerInput;

		[SerializeField]
		private float speed;

		public float HP { get; private set; }
		public float MaxHP { get; private set; }

		private void OnEnable() {
			transform.position = Vector2.zero;
		}

		private void Start() {
			Instance = this;

			rb = GetComponent<Rigidbody2D>();

			playerInput = ArenaController.Instance.gameObject.GetComponent<PlayerInput>();
		}

		private void Update() {
			GetInput();
			UpdateActive();
		}

		private void GetInput() {
			Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();

			x = input.x;

			if (x > 0.5)
			{
				x = 1;
			}
			else if (x < -0.5)
			{
				x = -1;
			}
			else
			{
				x = 0;
			}

			y = input.y;

			if (y > 0.5)
			{
				y = 1;
			}
			else if (y < -0.5)
			{
				y = -1;
			}
			else
			{
				y = 0;
			}
		}

		private void UpdateActive() {

		}

		private void FixedUpdate() {
			rb.velocity = new Vector2(x * speed, y * speed);
		}
	}

}