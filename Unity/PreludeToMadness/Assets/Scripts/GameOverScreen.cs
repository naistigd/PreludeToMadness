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
		AgainButton.SetScale(0.25f);
		ExitButton.SetScale(0.25f);
		
		float off = 50;
		
		Vector2 pos = new Vector2();
		pos.x = Screen.width/2.0f - (AgainButton.GetWidth() + ExitButton.GetWidth() + 50)/2.0f;
		pos.y = 40.0f;
		
		AgainButton.SetPosition(pos);
		
		// position the exit button

		pos.x = Screen.width/2.0f + (ExitButton.GetWidth())/2.0f;
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
			Application.LoadLevel(3);
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
		
		//BackgroundImage.SetAlpha(fader_.GetAlpha());
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

