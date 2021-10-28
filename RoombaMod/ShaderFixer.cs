using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Thicket
{
    public class ShaderSetter : MonoBehaviour
    {
        public ShaderEnum shader;
        public string[] shaders = 
            {
            "psx/vertexlit/vertexlit",
            "psx/unlit/unlit",
            "psx/vertexlit/transparent/transparent",
            "psx/vertexlit/transparent/zwrite",
            "psx/unlit/transparent/unlit"
             };
        Shader myShader;

        public enum ShaderEnum
        {
            psxVertexLit,psxUnlit,psxVertexLitTransparent,psxVertexLitTransparentZwrite,psxUnlitTransparent
        }

        public void Start()
        {
            myShader = Shader.Find(shaders[(int)shader]);

            // if this is true, we know that we are the top in the tree.
            if (GetComponentInParent<ShaderSetter>() == null)
            {
                // therefore it is our job to: set the shaders in all meshrenderers, then tell the shadersetters in the children to run afterwards so that they override our initial setting.
                SetShaderRecursively();
                foreach (var setter in GetComponentsInChildren<ShaderSetter>(true))
                {
                    setter.SetShaderRecursively();
                }
            }

            // do nothing if we arent the topmost setter, because the topmost setter will call us when it is our turn to run
        }


        public void SetShaderRecursively()
        {
            foreach (var mr in GetComponentsInChildren<MeshRenderer>(true))
            {
                var fuckoff = mr.materials;
                foreach (Material m in fuckoff)
                {
                    m.shader = myShader;
                }
            }
        }
    }
}
