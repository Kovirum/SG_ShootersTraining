using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour    // ������������ ������������� ��� ��������� ������
{
    public TMP_Text PointsReceived;          // ����� � ��������� ���-��� �����
    public void ResetGame()                  // ������� ��� ������ ������������ ����
    {
        SceneManager.LoadScene(0);           // ��������� ����� � �����
    }

    private void Start()
    {
        PointsReceived.text += GameController.Scores.ToString(); // ���������� �� ������ ��������� ���-�� �����
        Cursor.visible = true;                                   // ���������� ������
    }
}
