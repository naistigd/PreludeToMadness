using UnityEngine;
using System.Collections;


// A script for playing a sound

[RequireComponent(typeof(AudioSource))] 
public class Sound : MonoBehaviour 
{
	//-------------------------------------------------------------------------
	//					CLASS MEMBER DECLARATIONS/DEFINITIONS
	//-------------------------------------------------------------------------
	private AudioSource audioSource_;
	
	//-------------------------------------------------------------------------
	//						CLASS METHOD DEFINITIONS
	//-------------------------------------------------------------------------
	void Awake()
	{
		audioSource_ = GetComponent<AudioSource>();
	}
	//-------------------------------------------------------------------------
	public void SetVolume(float vol)
	{
		audioSource_.audio.volume = vol;	
	}
	//-------------------------------------------------------------------------
	public void Play()
	{
		audioSource_.Play();
	}
	//-------------------------------------------------------------------------
	public void Pause()
	{
		audioSource_.Stop();
	}
	//-------------------------------------------------------------------------
	public void Loop(bool loop)
	{
		audioSource_.loop = loop;
	}
	//-------------------------------------------------------------------------
}