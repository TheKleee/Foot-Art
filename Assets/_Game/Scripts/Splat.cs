using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour
{
    public Color col { get; private set; }

    [Header("Random Places:"), SerializeField]
    bool randomPlaces;
    [Space, SerializeField]
    Vector3[] randPlaces;

    [Header("Random Color:"), SerializeField]
    bool randomColor;
    [Space, SerializeField]
    Color[] randCol;

    private void Awake()
    {
        if (randomPlaces) transform.position = randPlaces[Random.Range(0, randPlaces.Length - 1)];
        if (randomColor) GetComponent<SpriteRenderer>().color = randCol[Random.Range(0, randPlaces.Length - 1)];
        col = GetComponent<SpriteRenderer>().color;
    }
}
