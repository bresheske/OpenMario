using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Actors
{
    public abstract class MovementActor : BaseActor
    {
        public abstract void Move();
        public abstract override void Load();
        public abstract override void Draw(System.Drawing.Graphics g);

        public override void Update(List<BaseActor> loadedactors)
        {
            Move();
        }
    }
}