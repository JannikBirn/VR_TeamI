using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int score = 0;
    public TextMesh mesh;

    public string text = "Score: ";

    // Start is called before the first frame update
    void Start()
    {
        mesh.text = text + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
