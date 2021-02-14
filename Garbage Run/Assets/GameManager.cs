using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager sharedInstance { get { return _instance; } }

    public float timerLength;
    int score;

    Timer timer;
    GarbageTruck scoreKeeper;
    //Timer time;

    public bool gamePlaying = false;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public GameObject pauseMenu;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 0;
        timer = GetComponent<Timer>();
        timer.timeRemaining = timerLength;
        StartCoroutine(StartTimer());
        scoreKeeper = GameObject.FindGameObjectWithTag("Garbage Truck").GetComponent<GarbageTruck>();
    }

    void Update()
    {
        Debug.Log(Time.timeScale);
        timeText.text = "Time: " + timer.time;

        score = scoreKeeper.points;
        scoreText.text = "Score: " + score;
    }

    IEnumerator StartTimer()
    {
        Debug.Log("CoRoutine Started");

        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1;
        timer.timerIsRunning = true;
        gamePlaying = true;

    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (gamePlaying && context.performed)
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gamePlaying = false;
            Time.timeScale = 0;
        }
        else if (!gamePlaying && context.performed)
        {
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gamePlaying = true;
            Time.timeScale = 1;
        }
    }
}
