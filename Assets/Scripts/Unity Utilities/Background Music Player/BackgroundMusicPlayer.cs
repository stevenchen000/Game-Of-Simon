using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BackgroundPlayer
{
	public class BackgroundMusicPlayer : MonoBehaviour
	{
		private static BackgroundMusicPlayer player;
		private AudioSource source;

		void Awake()
		{
			if (player == null)
			{
				player = this;
				source = transform.GetComponent<AudioSource>();
			}
            else
			{
				Destroy(this);
			}
		}

		public static void PlaySong(AudioClip song)
        {
			player.source.clip = song;
			player.source.Play();
        }

		public static void SetPitch(float pitch)
        {
			player.source.pitch = pitch;
        }
	}
}