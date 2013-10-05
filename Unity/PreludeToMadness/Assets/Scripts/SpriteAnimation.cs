using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour 
{
	//-------------------------------------------------------------------------
	//					CLASS MEMBER DECLARATIONS/DEFINITIONS
	//-------------------------------------------------------------------------
	public Texture TextureAtlas;
	public int NumFrames;
	
	private float tilingX_;
	private float offX_ = 0.0f;
	
	private float time_ = 0.0f;
	private float aniPeriod_ = 0.5f;
	private bool isAnimating_ = false;
	private int currentFrame_ = 0;
	
	//-------------------------------------------------------------------------
	//						CLASS METHOD DEFINITIONS
	//-------------------------------------------------------------------------
	void Awake()
	{
		tilingX_ = 1.0f/((float)(NumFrames));
		this.renderer.material.mainTexture = TextureAtlas;
		this.renderer.material.mainTextureScale = new Vector2(tilingX_, 1.0f);
	}
	//-------------------------------------------------------------------------
	void Update () 
	{
		if (!this.isAnimating_)
		{
			return;
		}
		
		// set offset according to the current frame
		this.renderer.material.mainTextureOffset = new Vector2(currentFrame_*tilingX_, 0.0f);
		
		// compute the next current frame
		float framePeriod = aniPeriod_/((float)(NumFrames));
		time_ += Time.deltaTime;
		
		currentFrame_ = (((int)(time_/framePeriod)) % NumFrames);
		
		
		// reset time if possible
		if (time_ >= aniPeriod_)
		{
			time_ -= aniPeriod_;
		}
	}
	//-------------------------------------------------------------------------
	public void Play(float aniPeriod)
	{
		this.isAnimating_ = true;
		this.aniPeriod_ = aniPeriod;
	}
	//-------------------------------------------------------------------------
	public void Stop()
	{
		isAnimating_ = false;
	}
	//-------------------------------------------------------------------------
}
