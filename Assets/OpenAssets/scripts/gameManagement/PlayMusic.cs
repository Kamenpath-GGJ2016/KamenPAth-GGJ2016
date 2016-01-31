using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour {
	public AudioSource BackgroundMusic;

	// Use this for initialization
	void Start () {
		if (BackgroundMusic != null)
			SoundManager.Instance.PlayBackgroundMusic (BackgroundMusic);
	}

}
