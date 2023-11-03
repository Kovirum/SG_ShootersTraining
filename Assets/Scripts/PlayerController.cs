using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float camSens = 0.25f;                            // чувствительность мыши
    private float _nextShoot = 0f;                           // оставшееся до следующего выстрела
    private Vector3 _lastMouse = Vector3.one; // начальное положение мыши

    public AudioClip bowShootSound;  // звук выстрела из лука



    void Update()
    {
        // поворот камеры
        _lastMouse = Input.mousePosition - _lastMouse;                                                                // определяем передвижение мыши
        _lastMouse = new Vector3(-_lastMouse.y * camSens, _lastMouse.x * camSens, 0);                                 // учитваем чувствительность
        _lastMouse = new Vector3(transform.eulerAngles.x + _lastMouse.x, transform.eulerAngles.y + _lastMouse.y, 0);  // задаём нужный поворот
        transform.eulerAngles = _lastMouse;                                                                           // фактическое изменение поворота камеры
        _lastMouse = Input.mousePosition;                                                                             // записываем новое положение мыши

        // выстрел
        if (GameController.isGameStarted && Input.GetMouseButtonUp(0)) // двойное условие: игра уже началась и нажата левая кнопка мыши
        {                             
            if (_nextShoot <= 0f)                                      // если мы уже можем выстрелить
            {
                Shoot();                                               // вызываем функцию выстрела
            }
        }
        _nextShoot -= Time.deltaTime;                                  // высчитываем оставшееся до выстрела время
    }

    private void Start()
    {
        Cursor.visible = false; // убираем курсор при старте игры
    }

    void Shoot()
    {
        AudioSource.PlayClipAtPoint(bowShootSound, transform.position); // вопроизводим звук выстрела
        _nextShoot = GameController.ShootDelay;                         // обновляем оставшееся до следующего выстрела время

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));  // создаём луч, исходящий из центра экрана

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity) && hit.transform.tag == "Target")  // определяем объект. в которых было совершено попадание и проверяем, что этот оьъект обладаем тэгом Target (т.е что это именно мишень)
        {


            Enemy enemy = hit.transform.GetComponent<Enemy>(); // получаем компонент Enemy из полученного объекта
            enemy.TakeDamage();                                // вызываем функцию попадания по мишени
        }
    }
}
