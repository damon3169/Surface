using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionDisplay : MonoBehaviour
{
    [SerializeField] GameObject[] cellArray = new GameObject[7]; //Ordre: Incident, Monstre, Batterie, Obstacle, Power-Up, Medkit, JSE
    [SerializeField] PlayerController submarine;
    private PortionGenerator portionGenerator = new PortionGenerator();

    private void Awake()
    {
        for (int i = 0; i < cellArray.Length; i++)
        {
            if(cellArray[i].GetComponent<CellController>()!= null) cellArray[i].GetComponent<CellController>().submarine = submarine;
        }
        int[][] portionNow = portionGenerator.Generate(10); //A remplacer par un GetPortion() quand le systeme d'arbre sera code
        float posX = 0;
        int posY = 0;
        for (int i = 0; i < portionNow.Length; i++)
        {
            posY++;
            float offset = 0;
            if (portionNow == null)
            {
                Debug.LogError("Portion null");
                return;
            }
            if (portionNow[i].Length == 0)
            {
                Debug.LogError("Ligne vide");
                return;
            }
            for (int j = 0; j < portionNow[i].Length; j++)
            {
                if(j == 0)
                {
                    if (portionNow[i].Length % 3 == 0)
                    {
                        posX = -2;
                    }
                    else if (portionNow[i].Length == 4) posX = -3;
                    else
                    {
                        posX = -portionNow[i].Length / 2;
                    }
                    offset = 2f;
                }
                Debug.Log("POSX BEFORE OFFSET AT LINE " + i + " AND CASE " + j +": " + posX);
                if (j > 0)posX += offset;
                Debug.Log("POSX AFTER OFFSET AT LINE " + i + " AND CASE " + j + ": " + posX);
                if (portionNow[i].Length == 1)
                {
                    GameObject go = Instantiate(cellArray[portionNow[i][i]]);
                    go.transform.position = new Vector3(posX, -portionNow.Length + 1, 0);
                }
                else if (portionNow[i].Length == 2)
                {
                    GameObject go = Instantiate(cellArray[portionNow[i][j]]);
                    go.transform.position = new Vector3(posX, -portionNow.Length + i + posY, 0);
                }
                else if (portionNow[i].Length == 3)
                {
                    GameObject go = Instantiate(cellArray[portionNow[i][j]]);
                    go.transform.position = new Vector3(posX, -portionNow.Length + i + posY, 0);
                }
                else
                {
                    GameObject go = Instantiate(cellArray[portionNow[i][j]]);
                    go.transform.position = new Vector3(posX, -portionNow.Length + i + posY, 0);
                }
            }
        }
    }
}
