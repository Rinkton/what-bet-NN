using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBetNN
{
    class NN
    {
        public int earnedMoney;

        private readonly int[] inputN = new int[] { 1 };
        public float iow { get; private set; } // input-output weight

        // Low - Important
        private const int liveRate = 50;
        // High - Important
        private const int productivityRate = 5;
        private const int unproductivityRate = 3;

        public NN(float iowNum = 5)
        {
            iow = iowNum;
        }

        public int Think(int money)
        {
            int betFormule;

            while(true)
            {
                betFormule = Convert.ToInt32(inputN[0] * iow);

                if (money == 0)
                {
                    return 0;
                }

                if (money >= betFormule)
                {
                    if (betFormule > 0)
                    {
                        break;
                    }
                    else
                    {
                        iow++;
                    }
                }
                else
                {
                    iow--;
                }
            }

            return betFormule;
        }

        public void CheckProductivity(int money, int howLongLive)
        {
            int unproductivityKoef = (productivityRate * howLongLive) - money;

            if(unproductivityKoef > 0)
            {
                iow += (liveRate / howLongLive) * new Random().Next(-2, 2);
            }
        }

        public void Lose(int howLongLive)
        {
            iow += (liveRate / howLongLive) * new Random().Next(-2, 2);
        }
    }
}
