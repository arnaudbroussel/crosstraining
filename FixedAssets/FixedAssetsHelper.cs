using System;

namespace crosstraining.FixedAssets
{
    public static class FixedAssetsHelper
    {

        public static void AmortissementDegressif()
        {
            // Paramètres
            double valeurInitiale = 10000;
            int duree = 5;
            double tauxLineaire = 100.0 / duree;

            // Coefficients fiscaux standards
            double coefficient = duree >= 6 ? 2.25 : (duree >= 5 ? 1.75 : 1.25);
            double tauxDegressif = tauxLineaire * coefficient;

            double baseAmortissable = valeurInitiale;
            double amortissementCumule = 0;

            Console.WriteLine("Année | Base amortissable | Méthode      | Amortissement | Amort. cumulé | Valeur résiduelle");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");

            for (int annee = 1; annee <= duree; annee++)
            {
                double amortissementDegressif = baseAmortissable * tauxDegressif / 100;
                double valeurRestante = valeurInitiale - amortissementCumule;
                int dureeRestante = duree - annee + 1;
                double amortissementLineaire = valeurRestante / dureeRestante;

                double amortissement;
                string methode;

                if (amortissementLineaire > amortissementDegressif)
                {
                    amortissement = amortissementLineaire;
                    methode = "Linéaire";
                }
                else
                {
                    amortissement = amortissementDegressif;
                    methode = "Dégressif";
                }

                if (amortissement > baseAmortissable)
                    amortissement = baseAmortissable;

                amortissementCumule += amortissement;
                baseAmortissable -= amortissement;

                Console.WriteLine($"{annee,5} | {baseAmortissable + amortissement,17:F2} | {methode,-12} | {amortissement,14:F2} | {amortissementCumule,14:F2} | {baseAmortissable,18:F2}");
            }
        }
    }
}