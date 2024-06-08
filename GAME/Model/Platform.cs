using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WindowsFormsApp1
{
    public class Platform
    {
        public Block[] Blocks { get; private set; }
        private Random random;
        private int blockCount = 20;

        public Platform()
        {
            random = new Random();
        }

        public void GeneratePlatform(Image targetColor, int targetBlocksCount, int level)
        {
            Blocks = new Block[blockCount];
            var rainbowColors = new [] { LevelDrawing.blueBlock, LevelDrawing.greenBlock, LevelDrawing.limeBlock, LevelDrawing.orangeBlock, LevelDrawing.purpleBlock, LevelDrawing.redBlock, LevelDrawing.yellowBlock };
            var availableColors = rainbowColors.Where(color => color != targetColor).ToArray();
            var allBlocks = new List<Block>();

            var minYOffset = Math.Max(-18, -2 * level);
            var maxYOffset = Math.Min(18, 2 * level);
            var minEmptySpaces = Math.Min(3, Math.Max(0, level - 5));

            for (int i = 0; i < targetBlocksCount; i++)
                allBlocks.Add(new Block(0, 240, targetColor));

            for (int i = targetBlocksCount; i < blockCount - 3 - minEmptySpaces; i++)
            {
                Image randomColor = availableColors[random.Next(availableColors.Length)];
                allBlocks.Add(new Block(0, 240, randomColor));
            }

            var whiteBlocksCount = random.Next(1, 4);
            for (int i = 0; i < whiteBlocksCount; i++)
                allBlocks.Add(new Block(0, 240, LevelDrawing.whiteBlock));

            allBlocks = allBlocks.OrderBy(x => random.Next()).ToList();

            var currentX = 0;
            var whiteBlockStreak = 0;
            var emptySpaceCount = 0;
            for (int i = 0; i < blockCount; i++)
            {
                if (i < allBlocks.Count)
                {
                    var yOffset = random.Next(minYOffset, maxYOffset + 1);

                    allBlocks[i].xCoord = currentX;
                    allBlocks[i].yCoord = Math.Max(0, Math.Min(480 - Block.size, 240 + yOffset));

                    if (allBlocks[i].blockColor == LevelDrawing.whiteBlock)
                        whiteBlockStreak++;
                    else
                        whiteBlockStreak = 0;

                    if (whiteBlockStreak > 3)
                    {
                        var randomColor = availableColors[random.Next(availableColors.Length)];
                        allBlocks[i].blockColor = randomColor;
                        whiteBlockStreak = 0;
                    }

                    Blocks[i] = allBlocks[i];
                }
                else
                {
                    if (emptySpaceCount < minEmptySpaces)
                    {
                        Blocks[i] = new Block(currentX, 240, LevelDrawing.whiteBlock);
                        emptySpaceCount++;
                    }
                    else
                        Blocks[i] = new Block(currentX, 240, LevelDrawing.whiteBlock);
                }
                currentX += Block.size;
            }

            while (Blocks.Length < blockCount)
            {
                Blocks.Append(new Block(currentX, 240, LevelDrawing.whiteBlock));
                currentX += Block.size;
            }
        }
    }
}
