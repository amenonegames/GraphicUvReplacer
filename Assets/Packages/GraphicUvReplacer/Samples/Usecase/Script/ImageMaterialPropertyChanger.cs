
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace Packages.GraphicUvReplacer.Samples.Usecase.Script
{
    public class ImageMaterialPropertyChanger :MonoBehaviour
    {
        private Image _image;
        private Image ThisImage => _image is null ? _image = GetComponent<Image>()
            : _image;
        
        private Material _material;
        private Material ThisMaterial => _material ??= ThisImage.material;

        private static readonly int DisolveIntensity = Shader.PropertyToID("_DisolveIntensity");

        private void Start()
        {
            var mat = new Material(ThisMaterial);
            ThisImage.material = mat;
            var duration = Random.Range(1, 5);
            ThisImage.material.SetFloat(DisolveIntensity,1);
            ThisImage.material.DOFloat(0, DisolveIntensity, duration).SetEase(Ease.Linear);
        }
    }
}