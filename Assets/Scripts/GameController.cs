using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static int Scores = 0;                  // ����
    public static float TimeLeft = 30f;            // ���������� ����� �� ����� ����
    public static float TimeToStart = 3f;          // ����� �� ������ ����
    public static bool isGameStarted = false;      // �������� �� ����
                                               // ��������� ��������� ��������
    public static float EnemySpeed = 2.5f;         // �������� �������� �������
    public static float ShootDelay = 0.5f;         // �������� ����� ��������� ���������
    public static float SpawnDelay = 1f;           // �������� ������ ����� �������


    public TMP_Text ScoreText;                // ����� �������� ���-�� �����
    public TMP_Text StartTimerText;           // ����� ����� �� ������ ����
    public TMP_Text TimerText;                // ����� �������

    public delegate void ScoreUpdate();
    public static event ScoreUpdate OnScoreUpdate;

    private void Start()
    {
        OnScoreUpdate += UpdateScoreText;

        Scores = 0;
        TimeLeft = 30f;                        // ��������� ��������� �������� ����� ����� ����� (��� ��� �������� �������� ����� ������ � ����������� ���)
        TimeToStart = 3f + 1f;                 // ��������� 1 ��� ����������� �������� (��� ���� ������ ���������� � 2 ��-�� ���������� ����)
        isGameStarted = false;
    }

    public static void UpdateScores()
    {
        OnScoreUpdate?.Invoke();
    }


    private void UpdateScoreText()
    {
        ScoreText.text = $"����: {Scores}";
    }

    public void Update()
    {
        if (TimeToStart <= 0f) 
        {
            if (!isGameStarted) { 
                isGameStarted = true; 
                StartTimerText.gameObject.SetActive(false);  // �������� ������ �� ������ ����
            }
            if (TimeLeft <= 0f)
            {
                SceneManager.LoadScene(1); // ���� ������ ����, ��������� ����� ����� ����
            }
            else
            {
                TimeLeft -= Time.deltaTime;
                TimerText.text = $"�����: {TimeLeft:F2}"; // ���������� ���������� ����� � ����������� 2 ����� ����� �������
            }
        } else
        {
            TimeToStart -= Time.deltaTime;
            StartTimerText.text = Mathf.Floor(TimeToStart).ToString(); // ���������� ����� �� ������ ���� � ����������� ����
        }
    }
}
