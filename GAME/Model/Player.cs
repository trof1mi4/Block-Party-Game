using System;

namespace WindowsFormsApp1
{
    public class Player
    {
        public int posX;
        public int posY;
        public int dirXRight;
        public int dirXLeft;
        public bool isMovingRight;
        public bool isMovingLeft;
        public bool isJumping = false;
        public bool isFalling = false;
        public readonly int sizeSprite = 32;
        public readonly int areaTuch = 14;

        public Player(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }

        public void Move()
        {
            if (isMovingRight)
                posX += dirXRight;
            if (isMovingLeft)
                posX += dirXLeft;
        }

        public bool PlayerIsDead() => posY >= 270;
    }
}
