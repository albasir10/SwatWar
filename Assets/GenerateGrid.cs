using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    public int rows = 5; // Количество строк (высота сетки)
    public int columns = 5; // Количество столбцов (ширина сетки)
    public float cellSize = 1f; // Размер клетки
    public GameObject cellPrefab; // Префаб клетки (может быть пустым объектом или кубом)

    private void Start()
    {
        Generating();
    }

    [ContextMenu("Generate")]
    private void Generating()
    {
        // Получаем размеры объекта для вычисления положения клеток
        Vector3 objectSize = GetComponent<Renderer>().bounds.size;

        // Цикл для создания клеток
        for (int x = 0; x < columns; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                // Создаем копию префаба клетки
                GameObject cellObject = Instantiate(cellPrefab, this.transform);

                // Вычисляем смещение позиции клетки относительно объекта
                float xOffset = (x - (columns - 1) * 0.5f) * cellSize;
                float zOffset = (z - (rows - 1) * 0.5f) * cellSize;

                // Позиция клетки с учетом размеров и расположения объекта
                Vector3 position = this.transform.position + new Vector3(xOffset, 0f, zOffset);

                // Устанавливаем позицию клетки
                cellObject.transform.position = position;
                cellObject.transform.position = new Vector3(cellObject.transform.position.x, cellObject.transform.position.y + 0.05f, cellObject.transform.position.z);
            }
        }
    }
}
