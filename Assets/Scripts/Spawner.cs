using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemyWays = new List<GameObject>();                                              // ������ �������� ��������� �������� �����
    public static List<bool> isWayAvailable = new List<bool>() { true, true, true, true, true, true, true }; // ������ ����������� ���� ��� ������. true - ����� ������, false - ���� �����

    public Material[] enemyColors = new Material[2]; // 1 ������� - �������, 2 - ���������
    public int[] enemyCosts = new int[2];            // ����������
    public float[] enemyLifeTimes = new float[2];    // ����������
    public GameObject EnemyPrefab;                   // ������ ������ (� ���� �������)

    private float _nextSpawn = 0f;                   // ���������� ����� �� ������ ����� ������

    public void Spawn(int wayNum, Material enemyColor, int cost, float lifeTime, float moveSpeed, Vector3 position)  // ������� ��� ����������������� ������ ����� ������
    {
        isWayAvailable[wayNum] = false;                                                                // ���������� ��������� ���� ��� �������
        Enemy enemy = Instantiate(EnemyPrefab, position, Quaternion.identity).GetComponent<Enemy>();   // ������ ����� ������ �� ���� �� ������ ������� � �������� �� ���� ��������� Enemy
        enemy.Init(wayNum, enemyColor, cost, lifeTime, moveSpeed);                                     // �������������� ������
    }

    public void Update()
    {
        _nextSpawn -= Time.deltaTime;  // ���������� ����� �� ���������� ������
        if (GameController.isGameStarted && _nextSpawn <= 0f && isWayAvailable.Count(x => x == true) > 0)  // ������� �������: ���� �������� � ����� �� ����. ������ ����� � � ��� ���� ����-�� 1 ��������� ����
        {
            List<int> AvaliableIndices = isWayAvailable.Select((available, index) => available ? index : -1).Where(index => index != -1).ToList(); // ������ ������ �������� ��������� �����
            
            int randomWayIndex = Random.Range(0, AvaliableIndices.Count); // ���������� ��������� ������ ��� ��������� � ������ ��������� ��������
            int index = AvaliableIndices[randomWayIndex];                 // �������� �������� ������ ���������� ����
            int randomTypeIndex = Random.value >= 0.3f ? 0 : 1;            // ���������� ��� ������ � ������� Random.Value � ���������� ���������. ����������� ���������: 70% ��� / 30% �����.
            
            Spawn(index, enemyColors[randomTypeIndex], enemyCosts[randomTypeIndex], enemyLifeTimes[randomTypeIndex], GameController.EnemySpeed, enemyWays[index].transform.position); // �������� ������� ������ ������
            _nextSpawn = GameController.SpawnDelay;  // ��������� ����� �� ���������� ������ ������
        }

    }
}
