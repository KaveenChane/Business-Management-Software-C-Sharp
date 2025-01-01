using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Manage_Client : ISupprimable
    {
        private List<Client> clients;

        public Manage_Client()
        {
            this.clients = new List<Client>();
        }

        public Manage_Client(List<Client> clients)
        {
            this.clients = clients;
        }

        public List<Client> Clients
        {
            get { return clients; }
            set { clients = value; }
        }


        /// <summary>
        /// Méthode pour créer un nouveau client et l'ajouter à la liste des clients.
        /// </summary>
        /// <param name="id">ID du client.</param>
        /// <param name="nom">Nom du client.</param>
        /// <param name="prenom">Prénom du client.</param>
        /// /// <param name="dateDeNaissance">Date de naissance du client.</param>
        /// <param name="adresse">Adresse du client.</param>
        /// <param name="email">Email du client.</param>
        /// <param name="telephone">Téléphone du client.</param>
        /// <param name="montantAchatsCumule">Montant des achats cumulés du client.</param>
        /// <returns>Le nouveau client créé ou null si la création a échoué.</returns>  
        public Client? CreerClient(int id, string nom, string prenom, DateTime dateDeNaissance, string adresse, string email, string telephone, float montantAchatsCumule)
        {
            Client nouveauClient = new Client(id, nom, prenom, dateDeNaissance, adresse, email, telephone, montantAchatsCumule, new List<Commande>());
            if (nouveauClient == null)
            {
                Console.WriteLine("Client null");
                return null;
            }
            clients.Add(nouveauClient);
            //Console.WriteLine("Client créé et ajouté avec succès.");
            return nouveauClient;
        }



        /// <summary>
        /// Méthode pour ajouter un client à la liste des clients.
        /// </summary>
        /// <param name="client">Client à ajouter.</param>
        public void Ajouter(Client client)
        {
            clients.Add(client);
            Console.WriteLine("Client ajouté avec succès.\n");
        }




        /// <summary>
        /// Méthode pour supprimer un client de la liste par son ID.
        /// </summary>
        /// <param name="id">ID du client à supprimer.</param>
        public void SupprimerParId(int id)
        {
            Client client = clients.Find(c => c.Id == id);
            if (client != null)
                clients.Remove(client);
            else
            {
                Console.WriteLine("Client non trouvé.\n");
            }
        }


        /// <summary>
        /// Méthode pour supprimer un client de la liste par son nom et prénom.
        /// </summary>
        /// <param name="nom">Nom du client à supprimer.</param>
        /// <param name="prenom">Prénom du client à supprimer.</param>
        public void SupprimerParNom(string nom, string prenom)
        {
            Client client = clients.Find(c => c.Nom == nom && c.Prenom == prenom);
            if (client != null)
            {
                clients.Remove(client);
                Console.WriteLine("Client supprimé avec succès.");
            }
            else
            {
                Console.WriteLine("Client non trouvé.");
            }
        }






        /// <summary>
        /// Méthode pour modifier les informations d'un client.
        /// </summary>
        /// <param name="clientId">ID du client à modifier.</param>
        /// <param name="nom">Nouveau nom du client.</param>
        /// <param name="prenom">Nouveau prénom du client.</param>
        /// <param name="email">Nouvel email du client.</param>
        /// <param name="adresse">Nouvelle adresse du client.</param>
        /// <param name="telephone">Nouveau téléphone du client.</param>
        public void ModifierClient(int clientId, string nom = null, string prenom = null, string email = null, string adresse = null, string telephone = null)
        {
            Client client = clients.Find(c => c.Id == clientId);
            if (client != null)
            {
                // Mettre à jour les atttributs qui sont fournis
                if (!string.IsNullOrEmpty(nom))
                {
                    client.Nom = nom;
                }
                if (!string.IsNullOrEmpty(prenom))
                {
                    client.Prenom = prenom;
                }
               
                if (!string.IsNullOrEmpty(email))
                {
                    client.Email = email;
                }

                
                if (!string.IsNullOrEmpty(adresse))
                {
                    client.Adresse = adresse;
                }

                
                if (!string.IsNullOrEmpty(telephone))
                {
                    client.Telephone = telephone;
                }
                
                Console.WriteLine("Client modifié avec succès.");
            }
            else
            {
                Console.WriteLine("Client non trouvé.");
            }
            
        }



        /// <summary>
        /// Méthode pour afficher les clients par ordre alphabétique de nom.
        /// </summary>
        public void AfficherClientsOrdreAlphabetique()
        {
            foreach (Client client in clients.OrderBy(c => c.Nom))
            {
                Console.WriteLine(client);
            }
        }


        /// <summary>
        /// Méthode pour afficher les clients par ville.
        /// </summary>
        public void AfficherClientsParVille()
        {
            Console.WriteLine("Saisissez la ville : ");
            string ville = Console.ReadLine();
            if (ville != null)
            {
                var clientsParVille = clients.Where(c => c.Adresse.Contains(ville)).ToList();
                foreach (var client in clientsParVille)
                {
                    Console.WriteLine(client);
                }
            }
            else
            {
                Console.WriteLine("Le nom de la ville est null");
            }
        }



        /// <summary>
        /// Méthode pour afficher les clients par montant des achats cumulés.
        /// </summary>
        public void AfficherClientsParMontantAchats()
        {
            foreach (Client client in clients.OrderByDescending(c => c.MontantAchatsCumule))
            {
                Console.WriteLine(client);
            }
        }


        /// <summary>
        /// Méthode pour initialiser une liste de clients fictifs.
        /// </summary>
        /// <returns>Liste initialisée de clients.</returns>
        public static List<Client> InitListeClients()
        {
            List<Client> clients = new List<Client>();
            for (int i = 1; i <= 10; i++)
            {
                clients.Add(new Client(
                    id: i,
                    nom: $"ClientNom{i}",
                    prenom: $"ClientPrenom{i}",
                    dateDeNaissance: new DateTime(1980 + i, 1, 1),
                    adresse: $"{i} Rue de l'Exemple, Ville{i}",
                    email: $"client{i}@example.com",
                    telephone: $"0123456{i:000}",
                    montantAchatsCumule: i * 1000,
                    commande: new List<Commande>()
                ));
            }
            string clientFilePath = "Data/clients.json";
            Enregistrement.SauvegarderDonnees(clients, clientFilePath);
            return clients;
        }


        /// <summary>
        /// Méthode pour trouver un client par son ID.
        /// </summary>
        /// <param name="id">ID du client à trouver.</param>
        /// <returns>Le client correspondant à l'ID ou null si non trouvé.</returns>
        public Client? TrouverClientParId(int id)
        {
            return clients.FirstOrDefault(c => c.Id == id);
        }



        /// <summary>
        /// Méthode pour afficher la liste des clients.
        /// </summary>
        /// <param name="clients">Liste de clients à afficher.</param>
        public static void Affichage(List<Client> clients)
        {
            foreach (Client c in clients)
            {
                Console.WriteLine("Id : " + c.Id + " ,Nom : " + c.Nom + " ,Prenom : " + c.Prenom);
            }
        }



        /// <summary>
        /// Méthode pour afficher la moyenne des montants des achats cumulés des clients.
        /// </summary>
        public void AfficherMoyenneMontantsAchatsCumules()
        {
            if (clients.Count == 0)
            {
                Console.WriteLine("Aucun client enregistré.");
                return;
            }

            double moyenne = clients.Average(c => c.MontantAchatsCumule);
            Console.WriteLine($"La moyenne des montants des achats cumulés des clients est: {moyenne} EUR");
        }
    }
}

