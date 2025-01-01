using System;
using System.Collections.Generic;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Salarie : Personne
    {
        private DateTime dateEntree;
        private string poste;
        private float salaire;

        public Salarie(int id, string nom, string prenom, DateTime dateDeNaissance, string adresse, string email, string telephone, DateTime dateEntree, string poste, float salaire)
            : base(id, nom, prenom, dateDeNaissance, adresse, email, telephone)
        {
            this.dateEntree = dateEntree;
            this.poste = poste;
            this.salaire = salaire;
        }

        public DateTime DateEntree { get => dateEntree; }
        public string Poste { get => poste; set => poste = value; }
        public float Salaire { get => salaire; set => salaire = value; }

    }
} 
