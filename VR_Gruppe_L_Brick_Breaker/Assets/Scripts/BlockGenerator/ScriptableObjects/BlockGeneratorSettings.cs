using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockGeneratorSettings", menuName = "States/BlockGeneratorSettings")]
public class BlockGeneratorSettings : ScriptableObject
{
    //Script for holding the settings for the BlockGenerator
    //To create a new level you can just create a new BlockGeneratorSettings SO. and change the settings


    //Settings
    [Tooltip("Block Count on the entire Sphere")]
    public int blockCount;

    [Tooltip("Scale of the sphere in x,y,z direction")]
    public Vector3 scale = Vector3.one;


    [Tooltip("Clamp in X Direction")]
    [SerializeField]
    [MinMaxSlider(0f, 1f)]
    public Vector2 clampX = new Vector2(0f, 1f);

    [Tooltip("Clamp in Y Direction")]
    [SerializeField]
    [MinMaxSlider(0f, 1f)]
    public Vector2 clampY = new Vector2(0f, 1f);


    [Tooltip("Clamp in Z Direction")]
    [SerializeField]
    [MinMaxSlider(0f, 1f)]
    public Vector2 clampZ = new Vector2(0f, 1f);



    //Reference to the block prefab that will be copied
    public GameObject blockPrefab;

}
