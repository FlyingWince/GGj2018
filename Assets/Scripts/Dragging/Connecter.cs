using UnityEngine;

public class Connecter : DraggingActions
{

    [HideInInspector] public int colorIndex;
    public Transform returnPos;
    public float returnSpeed = 1f;

    private BoxCollider2D boxCollider;
    private Connectable connectable;
    private LineRenderer lineRenderer;
    private float time;
    private bool isBack;

    private static float clickTime = 0.1f;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        SetLinePos(returnPos, 0);
        CanDrag = false;
    }

    private void Update()
    {
        if (isBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, returnPos.position, returnSpeed * Time.deltaTime);
            if ((transform.position - returnPos.position).magnitude <= 0.05)
            {
                isBack = false;
                lineRenderer.enabled = false;
            }
        }
        SetLinePos(transform);
    }

    public override void OnDraggingInUpdate()
    {
    }

    public override void OnEndDrag()
    {
        if (Time.time - time <= clickTime)
        {
            if (connectable != null)
                connectable.DisConnect();
            connectable = null;
            isBack = true;
            return;
        }

        RaycastHit2D[] hits = new RaycastHit2D[5];
        boxCollider.Cast(Vector2.zero, hits, 0);
        foreach (var c in hits)
        {
            if (c.collider != null && c.collider.tag == "Connectable")
            {
                connectable = c.collider.GetComponent<Connectable>();
                if (!connectable.isConnected)
                {
                    connectable.Connect(this);
                    return;
                }
            }
        }

        isBack = true;
        if (connectable != null)
            connectable.DisConnect();
        connectable = null;
    }

    public void Adsorb(Transform t)
    {
        transform.position = t.position;
    }

    public override void OnStartDrag()
    {
        isBack = false;
        if (connectable != null)
            connectable.DisConnect();
        time = Time.time;
        lineRenderer.enabled = true;
        AudioManger.instance.PlayEffectAudio(2);
    }

    private void SetLinePos(Transform t, int i = 1)
    {
        Vector3 pos = t.position;
        pos.z -= 0.1f;
        lineRenderer.SetPosition(i, pos);
    }

    public void StartGame()
    {
        CanDrag = true;
        isBack = false;
        transform.position = returnPos.position;
        lineRenderer.enabled = false;
        if (connectable != null)
        {
            connectable.StartGame();
            connectable = null;
        }
    }
}
