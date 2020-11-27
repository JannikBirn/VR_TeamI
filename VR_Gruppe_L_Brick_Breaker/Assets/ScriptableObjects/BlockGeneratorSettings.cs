using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockGeneratorSettings", menuName = "States/BlockGeneratorSettings")]
public class BlockGeneratorSettings : ScriptableObject
{
    //Script for holding the settings for the BlockGenerator
    //To create a new level you can just create a new BlockGeneratorSettings SO. and change the settings



    //Settings
    public Vector3Int blockCount;

}
