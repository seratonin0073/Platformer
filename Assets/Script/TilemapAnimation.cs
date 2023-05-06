using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapAnimation : MonoBehaviour
{
    [SerializeField] private float animationInterval = 0.5f; // �������� ���� �� ����� �����
    [SerializeField] private TileBase[] animationTiles; // ����� � ����������� ������� � ������� �����������
    
    private Tilemap tilemap; // ��������� �� Tilemap, ��� �� ������ ��������
    private int currentIndex = 0; // �������� ������ � ����� ���������� �����
    private float timer = 0f; // ������ ��� ���������� ��������� ����

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        // �������������, �� Tilemap �� � ��������
        if (tilemap == null)
        {
            Debug.LogError("Tilemap reference is missing!");
            return;
        }

        // �������������, �� ����� ���������� ����� �� � �������� ��� �������
        if (animationTiles == null || animationTiles.Length == 0)
        {
            Debug.LogError("Animation tiles array is missing or empty!");
            return;
        }

        // ������������ ���������� ���� �� Tilemap
        tilemap.SetTile(tilemap.origin, animationTiles[currentIndex]);
    }

    private void Update()
    {
        // ˳������� ���� ��� ���������� ��������� ����
        timer += Time.deltaTime;

        // ����������, �� ������� �������� ��� ��� ���� �����
        if (timer >= animationInterval)
        {
            // ������� �������� ������ ���������
            currentIndex++;

            // ����������, �� �� ���������� ������������ ������
            if (currentIndex >= animationTiles.Length)
            {
                currentIndex = 0; // ����������� �� ������� ������
            }

            // ������������ ��������� ���� �� Tilemap
            tilemap.SetTile(tilemap.origin, animationTiles[currentIndex]);

            // ������� ������
            timer = 0f;
        }
    }
}
