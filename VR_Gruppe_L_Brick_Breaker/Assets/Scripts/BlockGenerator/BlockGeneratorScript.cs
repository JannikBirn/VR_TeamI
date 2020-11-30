using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGeneratorScript : MonoBehaviour
{
    //Reference to the current blockgeneratorScript
    public BlockGeneratorSettings blockGeneratorSettings;

    float minVal, maxVal, minLimit, maxLimit;

    //Private Variables

    //Anchor of the first block that will be placed
    // following blocks will be placed with anchor + offset
    private Vector3 anchor = Vector3.zero;
    private Matrix4x4 matrix;

    //For Fibonacci sphere
    private float goldenRatio;
    private float angleIncrement;

    // Size of the block prefab
    private Vector3 blockSize;

    //Gizmos
    public bool gizmosDrawClamps;


    // Start is called before the first frame update
    void Start()
    {
        generateStartingValues();
        instantiatePrefabs();
    }

    private void instantiatePrefabs()
    {
        for (int x = 0; x < blockGeneratorSettings.blockCount; x++)
        {
            Vector3 v = getPointPosition(x);
            if (clampVector(v))
            {
                GameObject block = Instantiate(blockGeneratorSettings.blockPrefab, applyMatrix(v), Quaternion.identity, this.transform) as GameObject;
                block.transform.LookAt(this.transform, Vector3.up);
            }
        }

    }






    private void generateStartingValues()
    {
        //For Calculation (fibonacci Sphere)
        goldenRatio = (1 + Mathf.Sqrt(5f)) / 2f;
        angleIncrement = Mathf.PI * 2 * goldenRatio;


        //Setting the blockSize to the bound of the block prefab
        blockSize = blockGeneratorSettings.blockPrefab.GetComponent<Renderer>().bounds.size;
        if (blockSize == Vector3.zero)
        {
            blockSize = Vector3.one;
        }

        //Resetting the anchor
        anchor = this.transform.position;

        //Generating offset Matrix
        matrix = Matrix4x4.Translate(anchor) * Matrix4x4.Rotate(this.transform.rotation) * Matrix4x4.Scale(blockGeneratorSettings.scale + Vector3.one);
    }


    //return will be between 1 and -1 coordinates
    private Vector3 getPointPosition(int index)
    {
        //Genearting Points on the fibonacci Sphere

        float t = index / (float)blockGeneratorSettings.blockCount;
        float angle1 = Mathf.Acos(1 - 2 * t);
        float angle2 = angleIncrement * index;

        float x = Mathf.Sin(angle1) * Mathf.Cos(angle2);
        float y = Mathf.Sin(angle1) * Mathf.Sin(angle2);
        float z = Mathf.Cos(angle1);

        Vector3 v = new Vector3(x, y, z);

        return v;
    }


    //Is the given vector inside the clamp range
    private bool clampVector(Vector3 input)
    {
        //Clamping the values in x,y,z directions
        if (input.x <= (blockGeneratorSettings.clampX.x * 2 - 1))
        {
            return false;
        }
        if (input.x >= (blockGeneratorSettings.clampX.y * 2 - 1))
        {
            return false;
        }

        if (input.y <= (blockGeneratorSettings.clampY.x * 2 - 1))
        {
            return false;
        }
        if (input.y >= (blockGeneratorSettings.clampY.y * 2 - 1))
        {
            return false;
        }

        if (input.z <= (blockGeneratorSettings.clampZ.x * 2 - 1))
        {
            return false;
        }
        if (input.z >= (blockGeneratorSettings.clampZ.y * 2 - 1))
        {
            return false;
        }


        return true;
    }

    private Vector3 applyMatrix(Vector3 input)
    {
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
            Gizmos.color = Color.red;
            if (blockGeneratorSettings.clampX.x > 0)
            {
                Gizmos.DrawWireCube(applyMatrix(new Vector3(blockGeneratorSettings.clampX.x * 2 - 1, 0, 0)), (applyMatrix(new Vector3(0, 2, 2))));
            }
            if (blockGeneratorSettings.clampX.y < 1)
            {
                Gizmos.DrawWireCube(applyMatrix(new Vector3(blockGeneratorSettings.clampX.y * 2 - 1, 0, 0)), (applyMatrix(new Vector3(0, 2, 2))));
            }

            Gizmos.color = Color.green;
            if (blockGeneratorSettings.clampY.x > 0)
            {
                Gizmos.DrawWireCube(applyMatrix(new Vector3(0, blockGeneratorSettings.clampY.x * 2 - 1, 0)), (applyMatrix(new Vector3(2, 0, 2))));
            }
            if (blockGeneratorSettings.clampY.y < 1)
            {
                Gizmos.DrawWireCube(applyMatrix(new Vector3(0, blockGeneratorSettings.clampY.y * 2 - 1, 0)), (applyMatrix(new Vector3(2, 0, 2))));
            }

            Gizmos.color = Color.blue;
            if (blockGeneratorSettings.clampZ.x > 0)
            {
                Gizmos.DrawWireCube(applyMatrix(new Vector3(0, 0, blockGeneratorSettings.clampZ.x * 2 - 1)), (applyMatrix(new Vector3(2, 2, 0))));
            }
            if (blockGeneratorSettings.clampZ.y < 1)
            {
                Gizmos.DrawWireCube(applyMatrix(new Vector3(0, 0, blockGeneratorSettings.clampZ.y * 2 - 1)), (applyMatrix(new Vector3(2, 2, 0))));
            }
        }



        //Generating gizmos at the position of the blocks that will be placed at runtime
        Gizmos.color = Color.black;
        if (blockGeneratorSettings)
        {
            for (int x = 0; x < blockGeneratorSettings.blockCount; x++)
            {
                Vector3 v = getPointPosition(x);
                if (clampVector(v))
                {
                    Gizmos.DrawWireCube(applyMatrix(v), blockSize);
                }
            }
        }

        //Drawing a green sphere in the center
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(this.transform.position, blockSize.x * 2);
    }


    [ContextMenu("Validate")]
    private void OnValidate()
    {
        generateStartingValues();
    }

}
