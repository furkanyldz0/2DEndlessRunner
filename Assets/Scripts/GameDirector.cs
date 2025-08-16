using UnityEngine;
using UnityEngine.InputSystem;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    private bool isGameStarted;
    void Start()
    {
        //levelManager.RestartGame();
        isGameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
            levelManager.RestartGame();


        if (!isGameStarted && Input.GetButtonDown("Jump"))
        {
            isGameStarted = true; // a�a��daki fonksiyonu tekrar tekrar �a��rmas�n diye, oyunu ba�latm�yor
            levelManager.StartGame();
        }
    }
}
