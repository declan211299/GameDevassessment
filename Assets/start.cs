using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "GameScene";
    public string tutorialSceneName = "TutorialScene";

    public Button startButton;
    public Button quitButton;
    public Button tutorialButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        tutorialButton.onClick.AddListener(StartTutorial);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("fly over");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
