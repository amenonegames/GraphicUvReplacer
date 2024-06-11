using System;
using Cysharp.Threading.Tasks;
using Packages.GraphicUvReplacer.Samples.DotweenExtension;
using Packages.GraphicUvReplacer.Scripts.Abstract;
using Packages.GraphicUvReplacer.Scripts.MeshConrol;
using UnityEngine;

namespace Packages.GraphicUvReplacer.Samples.Usecase.Script
{
    public class MeshDisolveController : MonoBehaviour
    {
        private MeshUv1Replacer _replacer;
        private MeshUv1Replacer Replacer => _replacer??= GetComponent<MeshUv1Replacer>();

        private void Start()
        {
            Replacer.Uv1 = Vector2.one;
            
            Vector2 endValue = new Vector2(0, 0);

            Replacer.EaseUv2Async(endValue, 1.0f, this.GetCancellationTokenOnDestroy()).Forget();
        }
    }
}