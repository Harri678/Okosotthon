using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosKozpontiRiaszto :
        OkosEszkoz, IIdozitheto, IHangjelzo, IStatisztika
    {
        private int maradtIdo;
        private int hangjelzesekSzama;
        private int veszjelzesekSzama;

        public OkosKozpontiRiaszto(string azonosito, string nev)
            : base(azonosito, nev)
        {
            this.maradtIdo = 0;
            this.hangjelzesekSzama = 0;
            this.veszjelzesekSzama = 0;
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            if (parancs == "RIASZTAS")
            {
                this.VeszjelzesInditasa();
            }
            else if (parancs == "CSIPOG")
            {
                this.Csipog(500);
            }
        }

        public override string AllapotJelentes()
        {
            return $"{this.Nev}: hátralévő idő: {this.maradtIdo}, hangjelzések: {this.hangjelzesekSzama}, vészjelzések: {this.veszjelzesekSzama}.";
        }

        protected override bool OnTesztFuttatasa()
        {
            return true;
        }

        public override void GyariBeallitasokVisszaallitasa()
        {
            base.GyariBeallitasokVisszaallitasa();

            this.maradtIdo = 0;
            this.hangjelzesekSzama = 0;
            this.veszjelzesekSzama = 0;
        }

        public void IdozitestBeallit(int perc)
        {
            this.maradtIdo = perc;
            this.UtolsoFrissites = DateTime.Now;
        }

        public int GetMaradtIdo()
        {
            return this.maradtIdo;
        }

        public void Csipog(int hosszu)
        {
            this.hangjelzesekSzama++;

            Console.WriteLine($"{this.Nev}: hangjelzés {hosszu}ido.");
        }

        public void VeszjelzesInditasa()
        {
            this.veszjelzesekSzama++;

            Console.WriteLine($"{this.Nev} vászjelzet indított!");
        }

        public string StatisztikatKeszit()
        {
            return $"{this.Nev}: hátralévő idő: {this.maradtIdo}, hangjelzések: {this.hangjelzesekSzama}, vészjelzések: {this.veszjelzesekSzama}.";
        }
    }
}
