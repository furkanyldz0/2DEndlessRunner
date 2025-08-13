using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    void Start()
    {
        //levelManager.RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            levelManager.RestartGame();
    }
}
