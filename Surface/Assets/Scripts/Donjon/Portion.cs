using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Portion : MonoBehaviour
{
    private int[][] portionArray = new int[0][];
    PortionGenerator generator = new PortionGenerator();
    PortionDisplay portionDisplay;
    int stage = 0;
    [SerializeField]List<GameObject> next = new List<GameObject>();
    int portionHeight = 0;
	public List<GameObject> myCells;
    public bool hasPrevious = false;

    private void Start()
    {
        //Debug.Log("New Portion Instance");
        portionDisplay = GameObject.Find("EventSystem").GetComponent<PortionDisplay>();
    }
    //public Portion(int height)
    //{
    //    portionArray = generator.Generate(height);
    //}

    //public Portion(int height, int stage)
    //{
    //    //Debug.Log("New Portion");
    //    portionArray = generator.Generate(height);
    //    this.stage = stage;
    //}

    public int[][] GetPortion()
    {
        return portionArray;
    }

    public void SetNext(GameObject p)
    {
        next.Add(p);
    }

    public List<GameObject> GetNext()
    {
        return next;
    }

    public void SetNext(List<GameObject> portionsList)
    {
        next = portionsList;
    }

    public void SetPortionArray(int[][] pa)
    {
        //Debug.Log("INITIALIZED");
        portionArray = pa;
        portionHeight = portionArray.Length;
        //Debug.Log(portionArray.Length);
    }

    public void OnMouseDown()
    {
        Debug.Log(gameObject.name);
        portionDisplay.Display(portionArray,this);
    }

    public void SetStage(int stage)
    {
        this.stage = stage;
    }

    public void TraceLines(Material lineMaterial) //Tracer les lignes reliants aux voisins
    {
        gameObject.AddComponent<LineRenderer>();
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        if (next.Count != 1) lineRenderer.positionCount = next.Count * 2 - 1;
        else lineRenderer.positionCount = 2;
        lineRenderer.endWidth = 0.3f;
        lineRenderer.startWidth = 0.3f;
        lineRenderer.material = lineMaterial;
        int j = 0;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            if (i % 2 == 1)
            {
                lineRenderer.SetPosition(i, transform.position);
            }
            else
            {
                lineRenderer.SetPosition(i, next[j].transform.position);
                j++;
            }
        }
    }

}
