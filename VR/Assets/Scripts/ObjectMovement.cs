using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectMovement : MonoBehaviour
{
    public float fallSpeed = 5f;  // Aþaðýya düþüþ hýzý
    public float riseSpeed = 1f;  // Yukarýya çýkýþ hýzý
    private Vector3 startPos;
    private Vector3 endPos;

    private bool isFalling = true; // Modelin düþüp düþmediðini kontrol etmek için

    void Start()
    {
        // Baþlangýç pozisyonu
        startPos = transform.position;
        endPos = new Vector3(startPos.x, startPos.y + 3f, startPos.z); // 3 birim yukarý çýkacak
        Invoke("ChangeScene", 5f);  // 5 saniye sonra sahne geçiþi
    }

    void Update()
    {
        // Eðer model düþüyorsa
        if (isFalling)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(startPos.x, startPos.y - 3f, startPos.z), Time.deltaTime * fallSpeed);
            // Model yeterince düþerse, yukarý çýkmaya baþla
            if (Mathf.Abs(transform.position.y - (startPos.y - 3f)) < 0.1f)
            {
                isFalling = false;
            }
        }
        else
        {
            // Model yukarýya doðru çýkýyor
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * riseSpeed);
        }
    }

    // Sahne geçiþi yapmak için
    void ChangeScene()
    {
        SceneManager.LoadScene("SecondScene"); // "SecondScene" ile hedef sahnenizin adýný yazýn
    }
}
