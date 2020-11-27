using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGeneratorScript : MonoBehaviour
{
    //Reference to the current blockgeneratorScript
    public BlockGeneratorSettings blockGeneratorSettings;

    //Reference to the block prefab that will be copied
    public GameObject blockPrefab;

    //Offset between the blocks
    public Vector3 scale = Vector3.one;
    public float radius = 10f;
    [Tooltip("Angle Between the Blocks in x direction")]
    public float angleClampX = 0;
    [Tooltip("Angle of the last row in z direction")]
    public float AngleClampZ = 0;


    //Private Variables

    //Anchor of the first block that will be placed
    // following blocks will be placed with anchor + offset
    private Vector3 anchor = Vector3.zero;
    private Matrix4x4 matrix;

    // Size of the block prefab
    private Vector3 blockSize;

    // Start is called before the first frame update
    void Start()
    {
        generateStartingValues();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void generateStartingValues()
    {
        //Setting the blockSize to the bound of the block prefab
        blockSize = blockPrefab.GetComponent<Renderer>().bounds.size;

        //Resetting the anchor
        anchor = this.transform.position;
        //Centering the anchor if wanted
        // anchor.z -= ((blockGeneratorSettings.blockCount.z - 1) * (scale.z + blockSize.z) / 2f);


        //Generating offset Matrix
        matrix = Matrix4x4.Translate(anchor) * Matrix4x4.Rotate(this.transform.rotation) * Matrix4x4.Scale(scale + Vector3.one);

        // Debug.Log(matrix);
    }


    //Method to calculate the current Blocks Position in the Blocks Array x,y,z
    private Vector3 generateBlockPosition(int x, int y, int z)
    {
        float xOneAngle = (180f - angleClampX) / (blockGeneratorSettings.blockCount.x - 1);
        //Current angle on the sin/cos function, will determin the x and y position
        float currentAngle = ((xOneAngle * x) - (((blockGeneratorSettings.blockCount.x - 1) * xOneAngle) / 2f));

        //one angle between two blocks in the z direction
        float zOneAngle = (180f - AngleClampZ) / (blockGeneratorSettings.blockCount.z - 1);

        //Current radius, z=0 will be the max radius
        float currentRadius = radius * Mathf.Sin(Mathf.Deg2Rad * ((z * zOneAngle) + AngleClampZ / 2f));

        //One Step in the z direction (so that the radius will be the same and doesnt depend on the blockCount.z)
        float zOneStep = 2 * (-(radius / 2f) + (radius / (blockGeneratorSettings.blockCount.z - 1)) * z);

        //Output Vektor for the x y and z direction
        //TODO right now its only layer on, needs to add more layers with y depth
        Vector3 v = new Vector3(
            Mathf.Sin(Mathf.Deg2Rad * currentAngle) * currentRadius,
            Mathf.Cos(Mathf.Deg2Rad * currentAngle) * currentRadius,
            zOneStep);

        // Debug.Log(v);

        return matrix.MultiplyPoint3x4(v);
    }

    private void OnDrawGizmos()
    {
        generateStartingValues();

        Gizmos.color = Color.red;
        //Generating gizmos at the position of the blocks that will be placed at runtime
        if (blockGeneratorSettings)
        {
            for (int x = 0; x < blockGeneratorSettings.blockCount.x; x++)
            {
                for (int y = 0; y < blockGeneratorSettings.blockCount.y; y++)
                {
                    for (int z = 0; z < blockGeneratorSettings.blockCount.z; z++)
                    {
                        Gizmos.DrawWireCube(generateBlockPosition(x, y, z), blockSize);
                        // Gizmos.DrawWireCube(new Vector3(x, y, z), Vector3.one);
                    }
                }
            }
        }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(this.transform.position, blockSize.x / 2f);
    }

    private void OnValidate()
    {
        generateStartingValues();
    }

}
