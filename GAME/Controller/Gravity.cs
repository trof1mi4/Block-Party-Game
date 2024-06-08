using System.Diagnostics;

namespace WindowsFormsApp1
{
    public class Gravity 
    {
        private double jumpVelocity = 11.0;
        private const double g = 2.4;     
        private double verticalVelocity;
        private Stopwatch time;
        private double deltaTime;
        private Player player;
        private Collision collision;

        public Gravity(Player player, Collision collision)
        {
            this.player = player;
            this.collision = collision;
            time = new Stopwatch();
        }

        public void Jump()
        {
            if (!player.isJumping)
            {
                time.Start();
                verticalVelocity = jumpVelocity; 
                player.isJumping = true; 
            }
        }

        public void Falling()
        {
            if (!collision.IsOnBlock() && !player.isJumping)
                player.isFalling = true;
        }

        public void ApplyGravity() 
        {
            var blockUnderPlayer = collision.GetBlockUnderPlayer();

            if (player.isJumping)
            {
                deltaTime = time.ElapsedMilliseconds / 1000.0;
                verticalVelocity -= g * deltaTime;
                player.posY -= (int)((jumpVelocity + verticalVelocity) * deltaTime / 2);
                if (blockUnderPlayer != null && collision.PlayerFellOnBlock())
                {
                    player.posY = blockUnderPlayer.yCoord - player.sizeSprite;
                    player.isJumping = false;
                    time.Reset();
                    verticalVelocity = 0;
                }
            }

            if (player.isFalling)
            {
                player.posY += 7;
                if (blockUnderPlayer != null && collision.PlayerFellOnBlock())
                {
                    player.posY = blockUnderPlayer.yCoord - player.sizeSprite;
                    player.isFalling = false;
                }
            }
        }
    }
}
