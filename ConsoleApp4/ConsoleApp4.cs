using System;
using System.Collections.Generic;

namespace FordonApp
{
    // Enum för fordonstyp
    public enum FordonsTyp
    {
        Bil,
        MC
    }

    // Enum för parkeringsplatsstatus
    public enum ParkeringsStatus
    {
        Ledig,
        Upptagen
    }

    // Bas-klassen Fordon
    public abstract class Fordon
    {
        public string RegistreringsNummer { get; set; }
        public string Färg { get; set; }
        public FordonsTyp Typ { get; set; } // Ny egenskap för typ av fordon

        public Fordon(string registreringsNummer, string färg, FordonsTyp typ)
        {
            RegistreringsNummer = registreringsNummer;
            Färg = färg;
            Typ = typ;
        }

        public abstract void Starta();
    }

    // Bil-klassen som ärver från Fordon
    public class Bil : Fordon
    {
        public int AntalDörrar { get; set; }

        public Bil(string registreringsNummer, string färg, int antalDörrar)
            : base(registreringsNummer, färg, FordonsTyp.Bil) // Sätter typ till Bil
        {
            AntalDörrar = antalDörrar;
        }

        public override void Starta()
        {
            Console.WriteLine($"Bilen med registreringsnummer {RegistreringsNummer} startar.");
        }
    }

    // MC-klassen som ärver från Fordon
    public class MC : Fordon
    {
        public bool HarSidovagn { get; set; }

        public MC(string registreringsNummer, string färg, bool harSidovagn)
            : base(registreringsNummer, färg, FordonsTyp.MC) // Sätter typ till MC
        {
            HarSidovagn = harSidovagn;
        }

        public override void Starta()
        {
            Console.WriteLine($"MC:n med registreringsnummer {RegistreringsNummer} startar.");
        }
    }

    // Parkeringsplats-klassen
    public class ParkeringsPlats
    {
        public int PlatsNummer { get; set; }
        public Fordon UpptagenAv { get; private set; } = null;
        public ParkeringsStatus Status { get; private set; } = ParkeringsStatus.Ledig; // Ny status egenskap

        public ParkeringsPlats(int platsNummer)
        {
            PlatsNummer = platsNummer;
        }

        public bool ÄrLedig()
        {
            return Status == ParkeringsStatus.Ledig;
        }

        public void ParkeraFordon(Fordon fordon)
        {
            if (ÄrLedig())
            {
                UpptagenAv = fordon;
                Status = ParkeringsStatus.Upptagen; // Sätt status till Upptagen
                Console.WriteLine($"Fordon med registreringsnummer {fordon.RegistreringsNummer} parkerat på plats {PlatsNummer}.");
            }
            else
            {
                Console.WriteLine($"Plats {PlatsNummer} är redan upptagen.");
            }
        }

        public void TaBortFordon()
        {
            if (!ÄrLedig())
            {
                Console.WriteLine($"Fordon med registreringsnummer {UpptagenAv.RegistreringsNummer} har lämnat plats {PlatsNummer}.");
                UpptagenAv = null;
                Status = ParkeringsStatus.Ledig; // Återställ status till Ledig
            }
            else
            {
                Console.WriteLine($"Plats {PlatsNummer} är redan tom.");
            }
        }
    }

    // Parkeringshus-klassen
    public class ParkeringsHus
    {
        private List<ParkeringsPlats> parkeringsplatser = new List<ParkeringsPlats>();

        public ParkeringsHus(int antalPlatser)
        {
            for (int i = 1; i <= antalPlatser; i++)
            {
                parkeringsplatser.Add(new ParkeringsPlats(i));
            }
        }

        public void Parkera(Fordon fordon)
        {
            var ledigPlats = parkeringsplatser.Find(plats => plats.ÄrLedig());
            if (ledigPlats != null)
            {
                ledigPlats.ParkeraFordon(fordon);
            }
            else
            {
                Console.WriteLine("Inga lediga parkeringsplatser tillgängliga.");
            }
        }

        public void TaBortFordon(int platsNummer)
        {
            var plats = parkeringsplatser.Find(p => p.PlatsNummer == platsNummer);
            if (plats != null)
            {
                plats.TaBortFordon();
            }
            else
            {
                Console.WriteLine("Fel platsnummer.");
            }
        }
    }

    // Testar klasserna i Main-metoden
    class Program
    {
        static void Main(string[] args)
        {
            ParkeringsHus parkeringsHus = new ParkeringsHus(3);

            Bil bil1 = new Bil("ABC123", "Röd", 4);
            MC mc1 = new MC("XYZ789", "Svart", false);

            parkeringsHus.Parkera(bil1);
            parkeringsHus.Parkera(mc1);

            parkeringsHus.TaBortFordon(1);
            parkeringsHus.TaBortFordon(2);

            // Exempel på att använda FordonsTyp för att kontrollera typ
            if (bil1.Typ == FordonsTyp.Bil)
            {
                Console.WriteLine("Detta fordon är en bil.");
            }
        }
    }
}
