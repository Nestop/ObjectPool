using System;
using DG.Tweening;
using UnityEngine;
using Utils.Pool;

namespace Game.FX
{
    public class DamageScreen : MonoBehaviour, IPoolable
    {
        public event Action<IPoolable> ObjectDeactivation;

        [SerializeField] private Renderer backgroundRenderer;
        [SerializeField] private Renderer damageRenderer;
        [SerializeField] private TextMesh damageLabel;
        [Header("Initial values")]
        [SerializeField] private Color backgroundStartColor;
        [SerializeField] private Gradient damageLabelStartColor;
        [SerializeField] private float liftingHeightInUnits = 2f;

        public void ShowDamage(int damage, float timeForShow, Vector3 position, Vector3 lookAt)
        {
            var labelColor = damageLabelStartColor.Evaluate(damage/100f);
            damageRenderer.material.color = labelColor;
            backgroundRenderer.material.color = backgroundStartColor;
            transform.position = position;
            transform.LookAt(lookAt);
            damageLabel.text = $"-{damage.ToString()}";

            var screenAnimation = DOTween.Sequence();
            screenAnimation.Append(transform.DOMoveY(transform.position.y + liftingHeightInUnits, timeForShow));
            screenAnimation.Join(backgroundRenderer.material.DOColor(Color.clear, timeForShow));
            labelColor.a = 0;
            screenAnimation.Join(damageRenderer.material.DOColor(labelColor, timeForShow));
            screenAnimation.OnComplete(() => ObjectDeactivation?.Invoke(this));
        }
    }
}