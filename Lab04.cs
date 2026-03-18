using System;
using ASD.Graphs;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ASD
{
    public class Lab04 : MarshalByRefObject
    {
        /// <summary>
        /// Etap 1 - Szukanie mozliwych do odwiedzenia miast z grafu skierowanego
        /// przy zalozeniu, ze pociagi odjezdzaja co godzine.
        /// </summary>
        /// <param name="graph">Graf skierowany przedstawiający siatke pociagow</param>
        /// <param name="miastoStartowe">Numer miasta z ktorego zaczyna sie podroz pociagiem</param>
        /// <param name="K">Godzina o ktorej musi zakonczyc sie nasza podroz</param>
        /// <returns>Tablica numerow miast ktore mozna odwiedzic. Posortowana rosnaco.</returns>
        public int[] Lab04Stage1(DiGraph graph, int miastoStartowe, int K)
        {
            // TODO
            int[] dystans = new int[graph.VertexCount];
            for(int i = 0; i < graph.VertexCount; i++)
            {
                dystans[i] = -1;
            }
            dystans[miastoStartowe] = 0;
            foreach(Edge e in graph.BFS().SearchFrom(miastoStartowe))
            {
                if (dystans[e.To] == -1)
                {
                    dystans[e.To] = dystans[e.From] + 1;
                }
            }
            List<int> wynik = new List<int>();
            for(int i = 0; i< graph.VertexCount; i++)
            {
                if (dystans[i] != -1 && dystans[i] <= K - 8)
                {
                    wynik.Add(i);
                }
            }
            int[] miastaMozliweDoOdwiedzenia = new int[] { miastoStartowe };
        miastaMozliweDoOdwiedzenia = wynik.ToArray();
            return miastaMozliweDoOdwiedzenia;
        }

        /// <summary>
        /// Etap 2 - Szukanie mozliwych do odwiedzenia miast z grafu skierowanego.
        /// Waga krawedzi oznacza, ze pociag rusza o tej godzinie
        /// </summary>
        /// <param name="graph">Wazony graf skierowany przedstawiający siatke pociagow</param>
        /// <param name="miastoStartowe">Numer miasta z ktorego zaczyna sie podroz pociagiem</param>
        /// <param name="K">Godzina o ktorej musi zakonczyc sie nasza podroz</param>
        /// <returns>Tablica numerow miast ktore mozna odwiedzic. Posortowana rosnaco.</returns>
        public int[] Lab04Stage2(DiGraph<int> graph, int miastoStartowe, int K)
        {
            int n = graph.VertexCount;
            int[] najwczesniejszyDojazd = new int[n];
            for (int i = 0; i < n; i++)
            {
                najwczesniejszyDojazd[i] = int.MaxValue;
            }
            najwczesniejszyDojazd[miastoStartowe] = 8;
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            pq.Insert(miastoStartowe, 8);
            while(pq.Count > 0)
            {
                int aktualneMiasto = pq.Extract();
                int obecnyCzas = najwczesniejszyDojazd[aktualneMiasto];

                foreach(Edge<int> e in graph.OutEdges(aktualneMiasto))
                {
                    int czasOdjazdu = e.Weight;
                    int czasPrzyjazdu = czasOdjazdu + 1;

                    if(czasOdjazdu>= obecnyCzas && czasPrzyjazdu <= K)
                    {
                        if (czasPrzyjazdu < najwczesniejszyDojazd[e.To])
                        {
                            najwczesniejszyDojazd[e.To] = czasPrzyjazdu;
                            pq.Insert(e.To, czasPrzyjazdu);
                        }
                    }
                }
            }
            List<int> wynik = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (najwczesniejszyDojazd[i] != int.MaxValue)
                {
                    wynik.Add(i);
                }
            }
                int[] miastaMozliweDoOdwiedzenia = new int[] { miastoStartowe };
            miastaMozliweDoOdwiedzenia = wynik.ToArray();
            return miastaMozliweDoOdwiedzenia;
        }
    }
}
