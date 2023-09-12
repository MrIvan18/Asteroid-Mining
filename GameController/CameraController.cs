using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float movementSpeed = 5f;
    public float zoomSpeed = 10f;

    private float screenWidth;
    private float screenHeight;

    private void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    private void Update()
    {
        // Движение камеры на кнопки AWSD
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * movementSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Движение камеры при достижении границ
        Vector3 mousePosition = Input.mousePosition;

        // Регулировка размера камеры с помощью колесика мыши
        float scrollDelta = Input.mouseScrollDelta.y;
        virtualCamera.m_Lens.OrthographicSize -= scrollDelta * zoomSpeed * Time.deltaTime;

        if(virtualCamera.m_Lens.OrthographicSize <= 4)
        {
            virtualCamera.m_Lens.OrthographicSize = 5;
        }
    }
}