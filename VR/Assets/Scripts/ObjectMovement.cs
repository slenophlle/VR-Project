using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectMovement : MonoBehaviour
{
    public float fallSpeed = 5f;  // A�a��ya d���� h�z�
    public float riseSpeed = 1f;  // Yukar�ya ��k�� h�z�
    private Vector3 startPos;
    private Vector3 endPos;

    private bool isFalling = true; // Modelin d���p d��medi�ini kontrol etmek i�in

    void Start()
    {
        // Ba�lang�� pozisyonu
        startPos = transform.position;
        endPos = new Vector3(startPos.x, startPos.y + 3f, startPos.z); // 3 birim yukar� ��kacak
        Invoke("ChangeScene", 5f);  // 5 saniye sonra sahne ge�i�i
    }

    void Update()
    {
        // E�er model d���yorsa
        if (isFalling)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(startPos.x, startPos.y - 3f, startPos.z), Time.deltaTime * fallSpeed);
            // Model yeterince d��erse, yukar� ��kmaya ba�la
            if (Mathf.Abs(transform.position.y - (startPos.y - 3f)) < 0.1f)
            {
                isFalling = false;
            }
        }
        else
        {
            // Model yukar�ya do�ru ��k�yor
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * riseSpeed);
        }
    }

    // Sahne ge�i�i yapmak i�in
    void ChangeScene()
    {
        SceneManager.LoadScene("SecondScene"); // "SecondScene" ile hedef sahnenizin ad�n� yaz�n
    }
}
