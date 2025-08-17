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

        col = flashImage.color; //ne kadar parlayacaðýný editörden ayarlýyorum, (100)
        //gameObject.SetActive(false); //böyle yapýnca script çalýþmýyor, inspectordeki kutucukla alakasý yokmuþ
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

    public void MakeFlash() //sahne tekrar baþladýðý için deðerleri eski haline döndürmüyorum
    {
        gameObject.SetActive(true);
        isFlashing = true;
    }

}
