using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Graphe
    {

        // Dictionnaire représentant la liste d'adjacence où chaque ville est associée à une liste de tuples (ville, distance).
        public Dictionary<string, List<(string ville, double distance)>> ListeAdjacence = new Dictionary<string, List<(string ville, double distance)>>();

        // Constructeur par défaut initialisant une liste d'adjacence vide.
        public Graphe()
        {
            ListeAdjacence = new Dictionary<string, List<(string ville, double distance)>>();
        }

        /// <summary>
        /// Ajoute une arête au graphe entre deux villes avec une distance spécifiée.
        /// </summary>
        /// <param name="villeDepart">Ville de départ.</param>
        /// <param name="villeArrivee">Ville d'arrivée.</param>
        /// <param name="distance">Distance entre les deux villes.</param>
        public void AjouterArete(string villeDepart, string villeArrivee, double distance)
        {
            if (!ListeAdjacence.ContainsKey(villeDepart))
                ListeAdjacence[villeDepart] = new List<(string ville, double distance)>();
            if (!ListeAdjacence.ContainsKey(villeArrivee))
                ListeAdjacence[villeArrivee] = new List<(string ville, double distance)>();

            ListeAdjacence[villeDepart].Add((villeArrivee, distance));
            ListeAdjacence[villeArrivee].Add((villeDepart, distance));
        }


        /// <summary>
        /// Constructeur de la classe Graphe qui initialise le graphe à partir d'un fichier de distances.
        /// </summary>
        /// <param name="distancesPath">Chemin du fichier contenant les distances entre les villes.</param>
        public Graphe(string distancesPath)
        {
            try
            {
                string[] lignes = File.ReadAllLines(distancesPath);

                foreach (string ligne in lignes)
                {
                    string[] col = ligne.Split(';');
                    if (col.Length < 3) // Vérifiez que chaque ligne a suffisamment de données
                    {
                        Console.WriteLine("Ligne ignorée car elle ne contient pas assez de données: " + ligne);
                        continue;
                    }

                    string villeDepart = col[0].Trim();
                    string villeArrivee = col[1].Trim();
                    if (!float.TryParse(col[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float distance))
                    {
                        Console.WriteLine("Erreur de format de distance, ligne ignorée: " + ligne);
                        continue;
                    }

                    AjouterArete(villeDepart, villeArrivee, distance);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la lecture du fichier: " + ex.Message);
            }
        }

    }
}
