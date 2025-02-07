using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;
public class PlaySoundRandomly : MonoBehaviour
{
	public Sound[] sounds;

	void Awake() {
			foreach (Sound s in sounds) {
			s.source=gameObject.AddComponent<AudioSource>();
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
			s.source.clip = s.audioClip;
		}
	}

	public void PlayRandomSound() {
		Play(sounds[Random.Range(0, sounds.Length)].name);
	}

	private void Play(string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
		{
				return;
		}
		s.source.Play();
	}
}
