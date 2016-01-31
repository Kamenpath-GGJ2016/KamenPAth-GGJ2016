using UnityEngine;
using System.Collections;

public class MaskSensor : MonoBehaviour 
{
	public bool Scared = false;
	public int ScaryMask = 1;

	protected float _lastScaredUpdate;
	protected float _scaredUpdateInterval = 0.5f;

	void Start()
	{
		_lastScaredUpdate = Time.time;
	}

	/// <summary>
	/// triggered when the character stays inside of a trigger collider
	/// </summary>
	/// <param name="collider">the object we're colliding with.</param>
	protected virtual void OnTriggerStay2D(Collider2D collider)
	{
		if (CanChangeScared() && collider.name.Equals ("MaskRadius")) 
		{
			Scared = ScaryMask == MaskManager.Instance.MaskInUse;
			_lastScaredUpdate = Time.time;
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.name.Equals ("MaskRadius")) 
		{
			Scared = false;
			_lastScaredUpdate = Time.time;
		}
	}

	protected bool CanChangeScared()
	{
		return _lastScaredUpdate + _scaredUpdateInterval < Time.time;
	}
}
