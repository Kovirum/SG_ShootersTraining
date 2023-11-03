using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float camSens = 0.25f;                            // ���������������� ����
    private float _nextShoot = 0f;                           // ���������� �� ���������� ��������
    private Vector3 _lastMouse = Vector3.one; // ��������� ��������� ����

    public AudioClip bowShootSound;  // ���� �������� �� ����



    void Update()
    {
        // ������� ������
        _lastMouse = Input.mousePosition - _lastMouse;                                                                // ���������� ������������ ����
        _lastMouse = new Vector3(-_lastMouse.y * camSens, _lastMouse.x * camSens, 0);                                 // �������� ����������������
        _lastMouse = new Vector3(transform.eulerAngles.x + _lastMouse.x, transform.eulerAngles.y + _lastMouse.y, 0);  // ����� ������ �������
        transform.eulerAngles = _lastMouse;                                                                           // ����������� ��������� �������� ������
        _lastMouse = Input.mousePosition;                                                                             // ���������� ����� ��������� ����

        // �������
        if (GameController.isGameStarted && Input.GetMouseButtonUp(0)) // ������� �������: ���� ��� �������� � ������ ����� ������ ����
        {                             
            if (_nextShoot <= 0f)                                      // ���� �� ��� ����� ����������
            {
                Shoot();                                               // �������� ������� ��������
            }
        }
        _nextShoot -= Time.deltaTime;                                  // ����������� ���������� �� �������� �����
    }

    private void Start()
    {
        Cursor.visible = false; // ������� ������ ��� ������ ����
    }

    void Shoot()
    {
        AudioSource.PlayClipAtPoint(bowShootSound, transform.position); // ������������ ���� ��������
        _nextShoot = GameController.ShootDelay;                         // ��������� ���������� �� ���������� �������� �����

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));  // ������ ���, ��������� �� ������ ������

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity) && hit.transform.tag == "Target")  // ���������� ������. � ������� ���� ��������� ��������� � ���������, ��� ���� ������ �������� ����� Target (�.� ��� ��� ������ ������)
        {


            Enemy enemy = hit.transform.GetComponent<Enemy>(); // �������� ��������� Enemy �� ����������� �������
            enemy.TakeDamage();                                // �������� ������� ��������� �� ������
        }
    }
}
