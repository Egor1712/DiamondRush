using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class Hammer : Weapon
    {
        public Hammer()
        {
            IsFrozen = false;
            Force = 12;
            ImageName = "Simple Hummer";
        }
    }
}