using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine; 
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    //IF YOU ADD A TIER LIST, ADD IT TO THE ALL BLOCKS LIST 
    [SerializeField] List<BaseBlock> tier1Blocks = new List<BaseBlock>();
    [SerializeField] List<BaseBlock> tier2Blocks = new List<BaseBlock>();

    public List<List<BaseBlock>> allBlocksList = new List<List<BaseBlock>>();

    public int currentTierLevel = 1;
    public List<Vector2> positionToDisplayBlocks = new List<Vector2>();

    public List<GameObject> currentRollout = new List<GameObject>();

    //dictionary used to translate the block color to its related color 
    public Dictionary<BlockColor, Color> blockColorToColor = new Dictionary<BlockColor, Color>();
    public Dictionary<BlockColor, Sprite> blockColorToSprite = new Dictionary<BlockColor, Sprite>();

    public List<Sprite> blockSpriteList = new List<Sprite>();

    public int currentMoney = 10;
    [SerializeField] TMP_Text moneyDisplay;
    [SerializeField] TMP_Text descriptionDisplay;

    public int currentAttackScore = 0; 
    [SerializeField] TMP_Text attackScoreDisplay; 
     
    public int damage = -1;

    public List<PieceFolder> pieceCurrentlyInGrid = new List<PieceFolder>();

    public enum Phase {
        Menu,
        Shop,
        Battle,
    }

    [SerializeField] Dictionary<Phase, GameObject> phaseScenes;

    [SerializeField] public Phase phase = Phase.Menu;

    [SerializeField] private TetrisGridDisplay tetrisGridDisplay;

    public List<PieceFolder> actionList = new List<PieceFolder>();

    [SerializeField] public EnemyClickManager enemyClickManager;

    [SerializeField] public EnemyLineUp enemyLineUp;


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

    public void setDescriptionDisplay(int powerLevel, string description)
    {
        descriptionDisplay.text = "Power Level: " + powerLevel + "\nDescription: " + description; 
    }

    public void setAttackScore(int attackScore)
    {
        currentAttackScore = attackScore;
        attackScoreDisplay.text = currentAttackScore.ToString();
    }

    public void changeAttackScoreBy(int delta)
    {
        currentAttackScore += delta;
        attackScoreDisplay.text = currentAttackScore.ToString();
    }

    public void clearDescriptionDisplay()
    {
        descriptionDisplay.text = "";
    }

    public void changeMoneyBy(int money)
    {
        //update the UI 
        currentMoney += money;
        moneyDisplay.text = currentMoney.ToString();
        
    }

    public void setMoneyTo(int money)
    {
        currentMoney = money;
        moneyDisplay.text = currentMoney.ToString();
    }

    private void Awake()
    {
        phaseScenes = new Dictionary<Phase, GameObject>() {
        {Phase.Menu, GameObject.Find("/Canvas/UIMenuPhase")},
        {Phase.Shop, GameObject.Find("/Canvas/UIShopPhase")},
        {Phase.Battle, GameObject.Find("/Canvas/UIBattlePhase")},
        };
        
        foreach ((Phase key, GameObject value) in phaseScenes) {
            value.SetActive(false);
        }
        phaseScenes[phase].SetActive(true);

        instance = this;
        setMoneyTo(currentMoney);
        setAttackScore(0); 
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

        switch (phase) {
            case Phase.Battle:
                Button undo = phaseScenes[Phase.Battle].transform.Find("Undo").GetComponent<Button>() as Button;
                Button attack = phaseScenes[Phase.Battle].transform.Find("Attack").GetComponent<Button>() as Button;
                
                if (actionList.Count > 0) {
                    undo.interactable = true;
                    if (enemyClickManager.selectedEnemy != null) {
                        attack.interactable = true;
                    } else {
                        attack.interactable = false;
                    }
                } else {
                    undo.interactable = false;
                    attack.interactable = false;
                }
                break;
            default:
                break;
        }
    }

    public void Roll()
    {
        // Delete previously instantiated blocks
        for (int i = currentRollout.Count - 1; i >= 0; i--)
        {
            GameObject block = currentRollout[i];
            if (block != null && !block.GetComponent<PieceFolder>().isInsideGrid)
            {
                Destroy(block);
                currentRollout.RemoveAt(i);
            }
        }

        //I want to display three random block based on the current tier 
        for (int i = 0; i < 3; i++)
        {
            GameObject folderForPiece = new GameObject();
            folderForPiece.AddComponent<PieceFolder>();
            folderForPiece.name = "Piece" + i.ToString();
            currentRollout.Add(folderForPiece.gameObject);

            //temp array for all blocks of the right tier 
            List<BaseBlock> tempBlockList = new List<BaseBlock>();

            for (int j = 0; j < currentTierLevel; j++)
            {
                tempBlockList.AddRange(allBlocksList[j]);
            }
            //get a random block
            BaseBlock randomBlock = tempBlockList[UnityEngine.Random.Range(0, tempBlockList.Count)];

            //check the lenght of offsets = number of blocks to draw 
            for (int z = 0; z < randomBlock.offsetList.Count; z++)
            {
                BaseBlock instance = Instantiate(
                                       randomBlock,
                                       new Vector3(randomBlock.offsetList[z].x * 2, randomBlock.offsetList[z].y * 2, 0),
                                       Quaternion.identity
                                     );  
                instance.transform.SetParent(folderForPiece.transform, false);
                instance.GetComponent<BaseBlock>().GetComponent<SpriteRenderer>().sprite = blockColorToSprite[instance.GetComponent<BaseBlock>().blockColor];

            }
            folderForPiece.transform.position = positionToDisplayBlocks[i];
            folderForPiece.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
            folderForPiece.GetComponent<PieceFolder>().blockType = randomBlock.typeOfBlock; 
            folderForPiece.GetComponent<PieceFolder>().initialPowerLevel = folderForPiece.transform.childCount * folderForPiece.transform.childCount;
            folderForPiece.GetComponent<PieceFolder>().currentPowerLevel = folderForPiece.GetComponent<PieceFolder>().initialPowerLevel;
        }
    }

    public void ChangePhase(Phase newPhase) {
        phaseScenes[phase].SetActive(false);
        phaseScenes[newPhase].SetActive(true);

        phase = newPhase;
    }

    public void UI_StartShop() {
        ChangePhase(Phase.Shop);
    }

    public void UI_StartBattle() {
        ChangePhase(Phase.Battle);
        // Delete previously instantiated blocks
        for (int i = currentRollout.Count - 1; i >= 0; i--)
        {
            GameObject block = currentRollout[i];
            if (block != null && !block.GetComponent<PieceFolder>().isInsideGrid)
            {
                Destroy(block);
                currentRollout.RemoveAt(i);
            }
            else if (block.GetComponent<PieceFolder>().isInsideGrid)
            {
                pieceCurrentlyInGrid.Add(block.GetComponent<PieceFolder>()); 
            }
        }

        // 
        PieceFolder[] pieceFolders = FindObjectsOfType<PieceFolder>();
        foreach (PieceFolder pieceFolder in pieceFolders) {
            if (pieceFolder.isInsideGrid) {
                foreach (Transform child in pieceFolder.transform) {
                    child.GetComponent<BaseBlock>().isSelectable = true;
                }
            } else {
                Destroy(pieceFolder);
            }
        }
    }

    public void UI_UndoButton() {
        if (actionList.Count > 0) {
            actionList.Last().transform.GetChild(0).GetComponent<BaseBlock>().Deactivate();
            actionList.Last().setIsPieceSelected(false);
        }
    }

    public void UI_Attack() {
        //TODO: CHECK IF ENEMY IS TARGETED
        if (actionList.Count > 0) {
            StartCoroutine(HandleAttackSequence());
        }
    }

    private IEnumerator HandleAttackSequence() 
    {
        foreach(PieceFolder pieceFolder in pieceCurrentlyInGrid)
        {
            if (pieceFolder.blockType == TypeOfBlock.OnAttack) 
            {
                //make the on attack piece glow
                for (int i = 0; i < 4; ++i)
                {
                   BaseBlock[] childrens = pieceFolder.GetComponentsInChildren<BaseBlock>();
                   foreach(BaseBlock children in childrens)
                   {
                        children.setIsGlowing(!children.isGlowing); 
                        
                   }
                    yield return new WaitForSeconds(0.2f);
                }
                yield return StartCoroutine(pieceFolder.transform.GetChild(0).GetComponent<BaseBlock>().OnAttack());
            } 
        }
        // attack here.
        for (int i = actionList.Count - 1; i >= 0; i--)
        {
            PieceFolder pieceFolder = actionList[i];
            if(pieceFolder.blockType == TypeOfBlock.OnDestroyed)
            {
                //make the on destroyed piece glow
                for (int j = 0; j < 4; ++j)
                {
                    BaseBlock[] childrens = pieceFolder.GetComponentsInChildren<BaseBlock>();
                    foreach (BaseBlock children in childrens)
                    {
                        children.setIsGlowing(!children.isGlowing);

                    }
                    yield return new WaitForSeconds(0.2f);
                }
                yield return StartCoroutine(pieceFolder.transform.GetChild(0).gameObject.GetComponent<BaseBlock>().OnDestroyed());

            }
            pieceFolder.gameObject.SetActive(false);
            actionList.RemoveAt(i);
        }
        enemyClickManager.selectedEnemy.GetComponent<Enemy>().TakeDamage(currentAttackScore);
        yield return null;
    }

}
