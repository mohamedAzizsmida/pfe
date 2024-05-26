using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de Portefeuille
/// </summary>
public class Portefeuille
{
        public int IdAdherent { get; set; }
        public int id_titre{ get; set; }
        public int Quantite_Titre { get; set; }
        public double Cmp { get; set; }
        public double Montant { get; set; }

       /* public List<Titre> ConsulterPortefeuille()
        {
            // Implementation for consulting the portfolio
            return new List<Titre>();
        }*/
    
}

