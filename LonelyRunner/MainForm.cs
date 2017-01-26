using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace LonelyRunner
{
    public partial class MainForm : Form
    {

        public const int CircleCenter = 150;
        public const int Radius = 140;
        public const int MaxDegree = 360;
        public const int StartSize = 8;
        public const int RunnerSize = 4;
        public const int KSize = 4;
        public MainForm()
        {
            InitializeComponent();

            engine = new LonelyRunnerEngine();

            timer = new Timer() { Interval = 1000 };
            timer.Tick += RealizeNextMove;
            //Draw();
        }

        LonelyRunnerEngine engine;
        Timer timer;

        private void StartGameClick(object sender, EventArgs e)
        {
            string error = "";
            string nString, kString, tString;
            int n, k = 0, t;
            GameType gameType;
            Strategy strategy;

            nString = circleLengthTbx.Text;
            if (!int.TryParse(nString, out n) || n <= 0 || n % 2 == 1)
                error += "Niepoprawna wartość długości okręgu\\liczby biegaczy.\n";

            tString = roundCountTbx.Text;
            if (!int.TryParse(tString, out t) || t <= 0)
                error += "Niepoprawna liczba rund.\n";

            kString = speedAccuracyTbx.Text;
            if (kString.Length < 3 || kString[0] != '1' || kString[1] != '/' || !int.TryParse(kString.Substring(2), out k) || k <= 0)
                error += "Niepoprawna wartość dokładności prędkości.\n";

            if (error != "")
            {
                MessageBox.Show(error, "Błędne dane"); return;
            }

            if (playerComputerRbt.Checked) gameType = GameType.PLAYER_VS_COMPUTER;
            else gameType = GameType.COMPUTER_VS_PLAYER;

            if (strategyEasyRdb.Checked) strategy = Strategy.EASY;
            else if (strategyMediumRbt.Checked) strategy = Strategy.MEDIUM;
            else strategy = Strategy.HARD;


            circleLengthTbx.Enabled = false; roundCountTbx.Enabled = false; speedAccuracyTbx.Enabled = false;
            strategyGrb.Enabled = false; gameTypeGpb.Enabled = false; startBtn.Enabled = false;

            gameDatasLbx.Items.Clear();
            player1VelocityTbx.Text = player2VelocityTbx.Text = "";

            engine.StartGame(n, t, k, gameType, strategy);

            Draw();

            WaitForNextMove();
        }

        private void WaitForNextMove()
        {
            timer.Start();
        }

        private void RealizeNextMove(object sender, EventArgs e)
        {
            timer.Stop();

            //no new runners
            if (engine.N == engine.Positions.Count) { RefreshGameState(); }

            else
            {
                player1VelocityTbx.Text = player2VelocityTbx.Text = "";

                //computer moving
                switch (engine.GameType)
                {
                    case GameType.COMPUTER_VS_PLAYER:
                        int l = engine.GenerateComputerVelocity();
                        player1VelocityTbx.Text = l + "/" + engine.k;
                        player2VelocityTbx.Enabled = true;
                        break;
                    case GameType.PLAYER_VS_COMPUTER:
                        player1VelocityTbx.Enabled = true;
                        break;
                }
                nextBtn.Enabled = true;
                cancelBtn.Enabled = true;

                //wait for player move
            }
        }

        private void CheckPlayerMove(object sender, EventArgs e)
        {
            int l;
            switch (engine.GameType)
            {
                case GameType.COMPUTER_VS_PLAYER:
                    l = ReadVelocity(player2VelocityTbx.Text);
                    if (l == 0 || !engine.AddPlayerVelocity(l))
                    {
                        MessageBox.Show("Niepoprawna wartość prędkości", "Błędne dane"); return;
                    }
                    break;
                case GameType.PLAYER_VS_COMPUTER:
                    l = ReadVelocity(player1VelocityTbx.Text);
                    if (l == 0 || !engine.AddPlayerVelocity(l))
                    {
                        MessageBox.Show("Niepoprawna wartość prędkości", "Błędne dane"); return;
                    }
                    int l2 = engine.GenerateComputerVelocity();
                    player2VelocityTbx.Text = l2 + "/" + engine.k;
                    break;
            }
            nextBtn.Enabled = false;
            cancelBtn.Enabled = false;
            player1VelocityTbx.Enabled = player2VelocityTbx.Enabled = false;
            RefreshGameState();
        }

        private void RefreshGameState()
        {
            engine.MoveAll();


            Draw();
            gameDatasLbx.Items.Clear();
            for (int i = 0; i < engine.Positions.Count; i++)
            {
                gameDatasLbx.Items.Add(
                   (i + 1) + ". Pozycja: " + engine.Positions[i] + "/" + engine.k + "  " +
                    "Prędkość: " + engine.Velocities[i] + "/" + engine.k);
            }

            switch (engine.GetGameState())
            {
                case GameState.FIRST_WIN:
                    MessageBox.Show("Pierwszy gracz wygrał!!");
                    //disable down
                    circleLengthTbx.Enabled = true; roundCountTbx.Enabled = true; speedAccuracyTbx.Enabled = true;
                    strategyGrb.Enabled = true; gameTypeGpb.Enabled = true; startBtn.Enabled = true;
                    break;
                case GameState.SECOND_WIN:
                    MessageBox.Show("Drugi gracz wygrał!!");
                    //disable down
                    circleLengthTbx.Enabled = true; roundCountTbx.Enabled = true; speedAccuracyTbx.Enabled = true;
                    strategyGrb.Enabled = true; gameTypeGpb.Enabled = true; startBtn.Enabled = true;
                    break;
                case GameState.NONE:
                    WaitForNextMove();
                    break;
            }
        }

        private void Draw()
        {
            Graphics g;
            g = DrawCircle();
            DrawKPart(g);
            DrawRunners(g);

            //g.DrawLine(pen1, 10, 50, 400, 200);
        }

        private void DrawRunners(Graphics g)
        {
            SolidBrush brushFirstPlayer = new SolidBrush(Color.Red);
            SolidBrush brushSecondPlayer = new SolidBrush(Color.Black);

            var k = 0;
            var n = 0;
            var kString = speedAccuracyTbx.Text;
            var nString = circleLengthTbx.Text;
            int.TryParse(kString.Substring(2), out k);
            int.TryParse(nString, out n);

            for (int i = 0; i < engine.Positions.Count; i++)
            {
                //if (i%2 == 0)
                //    var tmp = 1;
                var position = engine.Positions[i];
                var truePosition = position;
                var degreeOffset = ((double)MaxDegree)/ (k * n);
                var degree = truePosition * degreeOffset;

                Point dPoint = new Point();
                dPoint.X = (int)(CircleCenter + Radius * Math.Cos(Math.PI * degree / 180.0)) - RunnerSize / 2;
                dPoint.Y = (int)(CircleCenter + Radius * Math.Sin(Math.PI * degree / 180.0)) - RunnerSize / 2;
                Rectangle rect = new Rectangle(dPoint, new Size(RunnerSize, RunnerSize));
                if (i % 2 == 0)
                    g.FillRectangle(brushFirstPlayer, rect);
                else
                    g.FillRectangle(brushSecondPlayer, rect);
            }
        }

        private Graphics DrawCircle()
        {
            Graphics g;
            Pen pen1 = new System.Drawing.Pen(Color.DeepPink, 2F);
            g = pbCircle.CreateGraphics();
            g.DrawEllipse(pen1, new Rectangle(10, 10, Radius * 2, Radius * 2));
            return g;
        }

        private void DrawKPart(Graphics g)
        {
            var k = 0;
            var n = 0;
            var kString = speedAccuracyTbx.Text;
            var nString = circleLengthTbx.Text;
            int.TryParse(kString.Substring(2), out k);
            int.TryParse(nString, out n);


            var degreeOffset = ((double)MaxDegree) / (k * n);

            var degree = 0.0;
            var count = 0;
            while (count < n * k)
            {
                SolidBrush brush;
                if (count % k != 0)
                {
                    brush = new SolidBrush(Color.Aqua);
                }
                else
                {
                    brush = new SolidBrush(Color.Yellow);
                }
                Point dPoint = new Point();
                dPoint.X = (int)(CircleCenter + Radius * Math.Cos(Math.PI * degree / 180.0)) - KSize / 2;
                dPoint.Y = (int)(CircleCenter + Radius * Math.Sin(Math.PI * degree / 180.0)) - KSize / 2;
                Rectangle rect = new Rectangle(dPoint, new Size(KSize, KSize));
                g.FillRectangle(brush, rect);
                degree += degreeOffset;
                count++;
            }

            SolidBrush brushStart = new SolidBrush(Color.LightYellow);
            Point startPoint = new Point();
            startPoint.X = (int)(CircleCenter + Radius * Math.Cos(Math.PI * 0 / 180.0)) - StartSize / 2;
            startPoint.Y = (int)(CircleCenter + Radius * Math.Sin(Math.PI * 0 / 180.0)) - StartSize / 2;
            Rectangle rectStart = new Rectangle(startPoint, new Size(StartSize, StartSize));
            g.FillRectangle(brushStart, rectStart);
        }

        private int ReadVelocity(string velocity)
        {
            int l = 0, k = 0;
            var parts = velocity.Split('/');
            if (parts.Length == 2 && int.TryParse(parts[0], out l) && int.TryParse(parts[1], out k) && l > 0 && l < engine.N * engine.k && k == engine.k)
                return l;
            else
                return 0;
        }

        private void CancelGameClick(object sender, EventArgs e)
        {
            circleLengthTbx.Enabled = true; roundCountTbx.Enabled = true; speedAccuracyTbx.Enabled = true;
            strategyGrb.Enabled = true; gameTypeGpb.Enabled = true; startBtn.Enabled = true;

            cancelBtn.Enabled = false; nextBtn.Enabled = false;
            player1VelocityTbx.Enabled = false; player2VelocityTbx.Enabled = false;
            pbCircle.Image = null;
            //disable down
        }
    }
}
