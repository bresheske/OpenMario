using OpenMario.Core.Actors.Concrete;
using OpenMario.Core.Players;
using VectorClass;

namespace OpenMario.Environments.OnePlayerEnvironments
{
    public class LevelOne : Core.Environment.Environment
    {

        public LevelOne()
        {
            //Sounds
            MusicAsset = @"Assets\overworldtheme.mp3";

            //Controls
            Players.Add(new PlayerOne());
            
            //Backgrounds
            Actors.Add(new Cloud());

            //Actors
            Actors.Add(new OrangeLand()
            {
                Position = new Vector2D_Dbl(0, 400),
                Width = 1500,
                Height = 10
            });
            Actors.Add(new QuestionBox()
            {
                Position = new Vector2D_Dbl(300, 300)
            });
            Actors.Add(new Goomba()
            {
                Position = new Vector2D_Dbl(400, 100),
                WalkingVelocity = new Vector2D_Dbl(1, 0)
            });
            Actors.Add(new GreenStaticPipe()
            {
                Position = new Vector2D_Dbl(380, 340)
            });
            Actors.Add(new GreenStaticPipe()
            {
                Position = new Vector2D_Dbl(520, 340)
            });

            //Players
            Actors.Add(new Mario(Players[0]));

            this.Width = 1500;
        }
    }
}