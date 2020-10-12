using System;

namespace ProblemA
{
    class Program
    {                    
        static void sorting(ref int[,] to_sort, double percent) //sortowanie przez wybieranie danego percentu tablicy    
        {
            int bufor1;
            int bufor2;
            for (int i = 0; i < to_sort.GetLength(0)*percent; i++)
            {
                for (int j = i + 1; j < to_sort.GetLength(0); j++)
                {
                    if (to_sort[i, 1] >= to_sort[j, 1])
                    {                                                        
                        bufor1 = to_sort[i, 1];
                        bufor2 = to_sort[i, 0];                       
                        to_sort[i, 1] = to_sort[j, 1];
                        to_sort[i, 0] = to_sort[j, 0];
                        to_sort[j, 1] = bufor1;
                        to_sort[j, 0] = bufor2;
                    }
                }
            }
        }
        static int distance_calculation(int[] arrayX, int[] arrayY, int[,] sorted, double percent)
        {
            int x; //współrzędna X do której będziemy szli
            int y; //współrzędna Y do której będziemy szli
            int subx; //wynik odejmowania dla X
            int suby; //wynik odejmowania dla Y
            int sum_of_X_and_Y; //zmienna przechowująca długośc drogi tylko z jednego punktu
            int sum=0; //zmienna przechowująca obliczoną długość drogi z wielu punktów
            int sum_min=0; //zmienna przechowująca najkrótszą do tej pory odkrytą drogę

            for(int i=0; i<sorted.GetLength(0)*percent; i++)
            {
                x = arrayX[sorted[i,0]];
                y = arrayY[sorted[i, 0]];
                for (int j=0; j<arrayY.Length; j++)
                {
                    subx = Math.Abs(arrayX[j] - x);
                    suby = Math.Abs(arrayY[j] - y);                   
                    sum_of_X_and_Y = subx + suby; //obliczenie drogi z jednego punktu do drugiego                   
                    sum += sum_of_X_and_Y; //sumowanie wszystkich dotyczasowych dróg                     
                    if(i==0)
                    {
                        sum_min = sum;  //zapisanie długości drogi w pierwszym przebiegu, by w kolejnych móc porównywać 
                    }                                    
                }
               if (sum < sum_min)
               {
                    sum_min = sum; //sprawdzenie czy nowa długość drogi jest mniejsza, jeśli tak to podmiana 
               }
               sum = 0;
            }
            return sum_min;
        }
        static void Main(string[] args)
        {         
            int avgX = 0;
            int avgY = 0;
            int Z; // ilość testów
            int N; // ilość punktów
            string line; // linia tekstu pobrana z pliku
            char[] sign = { ' ' }; //oddzielenie współrzędnych X i Y w linii
            long sum_X = 0;
            long sum_Y = 0;

            Z = int.Parse(Console.ReadLine());
            for (int i=0; i<Z; i++)
            {
                N = int.Parse(Console.ReadLine());
                int[] arrayX = new int[N]; 
                int[] arrayY = new int[N];
                for (int j=0; j<N; j++)
                {
                    line = Console.ReadLine();
                    string[] test2 = line.Split(sign);
                    arrayX[j] = Convert.ToInt32(test2[0]);
                    arrayY[j] = Convert.ToInt32(test2[1]);
                    avgX += arrayX[j];
                    avgY += arrayY[j];
                }      
                
                avgX /= N;
                avgY /= N;                                

                int[,] indexes_and_abs = new int[N,2]; // tablica zawierająca indeks współrzędnej oraz odchylenie od średniej danej współrzędnej (potrzebne do obliczenia odchylenia sandardowego)                  
                for (int j=0; j<N; j++)
                {                 
                    indexes_and_abs[j, 1] = Math.Abs(avgX - arrayX[j]) + Math.Abs(avgY - arrayY[j]);
                }
                for (int j = 0; j < N; j++)
                {
                    indexes_and_abs[j, 0] = j;                                        
                }
                for (int j = 0; j < N; j++)
                {
                    sum_X += (arrayX[j] - avgX) * (arrayX[j] - avgX);
                    sum_Y += (arrayY[j] - avgY) * (arrayY[j] - avgY);
                }
                double square_of_deviation_from_average_X = sum_X / N;
                double square_of_deviation_from_average_Y = sum_Y / N;
                double standard_deviation_X = Math.Sqrt(square_of_deviation_from_average_X);
                double standard_deviation_Y = Math.Sqrt(square_of_deviation_from_average_Y);

                if (standard_deviation_X < 5000 || standard_deviation_Y < 5000 || N <= 200 || square_of_deviation_from_average_X < 0 || square_of_deviation_from_average_Y < 0)
                {
                    Console.WriteLine(distance_calculation(arrayX, arrayY, indexes_and_abs, 1));
                }
                else if (standard_deviation_X < 7000 || standard_deviation_Y < 7000 || N <= 350)
                {
                    sorting(ref indexes_and_abs, 0.75);
                    Console.WriteLine(distance_calculation(arrayX, arrayY, indexes_and_abs, 0.75));
                }
                else if (standard_deviation_X < 9000 || standard_deviation_Y < 9000 || N <= 500)
                {
                    sorting(ref indexes_and_abs, 0.5);
                    Console.WriteLine(distance_calculation(arrayX, arrayY, indexes_and_abs, 0.5));
                }
                else if (standard_deviation_X < 12000 || standard_deviation_Y < 12000)
                {
                    sorting(ref indexes_and_abs, 0.25);
                    Console.WriteLine(distance_calculation(arrayX, arrayY, indexes_and_abs, 0.25));
                }
                else if (standard_deviation_X < 15000 || standard_deviation_Y < 15000)
                {
                    sorting(ref indexes_and_abs, 0.05);
                    Console.WriteLine(distance_calculation(arrayX, arrayY, indexes_and_abs, 0.05));
                }
                else
                {
                    sorting(ref indexes_and_abs, 0.01);
                    Console.WriteLine(distance_calculation(arrayX, arrayY, indexes_and_abs, 0.01));
                }
                sum_X = 0;
                sum_Y = 0;
                avgX = 0;
                avgY = 0;
            }
            Console.ReadKey();
        }
    }
}
