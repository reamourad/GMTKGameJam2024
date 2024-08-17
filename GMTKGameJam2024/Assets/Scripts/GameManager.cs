using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    //IF YOU ADD A TIER LIST, ADD IT TO THE ALL BLOCKS LIST 
    [SerializeField] List<BaseBlock> tier1Blocks = new List<BaseBlock>();
    [SerializeField] List<BaseBlock> tier2Blocks = new List<BaseBlock>();

    public List<List<BaseBlock>> allBlocksList = new List<List<BaseBlock>>();

    public int currentTierLevel = 1;
    public List<Vector2> positionToDisplayBlocks = new List<Vector2>();

    private List<GameObject> currentRollout = new List<GameObject>();

    //dictionnary used to translate the block color to its related color 
    public Dictionary<BlockColor, Color> blockColorToColor = new Dictionary<BlockColor, Color>();


    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("GameManager is missing");
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        allBlocksList.Add(tier1Blocks); 
        allBlocksList.Add(tier2Blocks);

        //set up the colors 
        blockColorToColor.Add(BlockColor.Red, new Color(191, 14, 0, 255)); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space key was pressed");
            currentTierLevel += 1; 
        }
    }

    public void Roll()
    {
        // Delete previously instantiated blocks
        foreach (GameObject block in currentRollout)
        {
            if(block != null)
            {
                Destroy(block);
            }
        }
        currentRollout.Clear();

        //I want to display three random block based on the current tier 
        for (int i = 0; i < 3; i++)
        {
            GameObject folderForPiece = new GameObject();
            folderForPiece.name = "Piece" + i.ToString();
            currentRollout.Add(folderForPiece.gameObject);

            //temp array for all blocks of the right tier 
            List<BaseBlock> tempBlockList = new List<BaseBlock>();

            for (int j = 0; j < currentTierLevel; j++)
            {
                tempBlockList.AddRange(allBlocksList[j]);
            }
            //get a random block
            BaseBlock randomBlock = tempBlockList[Random.Range(0, tempBlockList.Count)];

            //draw the original block 
            BaseBlock instance = Instantiate(
                                       randomBlock,
                                       new Vector3(0, 0, 0),
                                       Quaternion.identity
                                     );
            instance.transform.SetParent(folderForPiece.transform, false);

            //check the lenght of offsets = number of blocks to draw 
            for (int z = 0; z < randomBlock.offsetList.Count; z++)
            {
                instance = Instantiate(
                                       randomBlock,
                                       new Vector3(randomBlock.offsetList[z].x * 50, randomBlock.offsetList[z].y * 50, 0),
                                       Quaternion.identity
                                     );  
                instance.transform.SetParent(folderForPiece.transform, false);
            }
            folderForPiece.transform.position = positionToDisplayBlocks[i]; 
            folderForPiece.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f); 
        }
    }
}
