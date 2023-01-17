using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Combustion
{
    public class SoulController : MonoBehaviour
    {
        private Rigidbody2D rb;
		private Animator anim;
        private float x, y;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private float speed;
		[SerializeField] private float bulletBoxPadding;

		public float hp;
		[SerializeField] private float maxHP;

		[SerializeField] private BattleManager manager;

		private float invulnTime;
		[SerializeField] private float maxInvulnTime;

		[SerializeField] private float deathTime;
		[SerializeField] private float deathShakePower;
		[SerializeField] private float deathParticleDelayTime;
		[SerializeField] private GameObject deathParticleSystem;
		private float deathShakeFadeTime;
		private Vector3 deathStartPosition;

		#region Unity Methods

		private void OnEnable() {
			transform.position = Vector2.zero;
		}

		private void Start() {
			rb = GetComponent<Rigidbody2D>();
			anim = GetComponent<Animator>();
		}

		private void Update() {
			GetInput();
			ConstrainPosition();
			HandleAnimations();
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

		private IEnumerator Die() {
			manager.Lose();

			deathStartPosition = transform.position;

			float deathCounter = deathTime;

			deathShakeFadeTime = deathShakePower / deathTime;

			float currentShakePower = 0f;

			while (deathCounter > 0)
			{
				transform.position = deathStartPosition;

				deathCounter -= Time.deltaTime;

				float xAmount = Random.Range(-1f, 1f) * currentShakePower;
				float yAmount = Random.Range(-1f, 1f) * currentShakePower;

				transform.position += new Vector3(xAmount, yAmount, 0);

				currentShakePower = Mathf.MoveTowards(currentShakePower, deathShakePower, deathShakeFadeTime * Time.deltaTime);

				yield return null;
			}

			yield return new WaitForSeconds(deathParticleDelayTime);

			GetComponent<SpriteRenderer>().enabled = false;

			Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
		}

		private void HandleAnimations() {
			if (invulnTime > 0)
			{
				anim.SetBool("Invulnerable", true);
				invulnTime -= Time.deltaTime;
			} else
			{
				anim.SetBool("Invulnerable", false);
			}
		}

		#endregion

		#region Public Methods

		public void TakeDamage(int damage) {
			if (invulnTime <= 0)
			{
				hp -= damage;

				if (hp <= 0)
				{
					StartCoroutine(Die());
				}

				invulnTime = maxInvulnTime;
			}
		}

		#endregion
	}
}
