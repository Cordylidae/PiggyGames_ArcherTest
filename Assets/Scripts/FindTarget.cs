using UnityEngine;

public class FindTarget : MonoBehaviour
{
    [SerializeField] SpawnEnemy spawnEnemy;

    public Transform GetTarget()
    {
        if(spawnEnemy.enemies.Count == 0) return null;
        return spawnEnemy.enemies[0].transform;
    }
}
