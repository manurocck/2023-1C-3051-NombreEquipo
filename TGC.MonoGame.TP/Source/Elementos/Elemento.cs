
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PistonDerby.Autos.PowerUps;
using PistonDerby.Drawers;

namespace PistonDerby.Elementos;
public abstract class Elemento {
    private const bool DEBUG_GIZMOS = true;
    internal abstract Model Model { get; }
    internal abstract IDrawer Drawer { get; }
    internal abstract Matrix World { get; }

    internal virtual void Draw() {
        Drawer.Draw(Model, World);
        if (PistonDerby.DEBUG_GIZMOS) this.DebugGizmos();
    }

    internal virtual bool OnCollision(Elemento other) {
        if(other is MachineGun){
            return false;
        }
        return true;
    }
    internal virtual bool OnCollision(Elemento other, Vector3 normal, float depth) {
        if(other is MachineGun){
            return false;
        }
        return true;
    }
    protected abstract void DebugGizmos();
}