using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyRunner
{
    public class LonelyRunnerEngine
    {
        public List<int> Positions;
        public List<int> Velocities;
        private Random random;


        public int N { set; get; }
        public int k { set; get; }

        public int ActualRound { set; get; }
        public int T { set; get; }

        public GameType GameType { set; get; }
        public Strategy Strategy { set; get; }

        public LonelyRunnerEngine()
        {
            Positions = new List<int>();
            Velocities = new List<int>();
            random = new Random();
        }

        public void StartGame(int n, int t, int k)
        {
            Positions.Clear();
            Velocities.Clear();
            N = n; this.k = k; T = t;
            ActualRound = 0;
        }

        public int GenerateComputerVelocity()
        {
            int l;
            if (Strategy == Strategy.EASY)
            {
                List<int> possibleVel = new List<int>();
                for (int i = 0; i < N * k; i++)
                    if (!Velocities.Contains(i))
                        possibleVel.Add(i);

                l = possibleVel[random.Next(possibleVel.Count)];
                Positions.Add(0);
                Velocities.Add(l);
            }
            else
            {
                List<int> possibleVel = new List<int>();
                for (int i = 0; i < N * k; i++)
                    if (!Velocities.Contains(i))
                        possibleVel.Add(i);

                l = possibleVel[random.Next(possibleVel.Count)];
                Positions.Add(0);
                Velocities.Add(l);
            }
            return l;
        }

        public bool AddVelocity(int l)
        {
            if (Velocities.Contains(l))
            {
                return false;
            }
            else
            {
                Positions.Add(0);
                Velocities.Add(l);
                return true;
            }
        }

        public void MoveAll()
        {
            for (int i = 0; i < Positions.Count; i++)
                Positions[i] = (Positions[i] + Velocities[i]) % (N * k);
        }

        public bool CheckWinState()
        {
            var positions = new List<int>(Positions);
            positions.Sort();
            for (int i = 0; i < positions.Count; i++)
            {
                int leftDist = Math.Min(Math.Abs(positions[i] - positions[(i - 1+positions.Count) % positions.Count]),
                    N * k - Math.Abs(positions[i] - positions[(i - 1+positions.Count) % positions.Count]));
                int rightDist = Math.Min(Math.Abs(positions[i] - positions[(i + 1) % positions.Count]),
                N * k - Math.Abs(positions[i] - positions[(i + 1) % positions.Count]));
                if (Math.Min(leftDist, rightDist) > k) return true;
            }
            return false;
        }
    }


    public enum GameType
    {
        PLAYER_VS_COMPUTER,
        COMPUTER_VS_PLAYER
    }

    public enum Strategy
    {
        EASY, HARD
    }
}
