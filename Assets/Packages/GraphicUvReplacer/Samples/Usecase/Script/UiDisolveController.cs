using System;
using Cysharp.Threading.Tasks;
using Packages.GraphicUvReplacer.Samples.DotweenExtension;
using Packages.GraphicUvReplacer.Scripts.Abstract;
using UnityEngine;

namespace Packages.GraphicUvReplacer.Samples.Usecase.Script
{
    public class UiDisolveController : MonoBehaviour
    {
        private ZwReplacer _replacer;
        private ZwReplacer Replacer => _replacer??= GetComponent<ZwReplacer>();

        private void Start()
        {
            Replacer.ZW = Vector2.one;
            
            Vector2 endValue = new Vector2(0, 0);

            Replacer.EaseZWAsync(endValue, 1.0f, this.GetCancellationTokenOnDestroy()).Forget();
        }
    }
}