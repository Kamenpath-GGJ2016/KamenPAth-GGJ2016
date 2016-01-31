using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI_MenuManager : MonoBehaviour
{

    public GameObject Btn_Start;
    public GameObject Btn_Credits;
    public GameObject Btn_Quit;

    //  private UIScreen currentScreen;
    public GameObject CreditsCanvas;
    public GameObject ThisCanvas;

    // Use this for initialization
    void Start()
    {
        //        currentScreen = UIScreen.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        //Enable buttons for main menu only
            Btn_Start.SetActive(true);
            Btn_Credits.SetActive(true);
            Btn_Quit.SetActive(true);
    }
    public void Btn_CreditsClicked()
    {

        if (CreditsCanvas)
        {
            CreditsCanvas.SetActive(true);
            if (ThisCanvas)
            {
                ThisCanvas.SetActive(false);
            }

        }


    }


    public void Btn_QuitClicked()
    {
        Application.Quit();
    }
    public void MenuButtonClicked()
    {
        if (CreditsCanvas)
        {
            CreditsCanvas.SetActive(true);
            if (ThisCanvas)
            {
                ThisCanvas.SetActive(false);
            }
        }
    }

    public void Btn_StartClicked()
    {
		SceneManager.LoadScene("TestFase");
    }
}
/*public enum UIScreen
{
    MainMenu,
    Credits

}*/