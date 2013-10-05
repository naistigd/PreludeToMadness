using UnityEngine;
using System.Collections;

// plays background music

// attach an audio component for us
[RequireComponent(typeof(AudioSource))] 
[RequireComponent(typeof(AudioListener))] 
public class BackgroundMusic : MonoBehaviour 
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
		
	}
	//-------------------------------------------------------------------------
	public void SetVolume(float vol)
	{
		if (audioSource_ == null)
		{
			audioSource_ = GetComponent<AudioSource>();
		}		
		audioSource_.audio.volume = vol;	
	}
	//-------------------------------------------------------------------------
	public void Play()
	{
		if (audioSource_ == null)
		{
			audioSource_ = GetComponent<AudioSource>();
		}
		audioSource_.Play();
	}
	//-------------------------------------------------------------------------
	public void Pause()
	{
		if (audioSource_ == null)
		{
			audioSource_ = GetComponent<AudioSource>();
		}
		audioSource_.Stop();
	}
	//-------------------------------------------------------------------------
	public void Loop(bool loop)
	{
		if (audioSource_ == null)
		{
			audioSource_ = GetComponent<AudioSource>();
		}
		audioSource_.loop = loop;
	}
	//-------------------------------------------------------------------------
}
