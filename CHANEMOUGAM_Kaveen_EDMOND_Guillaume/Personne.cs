using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public abstract class Personne
    {
        protected int id;
        protected string nom;
        protected string prenom;
        protected DateTime dateDeNaissance;
        protected string adresse;
        protected string email;
        protected string telephone;

        public Personne(int id, string nom, string prenom, DateTime dateDeNaissance, string adresse, string email, string telephone)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.dateDeNaissance = dateDeNaissance;
            this.adresse = adresse;
            this.email = email;
            this.telephone = telephone;
        }

        // Propriétés pour accéder et modifier les informations
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public DateTime DateDeNaissance { get => dateDeNaissance; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Email { get => email; set => email = value; }
        public string Telephone { get => telephone; set => telephone = value; }

        public override string ToString()
        {
            return "ID: " + Id +
                   ", Nom: " + Nom +
                   ", Prénom: " + Prenom +
                   ", Date de Naissance: " + DateDeNaissance.ToShortDateString() +
                   ", Adresse: " + Adresse +
                   ", Email: " + Email +
                   ", Téléphone: " + Telephone;
        }

    }

}
