using OpenMario.Core.Players;
using OpenMario.Core.Players.Actions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenMario.Core.Extensions;

namespace OpenMario.Core.Actors.Sprites
{
    public class MarioSpriteManager : SpriteManager
    {
        private Bitmap _fullmap;
        private BasePlayer _player;

        #region constants
        public const int SMALL_MARIO_WIDTH = 15;
        public const int SMALL_MARIO_HEIGHT = 20;
        public Rectangle SMALL_MARIO_STANDING_LEFT = new Rectangle(168, 0, SMALL_MARIO_WIDTH, SMALL_MARIO_HEIGHT);
        public Rectangle SMALL_MARIO_STANDING_RIGHT = new Rectangle(209, 0, SMALL_MARIO_WIDTH, SMALL_MARIO_HEIGHT);
        #endregion

        public MarioSpriteManager(BaseActor a, Players.BasePlayer p)
            :base(a)
        {
            _player = p;
        }

        public override void Update(List<BaseActor> loadedactors)
        {
            if (CurrentSprite == null)
                CurrentSprite = new Bitmap(_fullmap.CropImage(SMALL_MARIO_STANDING_LEFT), _actor.Width, _actor.Height);

            if (_player.IsActionPressed(new KeyMapping() { Action = KeyMapping.KeyAction.LEFT }))
            {
                CurrentSprite = GetCrop(SMALL_MARIO_STANDING_LEFT);
            }
            else if (_player.IsActionPressed(new KeyMapping() { Action = KeyMapping.KeyAction.RIGHT }))
            {
                CurrentSprite = GetCrop(SMALL_MARIO_STANDING_RIGHT);
            }
        }

        private Bitmap GetCrop(Rectangle crop)
        {
            SpriteHeight = crop.Height;
            SpriteWidth = crop.Width;
            return new Bitmap(_fullmap.CropImage(crop), _actor.Width, _actor.Height);
        }

        public override void Load()
        {
            _fullmap = (Bitmap)Image.FromFile(@"Assets\mariosheet.png");
        }
    }
}