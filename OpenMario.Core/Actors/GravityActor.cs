using System.Collections.Generic;

namespace OpenMario.Core.Actors
{
    public abstract class GravityActor : MovementActor
    {
        public abstract override void Load();

        public abstract override void Draw(System.Drawing.Graphics g);

        public override void Move()
        {
            if (Velocity.Y < Physics.Physics.MAX_GRAVITY.Y)
                this.Velocity -= Physics.Physics.GRAVITY;
            else if (Velocity.Y > Physics.Physics.MAX_GRAVITY.Y)
                this.Velocity += Physics.Physics.GRAVITY;

            Position -= Velocity;
        }

        public override void Update(List<BaseActor> loadedactors)
        {
            Move();
        }
    }
}