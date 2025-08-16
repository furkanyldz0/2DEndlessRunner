using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI meterScoreText, meterIcon;
    private float meterScore;
    public bool isPlayerMoving; //background and playeridle


    private void Start()
    {
        meterScore = 0;
        meterScoreText.SetText(meterScore.ToString());
        meterScoreText.gameObject.SetActive(true);
        meterIcon.gameObject.SetActive(true);

        Tile.StopPlatforms(); //oyunu sýfýrdan aç kapa yaptýðýnda bu çaðrýlmazsa oyunu baþlýyor olarak sayýyor
    }

    private void FixedUpdate()
    {
        if (isPlayerMoving)
        {
            meterScore += Time.fixedDeltaTime * 8;
            meterScoreText.SetText(Mathf.RoundToInt(meterScore).ToString());
        }

        
    }

    public void StartGame()
    {
        isPlayerMoving = true;
        var tiles = FindObjectsByType<Tile>(FindObjectsSortMode.None);
        foreach (var t in tiles)
        {
            t.StartPlatform();
        }
    }

    public void StopGame()
    {
        isPlayerMoving = false;
        var tiles = FindObjectsByType<Tile>(FindObjectsSortMode.None);
        foreach (var t in tiles)
        {
            t.StopPlatform();
        }
    }
    public void RestartGame()
    {
        //Tile.StopPlatforms(); //tile içindeki hýz atamasýný 0'dan deðiþtirmek için(galiba iþe yaramýyo)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //public void StopPlayer()
    //{
    //    isPlayerMoving = false;
    //}
}
