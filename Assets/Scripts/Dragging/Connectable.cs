using UnityEngine;

public class Connectable : MonoBehaviour
{
    public SpriteRenderer spriteRender;

    public bool isConnected
    {
        private set;
        get;
    }

    public int colorIndex
    {
        get
        {
            if (connector == null) return -1;
            return connector.colorIndex;
        }
    }

    private Connecter connector;

    public void Connect(Connecter c)
    {
        isConnected = true;
        connector = c;
        UnHeightLight();
        connector.Adsorb(transform);
    }

    public void DisConnect()
    {
        isConnected = false;
        connector = null;
        HeightLight();
    }

    public void HeightLight()
    {
        spriteRender.color = Color.red;
    }

    public void UnHeightLight()
    {
        spriteRender.color = Color.blue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isConnected && collision.tag == "Connecter")
        {
            HeightLight();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isConnected && collision.tag == "Connecter")
        {
            UnHeightLight();
        }
    }

    public void StartGame()
    {
        isConnected = false;
        connector = null;
    }
}
