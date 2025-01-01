using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public static class Dijkstra
    {

        /// <summary>
        /// Exécute l'algorithme de Dijkstra sur le graphe donné à partir du noeud source.
        /// </summary>
        /// <param name="graphe">Le graphe sur lequel exécuter l'algorithme de Dijkstra.</param>
        /// <param name="source">Le noeud source à partir duquel les plus courts chemins sont calculés.</param>
        /// <returns>Un dictionnaire où les clés sont les noms des noeuds et les valeurs sont des tuples contenant la distance minimale et le précurseur pour chaque noeud.</returns>
        public static Dictionary<string, (double distance, string précurseur)> ExecuterDijkstra(Graphe graphe, string source)
        {
            // Dictionnaire pour stocker la distance minimale et le précurseur pour chaque noeud
            var distancesMinimales = new Dictionary<string, (double distance, string précurseur)>();
            // Ensemble trié pour gérer la file de priorité des noeuds à explorer
            var fileDePriorité = new SortedSet<(double distance, string ville)>();

            // Initialisation des distances et de la file de priorité
            foreach (var noeud in graphe.ListeAdjacence.Keys)
            {
                distancesMinimales[noeud] = (double.MaxValue, null);
                fileDePriorité.Add((double.MaxValue, noeud));
            }

            // Mise à jour de la distance du noeud source
            distancesMinimales[source] = (0, null);
            fileDePriorité.Add((0, source));

            // Boucle principale de l'algorithme de Dijkstra
            while (fileDePriorité.Any())
            {
                // Extraction du noeud avec la distance minimale
                var (distanceCourante, villeCourante) = fileDePriorité.Min;
                fileDePriorité.Remove(fileDePriorité.Min);

                // Mise à jour des distances pour les voisins du noeud courant
                foreach (var (voisin, poids) in graphe.ListeAdjacence[villeCourante])
                {
                    double nouvelleDistance = distanceCourante + poids;
                    if (nouvelleDistance < distancesMinimales[voisin].distance)
                    {
                        fileDePriorité.Remove((distancesMinimales[voisin].distance, voisin));
                        distancesMinimales[voisin] = (nouvelleDistance, villeCourante);
                        fileDePriorité.Add((nouvelleDistance, voisin));
                    }
                }
            }

            return distancesMinimales;
        }
    }
}
