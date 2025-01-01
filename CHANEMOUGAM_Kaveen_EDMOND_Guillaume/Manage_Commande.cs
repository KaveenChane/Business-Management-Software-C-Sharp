using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Manage_Commande
    {
        private Graphe grapheVilles = new Graphe();
        private List<Commande> commandes = new List<Commande>();
        private static int prochainIdCommande = 1;

        public Manage_Commande()
        {
            grapheVilles.AjouterArete("Paris", "Lyon", 465.0);
            grapheVilles.AjouterArete("Lyon", "Marseille", 315.0);
            grapheVilles.AjouterArete("Paris", "Marseille", 775.0);
        }
        


        public Manage_Commande(Graphe grapheVilles, string commandesFilePath)
        {
            this.grapheVilles = grapheVilles;
            this.commandes = Enregistrement.ChargerDonnees<Commande>("Data/commandes.json");
        }

        public Graphe GrapheVilles
        {
            get { return grapheVilles; }
        }


        /// <summary>
        /// Méthode pour créer une nouvelle commande.
        /// </summary>
        /// <param name="client">Client passant la commande.</param>
        /// <param name="dateLivraison">Date de livraison souhaitée.</param>
        /// <param name="villeDepart">Ville de départ.</param>
        /// <param name="villeArrivee">Ville d'arrivée.</param>
        /// <param name="chauffeur">Chauffeur assigné à la commande.</param>
        /// <param name="vehicule">Véhicule utilisé pour la commande.</param>
        /// <param name="baseDeClients">Base de clients existante.</param>
        public void CreerCommande(Client client, DateTime dateLivraison ,string villeDepart, string villeArrivee, Chauffeur chauffeur, Vehicule vehicule, List<Client> baseDeClients)
        {
            if (chauffeur == null)
            {
                Console.WriteLine("Aucun chauffeur disponible");
                return;
            }
            //Vehicule vehiculeUtilise = 
            if (!vehicule.EstDisponible(dateLivraison))
            {
                Console.WriteLine($"Ce vehicule n'est pas disponible à la date : {dateLivraison.ToShortDateString()} ");
                return;
            }

            Client clientQuiCommande = baseDeClients.FirstOrDefault(c => c.Id == client.Id);
            if (clientQuiCommande == null)
            {
                baseDeClients.Add(client);
                clientQuiCommande = client;
            }

            var distances = Dijkstra.ExecuterDijkstra(grapheVilles, villeDepart);
            if (!distances.ContainsKey(villeArrivee) || distances[villeArrivee].distance == double.MaxValue)
            {
                Console.WriteLine("Aucun chemin disponible pour la livraison.");
                return;
            }
            double distance = distances[villeArrivee].distance;
            float prix = CalculerPrix((float)distance);
            Commande commande = new Commande(
                prochainIdCommande,
                clientQuiCommande,
                chauffeur,
                vehicule,
                dateLivraison,
                villeDepart,
                villeArrivee,
                prix,
                (float)distance,
                false
            );
            commande.Prix = commande.Prix * commande.PrixVehicule();
            commandes.Add(commande);
            clientQuiCommande.MontantAchatsCumule += prix;
            clientQuiCommande.Commande.Add(commande);
            vehicule.Reserver(dateLivraison);
            Console.WriteLine($"Commande créée : ID de commande {commande.CommandeId}, Client : {clientQuiCommande.Nom}, Chauffeur : {chauffeur.NumChauffeur}, Vehicule : {vehicule.NumeroImmatriculation} ,Distance : {distance} km, Prix : {commande.Prix} EUR");
            prochainIdCommande++;
            string commandeFilePath = "Data/commandes.json";
            Enregistrement.SauvegarderDonnees<Commande>(commandes, commandeFilePath);

        }


        /// <summary>
        /// Retourne le prochain ID de commande.
        /// </summary>
        /// <returns>Le prochain ID de commande.</returns>
        public static int GetId()
        {
            return prochainIdCommande;
        }


        /// <summary>
        /// Propriété pour accéder à la liste des commandes.
        /// </summary>
        public List<Commande> Commandes
        {
            get { return commandes; }
            set { commandes = value; }
        }


        /// <summary>
        /// Calcule le prix d'une commande en fonction de la distance.
        /// </summary>
        /// <param name="distance">Distance de la commande.</param>
        /// <returns>Prix calculé.</returns>
        public float CalculerPrix(float distance)
        {
            float tarifParKilomètre = 1.5f;
            return distance * tarifParKilomètre;
        }
        /*public Chauffeur? TrouverChauffeurDisponible(List<Chauffeur> chauffeurs, DateTime date)
        {
            return chauffeurs.FirstOrDefault(c => Chauffeur.EstLibre(c, this.commandes, date));
        }*/


        /// <summary>
        /// Trouve un chauffeur disponible pour une date donnée.
        /// </summary>
        /// <param name="chauffeurs">Liste des chauffeurs.</param>
        /// <param name="date">Date de la commande.</param>
        /// <returns>Le chauffeur disponible ou null si aucun n'est disponible.</returns>
        public Chauffeur? TrouverChauffeurDisponible(List<Chauffeur> chauffeurs, DateTime date)
        {
            foreach (var chauffeur in chauffeurs)
            {
                if (Chauffeur.EstLibre(chauffeur, this.commandes, date))
                {
                    Console.WriteLine($"Chauffeur {chauffeur.NumChauffeur} est libre pour le {date.ToShortDateString()}.");
                    return chauffeur;
                }
                else
                {
                    Console.WriteLine($"Chauffeur {chauffeur.NumChauffeur} n'est pas libre pour le {date.ToShortDateString()}.");
                }
            }
            Console.WriteLine("Aucun chauffeur disponible.");
            return null;
        }


        /// <summary>
        /// Affiche les commandes effectuées par un client spécifique.
        /// </summary>
        /// <param name="manageClient">Gestionnaire des clients.</param>
        public void AfficherCommandeParIdClient(Manage_Client manageClient)
        {
            Console.Write("Entrez l'id du client : ");
            int idClient = int.Parse(Console.ReadLine());
            bool trouver = false;
            foreach (Client c in manageClient.Clients)
            {
                if (c.Id == idClient)
                {
                    trouver = true;
                }
            }
            if (trouver)
            {
                Console.WriteLine($"Voici la liste des commandes effectuees par le client {idClient} : ");
                foreach (Commande c in commandes)
                {
                    if (c.Client.Id == idClient)
                    {
                        Console.WriteLine(c);
                        Console.Write("\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("Aucun client n'a ete trouve pour cet id");
            }
        }

        /// <summary>
        /// Trouve une commande par son ID.
        /// </summary>
        /// <param name="id">ID de la commande à trouver.</param>
        /// <returns>La commande correspondante ou null si non trouvée.</returns>
        public Commande? TrouverCommandeParId(int id)
        {
            return commandes.FirstOrDefault(c => c.CommandeId == id);
        }




        /// <summary>
        /// Affiche toutes les commandes enregistrées.
        /// </summary>
        public void AfficherToutesLesCommandes()
        {
            if (commandes.Count == 0)
            {
                Console.WriteLine("Aucune commande enregistrée.");
                return;
            }

            Console.WriteLine("Liste de toutes les commandes:");
            foreach (Commande commande in commandes)
            {
                Console.WriteLine($"ID de commande: {commande.CommandeId}, Client: {commande.Client.Nom}, Ville de départ: {commande.VilleDepart}, Ville d'arrivée: {commande.VilleArrivee}, Date de la commande: {commande.DateCommande.ToShortDateString()}, Chauffeur: {commande.Chauffeur.NumChauffeur}, Véhicule: {commande.Vehicule.NumeroImmatriculation}, Distance: {commande.Distance} km, Prix: {commande.Prix} EUR, Statut: {(commande.Statut ? "Livré" : "En cours")}");
            }
        }



        /// <summary>
        /// Affiche les commandes pour une période de temps spécifiée.
        /// </summary>
        public void AfficherCommandesParPeriode()
        {
            Console.Write("Saisissez la date de debut (yyyy-MM-dd) : ");
            DateTime dateDebut = DateTime.Parse(Console.ReadLine());
            Console.Write("Saisissez la date de fin (yyyy-MM-dd) : ");
            DateTime dateFin = DateTime.Parse(Console.ReadLine());

            List<Commande> commandesFiltrees = commandes.Where(c => c.DateCommande >= dateDebut && c.DateCommande <= dateFin).ToList();

            if (commandesFiltrees.Count == 0)
            {
                Console.WriteLine("Aucune commande trouvée pour la période donnée.");
                return;
            }

            Console.WriteLine($"Commandes du {dateDebut.ToShortDateString()} au {dateFin.ToShortDateString()}:");
            foreach (Commande commande in commandesFiltrees)
            {
                Console.WriteLine(commande);
            }
        }


        /// <summary>
        /// Affiche les commandes par type de véhicule.
        /// </summary>
        /// <param name="choixVehicule">Choix du type de véhicule.</param>
        public void AfficherCommandesParTypeVehicule(int choixVehicule)
        {
            List<Commande> commandesFiltres = new List<Commande>();
            switch (choixVehicule)
            {
                case 1:
                    commandesFiltres = commandes.Where(c => c.Vehicule is Voiture).ToList();
                    break;
                case 2:
                    commandesFiltres = commandes.Where(c => c.Vehicule is Camionnette).ToList();
                    break;
                case 3:
                    commandesFiltres = commandes.Where(c => c.Vehicule is Camion_Benne).ToList();
                    break;
                case 4:
                    commandesFiltres = commandes.Where(c => c.Vehicule is Camion_Citerne).ToList();
                    break;
                case 5:
                    commandesFiltres = commandes.Where(c => c.Vehicule is Camion_Frigo).ToList();
                    break;
                default:
                    Console.WriteLine("Choix de véhicule invalide.");
                    return;
            }

            if (commandesFiltres.Count > 0)
            {
                Console.WriteLine($"Commandes avec le type de véhicule choisi ({choixVehicule}):");
                foreach (var commande in commandesFiltres)
                {
                    Console.WriteLine(commande);
                }
            }
            else
            {
                Console.WriteLine("Aucune commande trouvée pour ce type de véhicule.");
            }
        }



        /// <summary>
        /// Affiche les statistiques mensuelles pour un mois et une année donnés.
        /// </summary>
        /// <param name="mois">Mois pour les statistiques.</param>
        /// <param name="annee">Année pour les statistiques.</param>
        public void AfficherStatistiquesMensuelles(int mois, int annee)
        {
            var commandesDuMois = commandes.Where(c => c.DateCommande.Month == mois && c.DateCommande.Year == annee).ToList();

            if (commandesDuMois.Count == 0)
            {
                Console.WriteLine("Aucune commande trouvée pour ce mois.");
                return;
            }

            int nombreTotalCommandes = commandesDuMois.Count;
            float revenusTotaux = commandesDuMois.Sum(c => c.Prix);
            float distanceTotale = commandesDuMois.Sum(c => c.Distance);

            Console.WriteLine($"Statistiques pour {mois}/{annee}:");
            Console.WriteLine($"Nombre total de commandes: {nombreTotalCommandes}");
            Console.WriteLine($"Revenus totaux: {revenusTotaux} EUR");
            Console.WriteLine($"Distance totale parcourue: {distanceTotale} km");
        }


        /// <summary>
        /// Génère une facture pour une commande donnée.
        /// </summary>
        /// <param name="commandeId">ID de la commande.</param>
        /// <returns>Chaîne de caractères représentant la facture.</returns>
        public string GenererFacture(int commandeId)
        {
            var commande = TrouverCommandeParId(commandeId);
            if (commande == null)
            {
                return "Commande non trouvée.";
            }

            var sb = new StringBuilder();
            sb.AppendLine("FACTURE");
            sb.AppendLine("===============");
            sb.AppendLine($"Commande ID: {commande.CommandeId}");
            sb.AppendLine($"Client: {commande.Client.Nom} {commande.Client.Prenom}");
            sb.AppendLine($"Chauffeur: {commande.Chauffeur.Nom} {commande.Chauffeur.Prenom}");
            sb.AppendLine($"Véhicule: {commande.Vehicule.NumeroImmatriculation}");
            sb.AppendLine($"Date de la commande: {commande.DateCommande.ToShortDateString()}");
            sb.AppendLine($"Ville de départ: {commande.VilleDepart}");
            sb.AppendLine($"Ville d'arrivée: {commande.VilleArrivee}");
            sb.AppendLine($"Distance: {commande.Distance} km");
            sb.AppendLine($"Prix: {commande.Prix} EUR");
            sb.AppendLine($"Statut: {(commande.Statut ? "Livrée" : "En cours")}");
            sb.AppendLine("===============");
            return sb.ToString();
        }


        /// <summary>
        /// Affiche une facture pour une commande donnée.
        /// </summary>
        /// <param name="commandeId">ID de la commande.</param>
        public void AfficherFacture(int commandeId)
        {
            string facture = GenererFacture(commandeId);
            Console.WriteLine(facture);
        }


        /// <summary>
        /// Sauvegarde une facture pour une commande donnée dans un fichier.
        /// </summary>
        /// <param name="commandeId">ID de la commande.</param>
        /// <param name="filePath">Chemin du fichier de sauvegarde.</param>
        public void SauvegarderFacture(int commandeId, string filePath)
        {
            //string facturePath = "Facture/facture{nomprenom}.txt"
            string facture = GenererFacture(commandeId);
            File.WriteAllText(filePath, facture);
            Console.WriteLine("Facture sauvegardée avec succès.");
        }


        /// <summary>
        /// Affiche les statistiques d'un chauffeur.
        /// </summary>
        /// <param name="chauffeur">Chauffeur pour lequel afficher les statistiques.</param>
        public void AfficherStatistiquesChauffeur(Chauffeur chauffeur)
        {
            var commandesChauffeur = commandes.Where(c => c.Chauffeur.NumChauffeur == chauffeur.NumChauffeur).ToList();
            if (commandesChauffeur.Count == 0)
            {
                Console.WriteLine($"Aucune commande trouvée pour le chauffeur {chauffeur.NumChauffeur}.");
                return;
            }

            int nombreLivraisons = commandesChauffeur.Count;
            float revenuTotal = commandesChauffeur.Sum(c => c.Prix);
            float kilometresParcourus = commandesChauffeur.Sum(c => c.Distance);

            Console.WriteLine($"Statistiques pour le chauffeur {chauffeur.NumChauffeur}:");
            Console.WriteLine($"Nombre de livraisons: {nombreLivraisons}");
            Console.WriteLine($"Revenu total généré: {revenuTotal} EUR");
            Console.WriteLine($"Kilomètres parcourus: {kilometresParcourus} km");
        }



        /// <summary>
        /// Affiche la moyenne des prix des commandes.
        /// </summary>
        public void AfficherMoyennePrixCommandes()
        {
            if (commandes.Count == 0)
            {
                Console.WriteLine("Aucune commande enregistrée.");
                return;
            }

            double moyennePrix = commandes.Average(c => c.Prix);
            Console.WriteLine($"La moyenne des prix des commandes est: {moyennePrix} EUR");
        }

    }
}
