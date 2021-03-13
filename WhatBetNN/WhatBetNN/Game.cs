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
                allNNs[i] = new NN();
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
                    Console.WriteLine("Your money: " + money);

                    int bet = nn.Think(money);
                    Console.WriteLine("Your bet: " + bet);

                    if (bet <= 0 || bet > money)
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
                        Console.WriteLine("You Lose!");
                    }

                    if (money == 0)
                    {
                        Console.WriteLine("You bankrupted");
                        nn.Lose(howLongLive);
                        nn.earnedMoney = 0;
                        i = 0;
                        break;
                    }

                    int NNTimes = 1000;

                    if (i > NNTimes)
                    {
                        nn.earnedMoney = money;
                        i = 0;
                        break;
                    }

                    nn.CheckProductivity(money, howLongLive);
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
                allNNs[i] = new NN(allNNs[bestNumYet].iow);
            }

            Main();
        }
    }
}
