using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour 
{
	//-------------------------------------------------------------------------
	//					CLASS MEMBER DECLARATIONS/DEFINITIONS
	//-------------------------------------------------------------------------
	public TextureButton AgainButton;
	public TextureButton ExitButton;
	public BackgroundTexture BackgroundImage;
	
	private delegate void UpdateHandler();
	private UpdateHandler update_;
	
	private Fader fader_;
	
	//-------------------------------------------------------------------------
	//						CLASS METHOD DEFINITIONS
	//-------------------------------------------------------------------------
	void Awake() 
	{
		// position the again button
		AgainButton.SetScale(0.5f);
		
		Vector2 pos = new Vector2();
		pos.x = Screen.width/4.0f - AgainButton.GetWidth()/2.0f;
		pos.y = 40.0f;
		
		AgainButton.SetPosition(pos);
		
		// position the exit button
		ExitButton.SetScale(0.5f);
		
		pos.x = Screen.width/4.0f*3.0f - ExitButton.GetWidth()/2.0f;
		pos.y = 40.0f;
		
		ExitButton.SetPosition(pos);
		
		// set the fading parameters for the beginning fade in of the game
		// over screen
		fader_ = new Fader(0.0f, 3.0f, 3.0f);
		fader_.FadeIn();
		
		// set the initial update state to fade in
		update_ = fadeIn;
	}
	//-------------------------------------------------------------------------
	void Update() 
	{	
		update_();
	}
	//-------------------------------------------------------------------------
	void OnGUI()
	{
		if (AgainButton.IsPressed())
		{
			Application.LoadLevel(2);
		}
		
		if (ExitButton.IsPressed()) 
		{
			Application.Quit();	
		}
	}
	//-------------------------------------------------------------------------
	private void fadeIn()
	{
		fader_.Update(Time.deltaTime);
		
		BackgroundImage.SetAlpha(fader_.GetAlpha());
		AgainButton.SetAlpha(fader_.GetAlpha());
		ExitButton.SetAlpha(fader_.GetAlpha());
		
		
		// if everything was faded in set update state to do nothing
		if (fader_.GetAlpha() == 1.0f)
		{
			update_ = doNothing;
		}
	}
	//-------------------------------------------------------------------------
	private void doNothing()
	{
		
	}
	//-------------------------------------------------------------------------
}

