using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackgroundPlayer{
	[CreateAssetMenu(menuName = "Background Music")]
	public class BackgroundMusicData : ScriptableObject
	{
		[SerializeField] private AudioClip song;
		[Range(0,2)]
		[SerializeField] private float pitch = 1;

		public void PlaySong()
        {
			BackgroundMusicPlayer.PlaySong(song);
			BackgroundMusicPlayer.SetPitch(pitch);
        }
	}
}