using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Packages.GraphicUvReplacer.Scripts.Abstract;
using UnityEngine;

namespace Packages.GraphicUvReplacer.Samples.DotweenExtension
{
    public static class ReplacerExtension
    {
        public static async UniTask EaseZWAsync(this ZwReplacer replacer, Vector2 endValue, float duration, CancellationToken token)
        {
            await DOTween.To(() => replacer.ZW, (x) => replacer.ZW = x, endValue, duration).WithCancellation(token);
        } 
        
        
    }
}