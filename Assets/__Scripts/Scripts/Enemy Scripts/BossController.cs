using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Attributes")]
    public int bossCoin = 100;
    public ParticleSystem bossParticleSystem;

    private void Start()
    {
        bossParticleSystem.Play();
    }


}
