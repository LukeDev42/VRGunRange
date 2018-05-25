using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Button play;
    public Button controls;
    public Button back;
    public Button quit;
    public Text controllerExplination;

    private void Start()
    {
        play.onClick.AddListener(PlayClicked);
        controls.onClick.AddListener(ControlsClicked);
        quit.onClick.AddListener(QuitClicked);
        back.onClick.AddListener(BackClicked);

        back.gameObject.SetActive(false);
        controllerExplination.enabled = false;
    }

    void PlayClicked()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    void ControlsClicked()
    {
        play.gameObject.SetActive(false);
        controls.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);

        back.gameObject.SetActive(true);
        controllerExplination.enabled = true;
    }

    void BackClicked()
    {
        play.gameObject.SetActive(true);
        controls.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);

        back.gameObject.SetActive(false);
        controllerExplination.enabled = false;
    }

    void QuitClicked()
    {
        Application.Quit();
    }
}
