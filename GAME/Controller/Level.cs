using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

public class Level
{
    public int LevelNumber { get; private set; }
    private Image targetColor;
    private Random random;
    private int timeToReachTarget;
    private Form gameForm;
    private Label infoLabel;
    private PictureBox targetBlockPictureBox;
    private Player player;
    private Platform platform;

    public Level(Form form, Player player, Platform platform)
    {
        this.player = player;
        this.platform = platform;

        LevelNumber = 1;
        random = new Random();
        gameForm = form;
        infoLabel = new Label() { Location = new Point(10, 10), Size = new Size(300, 30) };
        targetBlockPictureBox = new PictureBox() { Location = new Point(320, 10), Size = new Size(Block.size, Block.size) };
        gameForm.Controls.Add(infoLabel);
        gameForm.Controls.Add(targetBlockPictureBox);

        StartNewLevel();
    }

    public async void StartNewLevel()
    {
        while (!player.PlayerIsDead())
        {
            timeToReachTarget = Math.Max(3, 6 - LevelNumber);
            targetColor = GetRandomColor();
            var targetBlocksCount = Math.Max(1, 6 - LevelNumber );
            platform.GeneratePlatform(targetColor, targetBlocksCount, LevelNumber);
            PlacePlayerOnHighestBlock();

            infoLabel.Text = "Приготовьтесь...";
            infoLabel.Visible = true;
            targetBlockPictureBox.Visible = false;

            await Task.Delay(3000);

            infoLabel.Text = $"У вас {timeToReachTarget} секунд, чтобы встать на блок";
            targetBlockPictureBox.Image = targetColor;
            targetBlockPictureBox.Visible = true;

            await Task.Delay(timeToReachTarget * 1000);

            foreach (var block in platform.Blocks)
                if (block.blockColor != targetColor)
                    block.blockColor = LevelDrawing.whiteBlock;

            infoLabel.Visible = false;
            targetBlockPictureBox.Visible = false;
            infoLabel.Text = string.Empty;
            targetBlockPictureBox.Image = null;

            await Task.Delay(2000);

            LevelNumber++;
        }
    }

    public void RemoveControls()
    {
        gameForm.Controls.Remove(infoLabel);
        gameForm.Controls.Remove(targetBlockPictureBox);
    }

    private void PlacePlayerOnHighestBlock()
    {
        var validBlocks = platform.Blocks.Where(block => block.blockColor != LevelDrawing.whiteBlock).ToList();
        var highestBlock = validBlocks.OrderBy(block => block.yCoord).FirstOrDefault();
        if (highestBlock != null)
            PlacePlayer(highestBlock.xCoord, highestBlock.yCoord);
    }

    private void PlacePlayer(int x, int y)
    {
        player.posX = x;
        player.posY = y - player.sizeSprite - 5;
    }

    private Image GetRandomColor()
    {
        var colors = new [] { LevelDrawing.blueBlock, LevelDrawing.greenBlock, LevelDrawing.limeBlock, LevelDrawing.orangeBlock, LevelDrawing.purpleBlock, LevelDrawing.redBlock, LevelDrawing.yellowBlock };
        return colors[random.Next(colors.Length)];
    }
}
