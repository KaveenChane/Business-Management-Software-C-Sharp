using System.ComponentModel.Design;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    internal class Program
    {
        //Chemin fichier JSON client
        static string clientFilePath = "Data/clients.json";
        //Init des clients
        static Manage_Client manageClient = new Manage_Client(Enregistrement.ChargerDonnees<Client>(clientFilePath));

        //Init des salaries
        //static Noeud testPDG = new Noeud(new Salarie(1, "Test", "PDG", DateTime.Now, "123 Boulevard", "pdg@test.com", "0102030405", DateTime.Now, "PDG", 50000));
        

        static string salarieFilePath = "Data/salaries.json";
        static ManageSalarie manageSalarie = new ManageSalarie(Enregistrement.ChargerDonneesNoeud(salarieFilePath));


        //Init de tous les chemins possibles et de la base de donnees de commande
        static Graphe graphe = new Graphe("Distances.csv");
        static string commandeFilePath = "Data/commandes.json";
        static Manage_Commande manageCommande = new Manage_Commande(graphe,commandeFilePath);

        //Init de la liste des chauffeurs
        static string chauffeurFilePath = "Data/salaries.json";
        static List<Chauffeur> chauffeurs = Chauffeur.InitListeChauffeur();

        // Init liste de voitures de l'entreprise
        static string voitureFilePath = "Data/voitures.json";
        static List<Voiture> voitures = Voiture.InitListeVoitures();

        //Init liste de camionnettes
        static string camionetteFilePath = "Data/camionettes.json";
        static List<Camionnette> camionnettes = Camionnette.InitListeCamionnettes();

        //Init liste Camion Benne
        static string camionbenneFilePath = "Data/camionbennes.json";
        static List<Camion_Benne> camion_bennes = Camion_Benne.InitListeCamionBennes();

        //Init liste Camion Citerne
        static string camionciterneFilePath = "Data/camionciternes.json";
        static List<Camion_Citerne> camion_citernes = Camion_Citerne.InitListeCamionCiternes();

        //Init liste Camion Frigo
        static string camionfrigoeFilePath = "Data/camionfrigos.json";
        static List<Camion_Frigo> camion_frigos = Camion_Frigo.InitListeCamionFrigos();

        /// <summary>
        /// Ajoute un nouveau client à la liste des clients.
        /// </summary>
        static void AjouterClient(Manage_Client manageClient)
        {
            int newId = manageClient.Clients[manageClient.Clients.Count - 1].Id + 1;
            Console.WriteLine("Entrer les informations du nouveau client:");
            Console.Write("Nom: ");
            string nom = Console.ReadLine();
            Console.Write("Prénom: ");
            string prenom = Console.ReadLine();
            Console.Write("Date de Naissance (YYYY-MM-DD): ");
            DateTime dateNaissance = DateTime.Parse(Console.ReadLine());
            Console.Write("Adresse: ");
            string adresse = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Téléphone: ");
            string telephone = Console.ReadLine();
            Console.Write("Montant des achats cumulé: ");
            float montantAchatsCumule = float.Parse(Console.ReadLine());
            
            Client client = new Client(
                newId,
                nom,
                prenom,
                dateNaissance,
                adresse,
                email,
                telephone,
                montantAchatsCumule,
                new List<Commande>()
            );

            manageClient.Ajouter(client);
            Enregistrement.SauvegarderDonnees(manageClient.Clients, clientFilePath);
        }


        /// <summary>
        /// Modifie les informations d'un client existant.
        /// </summary>
        static void ModifierClient(Manage_Client manageClient)
        {
            Console.Write("Entrez l'ID du client à modifier: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nouveau nom (laissez vide pour ne pas modifier): ");
            string nom = Console.ReadLine();
            Console.Write("Nouveau prenom (laissez vide pour ne pas modifier): ");
            string prenom = Console.ReadLine();
            Console.Write("Nouvel email (laissez vide pour ne pas modifier): ");
            string email = Console.ReadLine();
            Console.Write("Nouvelle adresse (laissez vide pour ne pas modifier): ");
            string adresse = Console.ReadLine();
            Console.Write("Nouveau téléphone (laissez vide pour ne pas modifier): ");
            string telephone = Console.ReadLine();

            manageClient.ModifierClient(id, nom, prenom, email, adresse, telephone);
            Enregistrement.SauvegarderDonnees(manageClient.Clients, clientFilePath);

        }


        /// <summary>
        /// Supprime un client de la liste des clients par ID.
        /// </summary>
        static void SupprimerClient(Manage_Client manageClient)
        {
            Console.Write("Entrez l'ID du client à supprimer: ");
            int id = int.Parse(Console.ReadLine());
            manageClient.SupprimerParId(id);
            Enregistrement.SauvegarderDonnees(manageClient.Clients, clientFilePath);

        }


        /// <summary>
        /// Licencie un salarié en le supprimant de l'arborescence.
        /// </summary>
        static void LicencierSalarie()
        {
            
            Console.Write("Entrez l'ID du salarié à licencier: ");
            int id = int.Parse(Console.ReadLine());
            if (manageSalarie.SupprimerSalarie(ManageSalarie.TrouverNoeudParId(manageSalarie.Pdg, id)))
            {
                Console.WriteLine("Salarie licencié avec succès.");
                Enregistrement.SauvegarderDonnees(manageSalarie.Pdg, salarieFilePath);
            }
            else
                Console.WriteLine("Echec de la suppression du salarie.");
            
        }

        /// <summary>
        /// Modifie les informations d'un salarié existant.
        /// </summary>
        static void ModifierSalarie()
        {
            Console.WriteLine("Modification d'un salarié:");
            Console.Write("Entrez l'ID du salarié à modifier: ");
            int id = int.Parse(Console.ReadLine());

            Noeud salarieNoeud = ManageSalarie.TrouverNoeudParId(manageSalarie.Pdg, id);
            if (salarieNoeud == null)
            {
                Console.WriteLine("Salarie non trouvé.");
                return;
            }
            //Console.Write("Nouvel id : (Renseignez le meme si pas de changement): ");
            //int idNouv = int.Parse(Console.ReadLine());
            Console.Write("Nouveau nom (laissez vide si pas de changement): ");
            string nom = Console.ReadLine();
            Console.Write("Nouveau prénom (laissez vide si pas de changement): ");
            string prenom = Console.ReadLine();
            Console.Write("Nouveau poste (laissez vide si pas de changement): ");
            string poste = Console.ReadLine();
            Console.Write("Nouveau salaire (laissez vide si pas de changement): ");
            float? salaire = null;
            string salaireStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(salaireStr)) salaire = float.Parse(salaireStr);
            Console.Write("Nouvel email (laissez vide si pas de changement): ");
            string email = Console.ReadLine();
            Console.Write("Nouveau téléphone (laissez vide si pas de changement): ");
            string telephone = Console.ReadLine();

            if (manageSalarie.ModifierSalarie(salarieNoeud, id, nom, prenom, poste, salaire, email, telephone))
            {
                Console.WriteLine("Les informations du salarié ont été mises à jour.");
                Enregistrement.SauvegarderDonnees(manageSalarie.Pdg, salarieFilePath);
            }

            else
                Console.WriteLine("Erreur lors de la mise à jour du salarié.");
        }





        /*static void AfficherClientsParVille(Manage_Client manageClient)
        {
            Console.Write("Entrez la ville pour afficher les clients: ");
            string ville = Console.ReadLine();
            manageClient.AfficherClientsParVille(ville);
        }
        */
        
        static void Main(string[] args)
        {
            Enregistrement.SauvegarderDonnees(manageSalarie.Pdg, salarieFilePath);
            //Le long commentaire ci dessous correspond à tous les tests que nous avons effectues avant de commencer
            // à créer un menu interactif
            /*
            Salarie salarie = new Salarie(
               id: 1,
               nom: "Dupont",
               prenom: "Jean",
               dateDeNaissance: new DateTime(1985, 5, 15),
               adresse: "123 Rue de la Paix, Paris",
               email: "jean.dupont@example.com",
               telephone: "0123456789",
               dateEntree: DateTime.Now,
               poste: "Directeur",
               salaire: 5500.00f
           );

            // Création d'une instance de Client
            Client client1 = new Client(
                id: 2,
                nom: "Durand",
                prenom: "Marie",
                dateDeNaissance: new DateTime(1990, 8, 25),
                adresse: "456 Avenue du Général, Lyon",
                email: "marie.durand@example.com",
                telephone: "0987654321",
                montantAchatsCumule: 3500.00f,
                commande : null
            );

            Client client2 = new Client(
                id: 3,
                nom: "Edmond",
                prenom: "Guigui",
                dateDeNaissance: new DateTime(2002, 8, 25),
                adresse: "ici",
                email: "bonjour@example.com",
                telephone: "974",
                montantAchatsCumule: 800000,
                commande: null
            );
            /*
            // Affichage des informations du salarié
            Console.WriteLine($"Salarie: {salarie.Nom} {salarie.Prenom}");
            Console.WriteLine($"Email: {salarie.Email}");
            Console.WriteLine($"Poste: {salarie.Poste}");
            Console.WriteLine($"Salaire: {salarie.Salaire}");

            Console.WriteLine();

            // Affichage des informations du client
            Console.WriteLine($"Client: {client.Nom} {client.Prenom}");
            Console.WriteLine($"Email: {client.Email}");
            Console.WriteLine($"Montant des achats cumulés: {client.MontantAchatsCumule}");

            Console.WriteLine();

            // Création d'une instance de Voiture
            Voiture voiture = new Voiture(
                vehiculeId: 1,
                typeVehicule: "Voiture",
                numeroImmatriculation: "ABC123",
                capaciteCharge: 500,
                nombreDePassagers: 4
            );
            voiture.AfficherDetails();

            // Création d'une instance de Camionnette
            Camionnette camionnette = new Camionnette(
                vehiculeId: 2,
                typeVehicule: "Camionnette",
                numeroImmatriculation: "XYZ987",
                capaciteCharge: 1000,
                usage: "Livraison de proximité"
            );
            camionnette.AfficherDetails();

            // Création d'une instance de Camion Benne
            Camion_Benne camionBenne = new Camion_Benne(
                vehiculeId: 3,
                typeVehicule: "Camion",
                numeroImmatriculation: "BEN456",
                capaciteCharge: 5000,
                typeCargaison: "Matériaux de construction",
                nombreDeBennes: 3,
                possedeGrue: true
            );
            camionBenne.AfficherDetails();

            // Création d'une instance de Camion Citerne
            Camion_Citerne camionCiterne = new Camion_Citerne(
                vehiculeId: 4,
                typeVehicule: "Camion-citerne",
                numeroImmatriculation: "CIT890",
                capaciteCharge: 8000,
                typeCargaison: "Produits chimiques",
                typeLiquide: "Carburant"
            );
            camionCiterne.AfficherDetails();

            // Création d'une instance de Camion Frigo
            Camion_Frigo camionFrigo = new Camion_Frigo(
                vehiculeId: 5,
                typeVehicule: "Camion",
                numeroImmatriculation: "FRG123",
                capaciteCharge: 6000,
                typeCargaison: "Produits alimentaires",
                temperatureMin: -10
            );
            camionFrigo.AfficherDetails();
            */
            /*
            Console.WriteLine();

            Manage_Client m = new Manage_Client();
            m.Ajouter(client1);
            m.Ajouter(client2);

            Console.WriteLine();

            // Modification du client par ID
            m.ModifierClient(2, email: "updated.marie@example.com", telephone: "1234567890");

            Console.WriteLine();

            // Affichage des clients par ordre alphabétique
            Console.WriteLine("Affichage des clients par ordre alphabétique:");
            m.AfficherClientsOrdreAlphabetique();

            Console.WriteLine();

            // Suppression d'un client par Nom et Prénom
            m.SupprimerParNom("Edmond", "Guigui");

            // Affichage des clients par ville
            Console.WriteLine("Affichage des clients par ville (Lyon):");
            m.AfficherClientsParVille("Lyon");

            Console.WriteLine();

            // Affichage des clients par montant des achats cumulé
            Console.WriteLine("Affichage des clients par montant des achats cumulé:");
            m.AfficherClientsParMontantAchats();

            Console.WriteLine();



            // Création des instances de Salarie
            Salarie directeurGeneral = new Salarie(1, "Dupond", "Jean", new DateTime(1970, 1, 1), "123 Boulevard du General, Paris", "dupond@example.com", "0102030405", DateTime.Now.AddYears(-10), "Directeur Général", 15000);
            Salarie directeurOperations = new Salarie(2, "Martin", "Paul", new DateTime(1975, 5, 5), "127 Boulevard des Opérations, Paris", "martin@example.com", "0102030406", DateTime.Now.AddYears(-8), "Directeur des Opérations", 14000);
            Salarie chefEquipe = new Salarie(3, "Leroy", "Alice", new DateTime(1982, 6, 6), "128 Boulevard des Opérations, Paris", "leroy@example.com", "0102030407", DateTime.Now.AddYears(-2), "Chef d'Équipe", 10000);

            // Création des noeuds pour l'arbre organisationnel
            Noeud noeudDirecteurGeneral = new Noeud(directeurGeneral);
            Noeud noeudDirecteurOperations = new Noeud(directeurOperations);
            Noeud noeudChefEquipe = new Noeud(chefEquipe);

            // Construction de l'arbre organisationnel
            ManageSalarie manageSalarie = new ManageSalarie(noeudDirecteurGeneral);
            manageSalarie.EmbaucherSousPoste(noeudDirecteurGeneral, noeudDirecteurOperations);
            manageSalarie.EmbaucherSousPoste(noeudDirecteurOperations, noeudChefEquipe);

            // Affichage initial de l'organigramme
            Console.WriteLine("Organigramme initial:");
            ManageSalarie.Affichage(manageSalarie.Pdg);
            
            // Suppression du Directeur des Opérations
            Console.WriteLine("\nSuppression du Chef d equipe:");
            if (manageSalarie.SupprimerSalarie(noeudChefEquipe))
            {
                Console.WriteLine("chef equipe supprimé.");
            }
            else
            {
                Console.WriteLine("Échec de la suppression du Directeur des Opérations.");
            }

            Console.WriteLine();

            // Réaffichage de l'organigramme après suppression
            Console.WriteLine("\nOrganigramme après suppression:");
            ManageSalarie.Affichage(manageSalarie.Pdg);

            Console.WriteLine();

            // Réintégration du chef d equipe
            Console.WriteLine("\nRéintégration du chef d equipe:");
            manageSalarie.EmbaucherSousPoste(noeudDirecteurOperations, noeudChefEquipe);
            //manageSalarie.EmbaucherSousPoste(noeudDirecteurOperations, noeudChefEquipe);

            // Affichage final de l'organigramme
            Console.WriteLine("\nOrganigramme après réintégration:");
            ManageSalarie.Affichage(manageSalarie.Pdg);

            Console.WriteLine();


            // Modification du Directeur des Opérations
            Salarie nouveauDirecteurOp = new Salarie(2, "Lecomte", "Philippe", new DateTime(1980, 10, 10), "127 Boulevard des Opérations, Paris", "lecomte@example.com", "0102030409", DateTime.Now.AddYears(-5), "Directeur des Opérations", 14500);

            Console.WriteLine("\nModification du Directeur des Opérations:");
            if (manageSalarie.ModifierSalarie(noeudDirecteurOperations, nouveauDirecteurOp)) // Null car le Directeur général est le parent direct
            {
                Console.WriteLine("Modification réussie du Directeur des Opérations.");
            }
            else
            {
                Console.WriteLine("Échec de la modification du Directeur des Opérations.");
            }

            // Réaffichage de l'organigramme après modification
            Console.WriteLine("\nOrganigramme après modification:");
            ManageSalarie.Affichage(manageSalarie.Pdg);
            */

            // Création des villes et connexions dans le graphe
            /*Graphe graphe = new Graphe();
            graphe.AjouterArete("Paris", "Lyon", 465.0);
            graphe.AjouterArete("Lyon", "Marseille", 315.0);
            graphe.AjouterArete("Paris", "Marseille", 775.0);
            graphe.AjouterArete("Lyon", "Grenoble", 104.0);
            graphe.AjouterArete("Grenoble", "Marseille", 345.0);*/
            //--------------------------------------------------------
            /*
            Graphe graphe = new Graphe("Distances.csv");

            // Test de l'algorithme de Dijkstra depuis Paris
            var resultatsDijkstra = Dijkstra.ExecuterDijkstra(graphe, "Paris");
            Console.WriteLine("Distances depuis Paris:");
            foreach (var ville in resultatsDijkstra.Keys)
            {
                var (distance, précurseur) = resultatsDijkstra[ville];
                Console.WriteLine($"Distance à {ville}: {distance} km (via {précurseur})");
            }

            // Création de clients et de salariés pour les tests
            Client clientNouv = new Client(
                id: 11,
                nom: "Dupont",
                prenom: "Jean",
                dateDeNaissance: new DateTime(1990, 5, 15),
                adresse: "456 rue de Paris, Lyon",
                email: "jean.dupont@example.com",
                telephone: "0987654321",
                montantAchatsCumule: 2500.0f,
                commande: new List<Commande>()
            );
            
            Console.WriteLine();

            List<Client> baseClients = Manage_Client.InitListeClients();
            Manage_Client.Affichage(baseClients);

            /*
            Chauffeur chauffeur1 = new Chauffeur(
                id: 1,
                nom: "Leroy",
                prenom: "Marc",
                dateDeNaissance: new DateTime(1985, 1, 23),
                adresse: "123 rue du Port, Marseille",
                email: "marc.leroy@example.com",
                telephone: "0123456789",
                dateEntree: DateTime.Now,
                poste: "Chauffeur",
                salaire: 3000.0f,
                numChauffeur : "C1"
            );
            */
            /*
            // Création d'un véhicule
            Vehicule vehicule1 = new Poids_Lourd(
                vehiculeId: 1,
                typeVehicule: "Camion",
                numeroImmatriculation: "AB123CD",
                capaciteCharge: 20000,
                typeCargaison : "Alimentaire"
            );

            //Init liste de chauffeurs

            List<Chauffeur> chauffeurs = Chauffeur.InitListeChauffeur();
            
            // Initialisation du gestionnaire de commandes
            Manage_Commande manageCommande = new Manage_Commande(graphe);

            // Création et test de commandes
            Console.WriteLine("\nCréation de la commande de Paris à Marseille:");
            manageCommande.CreerCommande(baseClients[0], "Paris", "Rouen", chauffeurs[0], vehicule1, baseClients);

            Console.WriteLine("\nCréation de la commande de Lyon à Grenoble:");
            manageCommande.CreerCommande(baseClients[0], "Paris", "La Rochelle", chauffeurs[1], vehicule1, baseClients);

            Console.WriteLine("\nCréation de la commande de Grenoble à Marseille:");
            manageCommande.CreerCommande(baseClients[0], "Nimes", "Avignon", chauffeurs[2], vehicule1, baseClients);

            Console.WriteLine("------------------------------------");

            manageCommande.CreerCommande(clientNouv, "Paris", "Bordeaux", manageCommande.TrouverChauffeurDisponible(chauffeurs, DateTime.Now), vehicule1, baseClients);

            Console.WriteLine();

            Manage_Client.Affichage(baseClients);
            /*
            List<Chauffeur> chauffeurs = Chauffeur.InitListeChauffeur();
            foreach (Chauffeur c in chauffeurs)
            {
                Console.WriteLine(c);
            }
            */
            //Console.WriteLine(Chauffeur.EstLibre(chauffeurs[0], manageCommande.Commandes, manageCommande.Commandes[2].DateCommande));
            /*Console.WriteLine();

            foreach (Client c in baseClients)
            {
                foreach (Commande co in c.Commande)
                {
                    Console.WriteLine(co);
                }
                
            }

            Console.WriteLine();

            manageCommande.AfficherCommandeParId(8);

            Console.WriteLine("------------------------------------------");
            */


            /*
            Noeud racine = ManageSalarie.InitListeSalaries();
            ManageSalarie manageSalarie = new ManageSalarie(racine);

            // Affichage de l'arborescence initiale
            Console.WriteLine("Arborescence initiale de l'entreprise:");
            ManageSalarie.Affichage(manageSalarie.Pdg);

            // Simulation de l'embauche d'un nouveau chef d'équipe sous le Directeur des Opérations
            Salarie nouveauChef = new Salarie(11, "Moreau", "Lucie", new DateTime(1992, 7, 10), "135 Boulevard des Opérations, Paris", "moreau@example.com", "0102030411", DateTime.Now.AddYears(-3), "Chef d'Équipe", 10500);
            Noeud nouveauChefNoeud = new Noeud(nouveauChef);
            manageSalarie.EmbaucherSousPoste(racine.Successeur, nouveauChefNoeud);

            // Affichage de l'arborescence après embauche
            Console.WriteLine("\nArborescence après embauche d'un nouveau Chef d'Équipe:");
            ManageSalarie.Affichage(racine);

            // Suppression d'un salarié (exemple: suppression du premier chef d'équipe ajouté)
            bool suppressionReussie = manageSalarie.SupprimerSalarie(nouveauChefNoeud);
            if (suppressionReussie)
            {
                Console.WriteLine("\nSuppression réussie du Chef d'Équipe.");
            }
            else
            {
                Console.WriteLine("\nÉchec de la suppression.");
            }

            // Affichage de l'arborescence après suppression
            Console.WriteLine("\nArborescence après suppression d'un Chef d'Équipe:");
            ManageSalarie.Affichage(racine);
            */

            MenuPrincipal();

            Console.ReadKey();
        }


        /// <summary>
        /// Affiche le menu principal de l'application et gère la navigation.
        /// </summary>
        static void MenuPrincipal()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("\n--- Menu Principal de l'Application ---");
                Console.WriteLine("1 - Gérer les Clients");
                Console.WriteLine("2 - Gérer les Salaries");
                Console.WriteLine("3 - Gérer les Commandes");
                Console.WriteLine("4 - Voir les Statistiques");
                Console.WriteLine("0 - Quitter");
                Console.Write("Entrez votre choix: ");

                int choix = Convert.ToInt32(Console.ReadLine());
                
                switch (choix)
                {
                    case 1:
                        MenuClient();
                        break;
                    case 2:
                        MenuSalaries();
                        break;
                    case 3:
                        MenuCommandes();
                        break;
                    case 4:
                        MenuStatistiques();
                        break;
                    case 0:
                        continuer = false;
                        Console.WriteLine("Fermeture de l'application.");
                        break;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
            }
        }




        //Partie pour gérer l'interface client
        static void MenuClient()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("\n--- Menu Client ---");
                Console.WriteLine("1 - Voir Base De Client");
                Console.WriteLine("2 - Entrer Nouveau Client");
                Console.WriteLine("3 - Supprimer Client");
                Console.WriteLine("4 - Modifier Client");
                Console.WriteLine("0 - Revenir au menu precedent");
                Console.Write("Entrez votre choix: ");

                int choix = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                switch (choix)
                {
                    case 1:
                        MenuAfficherClients();
                        break;
                    case 2:
                        AjouterClient(manageClient);
                        break;
                    case 3:
                        SupprimerClient(manageClient);
                        break;
                    case 4:
                        ModifierClient(manageClient);
                        break;
                    case 0:
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
            }
        }

        /// <summary>
        /// Affiche le sous-menu de gestion des clients.
        /// </summary>
        static void MenuAfficherClients()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("\n--- Gestion des Clients ---");
                Console.WriteLine("1 - Afficher les clients");
                Console.WriteLine("2 - Afficher les clients par ville");
                Console.WriteLine("3 - Afficher les clients par ordre alphabetique");
                Console.WriteLine("4 - Afficher les clients par montant d'achats cumules");
                Console.WriteLine("0 - Revenir au menu precedent");

                Console.Write("Entrez votre choix: ");

                int choix = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                switch (choix)
                {
                    case 1:
                        Manage_Client.Affichage(manageClient.Clients);
                        break;
                    case 2:
                        manageClient.AfficherClientsParVille();
                        break;
                    case 3:
                        manageClient.AfficherClientsOrdreAlphabetique();
                        break;
                    case 4:
                        manageClient.AfficherClientsParMontantAchats();
                        break;
                    case 0:
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
            }
        }


        /// <summary>
        /// Affiche le menu de gestion des salariés et gère la navigation.
        /// </summary>
        static void MenuSalaries()
        {
            
            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("\n--- Menu Salarie ---");
                Console.WriteLine("1 - Afficher l'Organigramme");
                Console.WriteLine("2 - Embaucher un nouveau salarie");
                Console.WriteLine("3 - Licencier un salarie"); //Ne peut etre licencie que s'il n'a personne sous sa reponsabilite
                Console.WriteLine("4 - Modifier un salarie"); //Si un directeur ou un chef/manager vient a changer au lieu de supprimer l'ancien directeur on le modifie plutot
                Console.WriteLine("0 - Revenir au menu precedent");
                Console.Write("Entrez votre choix: ");

               
                int choix = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                switch (choix)
                {
                    case 1:
                        ManageSalarie.Affichage(manageSalarie.Pdg);
                        break;
                    case 2:
                        EmbaucherNouveauSalarie();
                        break;
                    case 3:
                        LicencierSalarie();
                        break;
                    case 4:
                        ModifierSalarie();
                        break;
                    case 0:
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
            }
        }



        /// <summary>
        /// Permet l'embauche d'un nouveau salarié.
        /// </summary>
        static void EmbaucherNouveauSalarie()
        {
            Console.Write("Id (num securite sociale) : ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Embauche d'un nouveau salarié:");
            Console.Write("Nom: ");
            string nom = Console.ReadLine();
            Console.Write("Prénom: ");
            string prenom = Console.ReadLine();
            Console.Write("Adresse: ");
            string adresse = Console.ReadLine();
            Console.Write("Poste: ");
            string poste = Console.ReadLine();
            Console.Write("Salaire: ");
            float salaire = float.Parse(Console.ReadLine());
            Console.Write("Date d'entrée (yyyy-MM-dd): ");
            DateTime dateEntree = DateTime.Parse(Console.ReadLine());
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Téléphone: ");
            string telephone = Console.ReadLine();

            Salarie nouveauSalarie = new Salarie(id , nom, prenom, DateTime.Now, adresse, email, telephone, dateEntree, poste, salaire);
            Noeud nouveauNoeud = new Noeud(nouveauSalarie);

            Console.WriteLine("1 - Embaucher comme successeur d'un poste spécifique");
            Console.WriteLine("2 - Embaucher comme collègue d'un poste spécifique");
            int typeEmbauche = Convert.ToInt32(Console.ReadLine());

            if (typeEmbauche == 1)
            {
                Console.Write("ID du salarié à qui succéder: ");
                int idSalarie = int.Parse(Console.ReadLine());
                var salarieCible = ManageSalarie.TrouverNoeudParId(manageSalarie.Pdg, idSalarie);
                if (salarieCible != null)
                {
                    manageSalarie.EmbaucherSousPoste(salarieCible, nouveauNoeud);
                    Enregistrement.SauvegarderDonnees(manageSalarie.Pdg, salarieFilePath);
                }
                else
                {
                    Console.WriteLine("Salarie non trouvé.");
                }
            }
            else if (typeEmbauche == 2)
            {
                Console.Write("ID du salarié à côté duquel embaucher: ");
                int idSalarie = int.Parse(Console.ReadLine());
                var salarieCible = ManageSalarie.TrouverNoeudParId(manageSalarie.Pdg, idSalarie);
                if (salarieCible != null)
                {
                    manageSalarie.EmbaucherMemePoste(salarieCible, nouveauNoeud);
                    Enregistrement.SauvegarderDonnees(manageSalarie.Pdg, salarieFilePath);
                }
                else
                {
                    Console.WriteLine("Salarie non trouvé.");
                }
            }
            else
            {
                Console.WriteLine("Option d'embauche invalide.");
            }
        }



        /// <summary>
        /// Affiche le menu de gestion des commandes et gère la navigation.
        /// </summary>
        static void MenuCommandes()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("\n--- Menu Salarie ---");
                Console.WriteLine("1 - Créer une commande");
                Console.WriteLine("2 - Modifier une commande");
                Console.WriteLine("3 - Afficher toutes les commandes"); 
                Console.WriteLine("4 - Afficher commandes selon le type de vehicule");
                Console.WriteLine("5 - Afficher les statistiques mensuelles");
                Console.WriteLine("6 - Générer une facture");
                Console.WriteLine("7 - Sauvegarder une facture");
                Console.WriteLine("0 - Revenir au menu precedent");
                Console.Write("Entrez votre choix: ");

               
                int choix = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                switch (choix)
                {
                    case 1:
                        CreerUneCommande();
                        break;
                    case 2:
                        ModifierUneCommande();
                        break;
                    case 3:
                        manageCommande.AfficherToutesLesCommandes();
                        break;
                    case 4:
                        AfficheCommandesParTypeVehicule();
                        break;
                    case 5:
                        AfficherStatistiquesMensuelles();
                        break;
                    case 6:
                        GenererEtAfficherFacture();
                        break;
                    case 7:
                        SauvegarderFacture();
                        break;
                    case 0:
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
            }
        }


        /// <summary>
        /// Crée une nouvelle commande et l'ajoute à la liste des commandes.
        /// </summary>
        public static void CreerUneCommande()
        {
            Console.Write("Entrez l'ID du client: ");
            int clientId = Convert.ToInt32(Console.ReadLine());
            Client client = manageClient.TrouverClientParId(clientId);
            Client clientNouv = null;

            if (client == null)
            {
                Console.WriteLine("Aucun client trouvé avec cet ID. Voulez-vous créer un nouveau client ? (oui/non)");
                string reponse = Console.ReadLine().ToLower();
                if (reponse == "oui")
                {

                    Console.WriteLine("Entrer les informations du nouveau client:");
                    Console.Write("Nom: ");
                    string nom = Console.ReadLine();
                    Console.Write("Prénom: ");
                    string prenom = Console.ReadLine();
                    Console.Write("Date de Naissance (YYYY-MM-DD): ");
                    DateTime dateNaissance = DateTime.Parse(Console.ReadLine());
                    Console.Write("Adresse: ");
                    string adresse = Console.ReadLine();
                    Console.Write("Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Téléphone: ");
                    string telephone = Console.ReadLine();
                    Console.Write("Montant des achats cumulé: ");
                    float montantAchatsCumule = float.Parse(Console.ReadLine());

                    clientNouv = new Client(
                        manageClient.Clients.Count + 1,
                        nom,
                        prenom,
                        dateNaissance,
                        adresse,
                        email,
                        telephone,
                        montantAchatsCumule,
                        new List<Commande>()
                    );

                    manageClient.Ajouter(clientNouv);
                    Enregistrement.SauvegarderDonnees(manageClient.Clients, clientFilePath);

                }
            }

            Console.Write("Ville de départ: ");
            string villeDepart = Console.ReadLine().ToUpper() ;
            Console.Write("Ville d'arrivée: ");
            string villeArrivee = Console.ReadLine().ToUpper();

            Console.Write("Date de livraison souhaité (YYYY-MM-dd) : ");
            DateTime dateLivraison = DateTime.Parse(Console.ReadLine()); // Livraison sous 15 jours
            //DateTime dateRecherche = DateTime.Parse(Console.ReadLine());
            Chauffeur chauffeur = manageCommande.TrouverChauffeurDisponible(chauffeurs, dateLivraison);

            if (chauffeur == null)
            {
                Console.WriteLine("Aucun chauffeur disponible");
                return;
            }

            Console.WriteLine("Choisissez un type de véhicule:");
            Console.WriteLine("1. Voiture");
            Console.WriteLine("2. Camionnette");
            Console.WriteLine("3. Camion Benne");
            Console.WriteLine("4. Camion Citerne");
            Console.WriteLine("5. Camion Frigo");
            int choixVehicule = Convert.ToInt32(Console.ReadLine());

            //Vehicule vehicule = null;
            switch (choixVehicule)
            {
                case 1:
                    Voiture voit = voitures.FirstOrDefault(v => v.EstDisponible(dateLivraison));
                    if (client == null)
                    {
                        manageCommande.CreerCommande(clientNouv, dateLivraison, villeDepart, villeArrivee, chauffeur, voit, manageClient.Clients);
                    }
                    else
                    {
                        manageCommande.CreerCommande(client, dateLivraison, villeDepart, villeArrivee, chauffeur, voit, manageClient.Clients);
                    }
                    break;
                case 2:
                    Camionnette camio = camionnettes.FirstOrDefault(c => c.EstDisponible(dateLivraison));
                    if (client == null)
                    {
                        manageCommande.CreerCommande(clientNouv, dateLivraison, villeDepart, villeArrivee, chauffeur, camio, manageClient.Clients);
                    }
                    else
                    {
                        manageCommande.CreerCommande(client, dateLivraison, villeDepart, villeArrivee, chauffeur, camio, manageClient.Clients);
                    }
                    break;
                case 3:
                    Camion_Benne cb = camion_bennes.FirstOrDefault(c => c.EstDisponible(dateLivraison));
                    if (client == null)
                    {
                        manageCommande.CreerCommande(clientNouv, dateLivraison, villeDepart, villeArrivee, chauffeur, cb, manageClient.Clients);
                    }
                    else
                    {
                        manageCommande.CreerCommande(client, dateLivraison, villeDepart, villeArrivee, chauffeur, cb, manageClient.Clients);
                    }
                    break;
                case 4:
                    Camion_Citerne ct = camion_citernes.FirstOrDefault(c => c.EstDisponible(dateLivraison));
                    if (client == null)
                    {
                        manageCommande.CreerCommande(clientNouv, dateLivraison, villeDepart, villeArrivee, chauffeur, ct, manageClient.Clients);
                    }
                    else
                    {
                        manageCommande.CreerCommande(client, dateLivraison, villeDepart, villeArrivee, chauffeur, ct, manageClient.Clients);
                    }
                    break;
                case 5:
                    Camion_Frigo cf = camion_frigos.FirstOrDefault(c => c.EstDisponible(dateLivraison));
                    if (client == null)
                    {
                        manageCommande.CreerCommande(clientNouv, dateLivraison, villeDepart, villeArrivee, chauffeur, cf, manageClient.Clients);
                    }
                    else
                    {
                        manageCommande.CreerCommande(client, dateLivraison, villeDepart, villeArrivee, chauffeur, cf, manageClient.Clients);
                    }
                    break;
                default:
                    Console.WriteLine("Choix de véhicule invalide.");
                    return;
            }
            
            

            Console.WriteLine("Commande ajoutée. État actuel des commandes:");
            foreach (var cmd in manageCommande.Commandes)
            {
                Console.WriteLine($"Commande {cmd.CommandeId} pour {cmd.Client.Nom} le {cmd.DateCommande}");
            }

        }



        /// <summary>
        /// Modifie une commande existante.
        /// </summary>
        public static void ModifierUneCommande()
        {
            Console.Write("Entrez l'ID de la commande à modifier: ");
            int commandeId = Convert.ToInt32(Console.ReadLine());
            Commande commande = manageCommande.TrouverCommandeParId(commandeId);

            if (commande == null)
            {
                Console.WriteLine("Commande non trouvée.");
                return;
            }

            Console.Write("Nouvelle ville de départ (laisser vide si pas de changement): ");
            string newVilleDepart = Console.ReadLine();
            if (!string.IsNullOrEmpty(newVilleDepart))
            {
                commande.VilleDepart = newVilleDepart.ToUpper();
            }

            Console.Write("Nouvelle ville d'arrivée (laisser vide si pas de changement): ");
            string newVilleArrivee = Console.ReadLine();
            if (!string.IsNullOrEmpty(newVilleArrivee))
            {
                commande.VilleArrivee = newVilleArrivee.ToUpper();
            }


            var distances = Dijkstra.ExecuterDijkstra(graphe, commande.VilleDepart);
            if (!distances.ContainsKey(commande.VilleArrivee) || distances[commande.VilleArrivee].distance == double.MaxValue)
            {
                Console.WriteLine("Aucun chemin disponible pour la livraison.");
                //return;
            }
            float distance2Villes = (float)distances[commande.VilleArrivee].distance;
            float prix = manageCommande.CalculerPrix(distance2Villes);
            commande.Prix = prix;
            commande.Distance = distance2Villes;

            Console.Write("Nouvelle date de la commande (format: YYYY-MM-DD, laisser vide si pas de changement): ");
            string newDate = Console.ReadLine();
            if (!string.IsNullOrEmpty(newDate))
            {
                commande.Vehicule.DatesIndisponibles.Remove(commande.DateCommande);
                commande.DateCommande = DateTime.Parse(newDate);
                commande.Vehicule.DatesIndisponibles.Add(commande.DateCommande);
            }

            Console.Write("ID du nouveau chauffeur (laisser vide si pas de changement): ");
            string newChauffeurId = Console.ReadLine();
            if (!string.IsNullOrEmpty(newChauffeurId))
            {
                Chauffeur newChauffeur = chauffeurs.FirstOrDefault(c => c.Id == Convert.ToInt32(newChauffeurId));
                if (newChauffeur != null)
                {
                    commande.Chauffeur = newChauffeur;
                }
                else
                {
                    Console.WriteLine("Chauffeur non trouvé.");
                }
            }


            Console.Write("Voulez-vous modifier le vehicule (oui/non) : ");
            string choix = Console.ReadLine().ToLower();
            if (choix == "oui")
            {
                commande.Vehicule.DatesIndisponibles.Remove(commande.DateCommande);
                Console.WriteLine("Choisissez un nouveau type de véhicule:");
                Console.WriteLine("1. Voiture");
                Console.WriteLine("2. Camionnette");
                Console.WriteLine("3. Camion Benne");
                Console.WriteLine("4. Camion Citerne");
                Console.WriteLine("5. Camion Frigo");
                int choixVehicule = Convert.ToInt32(Console.ReadLine());
                
                
                switch (choixVehicule)
                {
                    case 1:
                        commande.Vehicule = voitures.FirstOrDefault(v => v.EstDisponible(commande.DateCommande));
                        break;
                    case 2:
                        commande.Vehicule = camionnettes.FirstOrDefault(c => c.EstDisponible(commande.DateCommande));
                        break;
                    case 3:
                        commande.Vehicule = camion_bennes.FirstOrDefault(c => c.EstDisponible(commande.DateCommande));
                        break;
                    case 4:
                        commande.Vehicule = camion_citernes.FirstOrDefault(c => c.EstDisponible(commande.DateCommande));
                        break;
                    case 5:
                        commande.Vehicule = camion_frigos.FirstOrDefault(c => c.EstDisponible(commande.DateCommande));
                        break;
                    default:
                        Console.WriteLine("Choix de véhicule invalide.");
                        return;
                }

                commande.Vehicule.DatesIndisponibles.Add(commande.DateCommande);
            }

            ModifierStatutCommande(commandeId);

            // Affiche les détails de la commande mise à jour
            Console.WriteLine("Détails de la commande mise à jour:");
            Console.WriteLine(commande);
            Enregistrement.SauvegarderDonnees<Commande>(manageCommande.Commandes, commandeFilePath);
        }

        /// <summary>
        /// Modifie le statut d'une commande (livrée ou en cours).
        /// </summary>
        public static void ModifierStatutCommande(int commandeId)
        {
            Commande commande = manageCommande.TrouverCommandeParId(commandeId);

            if (commande == null)
            {
                Console.WriteLine("Commande non trouvée.");
                return;
            }

            Console.WriteLine("Sélectionnez le nouveau statut:");
            Console.WriteLine("1. Livrée");
            Console.WriteLine("2. En cours");

            int choix = Convert.ToInt32(Console.ReadLine());
            switch (choix)
            {
                case 1:
                    commande.Statut = true;
                    Console.WriteLine($"Le statut de la commande ID {commandeId} a été mis à jour à 'Livrée'.");
                    break;
                case 2:
                    commande.Statut = false;
                    Console.WriteLine($"Le statut de la commande ID {commandeId} a été mis à jour à 'En cours'.");
                    break;
                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }
        }


        /// <summary>
        /// Affiche les commandes par type de véhicule.
        /// </summary>
        public static void AfficheCommandesParTypeVehicule()
        {
            Console.WriteLine("Choisissez un type de véhicule:");
            Console.WriteLine("1. Voiture");
            Console.WriteLine("2. Camionnette");
            Console.WriteLine("3. Camion Benne");
            Console.WriteLine("4. Camion Citerne");
            Console.WriteLine("5. Camion Frigo");
            int choixVehicule = Convert.ToInt32(Console.ReadLine());

            manageCommande.AfficherCommandesParTypeVehicule(choixVehicule);



        }


        /// <summary>
        /// Affiche les statistiques mensuelles.
        /// </summary>
        static void AfficherStatistiquesMensuelles()
        {
            Console.Write("Entrez le mois (1-12): ");
            int mois = Convert.ToInt32(Console.ReadLine());
            Console.Write("Entrez l'année: ");
            int annee = Convert.ToInt32(Console.ReadLine());

            manageCommande.AfficherStatistiquesMensuelles(mois, annee);
        }

        /// <summary>
        /// Génère et affiche la facture pour une commande spécifique.
        /// </summary>
        static void GenererEtAfficherFacture()
        {
            Console.Write("Entrez l'ID de la commande pour générer la facture: ");
            int commandeId = Convert.ToInt32(Console.ReadLine());
            manageCommande.AfficherFacture(commandeId);
        }


        /// <summary>
        /// Sauvegarde la facture pour une commande spécifique.
        /// </summary>
        static void SauvegarderFacture()
        {
            Console.Write("Entrez l'ID de la commande pour sauvegarder la facture: ");
            int commandeId = Convert.ToInt32(Console.ReadLine());
            //Console.Write("Entrez le chemin de fichier pour sauvegarder la facture: ");
            string filePath = "Facture/" + commandeId + ".txt";
            manageCommande.SauvegarderFacture(commandeId, filePath);
        }

        /// <summary>
        /// Affiche le menu des statistiques et gère la navigation.
        /// </summary>
        static void MenuStatistiques()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("\n--- Menu Statistiques ---");
                Console.WriteLine("1 - Afficher le nombre de livraisons effectuées par chauffeur");
                Console.WriteLine("2 - Afficher les commandes par période");
                Console.WriteLine("3 - Afficher la moyenne des prix des commandes");
                Console.WriteLine("4 - Afficher la moyenne des comptes clients");
                Console.WriteLine("5 - Afficher la liste des commandes pour un client");
                Console.WriteLine("6 - Afficher les statistiques d'un chauffeur");
                Console.WriteLine("0 - Revenir au menu precedent");
                Console.Write("Entrez votre choix: ");


                int choix = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                switch (choix)
                {
                    case 1:
                        Chauffeur.AfficherNombreDeLivraisons(chauffeurs, manageCommande);
                        break;
                    case 2:
                        manageCommande.AfficherCommandesParPeriode();
                        break;
                    case 3:
                        manageCommande.AfficherMoyennePrixCommandes();
                        break;
                    case 4:
                        manageClient.AfficherMoyenneMontantsAchatsCumules();
                        break;
                    case 5:
                        manageCommande.AfficherCommandeParIdClient(manageClient);
                        break;
                    case 6:
                        AfficherStatistiquesChauffeur();
                        break;
                    case 0:
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
            }

        }

        /// <summary>
        /// Affiche les statistiques pour un chauffeur spécifique.
        /// </summary>
        static void AfficherStatistiquesChauffeur()
        {
            Console.Write("Entrez l'ID du chauffeur: ");
            string numChauffeur = Console.ReadLine();
            Chauffeur chauffeur = chauffeurs.FirstOrDefault(c => c.NumChauffeur == numChauffeur);

            if (chauffeur != null)
            {
                manageCommande.AfficherStatistiquesChauffeur(chauffeur);
            }
            else
            {
                Console.WriteLine("Chauffeur non trouvé.");
            }
        }


    }
}
