using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject Target;

    [SerializeField] private Vector2 dist_vecX;
    [SerializeField] private Vector2 dist_vecY;

    public Action creatTarget;
    public List<GameObject> enemies;

    public void SpawnTarget()
    {
        Vector3 startPoint = new Vector3(
            UnityEngine.Random.Range(dist_vecX.x - 2.0f, 2.0f + dist_vecX.y), 
            UnityEngine.Random.Range(dist_vecY.x, dist_vecY.y),
            0.0f);

        GameObject newT = Instantiate(Target, Vector3.zero, Quaternion.identity);
        
        newT.transform.parent = this.transform;
        newT.transform.localPosition = startPoint;
        newT.GetComponent<IDamageble>().DestroyMe += DestroyTarget;

        enemies.Add(newT);
        creatTarget?.Invoke();
    }

    void DestroyTarget(GameObject target)
    {
        enemies.Remove(target);
        Destroy(target);
    }
}
