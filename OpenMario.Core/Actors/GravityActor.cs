using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Actors
{
    public abstract class GravityActor : MovementActor
    {
        public abstract override void Load();

        public abstract override void Draw(System.Drawing.Graphics g);

        public override void Move()
        {
            if (this.Velocity.Y < Physics.Physics.MAX_GRAVITY.Y)
                this.Velocity -= Physics.Physics.GRAVITY;
            else if (this.Velocity.Y > Physics.Physics.MAX_GRAVITY.Y)
                this.Velocity += Physics.Physics.GRAVITY;

            Position -= Velocity;
        }

        public override void Update(List<BaseActor> loadedactors)
        {
            Move();
        }
    }
}