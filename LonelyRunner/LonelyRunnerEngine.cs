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
            List<int> possibleVelocities;
            int lastVelocity;
            switch (GameType)
            {
                case GameType.COMPUTER_VS_PLAYER:
                    switch (Strategy)
                    {
                        case Strategy.EASY:
                            possibleVelocities = new List<int>();
                            for (int i = 1; i < N * k; i++)
                                if (!Velocities.Contains(i))
                                    possibleVelocities.Add(i);

                            l = possibleVelocities[random.Next(possibleVelocities.Count)];
                            Positions.Add(0);
                            Velocities.Add(l);
                            break;
                        case Strategy.MEDIUM:
                            l = CalculateComputerPlayerMedium();           
                            Positions.Add(0);
                            Velocities.Add(l);
                            break;
                        case Strategy.HARD:
                            //TODO: startegia
                            break;

                    }
                    break;
                case GameType.PLAYER_VS_COMPUTER:
                    switch (Strategy)
                    {
                        case Strategy.EASY:
                            lastVelocity=Velocities.Last();
                            for (int i = 1; i <= N * k; i++)
                            {
                                int greaterVelocity = (lastVelocity + i) % (N * k);
                                if (greaterVelocity != 0 && !Velocities.Contains(greaterVelocity))
                                { l = greaterVelocity; break; }
                                int lowerVelocity = (lastVelocity - i + (N * k)) % (N * k);
                                if (lowerVelocity != 0 && !Velocities.Contains(lowerVelocity))
                                { l = lowerVelocity; break; }
                            }
                            Positions.Add(0);
                            Velocities.Add(l);
                            break;
                        case Strategy.MEDIUM:
                            l = CalculatePlayerComputerMedium();           
                            Positions.Add(0);
                            Velocities.Add(l);
                            break;
                        case Strategy.HARD:
                            //TODO: startegia
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
                int leftDist = Math.Min(Math.Abs(positions[i] - positions[(i - 1 + positions.Count) % positions.Count]),
                    N * k - Math.Abs(positions[i] - positions[(i - 1 + positions.Count) % positions.Count]));
                int rightDist = Math.Min(Math.Abs(positions[i] - positions[(i + 1) % positions.Count]),
                N * k - Math.Abs(positions[i] - positions[(i + 1) % positions.Count]));
                if (Math.Min(leftDist, rightDist) > k) return GameState.FIRST_WIN;
            }
            if (ActualRound == T) return GameState.SECOND_WIN;

            return GameState.NONE;
        }

        #region Strategies

        private int CalculatePlayerComputerMedium()
        {
            int l=0, lastVelocity;
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
                l = random.Next(N * k - 2)+1;
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
                            l=(nextPositions[i] + k + 1) % (N * k);
                        }
                        else //actRightDist == minDistance
                        {
                            l = (nextPositions[i] - k - 1 +(N*k)) % (N * k);
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
