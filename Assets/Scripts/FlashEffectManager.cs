using UnityEngine;
using UnityEngine.UI;

public class FlashEffectManager : MonoBehaviour
{
    
    private Image flashImage;
    private Color col;

    private bool isFlashing;

    void Start()
    {
        flashImage = GetComponent<Image>();

        col = flashImage.color; //ne kadar parlayaca��n� edit�rden ayarl�yorum, (100)
        //gameObject.SetActive(false); //b�yle yap�nca script �al��m�yor, inspectordeki kutucukla alakas� yokmu�
    }

    void FixedUpdate()
    {
        if (isFlashing)
        {
            col.a -= 0.03f;
            flashImage.color = col;
            if(col.a <= 0f)
                isFlashing = false;
            
        }
    }

    public void MakeFlash() //sahne tekrar ba�lad��� i�in de�erleri eski haline d�nd�rm�yorum
    {
        gameObject.SetActive(true);
        isFlashing = true;
    }

}
