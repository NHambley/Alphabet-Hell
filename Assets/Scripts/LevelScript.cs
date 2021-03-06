﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Background
{
    public Vector3 speed;
    public Sprite sprite;
    public int zIndex;
    public Vector3 scale;
}

public class LevelScript : MonoBehaviour {
    public Background[] backgrounds;
    public GameObject enemy;
    public GameObject boss;
    public float enemySpawnTime;
    public int numOfEnemies;
}
