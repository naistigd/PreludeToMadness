using UnityEngine;
using System.Collections;

// Represents a fullscreen background image that can be faded in and faded out
// at any time. Time values are in seconds.
public class ImageSequenceElement : MonoBehaviour
{
	//-------------------------------------------------------------------------
	//						  MEMBER DEFINITIONS
	//-------------------------------------------------------------------------
	public Texture BackgroundImage = null;
	public float FadeInTime = 0.0f;
	public float FadeOutTime = 1.0f;
	public float FadeInPeriod = 0.5f;
	public float FadeOutPeriod = 0.5f;
	public float Alpha = 0.0f;
	public float GlobalAlpha = 1.0f; // used for fade out; DIRTY, whole intro level is FULL OF DIRT!!
	
	
	// NOTE: depth is important when mulitple texutures are supposed to be 
	// drown on top of each other. the texture with the lower depth is drawn 
	// on top of the texture with heigher depth. 
	public int Depth = 1;
	
	private float time_ = 0.0f;
	
	delegate void UpdateAlpha();
	UpdateAlpha updateAlpha_;
	
	//-------------------------------------------------------------------------
	//						  METHOD DEFINITIONS
	//-------------------------------------------------------------------------		
	void Awake()
	{
		updateAlpha_ = waitFadeIn;
	}	
	
	void Update()
	{
		time_ += Time.deltaTime;
		updateAlpha_();
	}
	
	void Start()	
	{

	}
	//-------------------------------------------------------------------------
	void OnGUI() 
	{
		// DRAW IMAGE ACCORDING TO LOCAL AND GLOBAL ALPHA.
		
		if (Alpha <= 0.0f)
		{
			return;
		}
		
		Rect r = new Rect(0, 0, Screen.width, Screen.height);
		GUI.color = new Color(1.0f, 1.0f, 1.0f, Alpha*GlobalAlpha);
		GUI.depth = Depth;
		GUI.DrawTexture(r, BackgroundImage);
		
		if (BackgroundImage == null)
		{
			Debug.Log(this.name);
		}
	}
	//-------------------------------------------------------------------------
	void waitFadeIn()
	{		
		if (time_ >= FadeInTime && time_ <= FadeOutTime)
		{
			Alpha = 0.0f;
			updateAlpha_ = fadeIn;
		}		
	}
	//-------------------------------------------------------------------------
	void waitFadeOut()
	{		
		if (time_ > FadeOutTime)
		{
			updateAlpha_ = fadeOut;	
			return;
		}
		
	}
	//-------------------------------------------------------------------------
	void fadeIn()
	{
		// handle the case where the fadein time is zero (or negative)
		if (FadeInPeriod <= 0.0f)
		{
			Alpha = 1.0f;
			updateAlpha_ = waitFadeOut;	
			return;
		}
		
		float timePast = time_ - FadeInTime;
		Alpha = timePast/FadeInPeriod;
	
		// consider the possibility of fading out while fading in
		if (time_ > FadeOutTime)
		{
			updateAlpha_ = fadeOut;	
			return;
		}
		
		if (timePast > FadeInPeriod)
		{
			Alpha = 1.0f;
			updateAlpha_ = waitFadeOut;
		}	
	}
	//-------------------------------------------------------------------------
	void fadeOut()
	{			
		// handle the case where the fadeout time is zero (or negative)
		if (FadeOutPeriod <= 0.0f)
		{
			Alpha = 0.0f;
			updateAlpha_ = doNothing;
			return;
		}
		
		float timePast = time_ - FadeOutTime; 		
		Alpha = 1.0f - timePast/FadeOutPeriod;
		
		if (timePast > FadeOutPeriod)
		{
			Alpha = 0.0f;
			updateAlpha_ = doNothing;
		}	
	}
	//-------------------------------------------------------------------------	
	void doNothing()
	{
		
	}
	//-------------------------------------------------------------------------
	public bool IsIdle()
	{
		return updateAlpha_ == doNothing;
	}
	//-------------------------------------------------------------------------
}
