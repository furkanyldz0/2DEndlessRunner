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

        Tile.StopPlatforms(); //oyunu s�f�rdan a� kapa yapt���nda bu �a�r�lmazsa oyunu ba�l�yor olarak say�yor
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
        //Tile.StopPlatforms(); //tile i�indeki h�z atamas�n� 0'dan de�i�tirmek i�in(galiba i�e yaram�yo)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //public void StopPlayer()
    //{
    //    isPlayerMoving = false;
    //}
}
