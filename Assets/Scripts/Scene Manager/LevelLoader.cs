using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    public Animator transitionAnimator;
    public GameObject loading;
    public Image img;
    public SpriteRenderer spriteA;

    bool noInput = false;

    FMOD.Studio.EventInstance movementUI;



    private void Start()
    {

        noInput = false;

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        /*
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        */
    }

    void Update()
    {
        //Pasar la pantalla de titulo
        if(Input.anyKeyDown && SceneManager.GetActiveScene().name == "TitleScreen" && !noInput)
        {
            LoadScene("mainMenu");
            PlayMovementUIEvent();
        }

        img.sprite = spriteA.sprite;
    }

    public void LoadScene(string _name)
    {
        if (!noInput)
        {
            noInput = true;
            StartCoroutine(LoadSceneTransition(_name));
        }
    }

    IEnumerator LoadSceneTransition(string _name)
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);
        if (_name == "Game")
        {
            loading.SetActive(true);
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(_name);
        yield return new WaitUntil(() => operation.isDone);
        transitionAnimator.SetTrigger("End");
        loading.SetActive(false);
        noInput = false;
    }

    public void PlayMovementUIEvent()
    {
        movementUI = FMODUnity.RuntimeManager.CreateInstance("event:/UI/startGame");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(movementUI, transform);
        movementUI.start();
        movementUI.release();
    }
}
