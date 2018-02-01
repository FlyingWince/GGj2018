using UnityEngine;

public class StartGame : MonoBehaviour {

    public SourceManager sourceManager;
    public Connecter[] connecters;
    public Attack_EnemyManager[] towers;

    public CanvasGroup canvasGroup;
    public float speed = 1;

    public void ClickToStartGame()
    {
        Time.timeScale = 1;
        foreach (var go in FindObjectsOfType<Bullet_Attributes>())
            DestroyImmediate(go.gameObject);

        foreach (var go in FindObjectsOfType<monster_manage>())
            DestroyImmediate(go.gameObject);

        gameObject.SetActive(false);
        sourceManager.restart();
        foreach (var c in connecters)
        {
            if (c != null) c.StartGame();
        }
        foreach (var c in towers)
        {
            if (c != null) c.StartGame();
        }

        GameObject spawn_gb = GameObject.FindGameObjectWithTag("Spawn");
        spawn_gb.GetComponent<monster_spawn>().Start_Game();

    }

    bool isUp;
    private void Update()
    {
        if (canvasGroup.alpha < 0.5) isUp = true;
        if (canvasGroup.alpha >= 1) isUp = false;
        if (isUp)
            canvasGroup.alpha += speed * Time.deltaTime;
        else
            canvasGroup.alpha += -speed * Time.deltaTime;
    }
}
