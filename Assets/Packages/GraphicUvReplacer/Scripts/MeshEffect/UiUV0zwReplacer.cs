﻿using System.Buffers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Amenone.GraphicUvReplacer.MeshEffect
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Graphic))]
    public class UiUV0zwReplacer : BaseMeshEffect
    {

        private Vector2 zw;

        public Vector2 ZW
        {
            get { return zw; }
            set
            {
                zw = value;
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
                vec4[i].x = vt.uv0.x;
                vec4[i].y = vt.uv0.y;
                vec4[i].z = ZW.x;
                vec4[i].w = ZW.y;
                
                vt.uv0 = vec4[i];
                verts[i] = vt;
            }

            ArrayPool<Vector4>.Shared.Return(vec4);

        }
        
        
    }
}