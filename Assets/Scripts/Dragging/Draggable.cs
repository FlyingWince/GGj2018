using UnityEngine;

public class Draggable : MonoBehaviour {

    private bool dragging = false;
    private Vector3 pointerDisplacement;
    private float zDisplacement;
    private BoxCollider2D boxCollider;
    private DraggingActions da;

    void Awake()
    {
        da = GetComponent<DraggingActions>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void MouseDown()
    {
        if (dragging || !Input.GetMouseButtonDown(0)) return;

        zDisplacement = -Camera.main.transform.position.z + transform.position.z;
        Vector3 mousePos = MouseInWorldCoords();
        mousePos.z = 0;
        if (!boxCollider.bounds.Contains(mousePos)) return;

        if (da!=null && da.CanDrag)
        {
            dragging = true;
            da.OnStartDrag();
            pointerDisplacement = -transform.position + MouseInWorldCoords();
        }
    }

    void Update ()
    {
        MouseDown();

        if (dragging)
        { 
            Vector3 mousePos = MouseInWorldCoords();
            transform.position = new Vector3(mousePos.x - pointerDisplacement.x, mousePos.y - pointerDisplacement.y, transform.position.z);   
            da.OnDraggingInUpdate();
        }

        MouseUp();
    }
	
    void MouseUp()
    {
        if (!Input.GetMouseButtonUp(0)) return;
        if (dragging)
        {
            dragging = false;
            da.OnEndDrag();
        }
    }   

    private Vector3 MouseInWorldCoords()
    {
        var screenMousePos = Input.mousePosition;
        screenMousePos.z = zDisplacement;
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }
}
