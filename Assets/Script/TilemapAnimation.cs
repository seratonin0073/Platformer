using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapAnimation : MonoBehaviour
{
    [SerializeField] private float animationInterval = 0.5f; // Інтервал часу між зміною тайлів
    [SerializeField] private TileBase[] animationTiles; // Масив з анімаційними тайлами в потрібній послідовності
    
    private Tilemap tilemap; // Посилання на Tilemap, яку ви хочете анімувати
    private int currentIndex = 0; // Поточний індекс в масиві анімаційних тайлів
    private float timer = 0f; // Таймер для відстеження інтервалу часу

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        // Переконайтеся, що Tilemap не є нульовим
        if (tilemap == null)
        {
            Debug.LogError("Tilemap reference is missing!");
            return;
        }

        // Переконайтеся, що масив анімаційних тайлів не є нульовим або порожнім
        if (animationTiles == null || animationTiles.Length == 0)
        {
            Debug.LogError("Animation tiles array is missing or empty!");
            return;
        }

        // Встановлюємо початковий тайл на Tilemap
        tilemap.SetTile(tilemap.origin, animationTiles[currentIndex]);
    }

    private void Update()
    {
        // Лічильник часу для відстеження інтервалу часу
        timer += Time.deltaTime;

        // Перевіряємо, чи пройшов достатній час для зміни тайлу
        if (timer >= animationInterval)
        {
            // Змінюємо поточний індекс наступним
            currentIndex++;

            // Перевіряємо, чи не перевищено максимальний індекс
            if (currentIndex >= animationTiles.Length)
            {
                currentIndex = 0; // Повертаємося до початку масиву
            }

            // Встановлюємо наступний тайл на Tilemap
            tilemap.SetTile(tilemap.origin, animationTiles[currentIndex]);

            // Скидаємо таймер
            timer = 0f;
        }
    }
}
