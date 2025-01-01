using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    internal class Enregistrement
    {
        /*public static void SauvegarderDonnees<T>(List<T> data, string filepath)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filepath, json);
        }
        */

        /// <summary>
        /// Sauvegarde une liste de données génériques dans un fichier spécifié en utilisant la sérialisation JSON.
        /// </summary>
        /// <typeparam name="T">Type des objets dans la liste à sauvegarder.</typeparam>
        /// <param name="data">Liste des données à sauvegarder.</param>
        /// <param name="filepath">Chemin du fichier où les données seront sauvegardées.</param>
        public static void SauvegarderDonnees<T>(List<T> data, string filepath)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Objects
            };
            string json = JsonConvert.SerializeObject(data, settings);
            File.WriteAllText(filepath, json);
        }

        /// <summary>
        /// Sauvegarde un Noeud dans un fichier spécifié en utilisant la sérialisation JSON.
        /// </summary>
        /// <param name="data">Objet Noeud à sauvegarder.</param>
        /// <param name="filepath">Chemin du fichier où les données seront sauvegardées.</param>
        public static void SauvegarderDonnees(Noeud data, string filepath)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                File.WriteAllText(filepath, json);
                Console.WriteLine("Données sauvegardées : " + filepath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la sauvegarde : " + ex.Message);
            }
        }


        /// <summary>
        /// Charge une liste de données génériques à partir d'un fichier spécifié en utilisant la désérialisation JSON.
        /// </summary>
        /// <typeparam name="T">Type des objets dans la liste à charger.</typeparam>
        /// <param name="filepath">Chemin du fichier où les données sont sauvegardées.</param>
        /// <returns>Liste des données chargées à partir du fichier, ou une nouvelle liste vide si le fichier n'existe pas.</returns>
        public static List<T> ChargerDonnees<T>(string filepath)
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath);
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
                return JsonConvert.DeserializeObject<List<T>>(json, settings);
            }
            return new List<T>();
        }


        /// <summary>
        /// Charge un objet Noeud à partir d'un fichier spécifié en utilisant la désérialisation JSON.
        /// </summary>
        /// <param name="filepath">Chemin du fichier où les données sont sauvegardées.</param>
        /// <returns>Objet Noeud chargé à partir du fichier, ou null si le fichier n'existe pas ou est vide.</returns>
        public static Noeud ChargerDonneesNoeud(string filepath)
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath);
                return JsonConvert.DeserializeObject<Noeud>(json);
            }
            return null; // Retourner null pour indiquer qu'aucun noeud n'a été chargé
        }

        
    }
}
