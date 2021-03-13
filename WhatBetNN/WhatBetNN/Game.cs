using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBetNN
{
    class Game
    {
        private NN[] allNNs = new NN[100];

        public Game()
        {
            for (int i = 0; i < allNNs.Length; i++)
            {
                allNNs[i] = new NN(new float[] { 1, 1 });
            }
        }

        public void Main()
        {
            int i = 1;

            foreach (NN nn in allNNs)
            {
                int money = 10;
                int howLongLive = 1;

                while (true)
                {
                    if (money == 0)
                    {
                        Console.WriteLine("You bankrupted");
                        nn.earnedMoney = 0;
                        i = 0;
                        break;
                    }

                    Console.WriteLine("Your money: " + money);

                    int bet = nn.Think(money, howLongLive);
                    Console.WriteLine("Your bet: " + bet);

                    if (bet < 0)
                    {
                        Console.WriteLine("You dumb");
                        nn.earnedMoney = -1;
                        i = 0;
                        break;
                    }

                    if (bet > money)
                    {
                        Console.WriteLine("You don't have those money!");
                        continue;
                    }

                    money -= bet;

                    bool won = new Random().Next(0, 2) == 1;

                    if (won)
                    {
                        Console.WriteLine("You won!");
                        money += bet * 2;
                    }
                    else
                    {
                        Console.WriteLine("You lose!");
                    }

                    int NNTimes = 5;

                    if (i > NNTimes)
                    {
                        nn.earnedMoney = money;
                        i = 0;
                        break;
                    }

                    howLongLive++;
                    i++;
                }
            }

            newNNs();
        }

        private void newNNs()
        {
            List<int> earnedMoney = new List<int>();

            for (int nnNum = 0; nnNum < allNNs.Length; nnNum++)
            {
                earnedMoney.Add(allNNs[nnNum].earnedMoney);
            }


            int iter = 0;
            int bestNumYet = 0;
            int bestMoneyYet = 0;

            foreach (int earned in earnedMoney)
            {
                if (earned > bestMoneyYet)
                {
                    bestNumYet = iter;
                    bestMoneyYet = earned;
                }

                Console.WriteLine(earned);

                iter++;
            }

            Console.ReadKey();

            for (int i = 0; i < allNNs.Length; i++)
            {
                allNNs[i] = new NN(allNNs[bestNumYet].Weights);
            }

            Main();
        }
    }
}
