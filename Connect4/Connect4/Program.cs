using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {

            Player player1 = new Player();
            //oggetto player2
            Player player2 = new Player();
            //pedina del player1
            player1.casella = 1;
            //pedina del player2
            player2.casella = 2;
            int numeroCasellePerVittoria = 4;
            int numeroRiga = 6;
            int numeroColonna = 5;
            Console.WriteLine();
            Console.WriteLine("***[ G ][ I ][ O ][ C ][ H ][ I ][ A ][ M ][ O ]  [ A ]  [ F ][ O ][ R ][ Z ][ A ][ 4 ]***");
            Console.WriteLine();
            Console.WriteLine("Inserisci il nome del giocatore 1:");
            player1.nomeplayer = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Inserisci il nome del giocatore 2:");
            player2.nomeplayer = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Benvenuti {0} e {1}, Giochiamo", player1.nomeplayer.ToUpper(), player2.nomeplayer.ToUpper());
            Console.WriteLine();
            int turno = 0;
            //indice della griglia
            int[] indexrow = { 0, 1, 2, 3, 4, 5 };
            //matrice di gioco
            int[,] griglia = new int[7, 6];
            //metodo di stampa della griglia di gioco e dell'indice delle colonne
            do
            {

                VisualizzaGriglia(indexrow, griglia);
                PosizionaCasellaEcontrolla(griglia, player1);
                if (Forza4(griglia, numeroRiga, numeroColonna, numeroCasellePerVittoria, player1) != 0)
                {
                    Vittoria(player1);
                    break;
                }
                Console.WriteLine();
                turno++;
                VisualizzaGriglia(indexrow, griglia);
                Console.WriteLine();
                PosizionaCasellaEcontrolla(griglia, player2);
                if (Forza4(griglia, numeroRiga, numeroColonna, numeroCasellePerVittoria, player2) != 0)
                {
                    Vittoria(player2);
                    break;

                }
                Console.WriteLine();
            } while (turno != 0);

            Console.ReadLine();
        }

        private static void VisualizzaGriglia(int[] indexrow, int[,] griglia)
        {
            //stampa dell'indice delle colonne
            Console.WriteLine();
            for (int i = 0; i < griglia.GetLength(1); i++)
                Console.Write("  |  " + indexrow[i]);
            Console.Write("  |");
            Console.WriteLine();
            for (int i = 0; i < griglia.GetLength(1) + 1; i++)
                Console.Write("  _   ");
            Console.WriteLine();
            for (int row = 0; row < griglia.GetLength(0); row++)
            {
                {
                    Console.WriteLine("  |     |     |     |     |     |     |");
                }
                for (int col = 0; col < griglia.GetLength(1); col++)
                    Console.Write("  |  {0}", griglia[row, col]);
                Console.WriteLine("  |  ");
            }

            Console.WriteLine("  |-----|-----|-----|-----|-----|-----|");
            Console.WriteLine("  -     -     -     -     -     -     -");
        }

        private static int PosizionaCasellaEcontrolla(int[,] griglia, Player player)
        {
            int righe = griglia.GetLength(1);
            int ultimaRiga = 0;
            int colonne = griglia.GetLength(1) - 1;
            bool isColonnaVuota;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Giocatore {0}, dove vuoi posizionare la tua casella?", player.nomeplayer);
                Console.WriteLine();
                string inserimentocasella = Console.ReadLine();
                bool parse = int.TryParse(inserimentocasella, out int colonna);
                isColonnaVuota = true;
                for (int i = 0; i < righe; i--)
                {
                    if (colonna < 0 || colonna > colonne)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Puoi inserire solo valori specificati nell'indice della griglia");
                        Console.WriteLine();
                        break;
                    }

                    if (parse == false)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Puoi inserire solo numeri !");
                        Console.WriteLine();
                        break;
                    }

                    if (griglia[righe, colonna] != 1 && griglia[righe, colonna] != 2)
                    {
                        isColonnaVuota = false;
                        griglia[righe, colonna] = player.casella;
                        break;
                    }

                    if (griglia[ultimaRiga, colonna] == 1 || griglia[ultimaRiga, colonna] == 2)
                    {
                        isColonnaVuota = false;
                        Console.WriteLine("La Colonna è piena !");
                        break;
                    }

                    --righe;
                }
            } while (isColonnaVuota);

            return player.casella;
        }

        static int Forza4(int[,] griglia, int numeroRiga, int numeroColonna, int numeroCasellePerVittoria, Player player)
        {
            int colonne = griglia.GetLength(1);
            int righe = griglia.GetLength(0);
            const int nessunPlayer = 0;
            for (numeroRiga = 0; numeroRiga < righe; numeroRiga++)
            {
                for (numeroColonna = 0; numeroColonna < colonne; numeroColonna++)
                {
                    if (ControllaRiga(griglia, numeroRiga, numeroCasellePerVittoria) != 0)
                    {
                        return 1; ;
                    }
                    else if (ControllaColonna(griglia, numeroColonna, numeroCasellePerVittoria) != 0)
                    {
                        return 1;
                    }
                    /*else if (ControllaDiagonaleDestra() != 0)
                    {
                        return 1;
                    }
                    else if (ControllaDiagonaleSinistra() != 0)
                    {
                        return 1;
                    }*/
                }
            }

            return nessunPlayer;
        }



        static int ControllaColonne(int[,] griglia, int numeroColonna, int numeroRiga, int numeroCasellePerVittoria)
        {
            const int nessunPlayer = 0;
            int righe = griglia.GetLength(0);
            int colonne = griglia.GetLength(1);

            for (numeroColonna = 0; numeroColonna < colonne; numeroColonna++)
            {
                ControllaColonna(griglia, numeroColonna, numeroCasellePerVittoria);
            }

            return nessunPlayer;
        }
        static int ControllaRighe(int[,] griglia, int numeroRiga, int numeroCasellePerVittoria)
        {
            int righe = griglia.GetLength(0);
            const int nessunPlayer = 0;

            for (numeroRiga = 0; numeroRiga < righe; numeroRiga++)
            {
                ControllaRiga(griglia, numeroRiga, numeroCasellePerVittoria);
            }
            return nessunPlayer;
        }

        static int ControllaRiga(int[,] griglia, int numeroRiga, int numeroCasellePerVittoria)
        {
            int numeroColonne = griglia.GetLength(1);
            const int nessunPlayer = 0;
            int playerOccorrenze = nessunPlayer;
            int occorrenze = 0;

            for (int i = 0; i < numeroColonne; i++)
            {
                int cellaCorrente = griglia[numeroRiga, i];
                if (cellaCorrente == nessunPlayer)
                {
                    occorrenze = 0;
                    playerOccorrenze = nessunPlayer;
                    continue;
                }

                if (cellaCorrente == playerOccorrenze)
                {
                    occorrenze++;
                }
                else
                {
                    occorrenze = 1;
                    playerOccorrenze = cellaCorrente;
                }

                if (occorrenze >= numeroCasellePerVittoria)
                {
                    return playerOccorrenze;
                }
            }

            return nessunPlayer;
        }
        static int ControllaColonna(int[,] griglia, int numeroColonna, int numeroCasellePerVittoria)
        {
            int numeroRighe = griglia.GetLength(0);
            const int nessunPlayer = 0;
            int playerOccorrenze = nessunPlayer;
            int occorrenze = 0;
            for (int i = 0; i < numeroRighe; i++)
            {
                int cellaCorrente = griglia[i, numeroColonna];
                if (cellaCorrente == nessunPlayer)
                {
                    playerOccorrenze = 0;
                    occorrenze = 0;
                }
                if (cellaCorrente == playerOccorrenze)
                {
                    occorrenze++;
                }
                else
                {
                    occorrenze = 1;
                    playerOccorrenze = cellaCorrente;
                }
                if (occorrenze >= numeroCasellePerVittoria)
                {
                    return playerOccorrenze;
                }

            }

            return nessunPlayer;
        }
        //struct che contiene due variabili che serviranno ad assegnare la pedina al giocatore e il nome
        public struct Player
        {
            public string nomeplayer;
            public int casella;
        }
        static void Vittoria(Player player)
        {
            Console.WriteLine(" {0} V  I  T  T  O  R  I  A  ",player.nomeplayer.ToUpper());
        }

    }

        }
    

