using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerCamera : MonoBehaviour
{
    [Header("��������� ������")]
    public bool move_in_border = false;
    public float moveSpeed = 10f;
    public float boundary = 25f;
    public Camera cameraPlayer;

    [Header("��������� UI")]
    public InfoUnitCanvas infoUnitCanvas;


    [Header("��������� ������������� ���������� �����")]
    public GameObject unitMirror;
    PlayerEvents playerEvents;

    private void Awake()
    {
        
        playerEvents = new(infoUnitCanvas, cameraPlayer, unitMirror);

    }

    void Update()
    {

        Move();

        playerEvents.UpdatePlayerEvents();

        // �������������� � ��������� ��� ������� ������ ������ ����
        if (Input.GetMouseButtonDown(1))
        {
        }

        // ��������� �������� ��� ������� ����� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            playerEvents.LeftClickMouse();
        }

    }



    private void Move()
    {
        Vector3 newTransform = new(0, 0, 0);
        // ������ ��� ������� ���� ������ ������
        if (Input.mousePosition.x >= Screen.width - boundary && move_in_border)
        {
            newTransform.x = moveSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.x <= boundary && move_in_border)
        {
            newTransform.x = -(moveSpeed * Time.deltaTime);
        }

        if (Input.mousePosition.y >= Screen.height - boundary && move_in_border)
        {
            newTransform.z = (moveSpeed * Time.deltaTime);
        }
        else if (Input.mousePosition.y <= boundary && move_in_border)
        {
            newTransform.z = -(moveSpeed * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            if (horizontalInput < 0)
            {
                newTransform.x = -(moveSpeed * Time.deltaTime);
            }
            else if (horizontalInput > 0)
            {
                newTransform.x = (moveSpeed * Time.deltaTime);
            }
            float verticalInput = Input.GetAxis("Vertical");
            if (verticalInput < 0)
            {
                newTransform.z = -(moveSpeed * Time.deltaTime);
            }
            else if (verticalInput > 0)
            {
                newTransform.z = (moveSpeed * Time.deltaTime);
            }
        }
        transform.position += newTransform;
    }

    public void ClickedCommandMove()
    {
        playerEvents.ClickedCommandMove();
    }

}
