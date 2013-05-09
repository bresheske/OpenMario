using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Physics
{
    public static class Physics
    {
        public static readonly VectorClass.Vector2D_Int GRAVITY = new VectorClass.Vector2D_Int(0, -1);
        public static readonly VectorClass.Vector2D_Int MAX_GRAVITY = new VectorClass.Vector2D_Int(0, -9);
    }
}
