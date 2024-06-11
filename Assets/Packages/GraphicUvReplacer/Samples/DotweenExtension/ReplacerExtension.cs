using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Packages.GraphicUvReplacer.Scripts.Abstract;
using Packages.GraphicUvReplacer.Scripts.MeshConrol;
using UnityEngine;

namespace Packages.GraphicUvReplacer.Samples.DotweenExtension
{
    public static class ReplacerExtension
    {
        public static async UniTask EaseZWAsync(this ZwReplacer replacer, Vector2 endValue, float duration, CancellationToken token)
        {
            await DOTween.To(() => replacer.ZW, (x) => replacer.ZW = x, endValue, duration).WithCancellation(token);
        } 
        
        public static async UniTask EaseUv1Async(this MeshUv1Replacer replacer, Vector2 endValue, float duration, CancellationToken token)
        {
            await DOTween.To(() => replacer.Uv1 , ( Vector2 x) => replacer.Uv1 = x, endValue, duration);
        }
        
        public static async UniTask EaseUv2Async(this MeshUv1Replacer replacer, Vector2 endValue, float duration, CancellationToken token)
        {
            await DOTween.To(() => replacer.Uv2 , ( Vector2 x) => replacer.Uv2 = x, endValue, duration);
        }
        
        public static async UniTask EaseUv3Async(this MeshUv1Replacer replacer, Vector2 endValue, float duration, CancellationToken token)
        {
            await DOTween.To(() => replacer.Uv3 , ( Vector2 x) => replacer.Uv3 = x, endValue, duration);
        }
        
        public static async UniTask EaseUv4Async(this MeshUv1Replacer replacer, Vector2 endValue, float duration, CancellationToken token)
        {
            await DOTween.To(() => replacer.Uv4 , ( Vector2 x) => replacer.Uv4 = x, endValue, duration);
        }
        
        public static async UniTask EaseUv5Async(this MeshUv1Replacer replacer, Vector2 endValue, float duration, CancellationToken token)
        {
            await DOTween.To(() => replacer.Uv5 , ( Vector2 x) => replacer.Uv5 = x, endValue, duration);
        }
        
        public static async UniTask EaseUv6Async(this MeshUv1Replacer replacer, Vector2 endValue, float duration, CancellationToken token)
        {
            await DOTween.To(() => replacer.Uv6 , ( Vector2 x) => replacer.Uv6 = x, endValue, duration);
        }
        
        
    }
}