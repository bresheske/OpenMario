using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Actors.Concrete
{
    public class StaticTransparentBox : StaticBox
    {
        public StaticTransparentBox()
        {
            //Not collidable.
            CollisionAction = CollisionType.NO_ACTION;
        }
    }
}