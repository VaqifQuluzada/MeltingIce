using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private TailFollowPlayer particleTailController;

    #region Collision Sounds
    [SerializeField] private AudioSource coinSound;

    [SerializeField] private AudioSource waterSound;

    [SerializeField] private AudioSource obstacleSound;
    #endregion

    #region Collision particles

    [SerializeField] private ParticleSystem waterParticles;

    [SerializeField] private ParticleSystem coinParticles;

    #endregion

    [SerializeField] CoinTweenController coinTweenController;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            //transform.DOPunchScale((transform.localScale / 2), 0.5f, 5).OnComplete(() => transform.localScale = transform.localScale / 2);
            
         obstacleSound.Stop();

            obstacleSound.Play();

            transform.DOScale(transform.localScale / 2, 0.5f);

            if (transform.localScale.x < 0.1f)
            {
                GamePlayManager.instance.onGameOver();
            }

            particleTailController.ChangeScaleOfParticleEffect(transform.localScale.x);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Collectible"))
        {
            waterSound.Stop();

            waterSound.Play();

            ParticleSystem coinParticleInstance = Instantiate(coinParticles);

            coinParticleInstance.transform.position = other.gameObject.transform.position;

            Destroy(coinParticleInstance.gameObject, 2f);

            if (transform.localScale.x < 1)
            {
                //transform.DOPunchScale((transform.localScale*2), 0.5f, 1).OnComplete(()=> transform.localScale = transform.localScale.x * 2 > 1f ? new Vector3(1, 1, 1) : transform.localScale * 2);

                transform.DOScale(transform.localScale.x * 2 > 1f ? new Vector3(1, 1, 1) : transform.localScale * 2, 0.5f);

                particleTailController.ChangeScaleOfParticleEffect(transform.localScale.x);
            }

            Destroy(other.gameObject);

        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Coin"))
        {

            //increase coin count

            coinSound.Stop();

            coinSound.Play();

            ParticleSystem waterParticleInstance = Instantiate(waterParticles);

            waterParticleInstance.transform.position = other.gameObject.transform.position;

            Destroy(waterParticleInstance.gameObject, 2f);

            Vector2 coinScreenPos = Camera.main.WorldToScreenPoint(other.transform.position);


            Destroy(other.gameObject);


            coinTweenController.TweenCoins(coinScreenPos);

            


        }

        other.GetComponent<Collider>().enabled = false;

    }

}
