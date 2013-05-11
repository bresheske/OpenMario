using OpenMario.Core.Actors.Concrete;
using OpenMario.Core.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorClass;

namespace OpenMario.Environments.OnePlayerEnvironments
{
    public class LevelOne : Core.Environment.Environment
    {

        public LevelOne()
        {

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


            //Players
            Actors.Add(new Mario(Players[0]));

            this.Width = 1500;
        }
    }
}