using System;

namespace ProblemA
{
    class Program
    {                    
        static void sortowanie(ref int[,] do_posortowania, double procent) //sortowanie przez wybieranie danego procentu tablicy    
        {
            int bufor1;
            int bufor2;
            for (int i = 0; i < do_posortowania.GetLength(0)*procent; i++)
            {
                for (int j = i + 1; j < do_posortowania.GetLength(0); j++)
                {
                    if (do_posortowania[i, 1] >= do_posortowania[j, 1])
                    {                                                        
                        bufor1 = do_posortowania[i, 1];
                        bufor2 = do_posortowania[i, 0];                       
                        do_posortowania[i, 1] = do_posortowania[j, 1];
                        do_posortowania[i, 0] = do_posortowania[j, 0];
                        do_posortowania[j, 1] = bufor1;
                        do_posortowania[j, 0] = bufor2;
                    }
                }
            }
        }
        static int obliczanie_drogi(int[] arrayX, int[] arrayY, int[,] posortowane, double procent)
        {
            int x; //współrzędna X do której będziemy szli
            int y; //współrzędna Y do której będziemy szli
            int subx; //wynik odejmowania dla X
            int suby; //wynik odejmowania dla Y
            int suma_X_i_Y; //zmienna przechowująca długośc drogi tylko z jednego punktu
            int suma=0; //zmienna przechowująca obliczoną długość drogi z wielu punktów
            int suma_min=0; //zmienna przechowująca najkrótszą do tej pory odkrytą drogę

            for(int i=0; i<posortowane.GetLength(0)*procent; i++)
            {
                x = arrayX[posortowane[i,0]];
                y = arrayY[posortowane[i, 0]];
                for (int j=0; j<arrayY.Length; j++)
                {
                    subx = Math.Abs(arrayX[j] - x);
                    suby = Math.Abs(arrayY[j] - y);                   
                    suma_X_i_Y = subx + suby; //obliczenie drogi z jednego punktu do drugiego                   
                    suma += suma_X_i_Y; //sumowanie wszystkich dotyczasowych dróg                     
                    if(i==0)
                    {
                        suma_min = suma;  //zapisanie długości drogi w pierwszym przebiegu, by w kolejnych móc porównywać 
                    }                                    
                }
               if (suma < suma_min)
               {
                    suma_min = suma; //sprawdzenie czy nowa długość drogi jest mniejsza, jeśli tak to podmiana 
               }
               suma = 0;
            }
            return suma_min;
        }
        static void Main(string[] args)
        {         
            int avgX = 0;
            int avgY = 0;
            int Z; // ilość testów
            int N; // ilość punktów
            string linia; // linia tekstu pobrana z pliku
            char[] znak = { ' ' }; //oddzielenie współrzędnych X i Y w linii
            long sumaX = 0;
            long sumaY = 0;

            Z = int.Parse(Console.ReadLine());
            for (int i=0; i<Z; i++)
            {
                N = int.Parse(Console.ReadLine());
                int[] arrayX = new int[N]; 
                int[] arrayY = new int[N];
                for (int j=0; j<N; j++)
                {
                    linia = Console.ReadLine();
                    string[] test2 = linia.Split(znak);
                    arrayX[j] = Convert.ToInt32(test2[0]);
                    arrayY[j] = Convert.ToInt32(test2[1]);
                    avgX += arrayX[j];
                    avgY += arrayY[j];
                }      
                
                avgX /= N;
                avgY /= N;                                

                int[,] indeksy_i_abs = new int[N,2]; // tablica zawierająca indeks współrzędnej oraz odchylenie od średniej danej współrzędnej (potrzebne do obliczenia odchylenia sandardowego)                  
                for (int j=0; j<N; j++)
                {                 
                    indeksy_i_abs[j, 1] = Math.Abs(avgX - arrayX[j]) + Math.Abs(avgY - arrayY[j]);
                }
                for (int j = 0; j < N; j++)
                {
                    indeksy_i_abs[j, 0] = j;                                        
                }
                for (int j = 0; j < N; j++)
                {
                    sumaX += (arrayX[j] - avgX) * (arrayX[j] - avgX);
                    sumaY += (arrayY[j] - avgY) * (arrayY[j] - avgY);
                }
                double kwadrat_odchylenia_od_średniej_X = sumaX / N;
                double kwadrat_odchylenia_od_średniej_Y = sumaY / N;
                double odchylenie_standardowe_X = Math.Sqrt(kwadrat_odchylenia_od_średniej_X);
                double odchylenie_standardowe_Y = Math.Sqrt(kwadrat_odchylenia_od_średniej_Y);

                if (odchylenie_standardowe_X < 5000 || odchylenie_standardowe_Y < 5000 || N <= 200 || kwadrat_odchylenia_od_średniej_X < 0 || kwadrat_odchylenia_od_średniej_Y < 0)
                {
                    Console.WriteLine(obliczanie_drogi(arrayX, arrayY, indeksy_i_abs, 1));
                }
                else if (odchylenie_standardowe_X < 7000 || odchylenie_standardowe_Y < 7000 || N <= 350)
                {
                    sortowanie(ref indeksy_i_abs, 0.75);
                    Console.WriteLine(obliczanie_drogi(arrayX, arrayY, indeksy_i_abs, 0.75));
                }
                else if (odchylenie_standardowe_X < 9000 || odchylenie_standardowe_Y < 9000 || N <= 500)
                {
                    sortowanie(ref indeksy_i_abs, 0.5);
                    Console.WriteLine(obliczanie_drogi(arrayX, arrayY, indeksy_i_abs, 0.5));
                }
                else if (odchylenie_standardowe_X < 12000 || odchylenie_standardowe_Y < 12000)
                {
                    sortowanie(ref indeksy_i_abs, 0.25);
                    Console.WriteLine(obliczanie_drogi(arrayX, arrayY, indeksy_i_abs, 0.25));
                }
                else if (odchylenie_standardowe_X < 15000 || odchylenie_standardowe_Y < 15000)
                {
                    sortowanie(ref indeksy_i_abs, 0.05);
                    Console.WriteLine(obliczanie_drogi(arrayX, arrayY, indeksy_i_abs, 0.05));
                }
                else
                {
                    sortowanie(ref indeksy_i_abs, 0.01);
                    Console.WriteLine(obliczanie_drogi(arrayX, arrayY, indeksy_i_abs, 0.01));
                }
                sumaX = 0;
                sumaY = 0;
                avgX = 0;
                avgY = 0;
            }
            Console.ReadKey();
        }
    }
}
