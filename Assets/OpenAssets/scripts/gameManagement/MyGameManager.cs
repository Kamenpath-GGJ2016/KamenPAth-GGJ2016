using UnityEngine;
using System.Collections;
/// <summary>
/// The game manager is a persistent singleton that handles points and time
/// </summary>
public class MyGameManager : PersistentSingleton<MyGameManager>
{		
	/// the current number of game points
	public int Points { get; private set; }
	/// the current time scale
	public float TimeScale { get; private set; }
	/// true if the game is currently paused
	public bool Paused { get; set; } 
	/// true if the player is not allowed to move (in a dialogue for example)
	public bool CanMove=true;
	/// the current player
	public CharacterBehavior Player { get; set; }

	// storage
	protected float _savedTimeScale=1f;

	/// <summary>
	/// this method resets the whole game manager
	/// </summary>
	public virtual void Reset()
	{
		Points = 0;
		TimeScale = 1f;
		Paused = false;
		CanMove=false;
		MyGUIManager.Instance.RefreshPoints ();
	}	

	/// <summary>
	/// Adds the points in parameters to the current game points.
	/// </summary>
	/// <param name="pointsToAdd">Points to add.</param>
	public virtual void AddPoints(int pointsToAdd)
	{
		Points += pointsToAdd;
		MyGUIManager.Instance.RefreshPoints ();
	}

	/// <summary>
	/// use this to set the current points to the one you pass as a parameter
	/// </summary>
	/// <param name="points">Points.</param>
	public virtual void SetPoints(int points)
	{
		Points = points;
		MyGUIManager.Instance.RefreshPoints ();
	}

	/// <summary>
	/// sets the timescale to the one in parameters
	/// </summary>
	/// <param name="newTimeScale">New time scale.</param>
	public virtual void SetTimeScale(float newTimeScale)
	{
		_savedTimeScale = Time.timeScale;
		Time.timeScale = newTimeScale;
	}

	/// <summary>
	/// Resets the time scale to the last saved time scale.
	/// </summary>
	public virtual void ResetTimeScale()
	{
		Time.timeScale = _savedTimeScale;
	}

	/// <summary>
	/// Pauses the game or unpauses it depending on the current state
	/// </summary>
	public virtual void Pause()
	{	
		// if time is not already stopped		
		if (Time.timeScale>0.0f)
		{
			Instance.SetTimeScale(0.0f);
			Instance.Paused=true;
			MyGUIManager.Instance.SetPause(true);
		}
		else
		{
			UnPause();
		}		
	}

	/// <summary>
	/// Unpauses the game
	/// </summary>
	public virtual void UnPause()
	{
		Instance.ResetTimeScale();
		Instance.Paused = false;
		if (MyGUIManager.Instance!= null)
		{ 
			MyGUIManager.Instance.SetPause(false);
		}
	}

	/// <summary>
	/// Freezes the character.
	/// </summary>
	public virtual void FreezeCharacter()
	{
		Player.SetHorizontalMove(0);
		Player.SetVerticalMove(0);
		Instance.CanMove=false;
	}		
}
