using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerRef;

    //xyz offset between tail and player.
    [SerializeField] Vector3 offset;
    private void Update()
    {
        transform.position=playerRef.transform.position+offset;
    }

    public void ChangeScaleOfParticleEffect(float size)
    {
        transform.localScale = new Vector3(size, size, size);
    }
}
