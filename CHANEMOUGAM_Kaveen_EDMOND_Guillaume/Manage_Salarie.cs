using System;
using System.Collections.Generic;
using System.Linq;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class ManageSalarie
    {
        private Noeud pdg;

        public ManageSalarie(Noeud pdg = null)
        {
            this.pdg = pdg;
        }

        public Noeud Pdg
        {
            get { return pdg; }

            set { pdg = value; }
        }


        /// <summary>
        /// Méthode qui affiche l'arborescence de l'arbre dans l'ordre
        /// </summary>
        /// <param name="start">Noeud salarié</param>
        /// <param name="niveau">Niveau du poste</param>
        public static void Affichage(Noeud start, int niveau = 0)
        {
            if (start != null)
            {
                Console.Write(start.Salarie.Id + " :\t" + new string(' ', niveau * 2) + start.Salarie.Poste + " : " + start.Salarie.Nom + "" + "\n");

                Affichage(start.Successeur, niveau + 1);

                Affichage(start.Frere, niveau);
            }
        }

        /// <summary>
        /// Méthode qui insère un frère à un noeud déjà existant
        /// </summary>
        /// <param name="start">Noeud actuel</param>
        /// <param name="nouveau">Noeud collegue</param>
        /// <returns>Booléen</returns>
        public bool EmbaucherMemePoste(Noeud salarie, Noeud nouveau)
        {
            if (salarie == null)
            {
                return false;
            }

            if (salarie.Frere == null)
            {
                salarie.Frere = nouveau;
                return true;
            }

            return EmbaucherMemePoste(salarie.Frere, nouveau);
        }

        /// <summary>
        /// Méthode qui insère un successeur à un noeud existant
        /// </summary>
        /// <param name="start">Noeud actuel</param>
        /// <param name="nouveau">Noeud sous-fifre</param>
        /// <returns>Booléen</returns>
        public bool EmbaucherSousPoste(Noeud start, Noeud nouveau) // AjouterSuccesseur EmbaucherSuccesseur
        {
            if (start == null)
            {
                return false;
            }

            if (start.Successeur == null)
            {
                start.Successeur = nouveau;

                return true;
            }
            else
            {
                //Console.WriteLine(start.Salarie_.Poste + " à déjà un successeur, nouvel employé ajouté en tant que frère du successeur : " + start.Successeur.Salarie_.Poste);
                EmbaucherMemePoste(start.Successeur, nouveau);
                //start.Successeur.Frere = nouveau;
            }

            return false;
        }

        /// <summary>
        /// Méthode qui recherche la largeur d'un arbre donc de l'entreprise par rapport à un salarié
        /// </summary>
        /// <param name="start">Noeud actuel</param>
        /// <param name="salarie_">Salarié</param>
        /// <returns>Booléen</returns>
        public bool RechercheLargeur(Noeud start, Salarie salarie) // RechercherParId
        {
            if (start == null)
            {
                return false;
            }

            if (start.Salarie == salarie)
            {
                return true;
            }

            bool frereTrouve = RechercheLargeur(start.Frere, salarie);

            if (frereTrouve)
            {
                return frereTrouve;
            }

            return RechercheLargeur(start.Successeur, salarie);
        }


        /// <summary>
        /// Trouve un noeud par l'ID du salarié.
        /// </summary>
        /// <param name="start">Noeud de départ.</param>
        /// <param name="id">ID du salarié.</param>
        /// <returns>Noeud correspondant à l'ID du salarié.</returns>
        public static Noeud TrouverNoeudParId(Noeud start, int id)
        {
            if (start == null)
            {
                return null;
            }

            if (start.Salarie.Id == id)
            {
                return start;
            }

            var result = TrouverNoeudParId(start.Successeur, id);
            if (result != null)
            {
                return result;
            }

            return TrouverNoeudParId(start.Frere, id);
        }


        /// <summary>
        /// Supprime un salarié de l'arborescence.
        /// </summary>
        /// <param name="salarie">Noeud du salarié à supprimer.</param>
        /// <returns>Booléen indiquant le succès de l'opération.</returns>
        public bool SupprimerSalarie(Noeud salarie)
        {
            // Vérifier que le salarié est une feuille
            if (salarie.Successeur != null)
            {
                Console.WriteLine("Suppression refusée: le salarié a des subordonnés.");
                return false;
            }

            // Trouver et déconnecter le salarié dans l'arbre
            if (pdg != null && pdg == salarie)
            {
                pdg = null; // Si le PDG doit être supprimé et n'a pas de subordonnés
                return true;
            }

            // Utiliser une méthode récursive pour trouver et supprimer le salarié de l'arbre
            return SupprimerSalarieRecur(pdg, salarie);
        }


        /// <summary>
        /// Méthode récursive pour supprimer un salarié de l'arborescence.
        /// </summary>
        /// <param name="parent">Noeud parent.</param>
        /// <param name="salarie">Noeud du salarié à supprimer.</param>
        /// <returns>Booléen indiquant le succès de l'opération.</returns>
        private bool SupprimerSalarieRecur(Noeud parent, Noeud salarie)
        {
            if (parent == null) return false;

            // Vérifier les frères du successeur direct
            Noeud current = parent.Successeur;
            Noeud prev = null;
            while (current != null)
            {
                if (current == salarie)
                {
                    if (prev == null) // Le salarié à supprimer est le successeur direct
                        parent.Successeur = current.Frere;
                    else // Le salarié à supprimer est un frère
                        prev.Frere = current.Frere;

                    return true;
                }
                prev = current;
                current = current.Frere;
            }

            // Répéter la recherche pour le successeur et les frères
            return SupprimerSalarieRecur(parent.Successeur, salarie) || SupprimerSalarieRecur(parent.Frere, salarie);
        }



        /// <summary>
        /// Modifie les informations d'un salarié.
        /// </summary>
        /// <param name="noeudActuel">Noeud du salarié à modifier.</param>
        /// <param name="id">ID du salarié.</param>
        /// <param name="nom">Nouveau nom du salarié.</param>
        /// <param name="prenom">Nouveau prénom du salarié.</param>
        /// <param name="poste">Nouveau poste du salarié.</param>
        /// <param name="salaire">Nouveau salaire du salarié.</param>
        /// <param name="email">Nouvel email du salarié.</param>
        /// <param name="telephone">Nouveau téléphone du salarié.</param>
        /// <returns>Booléen indiquant le succès de l'opération.</returns>
        public bool ModifierSalarie(Noeud noeudActuel, int id ,string nom = null, string prenom = null, string poste = null, float? salaire = null, string email = null, string telephone = null)
        {
            if (noeudActuel == null || noeudActuel.Salarie == null)
            {
                Console.WriteLine("Erreur: Le salarié spécifié n'existe pas.");
                return false;
            }

            // Mise à jour des informations du salarié dans le noeud existant
            noeudActuel.Salarie.Id = id;
            if (!string.IsNullOrEmpty(nom)) noeudActuel.Salarie.Nom = nom;
            if (!string.IsNullOrEmpty(prenom)) noeudActuel.Salarie.Prenom = prenom;
            if (!string.IsNullOrEmpty(poste)) noeudActuel.Salarie.Poste = poste;
            if (salaire.HasValue) noeudActuel.Salarie.Salaire = salaire.Value;
            if (!string.IsNullOrEmpty(email)) noeudActuel.Salarie.Email = email;
            if (!string.IsNullOrEmpty(telephone)) noeudActuel.Salarie.Telephone = telephone;

            Console.WriteLine("Modification réussie.");
            return true;
        }



        /// <summary>
        /// Initialise une liste de salariés avec une hiérarchie.
        /// </summary>
        /// <returns>Noeud racine représentant le PDG.</returns>
        public static Noeud InitListeSalaries()
        {
            // Création du PDG
            Noeud pdg = new Noeud(new Salarie(1, "Dupont", "Jean", new DateTime(1960, 1, 1), "123 Boulevard du General, Paris", "dupond@example.com", "0102030405", DateTime.Now.AddYears(-30), "Directeur Général", 18000));
            ManageSalarie manageSalarie = new ManageSalarie(pdg);
            // Directeur des Opérations
            Noeud directeurOperations = new Noeud(new Salarie(2, "Martin", "Paul", new DateTime(1970, 5, 5), "127 Boulevard des Opérations, Paris", "martin@example.com", "0102030406", DateTime.Now.AddYears(-20), "Directeur des Opérations", 16000));
            manageSalarie.EmbaucherSousPoste(pdg, directeurOperations);

            // Chef d'Équipe
            Noeud chefEquipe = new Noeud(new Salarie(3, "Leroy", "Alice", new DateTime(1980, 6, 6), "128 Boulevard des Opérations, Paris", "leroy@example.com", "0102030407", DateTime.Now.AddYears(-10), "Chef d'Équipe", 12000));
            manageSalarie.EmbaucherSousPoste(directeurOperations, chefEquipe);

            // Plusieurs chefs d'équipe sous le directeur des opérations
            for (int i = 4; i <= 6; i++)
            {
                Noeud chefEquipeSupp = new Noeud(new Salarie(i, $"Durand{i}", $"Alice{i}", new DateTime(1990, i, i), $"130{i} Bd des Opérations, Paris", $"durand{i}@example.com", "0102030408", DateTime.Now.AddYears(-5), "Chef d'Équipe", 11000));
                manageSalarie.EmbaucherSousPoste(directeurOperations, chefEquipeSupp);
            }

            // Directeur des Ressources Humaines
            Noeud directeurRH = new Noeud(new Salarie(7, "Blanc", "Sophie", new DateTime(1975, 2, 3), "132 Boulevard du RH, Paris", "blanc@example.com", "0102030409", DateTime.Now.AddYears(-15), "Directeur RH", 15000));
            manageSalarie.EmbaucherSousPoste(pdg, directeurRH);

            // Sous-directeurs et managers sous le Directeur RH
            Noeud managerRH1 = new Noeud(new Salarie(8, "Petit", "Julie", new DateTime(1985, 4, 5), "134 Boulevard du RH, Paris", "petit@example.com", "0102030410", DateTime.Now.AddYears(-8), "Manager RH", 9000));
            manageSalarie.EmbaucherSousPoste(directeurRH, managerRH1);

            //string salarieFilePath = "Data/salaries.json";
            //Enregistrement.SauvegarderDonnees(pdg, salarieFilePath);
            return pdg; // Retourne le PDG comme racine de l'arborescence
        }
    }
}
