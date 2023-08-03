using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    public int rows = 5; // ���������� ����� (������ �����)
    public int columns = 5; // ���������� �������� (������ �����)
    public float cellSize = 1f; // ������ ������
    public GameObject cellPrefab; // ������ ������ (����� ���� ������ �������� ��� �����)

    private void Start()
    {
        Generating();
    }

    [ContextMenu("Generate")]
    private void Generating()
    {
        // �������� ������� ������� ��� ���������� ��������� ������
        Vector3 objectSize = GetComponent<Renderer>().bounds.size;

        // ���� ��� �������� ������
        for (int x = 0; x < columns; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                // ������� ����� ������� ������
                GameObject cellObject = Instantiate(cellPrefab, this.transform);

                // ��������� �������� ������� ������ ������������ �������
                float xOffset = (x - (columns - 1) * 0.5f) * cellSize;
                float zOffset = (z - (rows - 1) * 0.5f) * cellSize;

                // ������� ������ � ������ �������� � ������������ �������
                Vector3 position = this.transform.position + new Vector3(xOffset, 0f, zOffset);

                // ������������� ������� ������
                cellObject.transform.position = position;
                cellObject.transform.position = new Vector3(cellObject.transform.position.x, cellObject.transform.position.y + 0.05f, cellObject.transform.position.z);
            }
        }
    }
}
