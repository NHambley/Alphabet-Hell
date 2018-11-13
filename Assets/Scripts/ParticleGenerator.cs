using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SPRITE
{
    FIRE,
    SPARK,
    WATER,
    DIRT,
    METAL,
    ROCK,
    FIRE1,
    FIRE2,
    FIRE3
};

public class ParticleGenerator : MonoBehaviour {

    public GameObject particlePrefab;
    
    public Sprite[] particleSprites;

    List<GameObject> particles = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = particles.Count - 1; i >= 0; i--)
        {
            if (particles[i].GetComponent<ParticleScript>().isDead)
            {
                Destroy(particles[i]);
                particles.RemoveAt(i);
            }
        }
	}

    public void GenerateParticles(SPRITE type, int number, Vector3 pos, Vector3 approxVel, Vector3 approxScale, float radiusInDegrees, float timeout, float zIndex)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject newParticle = Instantiate(particlePrefab);
            if ((int)type < number)
            {
                newParticle.GetComponent<ParticleScript>().SetSprite(particleSprites[(int)type]);
                newParticle.GetComponent<ParticleScript>().timeout = timeout;
                pos.z = zIndex;
                newParticle.transform.position = pos;
                newParticle.transform.localScale = approxScale * Random.Range(0.8f, 1.2f);
                float angle = Mathf.Rad2Deg * (Mathf.Atan2(approxVel.normalized.y, approxVel.normalized.x)) + (radiusInDegrees * Random.Range(0f, 1f) - radiusInDegrees / 2);
                newParticle.GetComponent<ParticleScript>().velocity = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * approxVel.magnitude * Random.Range(0.9f, 1.1f);
                particles.Add(newParticle);
            }
        }
    }
}
