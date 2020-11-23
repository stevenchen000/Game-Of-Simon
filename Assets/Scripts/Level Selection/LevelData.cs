using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Level Data")]
public class LevelData : ScriptableObject
{
	public string levelName;
	public Sprite background;
	public AudioClip music;
}
