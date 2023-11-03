using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour    // используется исключительно для конечного экрана
{
    public TMP_Text PointsReceived;          // текст с финальным кол-вом очков
    public void ResetGame()                  // функция для кнопки перезагрузки игры
    {
        SceneManager.LoadScene(0);           // открываем сцену с игрой
    }

    private void Start()
    {
        PointsReceived.text += GameController.Scores.ToString(); // отображаем на экране финальное кол-во очков
        Cursor.visible = true;                                   // отображаем курсор
    }
}
