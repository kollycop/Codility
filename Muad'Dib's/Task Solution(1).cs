namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] travelDays = new int[] {5, 7, 8, 9, 10, 12, 13}; // for example
            int[] defTravelCost = new int[travelDays.Length]; // cost array
            Array.Fill(defTravelCost, 2); // default one-day cost for every day of travelling
            int[] sevenDayTickets  = Solution(travelDays, 6, 7, defTravelCost);

            int[] finishCost = Solution(travelDays, 30, 25, sevenDayTickets);
            Console.WriteLine(finishCost.Sum());
        }

        public static int sum (int[] mass, int start, int end)
        {
            int sum = 0;
            for(int i = start; i< end; i++)
            {
                sum += mass[i];
            }
            return sum;
        }
        public static int[] Solution(int[] days, int step, int cost, int[] costAll)
        {

            for (int index = 0; index < days.Length - 1;)
            {
                int startPos = index;
                int finishPos = Array.IndexOf(days, days.Where(i => i > days[startPos] && i - days[startPos] <= step).OrderBy(i => -i).FirstOrDefault());

                if (finishPos == -1)
                {
                    finishPos = startPos;
                }

                // We need to compare cost with old ticket type and with new type, if cost with new type will be less, we modifing the cost array
                // Range(), Slice does not work here by some reason... 
                // and couse of this we need to use our function sum(), but it must be costAll[startPos..finishPos+1]
                if (sum(costAll, startPos, finishPos) > cost && finishPos != 0)
                {
                    costAll[index] = cost;
                    for (int ind = index + 1; ind <= finishPos; ind++)
                    {
                        costAll[ind] = 0;
                    }
                    // We already decided what type of ticket we will use for these days (from startPos to finishPos)
                    // and we chose the next day
                    index = finishPos + 1;
                }
                else
                {
                    // we decided to use standart one-day ticket, that's mean that we need to check next day after startPos
                    index++;
                }
            }
            return costAll;
        }
    }
}