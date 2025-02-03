using UnityEngine;

public class LookAtUser : MonoBehaviour
{
    public Transform xrCamera; // XR Kamerasýný (Headset) buraya atayýn
    public float followSpeed = 5f; // UI'nin dönüþ hýzýný belirler
    public float distanceFromUser = 2f; // UI'nin kullanýcýdan uzaklýðý
    private void Awake()
    {
        if (xrCamera == null)
        {
            Debug.LogWarning("XR Camera atanmamýþ!");
            return;
        }

        // Kullanýcýnýn bakýþ yönünü belirle
        Vector3 targetPosition = xrCamera.position + xrCamera.forward * distanceFromUser;
        targetPosition.y = xrCamera.position.y; // UI'nin dikeyde hizalanmasýný saðla

        // UI'yi kullanýcýnýn yüzüne döndür
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        transform.LookAt(new Vector3(xrCamera.position.x, transform.position.y, xrCamera.position.z));
    }
    void Update()
    {
        if (xrCamera == null)
        {
            Debug.LogWarning("XR Camera atanmamýþ!");
            return;
        }

        // Kullanýcýnýn bakýþ yönünü belirle
        Vector3 targetPosition = xrCamera.position + xrCamera.forward * distanceFromUser;
        targetPosition.y = xrCamera.position.y; // UI'nin dikeyde hizalanmasýný saðla

        // UI'yi kullanýcýnýn yüzüne döndür
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        transform.LookAt(new Vector3(xrCamera.position.x, transform.position.y, xrCamera.position.z));
    }
}
