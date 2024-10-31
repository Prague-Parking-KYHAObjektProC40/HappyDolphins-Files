using System;
using System.Collections.Generic;

namespace FordonApp
{
    // Enum för fordonsstorlek och typ
    public enum FordonsTyp
    {
        Bil,
        MC,
        Buss,
        Cykel
    }

    public enum ParkeringsStatus
    {
        Ledig,
        Upptagen
    }

    // Bas-klassen Fordon
    public abstract class Fordon
    {
        public string RegistreringsNummer { get; set; }
        public FordonsTyp Typ { get; set; }
        public int Storlek { get; set; } // Storleksegenskap

        public Fordon(string registreringsNummer, FordonsTyp typ, int storlek)
        {
            RegistreringsNummer = registreringsNummer;
            Typ = typ;
            Storlek = storlek;
        }

        public abstract void Starta();
    }

    // Bil-klass
    public class Bil : Fordon
    {
        public Bil(string registreringsNummer)
            : base(registreringsNummer,  FordonsTyp.Bil, 4) { }

        public override void Starta()
        {
            Console.WriteLine($"Bilen med registreringsnummer {RegistreringsNummer} startar.");
        }
    }

    // MC-klass
    public class MC : Fordon
    {
        public MC(string registreringsNummer)
            : base(registreringsNummer, FordonsTyp.MC, 2) { }

        public override void Starta()
        {
            Console.WriteLine($"MC:n med registreringsnummer {RegistreringsNummer} startar.");
        }
    }

    // Buss-klass
    public class Buss : Fordon
    {
        public Buss(string registreringsNummer)
            : base(registreringsNummer, FordonsTyp.Buss, 16) { }

        public override void Starta()
        {
            Console.WriteLine($"Bussen med registreringsnummer {RegistreringsNummer} startar.");
        }
    }

    // Cykel-klass
    public class Cykel : Fordon
    {
        public Cykel(string registreringsNummer)
            : base(registreringsNummer, FordonsTyp.Cykel, 1) { }

        public override void Starta()
        {
            Console.WriteLine($"Cykeln med registreringsnummer {RegistreringsNummer} startar.");
        }
    }

    // Parkeringsplats-klass
    public class ParkeringsPlats
    {
        public int PlatsNummer { get; set; }
        public int MaxStorlek { get; set; }
        public bool HarHogtTak { get; set; } // För bussar
        public int UpptagenStorlek { get; private set; } = 0; // Total upptagen storlek
        public List<Fordon> FordonPåPlats { get; private set; } = new List<Fordon>();

        public ParkeringsPlats(int platsNummer, bool harHogtTak)
        {
            PlatsNummer = platsNummer;
            MaxStorlek = 4; // Alla parkeringsplatser har standardstorlek för bilar
            HarHogtTak = harHogtTak;
        }

        public bool KanParkera(Fordon fordon)
        {
            // Kontrollera höjdkrav för bussar
            if (fordon.Typ == FordonsTyp.Buss && !HarHogtTak)
            {
                Console.WriteLine($"Bussar kan endast parkera på de första 50 platserna med högt tak.");
                return false;
            }
            // Kontrollera om platsen har tillräcklig ledig storlek
            return (UpptagenStorlek + fordon.Storlek <= MaxStorlek);
        }

        public void ParkeraFordon(Fordon fordon)
        {
            if (KanParkera(fordon))
            {
                FordonPåPlats.Add(fordon);
                UpptagenStorlek += fordon.Storlek;
                Console.WriteLine($"Fordon med registreringsnummer {fordon.RegistreringsNummer} parkerat på plats {PlatsNummer}.");
            }
            else
            {
                Console.WriteLine($"Plats {PlatsNummer} har inte tillräckligt med utrymme eller höjd för detta fordon.");
            }
        }

        public void TaBortFordon(Fordon fordon)
        {
            if (FordonPåPlats.Remove(fordon))
            {
                UpptagenStorlek -= fordon.Storlek;
                Console.WriteLine($"Fordon med registreringsnummer {fordon.RegistreringsNummer} har lämnat plats {PlatsNummer}.");
            }
        }
    }

    // Parkeringshus-klass
    public class ParkeringsHus
    {
        private List<ParkeringsPlats> parkeringsplatser = new List<ParkeringsPlats>();

        public ParkeringsHus(int totalPlatser)
        {
            for (int i = 1; i <= totalPlatser; i++)
            {
                bool harHogtTak = i <= 50; // Endast de första 50 platserna har högt tak för bussar
                parkeringsplatser.Add(new ParkeringsPlats(i, harHogtTak));
            }
        }

        public void Parkera(Fordon fordon)
        {
            var lämpligPlats = parkeringsplatser.Find(plats => plats.KanParkera(fordon));
            if (lämpligPlats != null)
            {
                lämpligPlats.ParkeraFordon(fordon);
            }
            else
            {
                Console.WriteLine("Inga lediga parkeringsplatser med tillräckligt utrymme tillgängliga.");
            }
        }

        public void TaBortFordon(string registreringsNummer)
        {
            foreach (var plats in parkeringsplatser)
            {
                var fordon = plats.FordonPåPlats.Find(f => f.RegistreringsNummer == registreringsNummer);
                if (fordon != null)
                {
                    plats.TaBortFordon(fordon);
                    return;
                }
            }
            Console.WriteLine("Fordonet hittades inte på någon parkeringsplats.");
        }
    }

    // Testar klasserna i Main-metoden
    class Program
    {
        static void Main(string[] args)
        {
            ParkeringsHus parkeringsHus = new ParkeringsHus(100);

            Bil bil1 = new Bil("ABC123");
            MC mc1 = new MC("XYZ789");
            Buss buss1 = new Buss("BUS001");
            Cykel cykel1 = new Cykel("CYC101");

            parkeringsHus.Parkera(bil1);
            parkeringsHus.Parkera(mc1);
            parkeringsHus.Parkera(buss1);
            parkeringsHus.Parkera(cykel1);

            parkeringsHus.TaBortFordon("ABC123");
            parkeringsHus.TaBortFordon("BUS001");
        }
    }
}

