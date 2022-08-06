using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Battle
{
	public class ArenaController : MonoBehaviour
	{
		public static ArenaController Instance { get; private set; }

		private SpriteRenderer ren;

		private float desiredX, desiredY;
		private float originalX, originalY;
		private float moveStartTime, moveDuration;
		private float desiredWidth, desiredHeight;
		private float originalWidth, originalHeight;
		private float scaleStartTime, scaleDuration;

		public bool IsMoving { get; private set; }

		[SerializeField]
		private float moveSnapping, scaleSnapping;

		private EdgeCollider2D edgeCollider;
		
		[SerializeField]
		private float edgeCorrection;

		private void Awake() {
			Instance = this;

			ren = GetComponent<SpriteRenderer>();

			edgeCollider = GetComponent<EdgeCollider2D>();

			originalX = transform.position.x;
			originalY = transform.position.y;
			desiredX = originalX;
			desiredY = originalY;

			originalWidth = ren.size.y;
			originalHeight = ren.size.y;
			desiredWidth = originalWidth;
			desiredHeight = originalHeight;
		}

		private void Update() {
			IsMoving = false;
			HandlePositions();
			HandleSize();
			HandleCollider();
		}

		private void HandleCollider() {
			List<Vector2> points = new List<Vector2>();

			for (int i = 0; i < 4; i++)
			{
				Vector2 point = new Vector2();
				point.x = (i % 3 == 0 ? -1 : 1) * (ren.size.x / 2 + edgeCorrection);
				point.y = (i < 2 || i > 3 ? -1 : 1) * (ren.size.y / 2 + edgeCorrection);
				points.Add(point);
			}

			points.Add(new Vector2(-(ren.size.x / 2 + edgeCorrection), -(ren.size.y / 2 + edgeCorrection)));

			edgeCollider.SetPoints(points);
		}

		private void HandlePositions() {
			float t = (Time.time - moveStartTime) / moveDuration;

			float x = transform.position.x;

			if (IsCloseEnough(x, desiredX))
			{
				x = desiredX;
			}
			else
			{
				x = Mathf.SmoothStep(originalX, desiredX, t);
				IsMoving = true;
			}

			float y = transform.position.y;

			if (IsCloseEnough(y, desiredY))
			{
				y = desiredY;
			}
			else
			{
				y = Mathf.SmoothStep(originalY, desiredY, t);
				IsMoving = true;
			}

			transform.position = new Vector3(x, y, 0);
		}

		private bool IsCloseEnough(float position, float desiredPosition, float threshold = .05f) {
			return Mathf.Abs(position - desiredPosition) < threshold;
		}

		private void HandleSize() {
			float t2 = (Time.time - scaleStartTime) / scaleDuration;

			float width = ren.size.x;

			if (Mathf.Abs(width - desiredWidth) < scaleSnapping)
			{
				width = desiredWidth;
			}
			else
			{
				width = Mathf.SmoothStep(originalWidth, desiredWidth, t2);
				IsMoving = true;
			}


			float height = ren.size.y;

			if (Mathf.Abs(height - desiredHeight) < scaleSnapping)
			{
				height = desiredHeight;
			}
			else
			{
				height = Mathf.SmoothStep(originalHeight, desiredHeight, t2);
				IsMoving = true;
			}

			ren.size = new Vector2(width, height);
		}

		public void MoveArena(float x, float y, float duration, bool immediate = false) {
			originalX = transform.position.x;
			originalY = transform.position.y;
			moveStartTime = Time.time;
			moveDuration = duration;
			desiredX = x;
			desiredY = y;
		}

		public void ScaleArena(float width, float height, float duration, bool immediate = false) {
			originalWidth = ren.size.x;
			originalHeight = ren.size.y;
			scaleStartTime = Time.time;
			scaleDuration = duration;
			desiredWidth = width;
			desiredHeight = height;
		}

		public void MoveAndScaleArena(float x, float y, float width, float height, float duration, bool immediate = false) {
			MoveArena(x, y, duration, immediate);
			ScaleArena(width, height, duration, immediate);
		}
	}
}
