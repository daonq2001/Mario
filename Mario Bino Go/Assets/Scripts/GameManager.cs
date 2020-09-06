using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public enum stateGame { PAUSE, DIE, WIN, PLAY }
    public stateGame status;

    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button leftBtn;
    [SerializeField] private Button rightBtn;
    [SerializeField] private Button jumpBtn;

    [SerializeField] private GameObject pannelWin;
    [SerializeField] private GameObject pannelPause;
    [SerializeField] private GameObject pannelDied;

    [SerializeField] private Text textScore;
    [SerializeField] private Text textTime;
    [SerializeField] private Text textCoin;

    [SerializeField] private int score;
    [SerializeField] private int time;
    [SerializeField] private int coin;
    
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        switch (status)
        {
            case stateGame.PAUSE:
                Pause();
                break;
            case stateGame.DIE:
                Die();
                break;
            case stateGame.WIN:
                Win();
                break;
            case stateGame.PLAY:
                Play();
                break;
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            instance = null;
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;

        pannelPause.SetActive(true);
        pannelDied.SetActive(false);
        pannelWin.SetActive(false);

        textCoin.gameObject.SetActive(false);
        textTime.gameObject.SetActive(false);
        textScore.gameObject.SetActive(false);

        pauseBtn.gameObject.SetActive(false);
        leftBtn.gameObject.SetActive(false);
        rightBtn.gameObject.SetActive(false);
        jumpBtn.gameObject.SetActive(false);
    }

    private void Die()
    {
        time = 400;
        Time.timeScale = 1f;
        pannelPause.SetActive(false);
        pannelDied.SetActive(true);
        pannelWin.SetActive(false);

        textCoin.gameObject.SetActive(false);
        textTime.gameObject.SetActive(false);
        textScore.gameObject.SetActive(false);

        pauseBtn.gameObject.SetActive(false);
        leftBtn.gameObject.SetActive(false);
        rightBtn.gameObject.SetActive(false);
        jumpBtn.gameObject.SetActive(false);
    }

    private void Win()
    {
        time = 400;
        Time.timeScale = 1f;
        pannelPause.SetActive(false);
        pannelDied.SetActive(false);
        pannelWin.SetActive(true);

        textCoin.gameObject.SetActive(false);
        textTime.gameObject.SetActive(false);
        textScore.gameObject.SetActive(false);

        pauseBtn.gameObject.SetActive(false);
        leftBtn.gameObject.SetActive(false);
        rightBtn.gameObject.SetActive(false);
        jumpBtn.gameObject.SetActive(false);
    }

    private void Play()
    {
        time -= Mathf.RoundToInt(Time.deltaTime);
        Time.timeScale = 1f;
        pannelPause.SetActive(false);
        pannelDied.SetActive(false);
        pannelWin.SetActive(false);

        textCoin.gameObject.SetActive(true);
        textTime.gameObject.SetActive(true);
        textScore.gameObject.SetActive(true);

        pauseBtn.gameObject.SetActive(true);
        leftBtn.gameObject.SetActive(true);
        rightBtn.gameObject.SetActive(true);
        jumpBtn.gameObject.SetActive(true);

        textCoin.text = "Coins: " + "\n" + coin;
        textScore.text = "Score: " + "\n" + score;
        textTime.text = "Time: " + "\n" + time;
    }


}
