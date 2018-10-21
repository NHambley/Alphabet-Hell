using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Background
{
    public Vector3 speed;
    public Sprite sprite;
    public int zIndex;
}

public class LevelScript : MonoBehaviour {
    public Background[] backgrounds;
    public GameObject enemy;
    public float enemySpawnTime;
    public int numOfEnemies;
}
