namespace OpenMario.Core.Actors.Concrete
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// The lava.
    /// </summary>
    public class Lava : StaticBox
    {
        private Bitmap drawable;

        /// <summary>
        /// The Load method for <see cref="Lava"/>
        /// </summary>
        /// <param name="env">The env.</param>
        public override void Load(Environment.Environment env)
        {
            this.Environment = env;

            Bitmap B = new Bitmap(this.Width, this.Height);
            for (int i = 0; i < B.Height; i++)
                for (int j = 0; j < B.Width; j++)
                    B.SetPixel(j, i, Color.DarkRed);
            this.drawable = B;

            this.CollisionAction = CollisionType.NoAction;
        }

        /// <summary>
        /// The drawing method for the <see cref="Lava"/> actor.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> for the <see cref="Lava"/> actor</param>
        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(this.drawable, (int)pos.X, (int)pos.Y);
        }

        public override void Update(List<BaseActor> loadedactors)
        {
            /* collision options */
            foreach (BaseActor actor in loadedactors)
            {
                if (actor is Mario && Physics.Physics.GetCollisionType(actor, this) != Physics.Physics.CollisionType.None)
                {
                    Environment.ActorsToRemove.Add(actor);
                }
            }
        }
    }
}
