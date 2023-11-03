using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemyWays = new List<GameObject>();                                              // список объектов начальных платформ путей
    public static List<bool> isWayAvailable = new List<bool>() { true, true, true, true, true, true, true }; // список доступности пути для спавна. true - пусть пустой, false - путь занят

    public Material[] enemyColors = new Material[2]; // 1 элемент - зеленый, 2 - оранжевый
    public int[] enemyCosts = new int[2];            // аналогично
    public float[] enemyLifeTimes = new float[2];    // аналогично
    public GameObject EnemyPrefab;                   // префаб мишени (в виде капсулы)

    private float _nextSpawn = 0f;                   // оставшееся время до спавна новой мишени

    public void Spawn(int wayNum, Material enemyColor, int cost, float lifeTime, float moveSpeed, Vector3 position)  // функция для непосредственного спавна новой мишени
    {
        isWayAvailable[wayNum] = false;                                                                // обозначаем указанный путь как занятый
        Enemy enemy = Instantiate(EnemyPrefab, position, Quaternion.identity).GetComponent<Enemy>();   // создаём новую мишень на поле на основе префаба и получаем из него компонент Enemy
        enemy.Init(wayNum, enemyColor, cost, lifeTime, moveSpeed);                                     // инийиализируем мишень
    }

    public void Update()
    {
        _nextSpawn -= Time.deltaTime;  // определяем время до следующего спавна
        if (GameController.isGameStarted && _nextSpawn <= 0f && isWayAvailable.Count(x => x == true) > 0)  // тройное условие: игра началась и время до след. спавна вышло и у нас есть хотя-бы 1 свободный путь
        {
            List<int> AvaliableIndices = isWayAvailable.Select((available, index) => available ? index : -1).Where(index => index != -1).ToList(); // создаём список индексов свободных путей
            
            int randomWayIndex = Random.Range(0, AvaliableIndices.Count); // генерируем случайный индекс для обращения к списку свободных индексов
            int index = AvaliableIndices[randomWayIndex];                 // получаем итоговый индекс свободного пути
            int randomTypeIndex = Random.value >= 0.3f ? 0 : 1;            // определяем тип мишени с помощью Random.Value и тернарного оператора. Соотношение соблюдено: 70% зел / 30% оранж.
            
            Spawn(index, enemyColors[randomTypeIndex], enemyCosts[randomTypeIndex], enemyLifeTimes[randomTypeIndex], GameController.EnemySpeed, enemyWays[index].transform.position); // вызываем функцию спавна мишени
            _nextSpawn = GameController.SpawnDelay;  // обновляем время до следующего спавна мишени
        }

    }
}
