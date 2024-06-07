using UnityEngine;
using UnityEngine.UI;

namespace Packages.GraphicUvReplacer.Scripts.Abstract
{
    public abstract class UvzwReplacer : BaseMeshEffect
    {
        public abstract Vector4 UVZW { get; set; }
    }
}