using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP.Drawers
{
    internal class TextureDrawer : IDrawer
    {
        protected Effect Effect = TGCGame.GameContent.E_TextureShader;
        protected readonly Model Model;
        protected readonly Texture2D Texture;

        internal TextureDrawer(Model Model, Texture2D Texture)
        {
            this.Model = Model;
            this.Texture = Texture;
        }

        void IDrawer.Draw(Matrix World)
        {
            ModelMeshCollection meshes = Model.Meshes;

            foreach (ModelMesh mesh in meshes)
            {
                Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * World);
                Effect.Parameters["Texture"].SetValue(Texture);
                mesh.Draw();
            }
        }
    }
}