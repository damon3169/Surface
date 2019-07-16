using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portion : MonoBehaviour
{
    private int[][] portionArray = new int[0][];
    PortionGenerator generator = new PortionGenerator();
    [SerializeField]PortionDisplay portionDisplay;
    int stage = 0;
    List<GameObject> next = new List<GameObject>();

    private void Start()
    {
        Debug.Log("New Portion Instance");
        portionDisplay = GameObject.Find("EventSystem").GetComponent<PortionDisplay>();
    }
    public Portion(int height)
    {
        portionArray = generator.Generate(height);
    }

    public Portion(int height, int stage)
    {
        Debug.Log("New Portion");
        portionArray = generator.Generate(height);
        this.stage = stage;
    }

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
        Debug.Log("INITIALIZED");
        portionArray = pa;
        //for (int i = 0; i < portionArray.Length; i++)
        //{
        //    for (int j = 0; j < portionArray[i].Length; j++)
        //    {
        //        Debug.Log(portionArray[i][j]);
        //    }
        //}
    }

    public void OnMouseDown()
    {

        portionDisplay.Display(portionArray);
    }

    public void SetStage(int stage)
    {
        this.stage = stage;
    }

}
