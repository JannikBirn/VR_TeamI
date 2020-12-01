﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGeneratorScript : MonoBehaviour
{

    [Header("Setting for each Spherelayer")]
    public SphereSettings[] sphereSettings;


    [Header("Gizmos Settings")]
    public bool gizmosDrawClamps = true;
    public bool gizmosDrawBlocks = true;


    //Private Variables

    //Matrix of the blockGenerator Transform
    private Matrix4x4 matrix;

    //For Fibonacci sphere
    private float goldenRatio;
    private float angleIncrement;

    // Size of the block prefab, just for drawing the Gizmos
    private Vector3 blockSize;


    // Start is called before the first frame update
    void Start()
    {
        generateSphere();
    }

    //Public Method to generate the sphere
    [ContextMenu("generate Sphere")]
    public void generateSphere()
    {
        deleteSphere();
        generateStartingValues();
        instantiatePrefabs();
    }

    //Public Method to delete all generated Prefabs (transform.child's)
    [ContextMenu("delete Sphere")]
    public void deleteSphere()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    //Generating some values for calculation and for the gizmos,
    //also generating the transform matrix
    private void generateStartingValues()
    {
        //These values will be the same 
        if (goldenRatio == 0f)
        {
            //For Calculation (fibonacci Sphere)
            goldenRatio = (1 + Mathf.Sqrt(5f)) / 2f;
            angleIncrement = Mathf.PI * 2 * goldenRatio;
        }

        //These values are just for the gizmos, it will only be generated once or it will be updated if running in Editor
        if (Application.isEditor || blockSize == Vector3.zero)
        {
            SphereSettings currentSettings = sphereSettings[0];

            //Setting the blockSize to the bound of the block prefab
            if (currentSettings.defaultPrefabs.Length > 0 && currentSettings.defaultPrefabs[0])
            {
                blockSize = currentSettings.defaultPrefabs[0].GetComponent<Renderer>().bounds.size;
            }

            if (blockSize == Vector3.zero)
            {
                blockSize = Vector3.one;
            }
        }

        //Generating offset Matrix
        matrix = Matrix4x4.Translate(this.transform.position) * Matrix4x4.Rotate(this.transform.rotation) * Matrix4x4.Scale(this.transform.localScale);
    }


    //Private Method for instantiating the prefabs of each sphereLayer
    private void instantiatePrefabs()
    {
        for (int sphereIndex = 0; sphereIndex < sphereSettings.Length; sphereIndex++)
        {
            SphereSettings currentSettings = sphereSettings[sphereIndex];

            for (int x = 0; x < currentSettings.blockCount; x++)
            {
                //Getting the points position on the current Sphere Layer
                Vector3 v = getPointPosition(x, sphereIndex);
                if (clampVector(v, sphereIndex))
                {
                    //Setting the randomBlock to a default block with all the same chance
                    GameObject randomBlock = currentSettings.defaultPrefabs[Random.Range(0, currentSettings.defaultPrefabs.Length)];

                    //Checking if one special chance had the chance to spawn. If so the randombBlock will get set to that special block
                    for (int specialBlockCount = 0; specialBlockCount < currentSettings.specialPrefabs.Length; specialBlockCount++)
                    {
                        if (Random.Range(0f, 1f) <= currentSettings.specialChance[specialBlockCount])
                        {
                            randomBlock = currentSettings.specialPrefabs[specialBlockCount];
                            break;
                        }
                    }


                    //Instantiateing the block and rotating at towards the generator 
                    GameObject block = Instantiate(randomBlock, applyMatrix(v, sphereIndex), Quaternion.identity, this.transform) as GameObject;
                    block.transform.LookAt(this.transform, Vector3.up);
                }
            }
        }
    }


    //return will be between 1 and -1 coordinates
    private Vector3 getPointPosition(int index, int sphereIndex)
    {
        SphereSettings currentSettings = sphereSettings[sphereIndex];
        //Genearting Points on the fibonacci Sphere

        float t = index / (float)currentSettings.blockCount;
        float angle1 = Mathf.Acos(1 - 2 * t);
        float angle2 = angleIncrement * index;

        float x = Mathf.Sin(angle1) * Mathf.Cos(angle2);
        float y = Mathf.Sin(angle1) * Mathf.Sin(angle2);
        float z = Mathf.Cos(angle1);

        Vector3 v = new Vector3(x, y, z);

        return v;
    }


    //Is the given vector inside the clamp range
    private bool clampVector(Vector3 input, int sphereIndex)
    {
        SphereSettings currentSettings = sphereSettings[sphereIndex];
        //Clamping the values in x,y,z directions
        if (currentSettings.clampX.x > 0 && input.x <= (currentSettings.clampX.x * 2 - 1))
        {
            return false;
        }
        if (currentSettings.clampX.y < 1 && input.x >= (currentSettings.clampX.y * 2 - 1))
        {
            return false;
        }

        if (currentSettings.clampY.x > 0 && input.y <= (currentSettings.clampY.x * 2 - 1))
        {
            return false;
        }
        if (currentSettings.clampY.y < 1 && input.y >= (currentSettings.clampY.y * 2 - 1))
        {
            return false;
        }

        if (currentSettings.clampZ.x > 0 && input.z <= (currentSettings.clampZ.x * 2 - 1))
        {
            return false;
        }
        if (currentSettings.clampZ.y < 1 && input.z >= (currentSettings.clampZ.y * 2 - 1))
        {
            return false;
        }


        return true;
    }

    //Applying the radius of the current sphereLayer and the transform matrix of this object
    private Vector3 applyMatrix(Vector3 input, int sphereIndex)
    {
        input *= sphereSettings[sphereIndex].radius;
        return matrix.MultiplyPoint3x4(input);
    }


    private void OnDrawGizmos()
    {
        if (transform.hasChanged)
        {
            OnValidate();
        }

        //Drawing the clamps
        if (gizmosDrawClamps)
        {
            for (int sphereIndex = 0; sphereIndex < sphereSettings.Length; sphereIndex++)
            {
                SphereSettings currentSettings = sphereSettings[sphereIndex];
                if (currentSettings)
                {
                    Gizmos.color = Color.red;
                    if (currentSettings.clampX.x > 0)
                    {
                        Gizmos.DrawWireCube(applyMatrix(new Vector3(currentSettings.clampX.x * 2 - 1, 0, 0), sphereIndex), (applyMatrix(new Vector3(0, 2, 2), sphereIndex)));
                    }
                    if (currentSettings.clampX.y < 1)
                    {
                        Gizmos.DrawWireCube(applyMatrix(new Vector3(currentSettings.clampX.y * 2 - 1, 0, 0), sphereIndex), (applyMatrix(new Vector3(0, 2, 2), sphereIndex)));
                    }

                    Gizmos.color = Color.green;
                    if (currentSettings.clampY.x > 0)
                    {
                        Gizmos.DrawWireCube(applyMatrix(new Vector3(0, currentSettings.clampY.x * 2 - 1, 0), sphereIndex), (applyMatrix(new Vector3(2, 0, 2), sphereIndex)));
                    }
                    if (currentSettings.clampY.y < 1)
                    {
                        Gizmos.DrawWireCube(applyMatrix(new Vector3(0, currentSettings.clampY.y * 2 - 1, 0), sphereIndex), (applyMatrix(new Vector3(2, 0, 2), sphereIndex)));
                    }

                    Gizmos.color = Color.blue;
                    if (currentSettings.clampZ.x > 0)
                    {
                        Gizmos.DrawWireCube(applyMatrix(new Vector3(0, 0, currentSettings.clampZ.x * 2 - 1), sphereIndex), (applyMatrix(new Vector3(2, 2, 0), sphereIndex)));
                    }
                    if (currentSettings.clampZ.y < 1)
                    {
                        Gizmos.DrawWireCube(applyMatrix(new Vector3(0, 0, currentSettings.clampZ.y * 2 - 1), sphereIndex), (applyMatrix(new Vector3(2, 2, 0), sphereIndex)));
                    }
                }
            }
        }



        //Generating gizmos at the position of the blocks that will be placed at runtime
        if (gizmosDrawBlocks)
        {
            Gizmos.color = Color.black;
            for (int sphereIndex = 0; sphereIndex < sphereSettings.Length; sphereIndex++)
            {
                SphereSettings currentSettings = sphereSettings[sphereIndex];
                for (int x = 0; x < currentSettings.blockCount; x++)
                {
                    Vector3 v = getPointPosition(x, sphereIndex);
                    if (clampVector(v, sphereIndex))
                    {
                        Gizmos.DrawWireCube(applyMatrix(v, sphereIndex), blockSize);
                    }
                }
            }
        }

        //Drawing a green sphere in the center
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(this.transform.position, blockSize.x * 2);
    }


    //For Editor Workflow, regenerating the values if they change, so the gizmos will change
    [ContextMenu("Validate")]
    private void OnValidate()
    {
        if (sphereSettings[0])
        {
            generateStartingValues();
        }

        //Setting the array length of specialChance as the same as specialPrefabs if its not
        for (int sphereIndex = 0; sphereIndex < sphereSettings.Length; sphereIndex++)
        {
            SphereSettings currentSettings = sphereSettings[sphereIndex];
            if (currentSettings)
            {
                if (currentSettings.specialPrefabs.Length != currentSettings.specialChance.Length)
                {
                    currentSettings.specialChance = new float[currentSettings.specialPrefabs.Length];
                }
            }
        }
    }

}