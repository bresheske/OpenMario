namespace OpenMario.Environments.OnePlayerEnvironments
{
    using OpenMario.Core.Actors.Concrete;
    using OpenMario.Core.Players;
    using VectorClass;

    /// <summary>
    /// The level class for Open Mario.  This will be the second level that the player interacts with.
    /// </summary>
    public class LevelTwo : Core.Environment.Environment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelTwo"/> class.
        /// </summary>
        public LevelTwo()
        {
            // Sounds
            this.MusicAsset = @"Assets\overworldtheme.mp3";

            // Controls
            Players.Add(new PlayerOne());

            // Backgrounds
            Actors.Add(new Cloud());

            // Actors
            Actors.Add(new OrangeLand { Position = new Vector2D_Dbl(0, 400), Width = 1500, Height = 50 });
            Actors.Add(new QuestionBox { Position = new Vector2D_Dbl(300, 300) });
            Actors.Add(new QuestionBox { Position = new Vector2D_Dbl(520, 300) });
            Actors.Add(new QuestionBox { Position = new Vector2D_Dbl(780, 300) });
            Actors.Add(new Goomba { Position = new Vector2D_Dbl(200, 100), WalkingVelocity = new Vector2D_Dbl(1, 0) });
            Actors.Add(new Goomba { Position = new Vector2D_Dbl(800, 100), WalkingVelocity = new Vector2D_Dbl(1, 0) });
            Actors.Add(new GreenStaticPipe { Position = new Vector2D_Dbl(380, 340) });
            Actors.Add(new GreenStaticPipe { Position = new Vector2D_Dbl(660, 340) });
            Actors.Add(new GreenStaticPipe { Position = new Vector2D_Dbl(900, 340) });
            Actors.Add(new Coin { Position = new Vector2D_Dbl(380, 300) });
            Actors.Add(new Coin { Position = new Vector2D_Dbl(450, 250) });
            Actors.Add(new Coin { Position = new Vector2D_Dbl(520, 200) });
            Actors.Add(new Coin { Position = new Vector2D_Dbl(780, 250) });
            Actors.Add(new Coin { Position = new Vector2D_Dbl(1000, 300) });
            Actors.Add(new Lava { Position = new Vector2D_Dbl(420, 380), Width = 240, Height = 20 });

            // Players
            Actors.Add(new Mario(Players[0]));

            this.Width = 1500;
        }
    }
}