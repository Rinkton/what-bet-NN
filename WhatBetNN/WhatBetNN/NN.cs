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

        /// <remarks>
        /// [0] - money
        /// [1] - how long live
        /// </remarks>
        public float[] Weights { get; private set; }

        private float mutationAmplitude = 2;

        public NN(float[] parentWeights)
        {
            Weights = new float[2];
            Weights = mutateWeights(parentWeights);
        }

        public int Think(int money, int howLongLive)
        {
            float betFormule;

            while (true)
            {
                betFormule = money * Weights[0] + howLongLive * Weights[1];

                if (money >= betFormule)
                {
                    if (Convert.ToInt32(betFormule) > 0)
                    {
                        break;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }

            return Convert.ToInt32(betFormule);
        }

        private float[] mutateWeights(float[] parentWeights)
        {
            float[] mutatedWeights = new float[parentWeights.Length];

            for (int i = 0; i < Weights.Length; i++)
            {
                mutatedWeights[i] = parentWeights[i] + (mutationAmplitude * (new Random().Next(0, 20000) / 10000f - 1));
            }

            return mutatedWeights;
        }
    }
}
