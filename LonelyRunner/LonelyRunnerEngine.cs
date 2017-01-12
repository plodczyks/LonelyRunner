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

        public void StartGame(int n, int t, int k, GameType gameType, Strategy strategy)
        {
            Positions.Clear();
            Velocities.Clear();

            N = n; this.k = k; T = t;
            ActualRound = 0;

            GameType = gameType;
            Strategy = strategy;
        }

        public int GenerateComputerVelocity()
        {
            int l = 0;
            switch (GameType)
            {
                case GameType.COMPUTER_VS_PLAYER:
                    switch (Strategy)
                    {
                        case Strategy.EASY:
                            l = CalculateComputerPlayerEasy();
                            Positions.Add(0);
                            Velocities.Add(l);
                            break;
                        case Strategy.MEDIUM:
                            l = CalculateComputerPlayerMedium();
                            Positions.Add(0);
                            Velocities.Add(l);
                            break;
                        case Strategy.HARD:
                            l = CalculateComputerPlayerHard();
                            Positions.Add(0);
                            Velocities.Add(l);
                            break;

                    }
                    break;
                case GameType.PLAYER_VS_COMPUTER:
                    switch (Strategy)
                    {
                        case Strategy.EASY:
                            l = CalculatePlayerComputerEasy();
                            Positions.Add(0);
                            Velocities.Add(l);
                            break;
                        case Strategy.MEDIUM:
                            l = CalculatePlayerComputerMedium();
                            Positions.Add(0);
                            Velocities.Add(l);
                            break;
                        case Strategy.HARD:
                            l = CalculatePlayerComputerHard();
                            Positions.Add(0);
                            Velocities.Add(l);
                            break;
                    }
                    break;
            }
            return l;
        }


        public bool AddPlayerVelocity(int l)
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
            ActualRound++;
        }

        public GameState GetGameState()
        {
            var positions = new List<int>(Positions);
            positions.Sort();
            for (int i = 0; i < positions.Count; i++)
            {
                int lonelyMeasure = CalculateNearestNeighbour(positions[i],
                    positions[(i - 1 + positions.Count) % positions.Count],
                    positions[(i + 1) % positions.Count]);
                if (lonelyMeasure > k) return GameState.FIRST_WIN;
            }
            if (ActualRound == T) return GameState.SECOND_WIN;

            return GameState.NONE;
        }

        private int CalculateNearestNeighbour(int center, int left, int right)
        {
            int leftDist = Math.Min(Math.Abs(center - left), N * k - Math.Abs(center - left));
            int rightDist = Math.Min(Math.Abs(center - right), N * k - Math.Abs(center - right));
            return Math.Min(leftDist, rightDist);
        }

        #region Strategies

        private int CalculateComputerPlayerEasy()
        {
            var possibleVelocities = new List<int>();
            for (int i = 1; i < N * k; i++)
                if (!Velocities.Contains(i))
                    possibleVelocities.Add(i);

            return possibleVelocities[random.Next(possibleVelocities.Count)];
        }

        private int CalculatePlayerComputerEasy()
        {
            int lastVelocity = Velocities.Last();
            int l = 0;
            for (int i = 1; i <= N * k; i++)
            {
                int greaterVelocity = (lastVelocity + i) % (N * k);
                if (greaterVelocity != 0 && !Velocities.Contains(greaterVelocity))
                { l = greaterVelocity; break; }
                int lowerVelocity = (lastVelocity - i + (N * k)) % (N * k);
                if (lowerVelocity != 0 && !Velocities.Contains(lowerVelocity))
                { l = lowerVelocity; break; }
            }
            return l;
        }

        private int CalculatePlayerComputerMedium()
        {
            int l = 0, lastVelocity;
            if (Positions.Count == 1)
            {
                lastVelocity = Velocities.Last();
                if (lastVelocity != N * k - 1)
                    l = lastVelocity + 1;
                else
                    l = lastVelocity - 1;
            }
            else
            {
                List<int> nextPositions = new List<int>();
                for (int i = 0; i < Positions.Count; i++)
                    nextPositions.Add((Positions[i] + Velocities[i]) % (N * k));
                nextPositions.Sort();
                int minDistance = -1;
                for (int i = 0; i < nextPositions.Count; i++)
                {
                    int actLeftDist = Math.Min(Math.Abs(nextPositions[i] - nextPositions[(i - 1 + nextPositions.Count) % nextPositions.Count]),
                        N * k - Math.Abs(nextPositions[i] - nextPositions[(i - 1 + nextPositions.Count) % nextPositions.Count]));
                    int actRightDist = Math.Min(Math.Abs(nextPositions[i] - nextPositions[(i + 1) % nextPositions.Count]),
                    N * k - Math.Abs(nextPositions[i] - nextPositions[(i + 1) % nextPositions.Count]));
                    int actMinDist = Math.Min(actLeftDist, actRightDist);
                    if (actMinDist > minDistance)
                    {
                        minDistance = actMinDist;
                        if (actLeftDist == minDistance)
                        {
                            if (i == 0)
                            { l = ((nextPositions[0] + nextPositions.Last() + N * k) / 2) % (N * k); }
                            else
                            {
                                l = (nextPositions[i] + nextPositions[i - 1]) / 2;
                            }
                        }
                        else //actRightDist == minDistance
                        {
                            if (i == nextPositions.Count - 1)
                            { l = ((nextPositions[0] + nextPositions.Last() + N * k) / 2) % (N * k); }
                            else
                            {
                                l = (nextPositions[i] + nextPositions[i + 1]) / 2;
                            }
                        }
                    }
                }

                if (Velocities.Contains(l))
                {
                    lastVelocity = l;
                    for (int i = 1; i <= N * k; i++)
                    {
                        int greaterVelocity = (lastVelocity + i) % (N * k);
                        if (greaterVelocity != 0 && !Velocities.Contains(greaterVelocity))
                        { l = greaterVelocity; break; }
                        int lowerVelocity = (lastVelocity - i + (N * k)) % (N * k);
                        if (lowerVelocity != 0 && !Velocities.Contains(lowerVelocity))
                        { l = lowerVelocity; break; }
                    }
                }
            }
            return l;
        }

        private int CalculateComputerPlayerMedium()
        {
            int l = 0, lastVelocity;
            if (Positions.Count == 0)
            {
                l = random.Next(N * k - 2) + 1;
            }
            else
            {
                List<int> nextPositions = new List<int>();
                for (int i = 0; i < Positions.Count; i++)
                    nextPositions.Add((Positions[i] + Velocities[i]) % (N * k));
                nextPositions.Sort();
                int minDistance = -1;
                for (int i = 0; i < nextPositions.Count; i++)
                {
                    int actLeftDist = Math.Min(Math.Abs(nextPositions[i] - nextPositions[(i - 1 + nextPositions.Count) % nextPositions.Count]),
                        N * k - Math.Abs(nextPositions[i] - nextPositions[(i - 1 + nextPositions.Count) % nextPositions.Count]));
                    int actRightDist = Math.Min(Math.Abs(nextPositions[i] - nextPositions[(i + 1) % nextPositions.Count]),
                    N * k - Math.Abs(nextPositions[i] - nextPositions[(i + 1) % nextPositions.Count]));
                    int actMinDist = Math.Min(actLeftDist, actRightDist);
                    if (actMinDist > minDistance)
                    {
                        minDistance = actMinDist;
                        if (actLeftDist == minDistance)
                        {
                            l = (nextPositions[i] + k + 1) % (N * k);
                        }
                        else //actRightDist == minDistance
                        {
                            l = (nextPositions[i] - k - 1 + (N * k)) % (N * k);
                        }
                    }
                }

                if (Velocities.Contains(l))
                {
                    lastVelocity = l;
                    for (int i = 1; i <= N * k; i++)
                    {
                        int greaterVelocity = (lastVelocity + i) % (N * k);
                        if (greaterVelocity != 0 && !Velocities.Contains(greaterVelocity))
                        { l = greaterVelocity; break; }
                        int lowerVelocity = (lastVelocity - i + (N * k)) % (N * k);
                        if (lowerVelocity != 0 && !Velocities.Contains(lowerVelocity))
                        { l = lowerVelocity; break; }
                    }
                }
            }
            return l;
        }


        private int CalculateComputerPlayerHard()
        {
            if (Positions.Count == 0)
            {
                return random.Next(N * k - 2) + 1;
            }

            List<int> nextPositions = new List<int>();
            for (int ii = 0; ii < Positions.Count; ii++)
                nextPositions.Add((Positions[ii] + Velocities[ii]) % (N * k));

            int baseLonelyCount = 0;
            for (int ii = 0; ii < nextPositions.Count; ii++)
            {
                int lonelyMeasure = CalculateNearestNeighbour(nextPositions[ii],
                        nextPositions[(ii + nextPositions.Count - 1) % nextPositions.Count],
                nextPositions[(ii + 1) % nextPositions.Count]);
                if (lonelyMeasure > k) baseLonelyCount++;
            }

            int maxLonelyMeasure = int.MinValue;
            int maxL = -1;

            for (int i = 1; i < N * k; i++)
            {
                if (Velocities.Contains(i)) { continue; }
                var extNewPositions = new List<int>(nextPositions);
                extNewPositions.Add(i);
                extNewPositions.Sort();

                int lonelyCount = 0;
                int sumLonelyMeasure = 0;
                for (int ii = 0; ii < extNewPositions.Count; ii++)
                {
                    int lonelyMeasure = CalculateNearestNeighbour(extNewPositions[ii],
                        extNewPositions[(ii + extNewPositions.Count - 1) % extNewPositions.Count],
                        extNewPositions[(ii + 1) % extNewPositions.Count]);
                    if (lonelyMeasure > k) lonelyCount++;
                    sumLonelyMeasure += lonelyMeasure;
                }
                if (baseLonelyCount <= lonelyCount && sumLonelyMeasure > maxLonelyMeasure) { maxL = i; maxLonelyMeasure = sumLonelyMeasure; }
            }

            if (maxL == -1)
            {
                var possibleVelocities = new List<int>();
                for (int i = 1; i < N * k; i++)
                    if (!Velocities.Contains(i))
                        possibleVelocities.Add(i);
                return possibleVelocities[random.Next(possibleVelocities.Count)];
            }
            else return maxL;
        }

        private int CalculatePlayerComputerHard()
        {

            List<int> nextPositions = new List<int>();
            for (int ii = 0; ii < Positions.Count; ii++)
                nextPositions.Add((Positions[ii] + Velocities[ii]) % (N * k));

            int minLonelyMeasure = int.MaxValue;
            int minL = -1;

            for (int i = 1; i < N * k; i++)
            {
                if (Velocities.Contains(i)) { continue; }
                var extNewPositions = new List<int>(nextPositions);
                extNewPositions.Add(i);
                extNewPositions.Sort();

                int lonelyCount = 0;
                int sumLonelyMeasure = 0;
                for (int ii = 0; ii < extNewPositions.Count; ii++)
                {
                    int lonelyMeasure = CalculateNearestNeighbour(extNewPositions[ii],
                        extNewPositions[(ii + extNewPositions.Count - 1) % extNewPositions.Count],
                        extNewPositions[(ii + 1) % extNewPositions.Count]);
                    if (lonelyMeasure > k) lonelyCount++;
                    sumLonelyMeasure += lonelyMeasure;
                }
                if (lonelyCount == 0 && sumLonelyMeasure < minLonelyMeasure) { minL = i; minLonelyMeasure = sumLonelyMeasure; }
            }

            if (minL == -1)
            {
                var possibleVelocities = new List<int>();
                for (int i = 1; i < N * k; i++)
                    if (!Velocities.Contains(i))
                        possibleVelocities.Add(i);
                return possibleVelocities[random.Next(possibleVelocities.Count)];
            }
            else return minL;
        }
        #endregion


    }
    public enum GameType
    {
        PLAYER_VS_COMPUTER,
        COMPUTER_VS_PLAYER
    }

    public enum Strategy
    {
        EASY, MEDIUM, HARD
    }

    public enum GameState
    {
        NONE, FIRST_WIN, SECOND_WIN
    }

}
