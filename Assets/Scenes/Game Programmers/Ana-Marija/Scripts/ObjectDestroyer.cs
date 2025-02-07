using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 3;

    private void Awake()
    {
        StartCoroutine("EffectDuration");
    }

    IEnumerator EffectDuration()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Object.Destroy(this.gameObject);
    }

}