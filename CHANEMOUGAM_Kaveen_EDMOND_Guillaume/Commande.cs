using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Commande
    {
        private int commandeId;
        private Client client;
        private Chauffeur chauffeur ;
        private Vehicule vehicule;
        private DateTime dateCommande ;
        private string villeDepart;
        private string villeArrivee ;
        private float prix;
        private float distance;
        private bool statut = false;


        public Commande(int commandeId, Client client, Chauffeur chauffeur, Vehicule vehicule, DateTime dateCommande, string villeDepart, string villeArrivee, float prix, float distance, bool statut)
        {
            this.commandeId = commandeId;
            this.client = client;
            this.vehicule = vehicule;
            this.villeDepart = villeDepart;
            this.villeArrivee = villeArrivee;
            this.dateCommande = dateCommande;
            this.chauffeur = chauffeur;
            this.prix = prix;
            this.distance = distance;
            this.statut = statut;
        }


        public int CommandeId
        {
            get { return commandeId; }
            set { commandeId = value; }
        }

        public Client Client
        {
            get { return client; }
            set { client = value; }
        }

        public string VilleDepart
        {
            get { return villeDepart; }
            set { villeDepart = value; }
        }

        public string VilleArrivee
        {
            get { return villeArrivee; }
            set { villeArrivee = value; }
        }

        public DateTime DateCommande
        {
            get { return dateCommande; }
            set { dateCommande = value; }
        }

        public Chauffeur Chauffeur
        {
            get { return chauffeur; }
            set { chauffeur = value; }
        }

        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public float Prix
        {
            get { return prix; }
            set { prix = value; }
        }

        public Vehicule Vehicule
        {
            get { return vehicule; }
            set { vehicule = value; }
        }

        public bool Statut
        {
            get { return statut; }
            set { statut = value; }
        }

        public float PrixVehicule()
        {
            float p = 1;
            if (vehicule is Voiture)
            {
                p = 1;
            }
            else if (vehicule is Camionnette)
            {
                p = 1.1f;
            }
            else if (vehicule is Camion_Benne)
            {
                p = 1.2f;
            }
            else if (vehicule is Camion_Citerne)
            {
                p = 1.22f;
            }
            else if (vehicule is Camion_Frigo)
            {
                p = 1.25f;
            }
            return p;

        }
        public override string ToString()
        {
            //Ici nous avons utilisé avec succes un nouvel outil pour la concaténation d'une longue chaine de caracteres
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Commande ID: {commandeId}");
            sb.AppendLine($"Client: {client.Nom}, {client.Prenom}");
            sb.AppendLine($"Chauffeur: {chauffeur.NumChauffeur}, {chauffeur.Nom} {chauffeur.Prenom}");
            sb.AppendLine($"Véhicule: {vehicule.NumeroImmatriculation}");
            sb.AppendLine($"Date de la commande: {dateCommande.ToShortDateString()}");
            sb.AppendLine($"Ville de départ: {villeDepart}");
            sb.AppendLine($"Ville d'arrivée: {villeArrivee}");
            sb.AppendLine($"Distance: {distance} km");
            sb.AppendLine($"Prix: {prix} EUR");
            sb.Append($"Statut: {(statut ? "Livrée" : "En cours")}");

            return sb.ToString();
        }
    }



}
