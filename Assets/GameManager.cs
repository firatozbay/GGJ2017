using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public enum GameState { None= -1, Pregame = 0, Ingame = 1, PostLevel = 2, PostGame = 4,PreBossLevel=5,BossLevel=6,YouWin=7 }
    public GameState gamestate;
    private float levelTimer;

    public const float PREGAME_TIME = 3;
    public const float POSTLEVEL_TIME = 15;
    public const float POSTGAME_TIME = 15;
    public float[] LevelTime;
    public GameObject[] levels;
    private int currentLevel;

    public Text CounterText;
    public Text TimerText;
    public Text WaveText;

    public Button PlayButton;
    public Button MainMenuButton;
    
    private bool[] counts;
    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        counts = new bool[3];
        LevelTime = new float[5];
        LevelTime[0] = 15;
        LevelTime[1] = 20;
        LevelTime[2] = 20;
        LevelTime[3] = 30;
        LevelTime[4] = 40;

        gamestate = GameState.None;
        currentLevel = 0;
        PlayAnimation(currentLevel+1);
	}
	
	// Update is called once per frame
	void Update () {
        if(gamestate == GameState.Pregame)
        {
            levelTimer -= Time.deltaTime;
            if (counts[0] && levelTimer > 2)
            {
                counts[0] = false;
                CounterText.gameObject.SetActive(false);
                CounterText.text = "3";
                CounterText.gameObject.SetActive(true);
            }
            else if (counts[1] && levelTimer < 2)
            {
                counts[1] = false;
                CounterText.gameObject.SetActive(false);
                CounterText.text = "2";
                CounterText.gameObject.SetActive(true);
            }
            else if (counts[2] && levelTimer < 1)
            {
                counts[2] = false;
                CounterText.gameObject.SetActive(false);
                CounterText.text = "1";
                CounterText.gameObject.SetActive(true);

            }
            if (levelTimer < 0)
            {
                //GO!
                CounterText.gameObject.SetActive(false);
                CounterText.text = "SURVIVE!";
                CounterText.gameObject.SetActive(true);
                StartLevel();
            }
        }
        else if(gamestate == GameState.Ingame)
        {
            levelTimer -= Time.deltaTime;
            TimerText.text = "REMAINING TIME: " + (int)levelTimer;
            if (levelTimer < 0)
            {
                CounterText.gameObject.SetActive(false);
                CounterText.text = "LEVEL CLEAR!";
                CounterText.gameObject.SetActive(true);
                FinishLevel();
            }
        }
        else if(gamestate == GameState.PostLevel)
        {
            levelTimer -= Time.deltaTime;
            TimerText.text = "NEXT LEVEL IN " + (int)levelTimer + "...";
            if (levelTimer < 0)
            {
                TimerText.text = "";
                MainMenuButton.gameObject.SetActive(false);
                PlayButton.gameObject.SetActive(false);
                NextLevel();
                gamestate = GameState.None;
            }
        }
        else if (gamestate == GameState.PostGame)
        {
            levelTimer -= Time.deltaTime;
            TimerText.text = "RESTARTING IN " + (int)levelTimer +"...";
            if (levelTimer < 0)
            {
                TimerText.text = "";
                MainMenuButton.gameObject.SetActive(false);
                PlayButton.gameObject.SetActive(false);
                PlayAnimation(currentLevel+1);
                gamestate = GameState.None;
            }
        }
    }
    public void GameOver()
    {
        Player.Instance.isDead = true;
        Player.Instance.SetDeathBool(Player.Instance.isDead);
        gamestate = GameState.PostGame;
        levelTimer = POSTGAME_TIME;
        levels[currentLevel].SetActive(false);
        PlayButton.GetComponentInChildren<Text>().text = "RETRY";
        MainMenuButton.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(true);
    }
    public void NextLevel() //Called upon a UI Button Call or next level timer
    {
        PlayAnimation(currentLevel+1);
    }
    void PlayAnimation(int level)
    {
        Player.Instance.isDead = false;
        Player.Instance.SetDeathBool(Player.Instance.isDead);
        Player.Instance.SetInitialPosition();
        WaveText.gameObject.SetActive(false);
        WaveText.text = "WAVE " + level;
        WaveText.gameObject.SetActive(true);
    }

    public void AnimationEnd()
    {
        gamestate = GameState.Pregame;
        levelTimer = PREGAME_TIME;
        counts[0] = true;
        counts[1] = true;
        counts[2] = true;
    }
    void StartLevel()
    {
        Player.Instance.Reactivate();
        gamestate = GameState.Ingame;
        levelTimer = LevelTime[currentLevel];
        levels[currentLevel].SetActive(true);
    }
    void FinishLevel()
    {
        gamestate = GameState.PostLevel;
        levelTimer = POSTLEVEL_TIME;
        levels[currentLevel].SetActive(false);
        PlayButton.GetComponentInChildren<Text>().text = "NEXT LEVEL";
        MainMenuButton.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(true);
        if (currentLevel < levels.Length)
        {
            gamestate = GameState.PostLevel;
            currentLevel++;
        } else {
            gamestate = GameState.PreBossLevel;
        }

    }
    public void OnPlayButtonPressed()
    {
        levelTimer = -1; //In order to activate time called play

        Player.Instance.SetInitialPosition();
        MainMenuButton.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(false);
        
    }
    public void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene("Menu");
    }
}
