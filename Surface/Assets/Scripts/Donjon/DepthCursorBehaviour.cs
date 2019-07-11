﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthCursorBehaviour : MonoBehaviour
{
    [SerializeField] private int marginTop = 100;
    [SerializeField] private int marginBottom = 100;
    [SerializeField] private Image depthSliderSprite;
    [SerializeField] private Image cursor;
    [SerializeField] private PlayerController submarine;
    private int depthHeight;
    private float offsetBetweenValues;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Depth Slider Height: " + depthSliderSprite.rectTransform.rect.height);
        depthHeight = (int)(depthSliderSprite.rectTransform.rect.height - marginTop - marginBottom);
        Debug.Log("Depth Height: " + depthHeight);
        offsetBetweenValues = depthHeight / 20f; //Profondeur max de 20 000 / 4 portions de 5 000 de profondeur / 1 unit = 1000 de profondeur
        Debug.Log("Offset: " + offsetBetweenValues);
        Debug.Log("Cursor Position: " + -(offsetBetweenValues * 10));

        cursor.rectTransform.position = new Vector3(cursor.rectTransform.position.x, -(offsetBetweenValues * 10) / 71.11111f, cursor.rectTransform.position.z); //Je sais pas pourquoi 71.11111
    }


    public void ChangeCursorPos()
    {
        if (submarine.previousCell != null) ;
        {
            if (submarine.previousCell.transform.position.y < submarine.currentCell.transform.position.y)
            {
                float newCursorPosY = (cursor.rectTransform.position.y * 71.11111f + offsetBetweenValues) / 71.11111f;
                cursor.rectTransform.position = new Vector3(cursor.rectTransform.position.x, newCursorPosY, cursor.rectTransform.position.z);
            } else if (submarine.previousCell.transform.position.y > submarine.currentCell.transform.position.y)
            {
                float newCursorPosY = (cursor.rectTransform.position.y * 71.11111f - offsetBetweenValues) / 71.11111f;
                cursor.rectTransform.position = new Vector3(cursor.rectTransform.position.x, newCursorPosY, cursor.rectTransform.position.z);
            }
        }
    }
}
