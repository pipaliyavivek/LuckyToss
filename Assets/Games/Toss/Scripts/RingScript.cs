using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;

 public class RingScript : MonoBehaviour
 {
        public GameObject CoinPrefab;
        public List<GameObject> ActiveObjects = new List<GameObject>();
        public List<GameObject> Objects = new List<GameObject>();

        private Tween m_Move;


        Sequence mySequence;
        private Tween m_WaitTween;
        public void OnTriggerStay(Collider other)
        {
            if (!other.gameObject.CompareTag("Bottle")) return;
            if (!ActiveObjects.Contains(other.gameObject) && transform.eulerAngles.x > -10f && transform.eulerAngles.x < 10f)
            {
                ActiveObjects.Add(other.gameObject);
            }
            if (!m_WaitTween.IsActive()) m_WaitTween = DOVirtual.DelayedCall(1, OnCollect);
        }
        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Bottle")) return;
            if (ActiveObjects.Contains(other.gameObject)) ActiveObjects.Remove(other.gameObject);
        }
        [Button]
        private void OnCollect()
        {
            GameManager.instance.currentLevel += 1;
            GameManager.instance.function();

            m_Move?.Kill();

            DOVirtual.Float(Projectile.instance.Score, (Projectile.instance.Score + 10), 2f, x =>
               {
                   Projectile.instance.Score = (int)x;
               });
            for (int i = 0; i < 10; i++)
            {
                GameObject l_Coin = Instantiate(CoinPrefab, transform.position, Quaternion.identity);
                Objects.Add(l_Coin);
                l_Coin.transform.DOMove(
                l_Coin.transform.position + UnityEngine.Random.insideUnitSphere * 0.40f + Vector3.up, 0.25f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    //StartCoroutine(SlowDown());
                    l_Coin.transform.DOMove(UIManager.Instance.CoinTarget.transform.GetChild(0).position, 2);
                    m_Move = l_Coin.transform.DOScale(Vector3.one * 15, 2.5f).OnComplete(() => Destroy(l_Coin));
                });
            }
            Destroy(ActiveObjects[0].gameObject);
            Destroy(gameObject);
        }
        IEnumerator SlowDown()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].transform.DOMove(UIManager.Instance.CoinTarget.transform.GetChild(0).position, 5);
                Objects[i].transform.DOScale(Vector3.one * 15, 5f).OnComplete(() =>
                 {
                     Debug.Log("LEPD");
                 });
            //  Destroy(Objects[i]);
            //   Destroy(gameObject); 
            yield return new WaitForSeconds(0.1f);
            }
        }
    }





