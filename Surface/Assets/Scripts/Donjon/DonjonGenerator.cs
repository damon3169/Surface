using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonjonGenerator : MonoBehaviour
{
    [SerializeField] int maxPortionHeight = 6;
    [SerializeField] int minPortionHeight = 6; //garder la taille a 6 pour le moment parce que j'ai probablement fait de la merde ailleurs
    [SerializeField] GameObject portionTokenPrefab;
    PortionGenerator generator = new PortionGenerator();
    GameObject pStage1;
    List<GameObject> pStage2 = new List<GameObject>();
    List<GameObject> pStage3 = new List<GameObject>();
    List<GameObject> pStage4 = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        int n = 1; //Generation portions stage 1
        //pStage1 = new Portion(Random.Range(minPortionHeight, maxPortionHeight), 1);

        pStage1 = portionTokenPrefab;
        pStage1.GetComponent<Portion>().SetStage(1);
        pStage1.GetComponent<Portion>().SetPortionArray(generator.Generate(Random.Range(minPortionHeight, maxPortionHeight)));
        //portionGoStage1.GetComponent<Portion>().SetNext(pStage1.GetNext());
        pStage1.transform.position = new Vector3(0, -8, 0);
        Instantiate(pStage1);



        n = Random.Range(2, 4); //Generation portions stage 2
        if (n > 3) n = 3;
        for (int i = 0; i < n; i++)
        {
            GameObject go = portionTokenPrefab;
            go.GetComponent<Portion>().SetStage(2);
            go.GetComponent<Portion>().SetPortionArray(generator.Generate(Random.Range(minPortionHeight, maxPortionHeight)));
            go.transform.position = new Vector3(-n + i * 2, -4, 0);
            Instantiate(go);
            pStage2.Add(go);
        }



        n = Random.Range(3, 5); //Generation portions stage 3
        if (n > 4) n = 4;
        for (int i = 0; i < n; i++)
        {
            GameObject go = portionTokenPrefab;
            go.GetComponent<Portion>().SetStage(3);
            go.GetComponent<Portion>().SetPortionArray(generator.Generate(Random.Range(minPortionHeight, maxPortionHeight)));
            go.transform.position = new Vector3(-n + i * 2, 0, 0);
            Instantiate(go);
            pStage3.Add(go);
        }

        n = Random.Range(3, 5); //Generation portions stage 4
        if (n > 4) n = 4;
        for (int i = 0; i < n; i++)
        {
            GameObject go = portionTokenPrefab;
            go.GetComponent<Portion>().SetStage(4);
            go.GetComponent<Portion>().SetPortionArray(generator.Generate(Random.Range(minPortionHeight, maxPortionHeight)));
            go.transform.position = new Vector3(-n + i * 2, 4, 0);
            Instantiate(go);
            pStage4.Add(go);
        }

        pStage1.GetComponent<Portion>().SetNext(pStage2);

        for(int i = 0; i < pStage2.Count; i++)
        {
            int randomNext = Random.Range(0, pStage3.Count - 1);
            pStage2[i].GetComponent<Portion>().SetNext(pStage3[randomNext]);
        }

        for (int i = 0; i < pStage3.Count; i++)
        {
            int randomNext = Random.Range(0, pStage4.Count - 1);
            pStage3[i].GetComponent<Portion>().SetNext(pStage4[randomNext]);
        }
        //Display();
    }

    //void Display()
    //{
    //    Debug.Log("Donjon Display");
    //    GameObject portionGo = portionTokenPrefab; //Display portion stage 1
    //    portionGo.GetComponent<Portion>().SetStage(1);
    //    portionGo.GetComponent<Portion>().portionArray = pStage1.portionArray;
    //    portionGo.GetComponent<Portion>().SetNext(pStage1.GetNext());
    //    portionGo.transform.position = new Vector3(0, -8, 0);
    //    Instantiate(portionGo);
    //}
}
