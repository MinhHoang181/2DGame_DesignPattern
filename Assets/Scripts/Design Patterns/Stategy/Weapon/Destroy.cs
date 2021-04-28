using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Strategy
{
    public class Destroy : MonoBehaviour
    {
        [SerializeField] float timeLive = Mathf.Infinity;

        void Awake()
        {
            StartCoroutine(TimeDestroy());
        }

        private IEnumerator TimeDestroy()
        {
            yield return new WaitForSeconds(timeLive);
            Destroy(gameObject);
        }
    }
}
