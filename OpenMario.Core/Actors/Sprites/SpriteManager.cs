using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Actors.Sprites
{
    public abstract class SpriteManager
    {
        protected readonly BaseActor _actor;

        public SpriteManager(BaseActor a)
        {
            _actor = a;
        }

        public Bitmap CurrentSprite { get; protected set; }
        public int SpriteWidth { get; protected set; }
        public int SpriteHeight { get; protected set; }

        public abstract void Load();
        public abstract void Update(List<BaseActor> loadedactors);
    }
}
