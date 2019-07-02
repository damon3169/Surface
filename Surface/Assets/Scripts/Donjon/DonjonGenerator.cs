using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonjonGenerator : MonoBehaviour
{
    List<int[]> donjon = new List<int[]>();
    LinesGenerator linesGenerator = new LinesGenerator();

    private void Start()
    {
        InitializeDonjon(10);
    }

    void InitializeDonjon(int height)
    {
        int randDifficulty = 0;
        int[] newLine = new int[1];
        float difficultyValue = 0f;
        newLine[0] = 6; //Case de depart vide
        donjon.Add(newLine);

        for (int i = 1; i < height; i++)
        {
            randDifficulty = Random.Range(1, 4);
            difficultyValue += randDifficulty;
            if(i == 1) newLine = linesGenerator.Generate(2, randDifficulty);
            else if(i == 2) newLine = linesGenerator.Generate(3, randDifficulty);
            else newLine = linesGenerator.Generate(4, randDifficulty);
            if (newLine[0] == -1)
            {
                Debug.LogError("Difficultés non valide");
                return;
            }
            donjon.Add(newLine);
        }
        difficultyValue /= height - 1;
        Debug.Log("Total Difficuty: " + difficultyValue);
        DebugDonjon();

    }

    private void DebugDonjon()
    {
        for(int i = 0; i < donjon.Count; i++)
        {
            Debug.Log(linesGenerator.DebugLine(donjon[i]));
        }
    }


}
