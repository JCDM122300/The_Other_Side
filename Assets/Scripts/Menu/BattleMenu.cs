using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenu : MonoBehaviour
{
    BattleManager BM;
    
    [SerializeField] Player player;
    [SerializeField] Creature creature;

    [Header("Window Values")]
    [SerializeField] TextMeshProUGUI BattleText;
    [SerializeField] TextMeshProUGUI PlayerNameText;
    [SerializeField] TextMeshProUGUI PlayerHealthText;
    [SerializeField] Slider PlayerHealthSlider;

    [SerializeField] TextMeshProUGUI EnemyNameText;
    [SerializeField] Slider EnemyHealthSlider;

    

    // Start is called before the first frame update
    void Start()
    {
        BM = new BattleManager();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();

        GameObject creatureObject = GameObject.FindGameObjectWithTag("Enemy");
        creature = creatureObject.GetComponent<Creature>();

        PopulateScreen();
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

    // Update is called once per frame
    void Update()
    {
        PlayerHealthText.text = ($"{player.HP}/{player.MaxHP}");
        PlayerHealthSlider.value = player.HP;

        EnemyHealthSlider.value = creature.HP;
    }

    double Damage;
    public void FightButton()
    {
        Damage = BM.Fight(player, creature);
        Debug.Log($"Damage: {Damage}");

        creature.HP -= (int)Damage;
        Debug.Log($"Enemies health is now: {creature.HP}");

        BattleText.text = ($"{player.Name} Damaged the {creature.Name} for {Damage} points");
    }

    public void ItemButton()
    {

    }

    public void RunButton()
    {

    }
}
