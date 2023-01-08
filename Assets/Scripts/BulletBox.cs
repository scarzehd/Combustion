using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion
{
	using Utility;

	public class BulletBox : MonoBehaviour {
        public static BulletBox Instance { get; private set; } = null;

        private SpriteRenderer ren;

        private Vector2 startPosition, desiredPosition;
        private Vector2 startSize, desiredSize;

        private float startTime, duration;

        [SerializeField]
        private float snapThreshold;

        public Rect Bounds
        {
            get {
                Rect result = new Rect();
                result.x = transform.position.x - ren.size.x / 2;
                result.y = transform.position.y - ren.size.y / 2;
                result.width = ren.size.x;
                result.height = ren.size.y;

                return result;
            }
        }

		#region Unity Methods

		private void Awake() {
            if (Instance != null)
            {
                Debug.LogError("Multiple Bullet Boxes may be in the scene!", Instance);
                return;
            }

            Instance = this;

            ren = GetComponent<SpriteRenderer>();

            startPosition = transform.position;
            desiredPosition = transform.position;
            startSize = ren.size;
            desiredSize = ren.size;
        }

		private void Update() {
            HandlePositionAndSize();
        }

		#endregion

		#region Private Methods

		private void HandlePositionAndSize() {
            float t = (Time.time - startTime) / duration;

			if (VectorUtils.IsCloseEnough(transform.position, desiredPosition, snapThreshold))
            {
                transform.position = desiredPosition;
            } else
            {
                transform.position = VectorUtils.SmoothStep(startPosition, desiredPosition, t);
            }

            if (VectorUtils.IsCloseEnough(ren.size, desiredSize, snapThreshold))
            {
                ren.size = desiredSize;
            } else
            {
				ren.size = VectorUtils.SmoothStep(startSize, desiredSize, t);
            }
        }

		#endregion

		#region Public Methods

        public void SetPosition(Vector2 position, float time) {
            SetShape(position, desiredSize, time);
        }

        public void SetSize(Vector2 size, float time) {
            SetShape(desiredPosition, size, time);
        }

        public void SetShape(Vector2 position, Vector2 size, float time) {
            startPosition = transform.position;
            startSize = ren.size;
            desiredPosition = position;
            desiredSize = size;
            duration = time;
            startTime = Time.time;
        }

        #endregion
    }
}
