using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Chauffeur : Salarie
    {
        private string numChauffeur;
        public Chauffeur(string numChauffeur, int id, string nom, string prenom, DateTime dateDeNaissance, string adresse, string email, string telephone, DateTime dateEntree, string poste, float salaire)
            : base(id, nom, prenom, dateDeNaissance, adresse, email, telephone, dateEntree, poste, salaire)
        {
            this.numChauffeur = numChauffeur;
        }

        public string NumChauffeur
        {
            get { return numChauffeur; }
            set { numChauffeur = value; }
        }

        public static List<Chauffeur> InitListeChauffeur()
        {
            List<Chauffeur> chauffeurs = new List<Chauffeur>();

            for (int i = 1; i <= 10; i++)
            {
                chauffeurs.Add(new Chauffeur(
                    numChauffeur: $"CH{i}",
                    id: i,
                    nom: $"Nom{i}",
                    prenom: $"Prénom{i}",
                    dateDeNaissance: new DateTime(1990, 1, 1).AddYears(i),
                    adresse: $"10{i} Rue de quelque part, Ville",
                    email: $"email{i}@example.com",
                    telephone: $"012345678{i}",
                    dateEntree: DateTime.Now.AddYears(-i),
                    poste: "Chauffeur",
                    salaire: 2000 + i * 100
                ));
            }
            return chauffeurs;
        }
        /// <summary>
        /// Permet de savoir si un chauffeur est libre le jour donné pour faire une livraison de commande
        /// </summary>
        /// <param name="chauffeur">Chauffeur qui fera la livraison</param>
        /// <param name="commandes">Liste contenant toutes les commandes passees</param>
        /// <param name="dt">Date a laquelle on veut savoir si le chauffeur est libre</param>
        /// <returns></returns>
        public static bool EstLibre(Chauffeur chauffeur, List<Commande> commandes, DateTime dt)
        {
            bool libre = true;
            if (commandes.Count > 0)
            {
                foreach (Commande c in commandes)
                {
                    if (c != null)
                    {
                        //Dt prend en compte la date mais aussi heure min et seconde attention car seul la date compte
                        if (chauffeur.NumChauffeur == c.Chauffeur.NumChauffeur && dt.Day == c.DateCommande.Day && dt.Month == c.DateCommande.Month && dt.Year == c.DateCommande.Year)
                        {
                            libre = false;
                        }
                    }
                }
            }
            return libre;
        }
        /// <summary>
        /// Calcule ce que le chauffeur rapporte à l'entreprise en effectuant sa livraison
        /// </summary>
        /// <returns></returns>
        public float CalculerTarifKm()
        {
            int years = DateTime.Now.Year - DateEntree.Year;
            return 0.15f + (years * 0.2f);
        }

        public override string ToString()
        {
            return "Numero chauffeur : " + NumChauffeur + " ,Nom : " + Nom + " ,Prenom : " + Prenom + " ,Date d'entree : " + DateEntree.ToShortDateString();
        }

        public static void AfficherNombreDeLivraisons(List<Chauffeur> chauffeurs, Manage_Commande manageCommande)
        {
            int nbLivraisons;
            foreach (Chauffeur chauffeur in chauffeurs)
            {
                nbLivraisons = manageCommande.Commandes.Count(c => c.Chauffeur.NumChauffeur == chauffeur.NumChauffeur);
                Console.WriteLine($"Chauffeur {chauffeur.numChauffeur} a effectue {nbLivraisons} au total");
            }
        }
    }
}
