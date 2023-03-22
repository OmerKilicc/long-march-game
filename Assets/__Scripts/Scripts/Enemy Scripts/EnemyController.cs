using System.Threading.Tasks;
using TMPro;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    
    [Header("Power Adjustments")]
    [SerializeField] public int enemyPower = 0;
    [SerializeField] public ParticleSystem enemyParticleSystem;

    [SerializeField] int _minEnemyLevel = 1;
    [SerializeField] int _maxEnemyLevel = 11;

    StatsManager statsManager;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyParticleSystem.Play();
        //Give enemy random power between 1,10 included and display it in above text if its not set before
        if (enemyPower == 0)
            enemyPower = Random.Range(_minEnemyLevel, _maxEnemyLevel);

        

        //Get the stats manager from child and update stat text
        statsManager = GetComponent<StatsManager>();
        statsManager.UpdateStats(enemyPower);
    }

    
}
