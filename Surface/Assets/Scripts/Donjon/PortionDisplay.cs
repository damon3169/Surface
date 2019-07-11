using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionDisplay : MonoBehaviour
{
    [SerializeField] GameObject[] cellArray = new GameObject[7]; //Ordre: Incident, Monstre, Batterie, Obstacle, Power-Up, Medkit, JSE
    [SerializeField] PlayerController submarine;
    [SerializeField] float casePosition = 2;
    [SerializeField] int portionHeight = 5;
    private PortionGenerator portionGenerator = new PortionGenerator();
    private List<GameObject[]> instancedCells = new List<GameObject[]>();

    private void Awake()
    {
        for (int i = 0; i < cellArray.Length; i++)
        {
            if(cellArray[i].GetComponent<CellController>()!= null) cellArray[i].GetComponent<CellController>().submarine = submarine;
        }

        int[][] portionNow = portionGenerator.Generate(portionHeight); //A remplacer par un GetPortion() quand le systeme d'arbre sera code



        float posX = 0;
        int posY = 0;
        for (int i = 0; i < portionNow.Length; i++)
        {
            posY++;
            float posXoffset = 0;
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

            List<GameObject> sameLineCells = new List<GameObject>();

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
                    posXoffset = 2f;
                }
                //Debug.Log("POSX BEFORE OFFSET AT LINE " + i + " AND CASE " + j +": " + posX);
                if (j > 0)posX += posXoffset;
                //Debug.Log("POSX AFTER OFFSET AT LINE " + i + " AND CASE " + j + ": " + posX);
                if (portionNow[i].Length == 1)
                {
                    GameObject go = Instantiate(cellArray[portionNow[i][i]]);
                    go.transform.position = new Vector3(posX + casePosition, -portionNow.Length + 1, 0);
                    submarine.currentCell = go.GetComponent<CellController>();
                    sameLineCells.Add(go);
                    GameObject[] sameLineCellsArray = sameLineCells.ToArray();
                    if (sameLineCells.Count.Equals(portionNow[i].Length)) instancedCells.Add(sameLineCellsArray);
                }
                else if (portionNow[i].Length == 2)
                {
                    GameObject go = Instantiate(cellArray[portionNow[i][j]]);
                    go.transform.position = new Vector3(posX + casePosition, -portionNow.Length + i + posY, 0);
                    sameLineCells.Add(go);
                    GameObject[] sameLineCellsArray = sameLineCells.ToArray();
                    if (sameLineCells.Count.Equals(portionNow[i].Length)) instancedCells.Add(sameLineCellsArray);
                }
                else if (portionNow[i].Length == 3)
                {
                    GameObject go = Instantiate(cellArray[portionNow[i][j]]);
                    go.transform.position = new Vector3(posX + casePosition, -portionNow.Length + i + posY, 0);
                    sameLineCells.Add(go);
                    GameObject[] sameLineCellsArray = sameLineCells.ToArray();
                    if (sameLineCells.Count.Equals(portionNow[i].Length)) instancedCells.Add(sameLineCellsArray);
                }
                else
                {
                    GameObject go = Instantiate(cellArray[portionNow[i][j]]);
                    go.transform.position = new Vector3(posX + casePosition, -portionNow.Length + i + posY, 0);
                    sameLineCells.Add(go);
                    GameObject[] sameLineCellsArray = sameLineCells.ToArray();
                    if (sameLineCells.Count.Equals(portionNow[i].Length)) instancedCells.Add(sameLineCellsArray);
                }
            }
        }

        GameObject[][] instancedCellsArray = instancedCells.ToArray();
        for (int i = 0; i < instancedCellsArray.Length; i++)
        {
            for(int j = 0; j < instancedCellsArray[i].Length; j++)
            {
                if(i == 0 && j == 0)
                {
                    instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i+1][j].GetComponent<CellController>());//au dessus a gauche
                    instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i + 1][j+1].GetComponent<CellController>());//au dessus a droite
                }
                if (i < instancedCellsArray.Length - 1 && i > 0)
                {
                    instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i + 1][j].GetComponent<CellController>());//au dessus
                }
                if (i > 0 && j == 0)
                {
                    instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i-1][j].GetComponent<CellController>()); //en dessous
                    instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i][j+1].GetComponent<CellController>()); //a droite
                    if (i <= 2)
                    {
                        instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i+1][j+1].GetComponent<CellController>());//au dessus a droite
                    }

                }
                else if (i > 0 && j == instancedCellsArray[i].Length - 1)
                {
                    //Debug.Log("i: " + i);
                    //Debug.Log("j: " + j);
                    instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i][j-1].GetComponent<CellController>()); //a gauche
                    if(instancedCellsArray[i].Length != instancedCellsArray[i - 1].Length) //en dessous
                    {
                        instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i - 1][j - 1].GetComponent<CellController>());
                    }
                    else
                    {
                        //Debug.Log("in else");
                        instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i - 1][j].GetComponent<CellController>());
                    }
                    if (i <= 2)
                    {
                        instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i + 1][j - 1].GetComponent<CellController>());//au dessus a gauche
                    }
                }
                else if(i > 0 && j != 0 && j != instancedCellsArray[i].Length - 1)
                {
                    if (i == 2)
                    {
                        instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i - 1][j - 1].GetComponent<CellController>());//en dessous a gauche
                        instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i + 1][j + 1].GetComponent<CellController>());//au dessus a droite

                    }
                    instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i - 1][j].GetComponent<CellController>());//en dessous (a droite)
                    instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i][j-1].GetComponent<CellController>());//a gauche
                    instancedCellsArray[i][j].GetComponent<CellController>().GetNearCellList().Add(instancedCellsArray[i][j + 1].GetComponent<CellController>());//a droite

                }
            }
        }
    }
}
