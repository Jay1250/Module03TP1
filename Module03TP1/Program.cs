using Module03TP1.BO;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Module03TP1
{
    class Program
    {
        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        static void Main(string[] args)
        {
            // init
            InitialiserDatas();

            // Q1
            Console.WriteLine("Question 1");
            foreach ( string auteurPrenoms in LinqQuestion1('G'))
            {
                Console.WriteLine(auteurPrenoms);
            }

            // Q2
            Console.WriteLine("\n Question 2");
            Console.WriteLine(LinqQuestion2().First().Nom);

            // Q3
            Console.WriteLine("\n Question 3");
            foreach (IGrouping<Auteur, Livre> auteur in LinqQuestion3())
            {
                Console.WriteLine("\n Auteur");
                Console.WriteLine(auteur.Key.Nom);
                IEnumerator<Livre> listLivres = auteur.GetEnumerator();
                Console.WriteLine("Ses livres");
                while (listLivres.MoveNext())
                {
                    Console.WriteLine(listLivres.Current.Titre);
                    Console.WriteLine("Nombre de pages");
                    Console.WriteLine(listLivres.Current.NbPages);
                }
                Console.WriteLine("Nombre de page moyen");
                Console.WriteLine(auteur.Average(auteur => auteur.NbPages));
            }

            //Q4
            Console.WriteLine("\n Question 4");
            Console.WriteLine(LinqQuestion4().First());

            //Q5
            Console.WriteLine("\n Question 5");
            Console.WriteLine("Les auteurs ont gagné");
            Console.WriteLine(LinqQuestion5());

            //Q6
            Console.WriteLine("\n Question 6");
            foreach (IGrouping<Auteur, Livre> auteur in LinqQuestion6())
            {
                Console.WriteLine("\n Auteur");
                Console.WriteLine(auteur.Key.Nom);
                IEnumerator<Livre> listLivres = auteur.GetEnumerator();
                Console.WriteLine("Ses livres");
                while (listLivres.MoveNext()){
                    Console.WriteLine(listLivres.Current.Titre);
                }
            }

            //Q7
            Console.WriteLine("\n Question 7");
            foreach (string titreLivre in LinqQuestion7())
            {
                Console.WriteLine(titreLivre);
            }

            //Q8
            Console.WriteLine("\n Question 8");
            foreach (Livre livre in LinqQuestion8())
            {
                Console.WriteLine(livre.Titre);
            }

            //Q9
            Console.WriteLine("\n Question 9");
            Console.WriteLine(LinqQuestion9().Nom);
        }



        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }

        private static IEnumerable<string> LinqQuestion1(char name)
        {
            return from auteur in ListeAuteurs 
                   where auteur.Nom[0].Equals(name) 
                   orderby auteur.Prenom 
                   select auteur.Prenom;
        }

        private static IEnumerable<Auteur> LinqQuestion2()
        {
           return from livres in ListeLivres
                  group livres by livres.Auteur into livresGroup
                  orderby livresGroup.Count() descending
                  select livresGroup.Key;
        }

        private static IEnumerable<IGrouping<Auteur, Livre>> LinqQuestion3()
        {
            return ListeLivres.GroupBy(l => l.Auteur);
        }

        private static IEnumerable<String> LinqQuestion4()
        {
            return from livres in ListeLivres
                   orderby livres.NbPages descending
                   select livres.Titre;
        }

        private static decimal LinqQuestion5()
        {
            return ListeAuteurs.Average(auteur => auteur.Factures.Sum(facture => facture.Montant));
        }

        private static IEnumerable<IGrouping<Auteur, Livre>> LinqQuestion6()
        {
            return from livres in ListeLivres
                   group livres by livres.Auteur into livresGroup
                   select livresGroup;
        }

        private static IEnumerable<string> LinqQuestion7()
        {
            return from livres in ListeLivres
                   orderby livres.Titre 
                   select livres.Titre;
        }

        private static IEnumerable<Livre> LinqQuestion8()
        {
            ListeLivres.Average(livre => livre.NbPages);
            return ListeLivres.Where(livre => livre.NbPages > ListeLivres.Average(livre => livre.NbPages));
        }

        private static Auteur LinqQuestion9()
        {
            return ListeLivres.GroupBy(livre => livre.Auteur).OrderBy(group => group.Count()).FirstOrDefault().Key;
        }
    }
}
