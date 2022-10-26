using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    private LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.positionCount = 0;
    }

    // Update is called once per frame
    public void DrawLine(Vector2 position)
    {
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, position);
    }

    public void LineReset()
    {
        line.positionCount = 0;
    }
}
