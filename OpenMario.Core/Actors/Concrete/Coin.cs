namespace OpenMario.Core.Actors.Concrete
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// The coin.
    /// </summary>
    public class Coin : StaticBox
    {
        private Bitmap drawable;

        /// <summary>
        /// Initializes a new instance of the <see cref="Coin"/> class.
        /// </summary>
        public Coin()
        {
            this.Width = 30;
            this.Height = 30;
        }

        /// <summary>
        /// The Load method for <see cref="Coin"/>
        /// </summary>
        /// <param name="env">The env.</param>
        public override void Load(Environment.Environment env)
        {
            this.Environment = env;

            Bitmap B = new Bitmap(this.Width, this.Height);
            for (int i = B.Height / 3; i < 2 * B.Height / 3; i++)
                for (int j = B.Width / 3; j < 2* B.Width / 3; j++)
                    B.SetPixel(j, i, Color.Yellow);
            this.drawable = B;

            this.CollisionAction = CollisionType.NoAction;
        }

        /// <summary>
        /// The drawing method for the <see cref="Coin"/> actor.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> for the <see cref="Coin"/> actor</param>
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
                    Environment.ActorsToRemove.Add(this);
                }
            }
        }
    }
}
