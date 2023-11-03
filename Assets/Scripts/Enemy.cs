using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _wayNum;         // »ндекс дорожки, по которой движетс€ мишень
    private Material _color;     // ÷вет мишени
    private int _cost;           //  ол-во очков, которое даЄтс€ за мишень
    private float _lifeTime;     // ¬рем€ жизни мишени
    private float _moveSpeed;    // —корость движени€ мишени
    public void Init(int wayNum, Material enemyColor, int cost, float lifeTime, float moveSpeed) // ћетод инициализации
    {
        _wayNum = wayNum;
        _color = enemyColor;
        _cost = cost;           // устанавливаем значени€
        _lifeTime = lifeTime;
        _moveSpeed = moveSpeed;

        gameObject.GetComponent<Renderer>().material = _color;     // перекрашиваем мишень в нужный цвет
    }

    public void TakeDamage()                     // метод, который срабатывает при попадании по мишени
    {
        GameController.Scores += _cost;          // начисл€ем очки в соответствии со стоимостью мишени
        GameController.UpdateScores();           // обновл€ем очки на экране
        Destroy(transform.gameObject);           // удал€ем сферу
        Spawner.isWayAvailable[_wayNum] = true;  // сообщаем о том, что путь теперь свободен
    }

    public void Update()
    {
        _lifeTime -= Time.deltaTime;  // рассчитываем актуальное врем€ жизни мишени
        if (_lifeTime <= 0f)          // если врем€ жизни истекло
        {
            Destroy(transform.gameObject);                                                         // удал€ем мишень
            Spawner.isWayAvailable[_wayNum] = true;                                                // сообщаем о том, что путь теперь свободен
            GameController.Scores = GameController.Scores > 0 ? GameController.Scores - 1 : 0;     // снимаем очки (используетс€ тернарный оператор с целью исключить уход очков в минус)
            GameController.UpdateScores();                                                         // обновл€ем очки на экране         
        }
        else                          // если врем€ жизни ещЄ не истекло
        {
            Vector3 newPosition = transform.position + Vector3.back * _moveSpeed * Time.deltaTime; // определ€ем место, куда должна передвинутс€ мишень
            transform.position = newPosition;                                                      // передвигаем
        } 


    }
}
