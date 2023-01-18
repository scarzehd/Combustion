using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Battle
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

		private AudioSource audioSource;

		private void Start() {
			Instance = this;

			audioSource = GetComponent<AudioSource>();
		}

		public void PlaySound(AudioClip clip) {
			audioSource.PlayOneShot(clip);
		}
	}
}
