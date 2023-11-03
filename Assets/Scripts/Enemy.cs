using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _wayNum;         // ������ �������, �� ������� �������� ������
    private Material _color;     // ���� ������
    private int _cost;           // ���-�� �����, ������� ����� �� ������
    private float _lifeTime;     // ����� ����� ������
    private float _moveSpeed;    // �������� �������� ������
    public void Init(int wayNum, Material enemyColor, int cost, float lifeTime, float moveSpeed) // ����� �������������
    {
        _wayNum = wayNum;
        _color = enemyColor;
        _cost = cost;           // ������������� ��������
        _lifeTime = lifeTime;
        _moveSpeed = moveSpeed;

        gameObject.GetComponent<Renderer>().material = _color;     // ������������� ������ � ������ ����
    }

    public void TakeDamage()                     // �����, ������� ����������� ��� ��������� �� ������
    {
        GameController.Scores += _cost;          // ��������� ���� � ������������ �� ���������� ������
        GameController.UpdateScores();           // ��������� ���� �� ������
        Destroy(transform.gameObject);           // ������� �����
        Spawner.isWayAvailable[_wayNum] = true;  // �������� � ���, ��� ���� ������ ��������
    }

    public void Update()
    {
        _lifeTime -= Time.deltaTime;  // ������������ ���������� ����� ����� ������
        if (_lifeTime <= 0f)          // ���� ����� ����� �������
        {
            Destroy(transform.gameObject);                                                         // ������� ������
            Spawner.isWayAvailable[_wayNum] = true;                                                // �������� � ���, ��� ���� ������ ��������
            GameController.Scores = GameController.Scores > 0 ? GameController.Scores - 1 : 0;     // ������� ���� (������������ ��������� �������� � ����� ��������� ���� ����� � �����)
            GameController.UpdateScores();                                                         // ��������� ���� �� ������         
        }
        else                          // ���� ����� ����� ��� �� �������
        {
            Vector3 newPosition = transform.position + Vector3.back * _moveSpeed * Time.deltaTime; // ���������� �����, ���� ������ ������������ ������
            transform.position = newPosition;                                                      // �����������
        } 


    }
}
