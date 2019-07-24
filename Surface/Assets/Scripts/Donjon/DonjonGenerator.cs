using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonjonGenerator : MonoBehaviour
{
    [SerializeField] int maxPortionHeight = 6;
    [SerializeField] int minPortionHeight = 6; //garder la taille a 6 pour le moment parce que j'ai probablement fait de la merde ailleurs
    //[SerializeField] GameObject portionTokenPrefab;
    [SerializeField] Sprite portionTokenSprite;
    [SerializeField] PlayerController submarine;
    [SerializeField] Material lineMaterial;
    PortionGenerator generator = new PortionGenerator();
    GameObject pStage1;
    List<GameObject> pStage2 = new List<GameObject>();
    List<GameObject> pStage3 = new List<GameObject>();
    public List<GameObject> pStage4 = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        submarine.gameObject.SetActive(false);
        int n = 1; //Generation portions stage 1
        //pStage1 = new Portion(Random.Range(minPortionHeight, maxPortionHeight), 1);

        pStage1 = new GameObject();
        pStage1.name = "Portion Stage 1";
        pStage1.tag = "PortionSelector";
        pStage1.AddComponent<Portion>();
        pStage1.AddComponent<SpriteRenderer>();
        pStage1.AddComponent<BoxCollider2D>();
        pStage1.GetComponent<SpriteRenderer>().sprite = portionTokenSprite;
        pStage1.GetComponent<BoxCollider2D>().isTrigger = true;
        pStage1.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
        //pStage1.transform.localScale = new Vector3(2f, 2f, 2f);
        pStage1.GetComponent<Portion>().SetStage(1);
        //portionGoStage1.GetComponent<Portion>().SetNext(pStage1.GetNext());
        pStage1.transform.position = new Vector3(0, -8, 0);
		//pStage1.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.4f, 0.3f);
		//Instantiate(pStage1);
		pStage1.GetComponent<Portion>().SetPortionArray(generator.Generate(Random.Range(minPortionHeight, maxPortionHeight)));



		n = Random.Range(2, 4); //Generation portions stage 2
        if (n > 3) n = 3;
        for (int i = 0; i < n; i++)
        {
            GameObject go = new GameObject();
            go.name = "Portion Stage 2";
            go.tag = "PortionSelector";
            go.AddComponent<Portion>();
            go.AddComponent<SpriteRenderer>();
            go.AddComponent<BoxCollider2D>();
            go.GetComponent<SpriteRenderer>().sprite = portionTokenSprite;
            go.GetComponent<BoxCollider2D>().isTrigger = true;
            go.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
            //go.transform.localScale = new Vector3(2f, 2f, 2f);
            go.GetComponent<Portion>().SetStage(2);
			float totalWidth = 6f;
			float f = totalWidth / (n-1);
            go.transform.position = new Vector3((-totalWidth / 2)+(i* f), -4, 0);
			Debug.Log(n);
			Debug.Log(f);
			go.GetComponent<Portion>().SetPortionArray(generator.Generate(Random.Range(minPortionHeight, maxPortionHeight)));

			//Instantiate(go);
			pStage2.Add(go);
        }



        n = Random.Range(3, 5); //Generation portions stage 3
        if (n > 4) n = 4;
        for (int i = 0; i < n; i++)
        {
            GameObject go = new GameObject();
            go.name = "Portion Stage 3";
            go.tag = "PortionSelector";
            go.AddComponent<Portion>();
            go.AddComponent<SpriteRenderer>();
            go.AddComponent<BoxCollider2D>();
            go.GetComponent<SpriteRenderer>().sprite = portionTokenSprite;
            go.GetComponent<BoxCollider2D>().isTrigger = true;
            go.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
          //  go.transform.localScale = new Vector3(2f, 2f, 2f);
            go.GetComponent<Portion>().SetStage(3);
            go.GetComponent<Portion>().SetPortionArray(generator.Generate(Random.Range(minPortionHeight, maxPortionHeight)));
			float totalWidth = 6f;
			float f = totalWidth / (n - 1);
			go.transform.position = new Vector3((-totalWidth / 2) + (i * f), 0, 0);
			//Instantiate(go);
			pStage3.Add(go);
        }

        n = Random.Range(3, 5); //Generation portions stage 4
        if (n > 4) n = 4;
        for (int i = 0; i < n; i++)
        {
            GameObject go = new GameObject();
            go.name = "Portion Stage 4";
            go.tag = "PortionSelector";
            go.AddComponent<Portion>();
            go.AddComponent<SpriteRenderer>();
            go.AddComponent<BoxCollider2D>();
            go.GetComponent<SpriteRenderer>().sprite = portionTokenSprite;
            go.GetComponent<BoxCollider2D>().isTrigger = true;
            go.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
           // go.transform.localScale = new Vector3(2f, 2f, 2f);
            go.GetComponent<Portion>().SetStage(4);
            go.GetComponent<Portion>().SetPortionArray(generator.Generate(Random.Range(minPortionHeight, maxPortionHeight)));
			float totalWidth = 6f;
			float f = totalWidth / (n - 1);
			go.transform.position = new Vector3((-totalWidth / 2) + (i * f), 4, 0);
			//Instantiate(go);
			pStage4.Add(go);
        }
		GameObject[] portionSelectors = GameObject.FindGameObjectsWithTag("PortionSelector");
		foreach (GameObject portion in portionSelectors)
		{
			portion.transform.parent = GameObject.FindGameObjectWithTag("Donjon").transform;
		}
		GameObject.FindGameObjectWithTag("Donjon").GetComponent<SizeInBox>().Resize();


		//Ajout des portions suivantes dans l'arbre
		pStage1.GetComponent<Portion>().SetNext(pStage2);
        pStage1.GetComponent<Portion>().TraceLines(lineMaterial);

        for(int i = 0; i < pStage2.Count; i++)
        {
            int numbOfNext = Random.Range(1, pStage3.Count - 1);
            for(int j = 0; j < numbOfNext; j++)
            {
                int randomNext = Random.Range(0, pStage3.Count - 1);
                pStage2[i].GetComponent<Portion>().SetNext(pStage3[randomNext]);
            }
        }
        for(int i = 0; i < pStage2.Count; i++) pStage2[i].GetComponent<Portion>().TraceLines(lineMaterial);

        for (int i = 0; i < pStage3.Count; i++)
        {
            int numbOfNext = Random.Range(1, pStage3.Count - 1);
            for (int j = 0; j < numbOfNext; j++)
            {
                int randomNext = Random.Range(0, pStage4.Count - 1);
                pStage3[i].GetComponent<Portion>().SetNext(pStage4[randomNext]);
            }
        }
        for (int i = 0; i < pStage3.Count; i++) pStage3[i].GetComponent<Portion>().TraceLines(lineMaterial);

		

	}



}
