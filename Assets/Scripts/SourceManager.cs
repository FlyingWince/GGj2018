using UnityEngine;
using UnityEngine.UI;

public class SourceManager : MonoBehaviour {

    public static SourceManager instance;

    public GameObject startGameButton;
    public GameObject faileImage;

    public Slider sourceSlider;
    public Text sourceText;
    public int MaxSource = 100;
    public int initSource = 50;

    public int source { get; private set; }

    private void Start()
    {
        instance = this;
        restart();
    }

    public void restart()
    {
        faileImage.SetActive(false);
        source = initSource;
        Refresh();
    } 

    public void ChangeSoure(int num)
    {
        source += num;
        if (source > MaxSource) source = MaxSource;
        Refresh();
        if (source <= 0)
            GameOver();
    }

    public void GameOver()
    {
        faileImage.SetActive(true);
        startGameButton.SetActive(true);
        //startGame.GameOver();
        Time.timeScale = 0;
    }

    public void Refresh()
    {
        sourceText.text = (source * 100f / MaxSource).ToString() + "%";
        sourceSlider.value = source * 100f / MaxSource * 100 ;
    }

}
