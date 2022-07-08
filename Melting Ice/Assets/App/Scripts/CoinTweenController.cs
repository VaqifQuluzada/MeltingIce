using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTweenController : MonoBehaviour
{
   [SerializeField] private Ease tweenType = Ease.InBounce;
   [Range(0f, 1f)]
   [SerializeField] private float speedMultiplier;

   [Range(0, 50)]
   [SerializeField] int maxPoolSize = 5;

   [Range(0, 10)]
   [SerializeField] int maxSpawnedCoinCount = 5;

   [SerializeField] Image coinImage;

   [SerializeField] private GameObject coinPrefab;

   [SerializeField] Transform coinImageParent;

   [SerializeField] Transform coinTargetPoint;

   private Queue<GameObject> coinPool=new Queue<GameObject>();

	 private void Start()
	 {
      CreateCoinPool();
	 }


   private void CreateCoinPool()
	 {
      for(int i = 0; i < maxPoolSize; i++)
			{
         GameObject coinInstance = Instantiate(coinPrefab,coinImageParent);

         coinInstance.SetActive(false);

         coinPool.Enqueue(coinInstance);
			}
	 }

   public void TweenCoins(Vector2 collectibleScreenPoint)
	 {
			if (coinPool.Count == 0)
			{
         return;
			}
      for (int i = 0; i < maxSpawnedCoinCount; i++)
      {
         GameObject coinInstance =coinPool.Dequeue();

         coinInstance.SetActive(true);

         coinInstance.transform.position = collectibleScreenPoint;

         coinInstance.transform.DOMove(coinTargetPoint.position, i * speedMultiplier).
            SetEase(tweenType).
            OnComplete(
            () => OnCoinReachedTarget(coinInstance)
            );
      }
   }

   private void OnCoinReachedTarget(GameObject coinInstance)
	 {
      GamePlayManager.instance.IncreaseCoinCount();

      coinInstance.SetActive(false);

      coinPool.Enqueue(coinInstance);

   }

   //public void TweenCoins(Vector2 collectibleScreenPoint)
   // {
   //    for (int i = 0; i < maxCoinCount; i++)
   //    {
   //       GameObject coinTweenInstance = Instantiate(coinImage.gameObject, collectibleScreenPoint, Quaternion.identity, coinImageParent);

   //       coinTweenInstance.transform.DOMove(coinTargetPoint.position, i * speedMultiplier).SetEase(tweenType).OnComplete(() => GamePlayManager.instance.IncreaseCoinCount());
   //    }
   // }
}
