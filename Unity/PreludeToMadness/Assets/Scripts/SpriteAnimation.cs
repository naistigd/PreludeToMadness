using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour 
{
	//-------------------------------------------------------------------------
	//					CLASS MEMBER DECLARATIONS/DEFINITIONS
	//-------------------------------------------------------------------------
	public Texture TextureAtlas;
	public int NumFrames;
	public int NumAnimations = 1;
	
	private float tilingX_;
	private float tilingY_;
	private float offX_ = 0.0f;
	private float offY_ = 0.0f;
	
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
		tilingY_ = 1.0f/((float)(NumAnimations));
		this.renderer.material.mainTexture = TextureAtlas;
		this.renderer.material.mainTextureScale = new Vector2(tilingX_, tilingY_);
	}
	//-------------------------------------------------------------------------
	void Update () 
	{
		if (!this.isAnimating_)
		{
			return;
		}
		
		// set offset according to the current frame
		this.renderer.material.mainTextureOffset = new Vector2(currentFrame_*tilingX_, offY_);
		
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
	public void Play(float aniPeriod, int animation)
	{
		offY_ = animation*tilingY_;
		
		
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
