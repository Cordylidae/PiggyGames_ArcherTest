using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShowDamage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextDamage;

    [SerializeField] private SpawnEnemy enemySpawn;
    [SerializeField] private List<IDamageble> targets = null;

    [SerializeField] private Vector3 offset = Vector3.zero;
    private Vector3 pos;
    Tween tween;

    private void Awake()
    {
        enemySpawn.creatTarget += UploadStatusTargets;
    }

    void UploadStatusTargets()
    {
        foreach (IDamageble target in targets)
        {
            target.ITakeDamage -= ShowDamageText;
        }
        targets.Clear();
        
        targets = enemySpawn.GetComponentsInChildren<IDamageble>().ToList();

        foreach (IDamageble target in targets)
        {
            target.ITakeDamage += ShowDamageText;
        }
    }
    void ShowDamageText(KeyValuePair<Transform,int> pair)
    {
        if(tween != null) tween.Kill();

        TextDamage.rectTransform.position = pos = Camera.main.transform.InverseTransformPoint(pair.Key.position + offset);
        TextDamage.gameObject.SetActive(true);

        tween = TextDamage.rectTransform
            .DOMove(pos + offset, 2);
        
        tween.Play();
        tween.onKill += DisableText;

        
        TextDamage.text = pair.Value.ToString();
    }

    void DisableText()
    {
       TextDamage.gameObject.SetActive(false);
    }
}
