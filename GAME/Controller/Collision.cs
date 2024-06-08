using System.Drawing;

namespace WindowsFormsApp1
{
    public class Collision
    {
        private Player player;
        private Platform platform;

        public Collision(Player player, Platform platform)
        {
            this.player = player;
            this.platform = platform;
        }

        public void CheckHorizontalCollisions()
        {
            var playerRect = new Rectangle(player.posX + 8, player.posY + 4, 16, 28);

            foreach (var block in platform.Blocks)
            {
                if (block.yCoord < player.posY + player.sizeSprite || block.yCoord + Block.size > player.posY + 4)
                {
                    if ((player.isMovingRight && block.xCoord > player.posX && block.xCoord <= player.posX + player.dirXRight + player.sizeSprite) ||
                        (player.isMovingLeft && block.xCoord < player.posX && block.xCoord + Block.size >= player.posX + player.dirXLeft))
                    {
                        var blockRect = new Rectangle(block.xCoord, block.yCoord, Block.size, Block.size);
                        if (playerRect.IntersectsWith(blockRect) && block.blockColor != LevelDrawing.whiteBlock)
                        {
                            if (player.isMovingRight)
                            {
                                if (block.yCoord > player.posY && player.posY + player.sizeSprite - block.yCoord <= 10 && !player.isJumping && !player.isFalling)
                                    player.posY = block.yCoord - player.sizeSprite; 
                                player.posX = block.xCoord - player.sizeSprite + 8;
                            }
                            if (player.isMovingLeft)
                            {
                                if (block.yCoord > player.posY && player.posY + player.sizeSprite - block.yCoord <= 10 && !player.isJumping && !player.isFalling)
                                    player.posY = block.yCoord - player.sizeSprite; 
                                player.posX = block.xCoord + Block.size - 8;
                            }
                        }
                    }
                }
            }
        }

        public bool PlayerFellOnBlock()
        {
            var blockUnderPlayer = GetBlockUnderPlayer();
            var playerRect = new Rectangle(player.posX + 9, player.posY + 4, 14, 28);
            var blockRect = new Rectangle(blockUnderPlayer.xCoord, blockUnderPlayer.yCoord, Block.size, Block.size);
            return blockRect.IntersectsWith(playerRect);
        }

        public Block GetBlockUnderPlayer()
        {
            Block highestBlock = null;

            foreach (Block block in platform.Blocks)
            {
                var intersectX = player.posX + (player.sizeSprite - player.areaTuch) / 2 + player.areaTuch >= block.xCoord &&
                                 player.posX + (player.sizeSprite - player.areaTuch) / 2 <= block.xCoord + Block.size;
                if (intersectX && block.blockColor != LevelDrawing.whiteBlock)
                    if (highestBlock == null || block.yCoord < highestBlock.yCoord)
                        highestBlock = block;
            }
            return highestBlock;
        }

        public bool IsOnBlock()
        {
            foreach (Block block in platform.Blocks)
            {
                var intersectX = player.posX + (player.sizeSprite - player.areaTuch) / 2 + player.areaTuch >= block.xCoord &&
                    player.posX + (player.sizeSprite - player.areaTuch) / 2 <= block.xCoord + Block.size;
                var intersectY = player.posY + player.sizeSprite >= block.yCoord && player.posY + player.sizeSprite <= block.yCoord + Block.size;
                if (intersectX && intersectY && block.blockColor != LevelDrawing.whiteBlock)
                    return true;
            }
            return false;
        }
    }
}
