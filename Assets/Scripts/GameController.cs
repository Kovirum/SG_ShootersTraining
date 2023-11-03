using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static int Scores = 0;                  // очки
    public static float TimeLeft = 30f;            // оставшеес€ врем€ до конца игры
    public static float TimeToStart = 3f;          // врем€ до начала игры
    public static bool isGameStarted = false;      // началась ли игра
                                               // объ€вл€ем начальные значени€
    public static float EnemySpeed = 2.5f;         // скорость движени€ мишеней
    public static float ShootDelay = 0.5f;         // задержка перед следующим выстрелом
    public static float SpawnDelay = 1f;           // задержка спавна новых мишеней


    public TMP_Text ScoreText;                // текст текущего кол-ва очков
    public TMP_Text StartTimerText;           // текст врем€ до начала игры
    public TMP_Text TimerText;                // текст таймера

    public delegate void ScoreUpdate();
    public static event ScoreUpdate OnScoreUpdate;

    private void Start()
    {
        OnScoreUpdate += UpdateScoreText;

        Scores = 0;
        TimeLeft = 30f;                        // обновл€ем изменЄнные значени€ перед новой игрой (все эти значени€ мен€ютс€ после первой и последюущих игр)
        TimeToStart = 3f + 1f;                 // добавл€ем 1 дл€ корректного значени€ (без него таймер начинаетс€ с 2 из-за округлени€ вниз)
        isGameStarted = false;
    }

    public static void UpdateScores()
    {
        OnScoreUpdate?.Invoke();
    }


    private void UpdateScoreText()
    {
        ScoreText.text = $"ќчки: {Scores}";
    }

    public void Update()
    {
        if (TimeToStart <= 0f) 
        {
            if (!isGameStarted) { 
                isGameStarted = true; 
                StartTimerText.gameObject.SetActive(false);  // скрываем отсчЄт до начала игры
            }
            if (TimeLeft <= 0f)
            {
                SceneManager.LoadScene(1); // если таймер истЄк, открываем экран конца игры
            }
            else
            {
                TimeLeft -= Time.deltaTime;
                TimerText.text = $"¬рем€: {TimeLeft:F2}"; // отображаем оставшеес€ врем€ с округлением 2 знака после зап€той
            }
        } else
        {
            TimeToStart -= Time.deltaTime;
            StartTimerText.text = Mathf.Floor(TimeToStart).ToString(); // отображаем врем€ до начала игры с округлением вниз
        }
    }
}
