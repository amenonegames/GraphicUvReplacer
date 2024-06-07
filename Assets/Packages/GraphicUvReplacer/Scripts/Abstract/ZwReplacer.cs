using UnityEngine;
using UnityEngine.UI;

namespace Packages.GraphicUvReplacer.Scripts.Abstract
{
    public abstract class ZwReplacer : BaseMeshEffect
    {
        public abstract Vector2 ZW { get; set; }
    }
}