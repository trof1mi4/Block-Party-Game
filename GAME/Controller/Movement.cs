using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Movement
    {
        private Player player;
        private Gravity gravity;
        private AnimationPlayer animation;
        public bool isMovingRight;
        public bool isMovingLeft;
        public int dirXRight;
        public int dirXLeft;

        public Movement(Player player, Gravity gravity, AnimationPlayer animation)
        {
            this.player = player;
            this.gravity = gravity;
            this.animation = animation;
        }

        public void PlayerMovement(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    player.dirXLeft = -4;
                    player.isMovingLeft = true;
                    break;
                case Keys.D:
                    player.dirXRight = 4;
                    player.isMovingRight = true;
                    break;
                case Keys.W:
                    gravity.Jump();
                    break;
                case Keys.Space:
                    gravity.Jump();
                    break;
            }
        }

        public void StoppingPlayer(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                player.isMovingRight = false;
                player.dirXRight = 0;
            }

            if (e.KeyCode == Keys.A)
            {
                player.isMovingLeft = false;
                player.dirXLeft = 0;
            }

            animation.GetStopSprite();
        }
    }
}
