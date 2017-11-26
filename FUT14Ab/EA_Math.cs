using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace FUT14AB
{
    public class EaMath
    {
        public int RoundPrice(double input)
        {
            int temp;
            if (input <= 1000)
            {
                temp = (int)Math.Round(input / 50.0) * 50;
            }
            else if (input <= 10000)
            {
                temp = (int)Math.Round(input / 100.0) * 100;
            }
            else if (input <= 50000)
            {
                temp = (int)Math.Round(input / 250.0) * 250;
            }
            else if (input <= 50000)
            {
                temp = (int)Math.Round(input / 500.0) * 500;
            }
            else
            {
                temp = (int)Math.Round(input / 1000.0) * 1000;
            }

            return temp;
        }

        public int AvgPrice(DataTable d)
        {
            if (d.Rows.Count != 0)
            {
                List<int> lowest = new List<int>();
                for (int k = 0; k < 3; k++)
                {
                    var hilfs = 100000;
                    var ihilf = -1;
                    for (int i = 0; i < d.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(d.Rows[i].ItemArray[1]
                                .ToString()) >= hilfs) continue;
                        hilfs = Convert.ToInt32(d.Rows[i].ItemArray[1].ToString());
                        ihilf = i;
                    }
                    lowest.Add(hilfs);
                    if (ihilf == -1)
                    {
                        break;
                    }
                    d.Rows.RemoveAt(ihilf);
                }
                double yeah = lowest.Average();
                int avg = RoundPrice(yeah);
                return (avg);
            }
            return 0;
        }

        public int ValueDown(int input)
        {
            input = input * 95 / 100;
            int hilf2 = GetModolu(input);
            int hilf = input - (input % hilf2);
            return hilf;
        }

        public int GetModolu(int input)
        {
            int output = 0;
            if (input <= 1000)
            {
                output = 50;
            }
            else if (input <= 10000)
            {
                output = 100;
            }
            else if (input <= 100000)
            {
                output = 500;
            }
            else if (input > 100000)
            {
                output = 1000;
            }
            return output;
        }
    }
}
