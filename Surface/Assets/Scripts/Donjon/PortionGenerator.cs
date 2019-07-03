using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionGenerator : MonoBehaviour
{
    List<int[]> portion = new List<int[]>();
    LinesGenerator linesGenerator = new LinesGenerator();

    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            Generate(10);
        }
    }

    void Generate(int height)
    {
        int randDifficulty = 0;
        int[] newLine = new int[1];
        float difficultyValue = 0f;
        newLine[0] = 6; //Case de depart vide
        portion.Add(newLine);

        for (int i = 1; i < height; i++)
        {
            randDifficulty = Random.Range(0, 5);
            /*if(randDifficulty <= 2)
            {
                //Debug.Log("Difficulty: " + randDifficulty);
                int tmp = Random.Range(0, 100);
                //Debug.Log("TMP: " + tmp);
                if (tmp < 75) randDifficulty = Random.Range(3,5);
                //Debug.Log("New Difficulty: " + randDifficulty);
            }*/
            if (randDifficulty > 4) randDifficulty = 4;
            if (randDifficulty < 1) randDifficulty = 1;
            difficultyValue += randDifficulty;
            if(i == 1) newLine = linesGenerator.Generate(2, randDifficulty);
            else if(i == 2) newLine = linesGenerator.Generate(3, randDifficulty);
            else newLine = linesGenerator.Generate(4, randDifficulty);
            if (newLine[0] == -1)
            {
                Debug.LogError("Difficultés non valide");
                return;
            }
            portion.Add(newLine);
        }
        difficultyValue /= height - 1;
        Debug.Log("Total Difficuty: " + difficultyValue);
        //DebugDonjon();

    }

    private void DebugDonjon()
    {
        for(int i = 0; i < portion.Count; i++)
        {
            Debug.Log(linesGenerator.DebugLine(portion[i]));
        }
    }


}
