using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour 
{

	//-------------------------------------------------------------------------
	//						  MEMBER DEFINITIONS
	//-------------------------------------------------------------------------		
	
	public BackgroundMusic Music;
	public GUISkin SkipGUISkin;
	public float SkipAfter = 40.0f;
	public int Depth = 0;
	public float SkipFadeOut = 2.0f;
	
	public ImageSequenceElement[] ImageSequenceElements;
	
	public float time_ = 0.0f;
	private bool isEnd_ = false;
	
	private Fader musicFader_;
	private Fader imseqFader_;

	
	//-------------------------------------------------------------------------
	//						  METHOD DEFINITIONS
	//-------------------------------------------------------------------------		
	void Start() 
	{
		musicFader_ = new Fader(0.0f, 10.0f, 4.0f);
		musicFader_.FadeIn();
		imseqFader_ = new Fader(1.0f, 0.0f, 4.0f);
	}
	//-------------------------------------------------------------------------
	void Update() 
	{
		if (isEnd_)
		{
			musicFader_.FadeOut();
			imseqFader_.FadeOut();
			
			// if everything has faded out, load the new level
			if (musicFader_.GetAlpha() == 0.0f && imseqFader_.GetAlpha() == 0.0f)
			{				
				Application.LoadLevel(3);
			}
		}
		
		musicFader_.Update(Time.deltaTime);
		imseqFader_.Update(Time.deltaTime);
		
		foreach (ImageSequenceElement e in ImageSequenceElements)
		{
			if (e != null)
			{
				e.GlobalAlpha = imseqFader_.GetAlpha();
			}
		}
		
		time_ += Time.deltaTime;
	}
	//-------------------------------------------------------------------------
	void OnGUI()
	{
		Music.SetVolume(musicFader_.GetAlpha());
		
		if (isEnd_) 
		{
			return;
		}
		
		if (time_ >= SkipAfter)
		{
			isEnd_ = true;
			return;
		}
		
		// position skip button and display it
		float w = Screen.width/6;
		float h = Screen.height/9;
		float x = 0.7f*((float)(Screen.width));
		float y = 0.1f*((float)(Screen.height));
		SkipGUISkin.button.fontSize = (int)(h*0.5f);
		
		GUI.skin = SkipGUISkin;
		GUI.depth = Depth;
		Rect r = new Rect(x, y, w, h);

		if (GUI.Button(r, "skip"))
		{
			isEnd_ = true;
		}
	}
	//-------------------------------------------------------------------------
}
