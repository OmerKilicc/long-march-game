using UnityEngine;
using System.Threading.Tasks;
using Euphrates;

public class FightHandler : MonoBehaviour
{
    // ENEMY REFERENCE
    //Takes player power and text to display the power
    Animator enemyAnimator;
    Collider enemyCollider;
    ParticleSystem enemyParticleSystem;
    BossController bossController;

    int enemyPower;
    int _indexOfOtherParts = 1;


    // PLAYER REFERENCE
    //Takes player power and text to display the power
    GameObject player;
    PlayerMovement playerMovement;
    Animator playerAnimator;
    PlayerParticleHandler playerParticleHandler;

    int _indexOfPlayerTextChild = 1;
    int playerPower;


    //OTHER SYSTEMS
    [SerializeReference] TriggerChannelSO _lose;
    MoneySystem moneySystem;
    AudioManager audioManager;


    void Start()
    {
        //Get animator and collider for enemy
        enemyAnimator = GetComponent<Animator>();
        enemyCollider = GetComponent<BoxCollider>();

        //Get money system and game manager
        moneySystem = MoneySystem.Instance;
        audioManager = FindObjectOfType<AudioManager>();
    }


    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            playerAnimator = player.GetComponent<Animator>();
            playerMovement = player.GetComponent<PlayerMovement>();
            playerParticleHandler = player.GetComponent<PlayerParticleHandler>();
            
            if (gameObject.CompareTag("Enemy"))
            {
                //Get both powers to conclude VS
                enemyPower = GetComponent<EnemyController>().enemyPower;
                enemyParticleSystem = GetComponent<EnemyController>().enemyParticleSystem;
                playerPower = other.GetComponent<PlayerUpgradeController>().playerPower;

                //if player wins
                if (playerPower >= enemyPower)
                {
                    //Begin fight wait then activate movement
                    FightBegins();
                    await Task.Delay(300);
                    PlayerWinsDuel(enemyPower);
                }
                else
                    EnemyWinsDuel();
            }
            if (gameObject.CompareTag("BOSS"))
            {
                //Get the coin in bosses chest
                bossController = GetComponent<BossController>();
                int bossCoin = bossController.bossCoin;
                enemyParticleSystem = bossController.bossParticleSystem;
                
                //Begin fight wait then activate movement
                FightBegins();
                await Task.Delay(300);
                PlayerWinsDuel(bossCoin);
            }
        }
    }
    private void EnemyWinsDuel()
    {
        //Play enemy attack,player death,enemy won animations in order
        audioManager.Play("Attack");
        enemyAnimator.SetTrigger("isAttacking");
        playerParticleHandler.deathFX.Play();

        //deactivate players level text
        player.transform.GetChild(_indexOfPlayerTextChild).gameObject.SetActive(false);

        //Game Over
        _lose.Invoke();
        enemyAnimator.SetBool("isWon", true);
    }

    private void PlayerWinsDuel(int enemyPower)
    {
        //Turn off the enemy particle system
        enemyParticleSystem.Stop();

        //Play player attack,enemy death, player walk animations in order
        enemyAnimator.SetBool("isDead", true);
        playerAnimator.SetBool("isWalking", true);

        //Add coin equal to defeated enemies power
        moneySystem.AddCoin(enemyPower);
        playerParticleHandler.coinFX.Play();

        //Disable enemies collider to move player without problem
        enemyCollider.enabled = false;

        //Resume player movement
        playerMovement.ResumePlayer();

        //BAÞKA BIR YONTEM DUSUN
        //Get the glow and text from enemy and set them invisable because its dead
        transform.GetChild(_indexOfOtherParts).gameObject.SetActive(false);

    }

    private void FightBegins()
    {
        //Fight Begins player stops, they both plays attack animations
        audioManager.Play("Attack");
        playerMovement.StopPlayer();
        playerAnimator.SetTrigger("isAttacking");
        enemyAnimator.SetTrigger("isAttacking");
    }
}
