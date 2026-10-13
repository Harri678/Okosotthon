using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Okosotthon
{
    public class Termosztat : OkosEszkoz, IIdozitheto, IStatisztika
    {
        private double jelenlegiHomerseklet;
        private double celHomerseklet;
        private DateTime? idozitoVege;

        public double JelenlegiHomerseklet { get => jelenlegiHomerseklet; private set => jelenlegiHomerseklet = value; }
        public double CelHomerseklet { get => celHomerseklet; private set => celHomerseklet = value; }

        public Termosztat(string azonosito, string nev, double celHomerseklet)
        : base(azonosito, nev)
        {
            this.CelHomerseklet = celHomerseklet;
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            if (parancs.StartsWith("BEALLIT_HOMERSEKLET:"))
            {
                string ertek = parancs.Substring("BEALLIT_HOMERSEKLET:".Length);

                if (double.TryParse(
                    ertek,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double homerseklet))
                {
                    this.CelHomerseklet = homerseklet;
                }
            }
        }

        public override string AllapotJelentes()
        {
            return $"Jelenlegi homerseklet: {JelenlegiHomerseklet}, celhomerseklet: {CelHomerseklet}";
        }

        protected override bool OnTesztFuttatasa()
        {

            return (CelHomerseklet > 5 && CelHomerseklet < 35);
        }

        public void IdozitestBeallit(int perc)
        {

            this.idozitoVege = DateTime.Now.AddMinutes(perc);
            this.UtolsoFrissites = DateTime.Now;
        }

        public int GetMaradtIdo()
        {
            if (this.idozitoVege == null)
                return 0;

            TimeSpan maradek = this.idozitoVege.Value - DateTime.Now;

            if (maradek <= TimeSpan.Zero)
                return 0;

            return (int)Math.Ceiling(maradek.TotalMinutes);
        }

        public string StatisztikatKeszit()
        {
            return $"Termosztát: {this.Nev}, jelenlegi hőmérséklet: {this.JelenlegiHomerseklet} °C, célhőmérséklet: {this.CelHomerseklet} °C, hátralévő idő: {this.GetMaradtIdo()} perc.";
        }
    }
}
