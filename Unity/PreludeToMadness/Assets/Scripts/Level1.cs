using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour 
{	
	//-------------------------------------------------------------------------
	//					CLASS MEMBER DECLARATIONS/DEFINITIONS
	//-------------------------------------------------------------------------
	public DialogField DialogField;
	public Patrolling Patrolling;
	public BackgroundMusic BackgroundMusic;
	public Move RatMove;
	public SpriteAnimation RatAnimation;
	public SpriteAnimation GuardAnimation;
	public Sound CellDoorSoundSound;
	public Sound PunchSound;
	public Sound RatSound;
	public Sound ClickSound;
	
	public GameObject IntroBackground; // ugly
	
	private InteractableManager manager;
	private bool isRatKillable = false;
	
	
	delegate void UpdateHandler();
	private UpdateHandler updateHandler_;
	private float time_ = 0.0f;
	private float timeEnd_;
	
	private Fader bgmFader_;
	private Fader introBgFader_;
	
	// game logic stuff
	private bool hasIntroDialogStarted_ = false;
	private bool guardTextStarted_ = false;
	private bool lockAlreadyClicked_ = false;
	private bool guardAlreadyClicked_ = false;
	private bool stoneAlreadyClicked_ = false;
	private bool chocoladeAlreadyClicked_ = false;
	private bool ratHoleAlreadyClicked_ = false;
	private bool punchSoundPlayed = false;
	private bool guardWasAlreadyAngry = false;
	private bool winTextAlreadyShown = false;
	private bool ratBonesAlreadyClicked = false;
	
	//-------------------------------------------------------------------------
	//						CLASS METHOD DEFINITIONS
	//-------------------------------------------------------------------------
	void Awake() 
	{
		manager = GetComponent<InteractableManager>();
		manager.Register(this);
		updateHandler_ = sceneIntro;
		
		// play the cell door sound
		CellDoorSoundSound.Play();
		
		// play background music but set volume to 0.
		// also set the fader for the bgm		
		BackgroundMusic.Play();
		BackgroundMusic.Loop(true);
		BackgroundMusic.SetVolume(0.0f);
		
		bgmFader_ = new Fader(0.0f, 30.0f, 0.0f);
		
		//
		introBgFader_ = new Fader(1.0f, 0.0f, 5.0f);
	}
	//-------------------------------------------------------------------------
	public void Notify(int selected, int clicked)
	{		
		// HANDLE HINTS.
		if (clicked == 4 && !lockAlreadyClicked_ && selected != 1)
		{
			// if lock was clicked first time ...
			lockAlreadyClicked_ = true;
			Color ye = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			
			string text0 = "The keyhole of this lock is unusual in that it seems to aim both ways, inside and out.";
			DialogField.Display(text0, ye, 0.0f, 5.0f);
		}
		
		if (clicked == 6 && !guardAlreadyClicked_)
		{
			// if guard was clicked first time ...
			guardAlreadyClicked_ = true;
			Color ye = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			
			string text0 = "The soldier appears to be in his mid-20's but despite his youth, you sense he is a competent soldier.";
			DialogField.Display(text0, ye, 0.0f, 5.0f);
		}
		
		if (clicked == 0 && !stoneAlreadyClicked_)
		{
			// if stone was clicked first time ...
			stoneAlreadyClicked_ = true;
			Color ye = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			
			string text0 = "You happen upon a stone that has conveniently sharp points.";
			DialogField.Display(text0, ye, 0.0f, 5.0f);
		}
		
		if (clicked == 3 && !chocoladeAlreadyClicked_) 
		{
			chocoladeAlreadyClicked_ = true;
			Color ye = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			string text0 = "The guards have confiscated all of your gear except for this piece of chocolate.";
			DialogField.Display(text0, ye, 0.0f, 5.0f);			
		}
		
		if (clicked == 7 && !ratHoleAlreadyClicked_)
		{
			ratHoleAlreadyClicked_ = true;
			Color ye = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			string text0 = "A small mouse hole is at the bottom of the Western wall.";
			DialogField.Display(text0, ye, 0.0f, 5.0f);				
		}
		
		// pick up rat bones
		if (clicked == 1 && !ratBonesAlreadyClicked)
		{
			ratBonesAlreadyClicked = true;
			Color ye = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			string text0 = "You recall from your training that you can utilize the bones of small creatures to pick locks.";
			DialogField.Display(text0, ye, 0.0f, 3.5f);					
		}		
		
		// put chocolate on the ground
		if (selected == 3 && clicked == 5)
		{
			manager.SetIsInventory(3, false);
			manager.SetPosition(3, new Vector2(-2.2f, - 2.2f));
			manager.SetIsActive(5, false);
			RatAnimation.Play(0.5f, 0);
			RatMove.MoveTo(new Vector2(-3.5f, - 2.0f), 4.0f);
			isRatKillable = true;
		}
		
		// kills the rat
		if (selected == 0 && clicked == 2 && isRatKillable == true)
		{
			RatSound.Play();
			manager.SetIsActive(2, false);
			manager.SetIsActive(1, true);
			manager.SetIsInventory(1, false);
			manager.SetPosition (1, new Vector2(-3.5f, - 2.0f));
			manager.Unselect();
		}
	

		
		// opens the door
		if (selected == 1 && clicked == 4)
		{
			Vector2 posGuard = manager.GetPosition(6);
			
			if (posGuard.x < 5.5f && posGuard.x > -1.0f)
			{
				manager.Unselect();
				RatAnimation.Stop();
				Patrolling.isPatrolling = false;
				GuardAnimation.Stop();
				updateHandler_ = gotCaught;
				timeEnd_ = time_ + 7.0f;
				return;
			}
			
			manager.Unselect();
			timeEnd_ = time_ + 2.0f;
			updateHandler_ = quit;
			manager.Enable(false);		
			Patrolling.isPatrolling = false;
		}
	}
	//-------------------------------------------------------------------------
	void Update()
	{
		// STATE INDEPENDENT UPDATES
		
		// update time
		time_ += Time.deltaTime;
		
		// update the bgm fader
		bgmFader_.Update(Time.deltaTime);
		
		// update vol of the bgm according to its fader
		if (bgmFader_.GetAlpha() != 1.0f)
		{
			BackgroundMusic.SetVolume(0.5f*bgmFader_.GetAlpha());
		}
			
		// STATE SPECIFIC UPDATES
		updateHandler_();
	}
	//-------------------------------------------------------------------------	
	void OnGUI()
	{
		
	}
	//-------------------------------------------------------------------------
	private void sceneIntro()
	{
		
		// update intro bg fader and fade the bg accordingly; UGLY
		introBgFader_.Update(Time.deltaTime);
		IntroBackground.renderer.material.color = 
			new Color(0.0f, 0.0f, 0.0f, introBgFader_.GetAlpha());
		
		// if the time is greater then 8.0 sec start playing the intro
		// dialog
		if (time_ > 8.0f && !hasIntroDialogStarted_)
		{
			hasIntroDialogStarted_ = true;
			Color yellow = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			
			string text1 = "You awaken with a start as the guard slams the cold iron bars shut.";
			string text2 = " He regards you with a look of pity and disgust.";
			
			DialogField.Display(text1, yellow, 0.0f, 5.0f);
			DialogField.Display(text2, yellow, 5.5f, 8.5f);
			
			// fade in bgm
			bgmFader_.FadeIn();
		}
		
		// fade in the actaul scene (fade out intro background)
		if (time_ > 14.0f)
		{
			introBgFader_.FadeOut();
		}
		
		// after 17 sec switch to guard dialog state
		if (time_ > 20.0f)
		{
			IntroBackground.SetActive(false);	
			updateHandler_ = guardDialog;
		}
	}	
	//-------------------------------------------------------------------------
	private void guardDialog()
	{
		// THIS FUNCTION HANDLES THE BEGINNING OF THE SCENE WHERE THE GUARD
		// IS TALKING TO THE PLAYER
		
		// while talking the guard is not patrolling 
		Patrolling.isPatrolling = false;
		
		// the guard says the following ...
		string text = "Don't move from that spot.";
		string text2 = "If you do, you will regret it. I promise you that.";
		
		if (!guardTextStarted_)
		{
			manager.Enable(false);
			BackgroundMusic.SetVolume(0.5f);
			guardTextStarted_ = true;
			Color red = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			DialogField.Display(text, red, 0.0f, 2.0f);	
			DialogField.Display(text2, red, 2.5f, 4.5f);	
		}
		
		// switch to patrolling state
		if (time_ >= 24.7f)
		{
			GuardAnimation.Play(1.0f, 0);
			manager.Enable(true);
			BackgroundMusic.SetVolume(0.5f);
			Patrolling.isPatrolling = true;
			updateHandler_ = guardPatrolling;
		}
	}
	//-------------------------------------------------------------------------
	private void guardPatrolling()
	{
		Vector2 posGuard = manager.GetPosition(6);
		
		// choose right animation for the guard
		if (Patrolling.IsGoingLeft())
		{
			GuardAnimation.Play(1.0f, 1);
		}
		else
		{
			GuardAnimation.Play(1.0f, 0);
		}
		
		// if the guard is visible
		if (posGuard.x < 5.5f && posGuard.x > -1.0f)
		{
			// and chocolate is not in the inventory
			if ((!manager.IsInInventory(3) && manager.IsActive(3)) || 
				(!manager.IsInInventory(1) && manager.IsActive(1)))
			{
				RatAnimation.Stop();
				Patrolling.isPatrolling = false;
				GuardAnimation.Stop();
				updateHandler_ = gotCaught;
				timeEnd_ = time_ + 7.0f;
			}
		}
		
		// stop the rat animations when it arrived at its destination
		if (isRatKillable && !RatMove.IsMoving())
		{
			RatAnimation.Stop();
		}
		
	}
	//-------------------------------------------------------------------------
	private void gotCaught()
	{
		if (!guardWasAlreadyAngry)
		{
			guardWasAlreadyAngry = true;
			string text = "The guard wastes no time in administering his punishment.";
			string text1 = "He advances on you and knocks you out.";
			Color yellow = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			DialogField.Display(text, yellow, 0.0f, 2.0f);
			DialogField.Display(text1, yellow, 2.5f, 4.0f);
		}
		
		if (time_ >= (timeEnd_ - 4.5f) && !punchSoundPlayed)
		{
			punchSoundPlayed = true;
			PunchSound.Play();
		}
		
		
		if (time_ >= timeEnd_)
		{
			Application.LoadLevel(5);
		}
	}
	//-------------------------------------------------------------------------
	private void quit()
	{
		if (!winTextAlreadyShown)
		{
			ClickSound.Play();
			string text = "You hear a satisfying click as you fiddle with the lock";
			string text1 = "and the cell door swings slightly ajar.";
			Color yellow = new Color(1.0f, 1.0f, 0.0f, 1.0f);
			DialogField.Display(text, yellow, 0.0f, 2.0f);
			DialogField.Display(text1, yellow, 2.5f, 4.0f);
			winTextAlreadyShown = true;
		}
		
		if (time_ >= timeEnd_)
		{
			Application.LoadLevel(4);
		}
	}
}
