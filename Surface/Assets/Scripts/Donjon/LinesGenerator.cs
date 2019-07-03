using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesGenerator : MonoBehaviour
{
    [Header("1 Star Difficulty")]
    [SerializeField]float[] difficultyOne = new float[7] { 9, 0, 1, 5, 10, 25, 50};

    [Header("2 Stars Difficulty")]
    [SerializeField] float[] difficultyTwo = new float[7] { 20, 5, 10, 15, 10, 10, 30 };

    [Header("3 Stars Difficulty")]
    [SerializeField] float[] difficultyThree = new float[7] { 15, 10, 20, 25, 10, 0, 20 };

    [Header("4 Stars Difficulty")]
    [SerializeField] float[] difficultyFour = new float[7] { 20, 15, 25,30, 2, 0, 8 };

    public int[] Generate(int lineSize, int difficulty)
    {
        /*
         * Incident = 0
         * Monstre = 1
         * Batterie = 2
         * Obstacle = 3
         * Power-Up = 4
         * Medkit = 5
         * Jeton Sans Effet (JSE) = 6
         */
        int[] line = new int[lineSize];

        if (isConform(difficultyOne) && isConform(difficultyTwo) && isConform(difficultyThree) && isConform(difficultyFour))
        {
            float[] difficultyLine = new float[7];

            switch (difficulty)
            {
                case 1:
                    difficultyLine = difficultyOne;
                    break;
                case 2:
                    difficultyLine = difficultyTwo;
                    break;
                case 3:
                    difficultyLine = difficultyThree;
                    break;
                case 4:
                    difficultyLine = difficultyFour;
                    break;
            }
            int randHole = Random.Range(0, line.Length - 1);
            line[randHole] = 6;
            int batteryCount = 0; //eviter qu'il n'y ai pas plus de 1 batterie sur une ligne

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != 6) //toujours generer au moins un trou pour pouvoir passer
                {
                    int randNum = Random.Range(0, 100);
                    //Debug.Log("DEBUG RANDOM " + i + ": " + randNum);
                    if (randNum >= 0 && randNum < difficultyLine[0]) line[i] = 0;
                    else if (randNum >= difficultyLine[0] && randNum < difficultyLine[0] + difficultyLine[1]) line[i] = 1;
                    else if (batteryCount < 1 && randNum >= difficultyLine[0] + difficultyLine[1] && randNum < difficultyLine[0] + difficultyLine[1] + difficultyLine[2])
                    {
                        line[i] = 2;
                        batteryCount++;

                    }
                    else if (randNum >= difficultyLine[0] + difficultyLine[1] + difficultyLine[2] && randNum < difficultyLine[0] + difficultyLine[1] + difficultyLine[2] + difficultyLine[3]) line[i] = 3;
                    else if (randNum >= difficultyLine[0] + difficultyLine[1] + difficultyLine[2] + difficultyLine[3] && randNum < difficultyLine[0] + difficultyLine[1] + difficultyLine[2] + difficultyLine[3] + difficultyLine[4]) line[i] = 4;
                    else if (randNum >= difficultyLine[0] + difficultyLine[1] + difficultyLine[2] + difficultyLine[3] + difficultyLine[4] && randNum < difficultyLine[0] + difficultyLine[1] + difficultyLine[2] + difficultyLine[3] + difficultyLine[4] + difficultyLine[5]) line[i] = 5;
                    else if (randNum >= difficultyLine[0] + difficultyLine[1] + difficultyLine[2] + difficultyLine[3] + difficultyLine[4] + difficultyLine[5] && randNum < 100) line[i] = 6;
                    else i--;
                }
            }
            return line;
        }
        line[0] = -1;
        return line;
    }


    public bool isConform (float[] difficulty)
    {
        float total = 0;
        foreach(float num in difficulty) total += num;
        return total == 100;
    }
    private void Update()
    {
        //Test();
    }

    private void Test()
    {
        Debug.Log("\n");
        Debug.Log("---------------------------------");
        Debug.Log("4 DIFFICILE: " + DebugLine(Generate(4, 4)));
        Debug.Log("5 DIFFICILE: " + DebugLine(Generate(5, 4)));
        Debug.Log("5 TRES FACILE: " + DebugLine(Generate(5, 1)));
        Debug.Log("5 TRES FACILE 2: " + DebugLine(Generate(5, 1)));
        Debug.Log("3 FACILE: " + DebugLine(Generate(3, 2)));
        Debug.Log("4 MOYEN: " + DebugLine(Generate(4, 3)));
        Debug.Log("---------------------------------");
        Debug.Log("\n");
    }

    public string DebugLine(int[] line)
    {
        string s = "|";
        for(int i = 0; i < line.Length; i++)
        {
            s += line[i] + "|";
        }
        return s;
    }
}
