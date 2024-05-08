using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenu : MonoBehaviour
{
    BattleManager BM;
    
    [SerializeField] Player player;
    public Creature creature;

    [Header("Window Values")]
    [SerializeField] TextMeshProUGUI BattleText;
    [SerializeField] TextMeshProUGUI PlayerNameText;
    [SerializeField] TextMeshProUGUI PlayerHealthText;
    [SerializeField] Slider PlayerHealthSlider;

    [SerializeField] GameObject FightPanel;
    [SerializeField] GameObject BattlePanel;

    [SerializeField] TextMeshProUGUI EnemyNameText;
    [SerializeField] Slider EnemyHealthSlider;

    [SerializeField] private Image PlayerSpriteBox;
    [SerializeField] private Image CreatureSpriteBox;


    private GameObject PlayerObject;

    // Start is called before the first frame update
    void Start()
    {
        BM = new BattleManager();
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        player = PlayerObject.GetComponent<Player>();

        //For Camera-space
        //gameObject.GetComponent<Canvas>().worldCamera = Camera.main;

        //GameObject creatureObject = GameObject.FindGameObjectWithTag("Enemy");
        //creature = creatureObject.GetComponent<Creature>();

        //PopulateScreen();
        BattleInitiate.OnEnemyInitiate += BattleInitiate_OnEnemyInitiate;

    }
    private void BattleInitiate_OnEnemyInitiate(object sender, (Creature, Sprite) e)
    {
        creature = e.Item1;

        PopulateScreen();

        Sprite s = PlayerObject.GetComponent<SpriteRenderer>().sprite;
        SetSprites(s, e.Item2);
    }
    public void PopulateScreen()
    {
        //Player values
        PlayerNameText.text = player.Name;
        PlayerHealthText.text = ($"{player.HP}/{player.MaxHP}");
        PlayerHealthSlider.value = player.HP;
        PlayerHealthSlider.maxValue = player.MaxHP;

        //Enemy values
        EnemyNameText.text = creature.Name;
        EnemyHealthSlider.value = creature.HP;
        EnemyHealthSlider.maxValue = creature.HPMax;
    }

    private void SetSprites(Sprite playerSprite, Sprite enemySprite)
    {
        PlayerSpriteBox.sprite = playerSprite;
        CreatureSpriteBox.sprite = enemySprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthText != null)
        {
            PlayerHealthText.text = ($"{player.HP}/{player.MaxHP}");

        }

        if(player.HP<= 0)
        {
            BattleText.text = "You have lost this battle, but it wont be your last";
        }

        if (creature.HP <= 0)
        {
            BattleText.text = "You have won this battle, you will win more!";
        }

        if (PlayerHealthSlider != null)
            PlayerHealthSlider.value = player.HP;

        if(EnemyHealthSlider!= null)
            EnemyHealthSlider.value = creature.HP;
    }

    double Damage;
    public void FightButton()
    {
        FightPanel.SetActive( true );
        BattlePanel.SetActive( false );
    }
    public void BackButton()
    {
        FightPanel.SetActive(false);
        BattlePanel.SetActive(true);
    }
    public void Attack()
    {
        Damage = BM.Fight(player, creature);
        BattleEffectsManager.Instance().CharacterShake(PlayerSpriteBox.gameObject, 15, 0.8f, 3,LockMovement.RIGHT);
        Debug.Log($"Damage: {Damage}");

        BackButton();
        BattleEffectsManager.Instance().CharcterFlash(CreatureSpriteBox.gameObject, 0.8f);
        creature.HP -= (int)Damage;
        Debug.Log($"Enemies health is now: {creature.HP}");

        BattleText.text = ($"{player.Name} Damaged the {creature.Name} for {Damage} points");

        // Check if the creature is defeated
        if (creature.HP <= 0)
        {
            Debug.Log($"The {creature.Name} has been defeated!");
            return;
        }

        EnemyAttack();
    }

    public void EnemyAttack()
    {
        // Enemy attacks the player
        double enemyDamage = BM.Fight(creature, player);

        // Update UI to reflect enemy's attack
        // Implement this part according to how you want to update the UI for enemy's attack

        // Reduce player's health based on enemy's attack
        player.HP -= (int)enemyDamage;

        // Update UI text
        // Implement this part according to how you want to update the UI for player's health

        Debug.Log($"The {creature.Name} attacked the player for {enemyDamage} HP.");
    }

    void SwitchPanelsWithDelay(GameObject PanelToClose, GameObject PanelToOpen, float delay)
    {

        // Close the FightPanel and open the BattlePanel
        PanelToClose.SetActive(false);
        PanelToOpen.SetActive(true);
    }

    public void ItemButton()
    {

    }

    public void RunButton()
    {
        BM.FleeBattle();
    }
    private void OnDestroy()
    {
        BattleInitiate.OnEnemyInitiate -= BattleInitiate_OnEnemyInitiate;
    }
}
