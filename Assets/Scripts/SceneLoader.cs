﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneLoader : MonoBehaviour
{
	public void LoadLevel(string levelName)
    {
        GameStateManager.LoadOut(levelName);
    }
}
