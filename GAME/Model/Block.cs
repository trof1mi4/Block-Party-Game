using System.Drawing;

namespace WindowsFormsApp1
{
    public class Block
    {
        public Image blockColor;
        public int xCoord;
        public int yCoord;
        public static readonly int size = 36;

        public Block(int x, int y, Image image)
        {
            xCoord = x;
            yCoord = y;
            blockColor = image;
        }
    }
}
