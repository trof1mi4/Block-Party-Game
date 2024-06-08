using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class GameForm : Form
    {
        Player player;
        Movement movement;
        Level level;
        Gravity gravity;
        AnimationPlayer animation;
        Platform platform;
        Collision collision;

        Panel mainMenuPanel;
        PictureBox mainMenuImage;

        public GameForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(720, 480);
            timer1.Interval = 20;
            timer1.Tick += new EventHandler(Update);
            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnKeyUp);

            FormBorderStyle = FormBorderStyle.FixedDialog; 
            MaximizeBox = false;

            CreateMainMenu();
        }

        private void CreateMainMenu()
        {

            mainMenuPanel = new Panel
            {
                Size = ClientSize,
                BackColor = Color.Black
            };

            mainMenuImage = new PictureBox
            {
                Size = new Size(512, 400),
                Location = new Point(104, -100),
                Image = LoadBitmap("MainMenu.png")
            };
            mainMenuPanel.Controls.Add(mainMenuImage);

            Button playButton = new Button
            {
                Text = "Играть",
                Location = new Point(310, 300),
                Size = new Size(100, 50),
                BackColor = Color.White
            };
            playButton.Click += (sender, e) =>
            {
                mainMenuPanel.Visible = false;
                mainMenuImage.Visible = false;
                Init();
                Focus();
            };

            mainMenuPanel.Controls.Add(playButton);
            Controls.Add(mainMenuPanel);
        }

        private void GameOver()
        {
            timer1.Stop();
            level.RemoveControls();
            Panel gameOverPanel = new Panel()
            {
                Size = new Size(300, 120),
                Location = new Point((this.ClientSize.Width - 300) / 2, (this.ClientSize.Height - 150) / 2),
                BackColor = Color.Red
            };

            Label gameOverLabel = new Label()
            {
                Text = $"Игра окончена! Пройдено уровней: {level.LevelNumber - 1}",
                Location = new Point(50, 20),
                Size = new Size(200, 30)
            };
            gameOverPanel.Controls.Add(gameOverLabel);

            Button mainMenuButton = new Button()
            {
                Text = "Выход в главное меню",
                Location = new Point(50, 60),
                Size = new Size(100, 30)
            };
            mainMenuButton.Click += (sender, e) =>
            {
                level = null;
                gameOverPanel.Visible = false;
                mainMenuImage.Visible = true;
                mainMenuPanel.Visible = true;
            };
            gameOverPanel.Controls.Add(mainMenuButton);
            Controls.Add(gameOverPanel);
        }

        public void OnKeyUp(object sender, KeyEventArgs e) => movement.StoppingPlayer(e);
        public void OnPress(object sender, KeyEventArgs e) => movement.PlayerMovement(e);
        public void Update(object sender, EventArgs e)
        {
            if (player.PlayerIsDead())
                GameOver();
            animation.UpdatingSprites();
            gravity.ApplyGravity();
            gravity.Falling();
            player.Move();
            collision.CheckHorizontalCollisions();

            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            LevelDrawing.DrawingSky(graphics);
            LevelDrawing.DrawingPlatform(graphics, platform);
            animation.PlayAnimation(graphics);
        }

        public void Init()
        {
            AnimationPlayer.stayR = LoadBitmap("MonsterRight.png");
            AnimationPlayer.stayL = LoadBitmap("MonsterLeft.png");
            AnimationPlayer.walkR = LoadBitmap("MonsterWalkRight.png");
            AnimationPlayer.walkL = LoadBitmap("MonsterWalkLeft.png");
            AnimationPlayer.jumpR = LoadBitmap("MonsterJumpRight.png");
            AnimationPlayer.jumpL = LoadBitmap("MonsterJumpLeft.png");

            LevelDrawing.blueBlock = LoadBitmap("blue.png");
            LevelDrawing.greenBlock = LoadBitmap("green.png");
            LevelDrawing.limeBlock = LoadBitmap("lime.png");
            LevelDrawing.orangeBlock = LoadBitmap("orange.png");
            LevelDrawing.purpleBlock = LoadBitmap("purple.png");
            LevelDrawing.redBlock = LoadBitmap("red.png");
            LevelDrawing.yellowBlock = LoadBitmap("yellow.png");
            LevelDrawing.whiteBlock = LoadBitmap("white.png");

            LevelDrawing.sky = LoadBitmap("sky.jpg");

            player = new Player(0, 208);
            platform = new Platform();
            level = new Level(this, player, platform);
            collision = new Collision(player, platform);
            gravity = new Gravity(player, collision);
            animation = new AnimationPlayer(player);
            movement = new Movement(player, gravity, animation);
            timer1.Start();
        }

        private Bitmap LoadBitmap(string fileName) => new Bitmap
            (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Sprites", fileName));
    }
}