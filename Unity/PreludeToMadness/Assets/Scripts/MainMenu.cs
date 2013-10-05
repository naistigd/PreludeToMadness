using UnityEngine;
using System.Collections;

// This class handles the main menu. It is in charge of fading
// in and out menu elements and handling buttons and button
// events. In particular it loads the intro scene if the start
// button was clicked or exits the program when exit was clicked.
public class MainMenu : MonoBehaviour 
{
	//-------------------------------------------------------------------------
	//						  MEMBER DEFINITIONS
	//-------------------------------------------------------------------------	
	
	// assets used by the main menu
	public BackgroundTexture BackgroundTex;
	public BackgroundMusic BackgroundMusic;
	public TextureButton StartButton;
	public TextureButton ExitButton;
	
	private bool isStartPressed_ = false;
	
	private Fader buttonFader_;
	private Fader backgFader_;
	private Fader musicFader_;
	
	// measures elapsed time
	private float time_ = 0.0f;

	// menu logic vars
	private bool haveButtonsFadedIn_ = false;
	private bool shallExitMM_ = false;
	
	
	//-------------------------------------------------------------------------
	//						  METHOD DEFINITIONS
	//-------------------------------------------------------------------------
	void Awake()
	{
		// scale and position start and exit button
		
		StartButton.SetScale(0.5f);
		
		float w = StartButton.GetWidth();
		float x = (Screen.width - w)/2.0f;
		float y = Screen.height/3.0f;
		
		StartButton.SetPosition(new Vector2(x, y));
		
		ExitButton.SetScale(0.5f);
		w = ExitButton.GetWidth();
		float h = ExitButton.GetHeight();
		x = (Screen.width - w)/2.0f;
		y += h;
		
		ExitButton.SetPosition(new Vector2(x, y));
		
		// set the fader for the buttons
		buttonFader_ = new Fader(0.0f, 2.0f, 0.5f);	
		backgFader_ = new Fader(0.0f, 5.0f, 5.0f);
		musicFader_ = new Fader(0.0f, 3.0f, 3.0f);
		
		// set buttons unclickable in the beginning
		StartButton.SetIsClickable(false);
		ExitButton.SetIsClickable(false);
	}
	//-------------------------------------------------------------------------
	void Start () 
	{
		// set background fader to fade in state in the beginning
		backgFader_.FadeIn();
		
		// set music fader to fade in state
		musicFader_.FadeIn();
	}
	//-------------------------------------------------------------------------
	void Update () 
	{
		// update time
		time_ += Time.deltaTime;
		
		// update the fader
		buttonFader_.Update(Time.deltaTime);
		backgFader_.Update(Time.deltaTime);
		musicFader_.Update(Time.deltaTime);
		
		// fade buttons in after ...
		if (time_ > 6.0f && !haveButtonsFadedIn_)
		{
			haveButtonsFadedIn_ = true;
			buttonFader_.FadeIn();
		}
		
		// if buttons are fully blended in set them clickable 
		// otherwise set them non clickable
		if (StartButton.GetAlpha() == 1.0f)
		{
			StartButton.SetIsClickable(true);
		}
	
		// if buttons are fully blended in set them clickable 
		// otherwise set them non clickable
		if (ExitButton.GetAlpha() == 1.0f)
		{
			ExitButton.SetIsClickable(true);
		}	
		
		// if the start button was pressed (shallExitMM) and the background and
		// buttons are blended out load the intro
		if (BackgroundTex.GetAlpha() == 0.0f && 
			buttonFader_.GetAlpha() == 0.0f && 
			shallExitMM_
		)
		{
			// Load intro
			Application.LoadLevel(1);				
		}
	}
	//-------------------------------------------------------------------------
	void OnGUI()
	{
		// update alpha values of the gui elements
		BackgroundTex.SetAlpha(backgFader_.GetAlpha());
		StartButton.SetAlpha(buttonFader_.GetAlpha());
		ExitButton.SetAlpha(buttonFader_.GetAlpha());
		
		// update volume
		BackgroundMusic.SetVolume(musicFader_.GetAlpha());
		
		if (StartButton.IsPressed())
		{
			shallExitMM_ = true;
			StartButton.SetIsClickable(false);
			ExitButton.SetIsClickable(false);
			backgFader_.FadeOut();	
			buttonFader_.FadeOut();
			musicFader_.FadeOut();
		}
	}
	//-------------------------------------------------------------------------
}
