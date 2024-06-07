using System.Buffers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace View.DialogueView.MeshEffect
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Graphic))]
    public class UiUV3xyzwReplacer : BaseMeshEffect
    {
        
        private Vector4 uvzw;

        public Vector4 UVXW
        {
            get { return uvzw; }
            set
            {
                uvzw = value;
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }
        
        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
                return;

            var output = ListPool<UIVertex>.Get();
            vh.GetUIVertexStream(output);
            ApplyVerts(output);
            vh.Clear();
            vh.AddUIVertexTriangleStream(output);
            ListPool<UIVertex>.Release(output);
        }

        private void ApplyVerts(List<UIVertex> verts)
        {
            UIVertex vt;
            Vector4[] vec4 = ArrayPool<Vector4>.Shared.Rent(verts.Count);

            for (int i = 0; i < verts.Count; ++i)
            {
                vt = verts[i];
                vec4[i].x = UVXW.x;
                vec4[i].y = UVXW.y;
                vec4[i].z = UVXW.z;
                vec4[i].w = UVXW.w;
                
                vt.uv3 = vec4[i];
                verts[i] = vt;
            }

            ArrayPool<Vector4>.Shared.Return(vec4);

        }
    }
}