using System.Buffers;
using UnityEngine;

namespace Packages.GraphicUvReplacer.Scripts.MeshConrol
{
    public class MeshUv1Replacer : MonoBehaviour
    {
        private MeshFilter _meshFilter;
        private MeshFilter MeshFilter => _meshFilter is null 
            ? _meshFilter = GetComponent<MeshFilter>()
            : _meshFilter;
        
        private Mesh _mesh;
        private Mesh Mesh => _mesh is null 
            ? _mesh = MeshFilter.mesh 
            : _mesh;

        public Vector2 Uv0
        {
            get => Mesh.uv[0];
            set
            {
                ApplyVerts(Mesh , 0 , value);
            }
        }
        
        public Vector2 Uv1
        {
            get => Mesh.uv2[0];
            set
            {
                ApplyVerts(Mesh , 1 , value);
            }
        }
        
        public Vector2 Uv2
        {
            get => Mesh.uv3[0];
            set
            {
                ApplyVerts(Mesh , 2 , value);
            }
        }
        
        public Vector2 Uv3
        {
            get => Mesh.uv4[0];
            set
            {
                ApplyVerts(Mesh , 3 , value);
            }
        }
        
        public Vector2 Uv4
        {
            get => Mesh.uv5[0];
            set
            {
                ApplyVerts(Mesh , 4 , value);
            }
        }
        
        public Vector2 Uv5
        {
            get => Mesh.uv6[0];
            set
            {
                ApplyVerts(Mesh , 5 , value);
            }
        }
        
        public Vector2 Uv6
        {
            get => Mesh.uv7[0];
            set
            {
                ApplyVerts(Mesh , 6 , value);
            }
        }
        
        private void ApplyVerts( Mesh mesh , int uvIndex , Vector2 value)
        {
            switch (uvIndex)
            {
                case 0 :
                    for (int i = 0; i < mesh.uv.Length; ++i)
                    {
                        mesh.uv[i] = value;
                    }
                    break;
                
                case 1 :
                    for (int i = 0; i < mesh.uv2.Length; ++i)
                    {
                        mesh.uv2[i] = value;
                    }
                    break;
                
                case 2 :
                    for (int i = 0; i < mesh.uv3.Length; ++i)
                    {
                        mesh.uv3[i] = value;
                    }
                    break;
                
                case 3 :
                    for (int i = 0; i < mesh.uv4.Length; ++i)
                    {
                        mesh.uv4[i] = value;
                    }
                    break;
                
                case 4 :
                    for (int i = 0; i < mesh.uv5.Length; ++i)
                    {
                        mesh.uv5[i] = value;
                    }
                    break;
                
                case 5 :
                    for (int i = 0; i < mesh.uv6.Length; ++i)
                    {
                        mesh.uv6[i] = value;
                    }
                    break;
                
                case 6 :
                    for (int i = 0; i < mesh.uv7.Length; ++i)
                    {
                        mesh.uv7[i] = value;
                    }
                    break;
                
                default :
                    Debug.LogError("Invalid Uv Index");
                    return;
            }
            
        }
        

        
        
    }
}