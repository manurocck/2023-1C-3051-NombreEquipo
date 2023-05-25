using System;
using Microsoft.Xna.Framework;

namespace PistonDerby.Utils;
public static class ModelExtensions {
    /// <summarize> Devuelve el tamaño de la caja que envuelve tu corazon </summarize> 
    public static Vector3 Dimensiones(this Microsoft.Xna.Framework.Graphics.Model model)
    {
        var minPoint = Vector3.One * float.MaxValue;
        var maxPoint = Vector3.One * float.MinValue;

        var transforms = new Matrix[model.Bones.Count];
        model.CopyAbsoluteBoneTransformsTo(transforms);

        var meshes = model.Meshes;
        for (int index = 0; index < meshes.Count; index++)
        {
            var meshParts = meshes[index].MeshParts;
            for (int subIndex = 0; subIndex < meshParts.Count; subIndex++)
            {
                var vertexBuffer = meshParts[subIndex].VertexBuffer;
                var declaration = vertexBuffer.VertexDeclaration;
                var vertexSize = declaration.VertexStride / sizeof(float);

                var rawVertexBuffer = new float[vertexBuffer.VertexCount * vertexSize];
                vertexBuffer.GetData(rawVertexBuffer);

                for (var vertexIndex = 0; vertexIndex < rawVertexBuffer.Length; vertexIndex += vertexSize)
                {
                    var transform = transforms[meshes[index].ParentBone.Index];
                    var vertex = new Vector3(rawVertexBuffer[vertexIndex], rawVertexBuffer[vertexIndex + 1], rawVertexBuffer[vertexIndex + 2]);
                    vertex = Vector3.Transform(vertex, transform);
                    minPoint = Vector3.Min(minPoint, vertex);
                    maxPoint = Vector3.Max(maxPoint, vertex);
                }
            }
        }
        return new Vector3(
            Math.Abs(minPoint.X - maxPoint.X),
            Math.Abs(minPoint.Y - maxPoint.Y),
            Math.Abs(minPoint.Z - maxPoint.Z)
            );
    }
}
