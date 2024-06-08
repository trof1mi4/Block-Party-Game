using System.Drawing;


namespace WindowsFormsApp1
{
    public static class LevelDrawing
    {
        public static Image sky;

        public static Image blueBlock;
        public static Image greenBlock;
        public static Image limeBlock;
        public static Image orangeBlock;
        public static Image purpleBlock;
        public static Image redBlock;
        public static Image yellowBlock;
        public static Image whiteBlock;

        public static void DrawingSky(Graphics g) => g.DrawImage(sky, 0, 0, new Rectangle(new Point(0, 0), new Size(720, 480)), GraphicsUnit.Pixel);

        public static void DrawingPlatform(Graphics g, Platform platform)
        {
            foreach (var block in platform.Blocks)
                g.DrawImage(
                    block.blockColor,
                    new Rectangle(block.xCoord, block.yCoord, Block.size, Block.size),
                    new Rectangle(0, 0, 64, 64),
                    GraphicsUnit.Pixel);
        }
    }
}
