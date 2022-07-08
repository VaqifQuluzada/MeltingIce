using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTweenController : MonoBehaviour
{
    [SerializeField] private Ease tweenType=Ease.InBounce;
    [Range(0f,1f)]
    [SerializeField] private float speedMultiplier;

    [Range(0, 10)]
    [SerializeField] int maxCoinCount=5;

    [SerializeField] Image coinImage;

    [SerializeField] Transform coinImageParent;

    [SerializeField] Transform coinTargetPoint;

    public void TweenCoins(Vector2 collectibleScreenPoint)
    {
        for(int i = 0; i < maxCoinCount; i++)
        {
            GameObject coinTweenInstance = Instantiate(coinImage.gameObject, collectibleScreenPoint,Quaternion.identity,coinImageParent);


            coinTweenInstance.transform.DOMove(coinTargetPoint.position,i*speedMultiplier).SetEase(tweenType).OnComplete(()=>GamePlayManager.instance.IncreaseCoinCount());
        }
    }
}
