using UnityEngine;
using System.Collections;

public class MaskWear : MonoBehaviour, IPlayerRespawnListener {
	public GameObject GroundMask;
	public GameObject FlyingMask;
	public GameObject MaskWearEffect;

	protected int _lastMaskInUse = 0;
	protected Transform _playerTransform;

	// Use this for initialization
	void Start () 
	{
		// we get the player from its tag
		_playerTransform = GameManager.Instance.Player.transform;
		GroundMask = GameObject.Instantiate (GroundMask);
		GroundMask.SetActive (false);
		FlyingMask = GameObject.Instantiate (FlyingMask);
		FlyingMask.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (MaskManager.Instance.MaskInUse) 
		{
		case 0:
			UpdateMasks (GroundMask, FlyingMask, null, false);
			UpdateLastMaskInUse (0);
			break;
		case 1:
			UpdateMasks (GroundMask, FlyingMask, null, true); 
			UpdateLastMaskInUse (1);
			break;
		case 2:
			UpdateMasks (FlyingMask, GroundMask, null, true); 
			UpdateLastMaskInUse (2);
			break;
		default:
			Debug.LogError ("Invalid mask in use " + MaskManager.Instance.MaskInUse);
			break;
		}
	}

	protected void UpdateLastMaskInUse(int newMask)
	{
		if (newMask != _lastMaskInUse) 
		{
			_lastMaskInUse = newMask;
			GameObject.Instantiate (MaskWearEffect, _playerTransform.position, _playerTransform.rotation);
		}
	}

	protected void UpdateMasks(GameObject go1, GameObject go2, GameObject go3, bool firstMaskBool)
	{
		go1.SetActive (firstMaskBool);
		go2.SetActive (false);
		//go3.SetActive (false);
	}

	/// <summary>
	/// When the player respawns, we reinstate the object
	/// </summary>
	/// <param name="checkpoint">Checkpoint.</param>
	/// <param name="player">Player.</param>
	public virtual void onPlayerRespawnInThisCheckpoint(CheckPoint checkpoint, CharacterBehavior player)
	{
		_lastMaskInUse = 0;
		UpdateMasks (GroundMask, FlyingMask, null, false);
	}
}
