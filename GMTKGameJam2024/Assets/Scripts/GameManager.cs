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
    public Dictionary<BlockColor, Sprite> blockColorToSprite = new Dictionary<BlockColor, Sprite>();

    public List<Sprite> blockSpriteList = new List<Sprite>();

     


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
        allBlocksList.Add(tier1Blocks);
        allBlocksList.Add(tier2Blocks);

        //set up the colors 
        blockColorToColor.Add(BlockColor.Red, new Color(199, 5, 5, 255));
        blockColorToColor.Add(BlockColor.White, new Color(255, 255, 255, 255));
        blockColorToColor.Add(BlockColor.Blue, new Color(0, 10, 190, 255));
        blockColorToColor.Add(BlockColor.Yellow, new Color(190, 90, 0, 255));

        blockColorToSprite.Add(BlockColor.Red, blockSpriteList[0]);
        blockColorToSprite.Add(BlockColor.Blue, blockSpriteList[1]);
        blockColorToSprite.Add(BlockColor.Yellow, blockSpriteList[2]);
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

            //check the lenght of offsets = number of blocks to draw 
            for (int z = 0; z < randomBlock.offsetList.Count; z++)
            {
                BaseBlock instance = Instantiate(
                                       randomBlock,
                                       new Vector3(randomBlock.offsetList[z].x * 2, randomBlock.offsetList[z].y * 2, 0),
                                       Quaternion.identity
                                     );  
                instance.transform.SetParent(folderForPiece.transform, false);
                instance.GetComponent<BaseBlock>().background.GetComponent<SpriteRenderer>().sprite = blockColorToSprite[instance.GetComponent<BaseBlock>().blockColor];

            }
            folderForPiece.transform.position = positionToDisplayBlocks[i]; 
            folderForPiece.transform.localScale = new Vector3(0.3f, 0.3f, 1f); 
        }
    }
}
