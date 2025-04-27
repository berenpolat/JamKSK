using UnityEngine;
using DG.Tweening; // DOTween kullanacağız

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private float fixedY;
    private float fixedZ;

    private Vector3 extraShakeOffset = Vector3.zero; // Ekstra titreme için

    void Start()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        float desiredX = target.position.x + offset.x;
        Vector3 desiredPosition = new Vector3(desiredX, fixedY + offset.y, fixedZ + offset.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position - extraShakeOffset, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition + extraShakeOffset;
    }

    public void ShakeCamera(float duration, float strength, int vibrato = 10, float randomness = 90f)
    {
        // Şu anki shake varsa iptal et
        DOTween.Kill(transform);

        // Kamera üzerine değil, extra offset üzerine DOShake yapıyoruz
        DOTween.To(() => extraShakeOffset, x => extraShakeOffset = x, Random.insideUnitSphere * strength, duration)
            .SetEase(Ease.OutQuad)
            .SetLoops(vibrato, LoopType.Yoyo)
            .OnComplete(() => extraShakeOffset = Vector3.zero);
    }
}