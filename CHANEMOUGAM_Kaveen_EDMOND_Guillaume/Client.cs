using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Client : Personne
    {
        private float montantAchatsCumule;
        private List<Commande> commande = new List<Commande>();

        public Client(int id, string nom, string prenom, DateTime dateDeNaissance, string adresse, string email, string telephone, float montantAchatsCumule, List<Commande> commande)
        : base(id, nom, prenom, dateDeNaissance, adresse, email, telephone)
        {
            this.montantAchatsCumule = montantAchatsCumule;
            this.commande = commande;
        }

        public override string ToString()
        {
            return base.ToString() + ", MontantAchatsCumule: " +  montantAchatsCumule + ", Commande: A AJOUTER" ;
        }

        public float MontantAchatsCumule { get => montantAchatsCumule; set => montantAchatsCumule = value; }
        public List<Commande> Commande { get => commande; set => commande = value; }

    }
}


