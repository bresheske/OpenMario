using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Actors
{
    public abstract class CollidableActor : BaseActor
    {
        public abstract override void Load();
        public abstract override void Draw(System.Drawing.Graphics g);
        public abstract override void Update(List<BaseActor> loadedactors);

        public List<CollidableActor> GetCollidableActors(List<BaseActor> actors)
        {
            return actors
                .Where(x => x.GetType().IsAssignableFrom(typeof(CollidableActor)))
                .Select(x => (CollidableActor)x)
                .ToList();
        }

        public bool CollidedWith(CollidableActor a)
        {
            return new Rectangle(Position.X, Position.Y, Width, Height)
                .IntersectsWith(new Rectangle(a.Position.X, a.Position.Y, a.Width, a.Height));
        }

        public List<CollidableActor> GetAllCollisions(List<CollidableActor> a)
        {
            return a
                .Where(x => CollidedWith(x))
                .ToList();
        }
    }
}