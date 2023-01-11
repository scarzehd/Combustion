using UnityEngine;
using UnityEngine.InputSystem;

namespace Combustion
{
    public class SoulController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float x, y;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private float speed;
		[SerializeField] private float bulletBoxPadding;

		public float HP { get; private set; }
		private float maxHP;

		#region Unity Methods

		private void OnEnable() {
			transform.position = Vector2.zero;
		}

		private void Start() {
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update() {
			GetInput();
			ConstrainPosition();
		}

		private void FixedUpdate() {
			rb.velocity = new Vector2(x * speed, y * speed);
		}

		#endregion

		#region Private Methods

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

		private void ConstrainPosition() {
			float newX, newY;
			Vector2 position = transform.position;
			Rect bulletBox = BulletBox.Instance.Bounds;

			newX = Mathf.Clamp(position.x, bulletBox.x + bulletBoxPadding, bulletBox.x + bulletBox.width - bulletBoxPadding);
			newY = Mathf.Clamp(position.y, bulletBox.y + bulletBoxPadding, bulletBox.y + bulletBox.height - bulletBoxPadding);

			transform.position = new Vector2(newX, newY);
		}

		private void Die() {

		}

		#endregion

		#region Public Methods

		public void TakeDamage(int damage) {
			HP -= damage;

			if (HP <= 0)
			{
				Die();
			}
		}

		#endregion
	}
}
