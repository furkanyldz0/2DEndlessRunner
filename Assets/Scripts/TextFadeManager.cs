using TMPro;
using UnityEngine;

public class TextFadeManager : MonoBehaviour
{
    public TextMeshProUGUI startText;
    public TextMeshProUGUI restartText;

    Color col;
    public float defaultFadeTime = 2;
    public float fadeTime;
    private float fadeAwayPerSecond;
    public bool isFading = true;

    private bool isStartTextFading = true;
    //public bool IsStartTextFading
    //{
    //    get { return isStartTextFading; }
    //    set { isStartTextFading = value; }
    //}
    
    void Start()
    {
        fadeTime = defaultFadeTime;
        fadeAwayPerSecond = 1 / defaultFadeTime; //1 tam alpha deðeri
        col = startText.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartTextFading && startText.IsActive())
        {
            InitiateTextFadeAnimation(startText);
        }
        else if(!isStartTextFading && restartText.IsActive()) //her seferinde boþuna çaðýrmasýn
        {
            InitiateTextFadeAnimation(restartText);
        }
    }

    private void InitiateTextFadeAnimation(TextMeshProUGUI text)
    {
        if (isFading && fadeTime >= -0.1f)
        {
            col.a -= fadeAwayPerSecond * Time.deltaTime;
            text.color = col;
            fadeTime -= Time.deltaTime;
            if(col.a <= 0f)
            {
                isFading = false;
                fadeTime = defaultFadeTime;
            }
        }
        else if (!isFading && fadeTime >= -0.1f)
        {
            col.a += fadeAwayPerSecond * Time.deltaTime;
            text.color = col;
            fadeTime -= Time.deltaTime;
            if (col.a >= 1f) //max deðer 1
            {
                isFading = true;
                fadeTime = defaultFadeTime;
            }
        }
    }

    public void SetRestartTextActive()
    {
        col = restartText.color;
        col.a = 0;
        restartText.color = col;
        isStartTextFading = false;

        isFading = false;
        //textFadeManager.IsStartTextFading = false; gerek kalmadý
        fadeTime = defaultFadeTime;
        restartText.gameObject.SetActive(true);
    }
}
