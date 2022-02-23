using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float jumpItr = 4.5f;
    [Tooltip("Period of each shake")]
    [SerializeField] private float shakePeriodTime = 0.25f;
    [Tooltip("How long it take to shake settle Down to nothing")]
    [SerializeField] private float dropOffTime = 1.25f;
    [SerializeField] private float shakeOffset = 0.01f;
    [SerializeField] private float heightOffset = 0.2f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))    Shake();
    }

    public void Shake()
    {
        float height = Mathf.PerlinNoise(jumpItr, 0) * 5;
        height = height * height * heightOffset;

        float shakeAmt = height * shakeOffset;

        LTDescr shakeTween = LeanTween.rotateAroundLocal(gameObject, Vector3.right, shakeAmt, shakePeriodTime)
            .setEase(LeanTweenType.easeShake).setLoopClamp().setRepeat(-1);

        LeanTween.value(gameObject, shakeAmt, 0, dropOffTime).setOnUpdate((float val) =>
        {
            shakeTween.setTo(Vector3.right * val);
        }).setEase(LeanTweenType.easeOutQuad);
    }
}
