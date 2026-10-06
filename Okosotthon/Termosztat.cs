using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class Termosztat : OkosEszkoz
    {
        private double jelenlegiHomerseklet;
        private double celHomerseklet;

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

                if (double.TryParse(ertek, out double homerseklet))
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
            return CelHomerseklet >= 5 && CelHomerseklet <= 35;
        }

    }
}
