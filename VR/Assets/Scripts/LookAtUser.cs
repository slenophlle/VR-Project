using UnityEngine;

public class LookAtUser : MonoBehaviour
{
    public Transform xrCamera; // XR Kameras�n� (Headset) buraya atay�n
    public float followSpeed = 5f; // UI'nin d�n�� h�z�n� belirler
    public float distanceFromUser = 2f; // UI'nin kullan�c�dan uzakl���
    private void Awake()
    {
        if (xrCamera == null)
        {
            Debug.LogWarning("XR Camera atanmam��!");
            return;
        }

        // Kullan�c�n�n bak�� y�n�n� belirle
        Vector3 targetPosition = xrCamera.position + xrCamera.forward * distanceFromUser;
        targetPosition.y = xrCamera.position.y; // UI'nin dikeyde hizalanmas�n� sa�la

        // UI'yi kullan�c�n�n y�z�ne d�nd�r
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        transform.LookAt(new Vector3(xrCamera.position.x, transform.position.y, xrCamera.position.z));
    }
    void Update()
    {
        if (xrCamera == null)
        {
            Debug.LogWarning("XR Camera atanmam��!");
            return;
        }

        // Kullan�c�n�n bak�� y�n�n� belirle
        Vector3 targetPosition = xrCamera.position + xrCamera.forward * distanceFromUser;
        targetPosition.y = xrCamera.position.y; // UI'nin dikeyde hizalanmas�n� sa�la

        // UI'yi kullan�c�n�n y�z�ne d�nd�r
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        transform.LookAt(new Vector3(xrCamera.position.x, transform.position.y, xrCamera.position.z));
    }
}
