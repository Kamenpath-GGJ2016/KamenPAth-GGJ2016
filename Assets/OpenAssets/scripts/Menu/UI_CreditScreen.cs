using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI_CreditScreen : MonoBehaviour {

    public GameObject Btn_CloseCredits;
    public GameObject ThisCanvas;
    public GameObject ReturnCanvas;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void Btn_CloseCreditsClicked()
    {

        if (ReturnCanvas)
        {
           ReturnCanvas.SetActive(true);
           if (ThisCanvas)
           {
              ThisCanvas.SetActive(false);
           }

        }
    }

	public void Btn_StartClicked()
	{
		SceneManager.LoadScene("Start");
	}

}
