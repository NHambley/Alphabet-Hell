using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	Sound[] sounds;
	Sound[] playerSounds;
	int playerSoundCount;

	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad(gameObject);
		foreach (Sound s in sounds)
		{
			s.SetSource(gameObject.AddComponent<AudioSource>());
		}
		PlaySound("Music");
		playerSounds = new Sound[100];
		playerSoundCount = 0;
	}

	// Update is called once per frame
	void Update () {

	}

	public void PlaySound(string soundName)
	{
		bool foundSound = false;
		foreach (Sound s in sounds)
		{
			if (s.name == soundName)
			{
				foundSound = true;
				if (!s.IsPlaying)
					s.Play();
				else
					Debug.Log(soundName + " is already playing");
			}
		}
		if (!foundSound)
		{
			Debug.Log("No sound found for name " + soundName);
		}
	}

	public void Play(string soundName)
	{
		bool foundSound = false;
		foreach (Sound s in sounds)
		{
			if (s.name == soundName)
			{
				foundSound = true;
				//s.Play();
				playerSounds[playerSoundCount] = s;
				playerSounds[playerSoundCount].Play();
				playerSoundCount++;

				if (playerSoundCount > 99)
					playerSoundCount = 0;
			}
		}
		if (!foundSound)
		{
			Debug.Log("No sound found for name " + soundName);
		}
	}
}
