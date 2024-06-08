using System.Drawing;

namespace WindowsFormsApp1
{
    public class AnimationPlayer
    {
        public static Image stayR;
        public static Image stayL;
        public static Image walkR;
        public static Image walkL;
        public static Image jumpR;
        public static Image jumpL;

        private Image sprite;

        private int currentFrame;
        private int frameLimit;

        private Player player;

        public AnimationPlayer (Player player) 
        {
            sprite = stayR;
            this.player = player;
        }

        public void PlayAnimation(Graphics g)
        {
            g.DrawImage(sprite, player.posX, player.posY, new Rectangle(new Point(32 * currentFrame, 0), new Size(player.sizeSprite, player.sizeSprite)), GraphicsUnit.Pixel);

            if ((player.isMovingRight ^ player.isMovingLeft) ^ player.isJumping)
                currentFrame++;
            if (currentFrame >= frameLimit)
                currentFrame = 0;
        }

        public void UpdatingSprites()
        {
            if (player.isMovingRight)
            {
                sprite = walkR;
                frameLimit = 6;
            }
            else if (player.isMovingLeft)
            {
                sprite = walkL;
                frameLimit = 6;
            }
            else if (player.isJumping)
            {
                if (sprite == walkR || sprite == stayR)
                    sprite = jumpR;
                else if (sprite == walkL || sprite == stayL)
                    sprite = jumpL;
                frameLimit = 0;
            }
        }

        public void GetStopSprite()
        {
            if (sprite == walkR || sprite == jumpR)
                sprite = stayR;
            else if (sprite == walkL || sprite == jumpL)
                sprite = stayL;
            currentFrame = 0;
        }
    }
}
