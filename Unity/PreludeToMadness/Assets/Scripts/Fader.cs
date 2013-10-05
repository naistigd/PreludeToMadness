// Utility class for making fading more easy. Updates, if [Update] was called, 
// a blending value,according to its state ("fade in", "fade out", "do nothing")
// according thefade in or fade out time and the time past as specified by dt in 
// the [Update] method. State changes may occur automatically if the blending 
// value reaches 0 or 1 or explicitly if the [FaceIn] or [FadeOut] functions
// were called.
public class Fader
{
	float alpha_ = 0.0f;
	float fadeInTime_ = 0.0f;
	float fadeOutTime_ = 0.0f;
	float deltaTime_;
	delegate void UpdateAlpha();
	UpdateAlpha updateAlpha_;
	
	public Fader(float alpha, float fadeInTime, float fadeOutTime)
	{
		alpha_ = alpha;
		fadeInTime_ = fadeInTime;
		fadeOutTime_ = fadeOutTime;
		updateAlpha_ = doNothing;
	}
	
	public float GetAlpha()
	{
		return alpha_;
	}
	
	public void Update(float dt)
	{		
		deltaTime_ = dt;
		updateAlpha_();
	}

	public void FadeIn()
	{
		updateAlpha_ = fadeIn;
	}
	
	public void FadeOut()
	{
		updateAlpha_ = fadeOut;
	}
	
	private void doNothing()
	{
		
	}
	//-------------------------------------------------------------------------
	private void fadeOut()
	{
		if (fadeOutTime_ == 0.0f)
		{
			alpha_ = 0.0f;
			updateAlpha_ = doNothing;
			return;
		}
		
		alpha_ -= deltaTime_/fadeOutTime_;	
		
		if (alpha_ < 0.0f) 
		{
			alpha_ = 0.0f;
			updateAlpha_ = doNothing;
		}		
	}
	//-------------------------------------------------------------------------
	private void fadeIn()
	{
		if (fadeInTime_ == 0.0f)
		{
			alpha_ = 1.0f;
			updateAlpha_ = doNothing;
			return;
		}
		
		alpha_ += deltaTime_/fadeInTime_;
		
		if (alpha_ >= 1.0f) 
		{
			alpha_ = 1.0f;		
			updateAlpha_ = doNothing;
		}		
	}
}


