using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    public class V6AccountingContext : DbContext, IV6AccountingContext, IObjectContextAdapter
    {
        public V6AccountingContext()
            : base("name=V6AccountingContext")
        {
            Database.SetInitializer<V6AccountingContext>(null);
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<AD81> AD81Set { get; set; }
        public DbSet<Alct> DMAlct { get; set; }
        public DbSet<Alkh> AlkhSet { get; set; }
        public DbSet<V6option> V6OptionSet { get; set; }
        public DbSet<Alnt> DMAlnt { get; set; }
        public DbSet<ALpost> DMALpost { get; set; }
        public DbSet<ALvt> ALvtSet { get; set; }
        public DbSet<AM81> AM81Set { get; set; }
        public DbSet<Albp> DMAlbp { get; set; }
        public DbSet<Almagia> DMAlmagia { get; set; }
        #region temporarily unused property

        //public DbSet<Abbpkh> DMAbbpkh { get; set; }
        //public DbSet<Abhd> DMAbhd { get; set; }
        //public DbSet<Abhdkh> DMAbhdkh { get; set; }
        //public DbSet<ABkh> DMABkh { get; set; }
        //public DbSet<ABKHDS> DMABKHDS { get; set; }
        //public DbSet<Abkhovvkh> DMAbkhovvkh { get; set; }
        //public DbSet<Abku> DMAbku { get; set; }
        //public DbSet<ABlkct> DMABlkct { get; set; }
        //public DbSet<ABlo> DMABlo { get; set; }
        //public DbSet<ABntxt> DMABntxt { get; set; }
        //public DbSet<Abphi> DMAbphi { get; set; }
        //public DbSet<ABSPDD> DMABSPDD { get; set; }
        //public DbSet<ABSPYTCP> DMABSPYTCP { get; set; }
        //public DbSet<AbTD> DMAbTD { get; set; }
        //public DbSet<AbTD2> DMAbTD2 { get; set; }
        //public DbSet<AbTD3> DMAbTD3 { get; set; }
        //public DbSet<ABtk> DMABtk { get; set; }
        //public DbSet<ABtknt> DMABtknt { get; set; }
        //public DbSet<ABvitri> DMABvitri { get; set; }
        //public DbSet<ABvt> DMABvt { get; set; }
        //public DbSet<ABvt13> DMABvt13 { get; set; }
        //public DbSet<ABvv> DMABvv { get; set; }
        //public DbSet<Abvvkh> DMAbvvkh { get; set; }
        //public DbSet<ACKU> DMACKU { get; set; }
        //public DbSet<ACvv> DMACvv { get; set; }
        //public DbSet<AD11> DMAD11 { get; set; }
        //public DbSet<AD21> DMAD21 { get; set; }
        //public DbSet<AD29> DMAD29 { get; set; }
        //public DbSet<AD31> DMAD31 { get; set; }
        //public DbSet<AD32> DMAD32 { get; set; }
        //public DbSet<AD39> DMAD39 { get; set; }
        //public DbSet<AD41> DMAD41 { get; set; }
        //public DbSet<AD42> DMAD42 { get; set; }
        //public DbSet<AD46> DMAD46 { get; set; }
        //public DbSet<AD47> DMAD47 { get; set; }
        //public DbSet<AD51> DMAD51 { get; set; }
        //public DbSet<AD52> DMAD52 { get; set; }
        //public DbSet<AD56> DMAD56 { get; set; }
        //public DbSet<AD57> DMAD57 { get; set; }
        //public DbSet<AD71> DMAD71 { get; set; }
        //public DbSet<AD72> DMAD72 { get; set; }
        //public DbSet<AD73> DMAD73 { get; set; }
        //public DbSet<AD74> DMAD74 { get; set; }
        //public DbSet<AD76> DMAD76 { get; set; }

        //public DbSet<AD81CT> DMAD81CT { get; set; }
        //public DbSet<AD81CT0> DMAD81CT0 { get; set; }
        //public DbSet<AD84> DMAD84 { get; set; }
        //public DbSet<AD85> DMAD85 { get; set; }
        //public DbSet<AD86> DMAD86 { get; set; }
        //public DbSet<AD91> DMAD91 { get; set; }
        //public DbSet<AD92> DMAD92 { get; set; }
        //public DbSet<ADALCC> DMADALCC { get; set; }
        //public DbSet<ADALTS> DMADALTS { get; set; }
        //public DbSet<ADBPCC> DMADBPCC { get; set; }
        //public DbSet<ADBPTS> DMADBPTS { get; set; }
        //public DbSet<ADCTCC> DMADCTCC { get; set; }
        //public DbSet<ADCTCCBP> DMADCTCCBP { get; set; }
        //public DbSet<ADCTTS> DMADCTTS { get; set; }
        //public DbSet<ADCTTSBP> DMADCTTSBP { get; set; }
        //public DbSet<ADHSCC> DMADHSCC { get; set; }
        //public DbSet<ADHSTS> DMADHSTS { get; set; }
        //public DbSet<ADKHTS> DMADKHTS { get; set; }
        //public DbSet<ADPBCC> DMADPBCC { get; set; }
        //public DbSet<ADSLCC> DMADSLCC { get; set; }
        //public DbSet<ADSLTS> DMADSLTS { get; set; }
        //public DbSet<ADThue43> DMADThue43 { get; set; }
        //public DbSet<Agltc1> DMAgltc1 { get; set; }
        //public DbSet<Agltc2> DMAgltc2 { get; set; }
        //public DbSet<Agltc3> DMAgltc3 { get; set; }
        //public DbSet<Agltc4> DMAgltc4 { get; set; }
        //public DbSet<Agltc5> DMAgltc5 { get; set; }
        //public DbSet<Agltc6> DMAgltc6 { get; set; }
        //public DbSet<Agltc8> DMAgltc8 { get; set; }
        //public DbSet<AKhungCK> DMAKhungCK { get; set; }
        //public DbSet<AKhungCkCt> DMAKhungCkCt { get; set; }
        
        //public DbSet<Albpcc> DMAlbpcc { get; set; }
        //public DbSet<Albpht> DMAlbpht { get; set; }
        //public DbSet<Albpts> DMAlbpts { get; set; }
        //public DbSet<Alcc> DMAlcc { get; set; }
        //public DbSet<Alck> DMAlck { get; set; }
        //public DbSet<ALck2> DMALck2 { get; set; }
        //public DbSet<ALCKM> DMALCKM { get; set; }
        //public DbSet<ALCKMCt> DMALCKMCt { get; set; }
        //public DbSet<Alcltg> DMAlcltg { get; set; }
        //     public DbSet<Alct1> DMAlct1 { get; set; }

        //public DbSet<Alctct> DMAlctct { get; set; }
        //public DbSet<Alcthd> DMAlcthd { get; set; }
        //public DbSet<Aldmpbct> DMAldmpbct { get; set; }
        //public DbSet<Aldmpbph> DMAldmpbph { get; set; }
        //public DbSet<Aldmvt> DMAldmvt { get; set; }
        //public DbSet<Aldmvtct> DMAldmvtct { get; set; }
        //public DbSet<Aldvcs> DMAldvcs { get; set; }
        //public DbSet<Aldvt> DMAldvt { get; set; }
        //public DbSet<ALgia> DMALgia { get; set; }
        //public DbSet<ALgia2> DMALgia2 { get; set; }
        //public DbSet<ALGIA200> DMALGIA200 { get; set; }
        //public DbSet<ALgiavon> DMALgiavon { get; set; }
        //public DbSet<ALgiavon3> DMALgiavon3 { get; set; }
        //public DbSet<ALgiavv> DMALgiavv { get; set; }
        //public DbSet<Alhd> DMAlhd { get; set; }
        //public DbSet<Alhttt> DMAlhttt { get; set; }
        //public DbSet<Alhtvc> DMAlhtvc { get; set; }
        //public DbSet<ALkc> DMALkc { get; set; }

        //public DbSet<Alkho> DMAlkho { get; set; }
        //public DbSet<AlkhTG> DMAlkhTG { get; set; }
        //public DbSet<ALKMB> DMALKMB { get; set; }
        //public DbSet<ALKMBCt> DMALKMBCt { get; set; }
        //public DbSet<ALKMM> DMALKMM { get; set; }
        //public DbSet<ALKMMCt> DMALKMMCt { get; set; }
        //public DbSet<Alku> DMAlku { get; set; }
        //public DbSet<Allnx> DMAllnx { get; set; }
        //public DbSet<Allo> DMAllo { get; set; }
        //public DbSet<ALloaicc> DMALloaicc { get; set; }
        //public DbSet<Alloaick> DMAlloaick { get; set; }
        //public DbSet<Alloaivc> DMAlloaivc { get; set; }
        //public DbSet<ALloaivt> DMALloaivt { get; set; }
        //public DbSet<Almagd> DMAlmagd { get; set; }
        
        //public DbSet<ALMAUHD> DMALMAUHD { get; set; }
        //public DbSet<ALnhCC> DMALnhCC { get; set; }
        //public DbSet<ALnhdvcs> DMALnhdvcs { get; set; }
        //public DbSet<ALnhhd> DMALnhhd { get; set; }
        //public DbSet<ALnhkh> DMALnhkh { get; set; }
        //public DbSet<Alnhkh2> DMAlnhkh2 { get; set; }
        //public DbSet<Alnhku> DMAlnhku { get; set; }
        //public DbSet<Alnhphi> DMAlnhphi { get; set; }
        //public DbSet<ALnhtk> DMALnhtk { get; set; }
        //public DbSet<ALnhtk0> DMALnhtk0 { get; set; }
        //public DbSet<ALnhts> DMALnhts { get; set; }
        //public DbSet<ALnhvt> DMALnhvt { get; set; }
        //public DbSet<Alnhvt2> DMAlnhvt2 { get; set; }
        //public DbSet<Alnhvv> DMAlnhvv { get; set; }
        //public DbSet<ALnhytcp> DMALnhytcp { get; set; }
        //public DbSet<ALnk> DMALnk { get; set; }
        //public DbSet<Alnv> DMAlnv { get; set; }
        //public DbSet<Alnvien> DMAlnvien { get; set; }
        //public DbSet<ALpb> DMALpb { get; set; }
        //public DbSet<ALpb1> DMALpb1 { get; set; }
        //public DbSet<Alphi> DMAlphi { get; set; }
        //public DbSet<Alphuong> DMAlphuong { get; set; }
        //public DbSet<ALplcc> DMALplcc { get; set; }
        //public DbSet<ALplts> DMALplts { get; set; }
        //public DbSet<ALqddvt> DMALqddvt { get; set; }
        //public DbSet<Alqg> DMAlqg { get; set; }
        //public DbSet<Alql> DMAlql { get; set; }
        //public DbSet<Alquan> DMAlquan { get; set; }
        //public DbSet<ALstt> DMALstt { get; set; }
        //public DbSet<Altd> DMAltd { get; set; }
        //public DbSet<Altd2> DMAltd2 { get; set; }
        //public DbSet<Altd3> DMAltd3 { get; set; }
        //public DbSet<ALtgcc> DMALtgcc { get; set; }
        //public DbSet<ALtgnt> DMALtgnt { get; set; }
        //public DbSet<ALtgts> DMALtgts { get; set; }
        //public DbSet<ALTHAU> DMALTHAU { get; set; }
        //public DbSet<ALTHAUCT> DMALTHAUCT { get; set; }
        //public DbSet<Althue> DMAlthue { get; set; }
        //public DbSet<Altinh> DMAltinh { get; set; }
        //public DbSet<Altk0> DMAltk0 { get; set; }
        //public DbSet<Altk1> DMAltk1 { get; set; }
        //public DbSet<Altk2> DMAltk2 { get; set; }
        //public DbSet<ALtklkKU> DMALtklkKU { get; set; }
        //public DbSet<ALtklkvv> DMALtklkvv { get; set; }
        //public DbSet<ALtknh> DMALtknh { get; set; }
        //public DbSet<Alts> DMAlts { get; set; }
        //public DbSet<Altt> DMAltt { get; set; }
        //public DbSet<ALttvt> DMALttvt { get; set; }
        //public DbSet<Alvc> DMAlvc { get; set; }
        //public DbSet<ALvitri> DMALvitri { get; set; }
        //public DbSet<ALvttg> DMALvttg { get; set; }
        //public DbSet<Alvv> DMAlvv { get; set; }
        //public DbSet<Alytcp> DMAlytcp { get; set; }
        //public DbSet<AM11> DMAM11 { get; set; }
        //public DbSet<AM21> DMAM21 { get; set; }
        //public DbSet<AM29> DMAM29 { get; set; }
        //public DbSet<AM31> DMAM31 { get; set; }
        //public DbSet<AM32> DMAM32 { get; set; }
        //public DbSet<AM39> DMAM39 { get; set; }
        //public DbSet<AM41> DMAM41 { get; set; }
        //public DbSet<AM42> DMAM42 { get; set; }
        //public DbSet<AM46> DMAM46 { get; set; }
        //public DbSet<AM47> DMAM47 { get; set; }
        //public DbSet<AM51> DMAM51 { get; set; }
        //public DbSet<AM52> DMAM52 { get; set; }
        //public DbSet<AM56> DMAM56 { get; set; }
        //public DbSet<AM57> DMAM57 { get; set; }
        //public DbSet<AM71> DMAM71 { get; set; }
        //public DbSet<AM72> DMAM72 { get; set; }
        //public DbSet<AM73> DMAM73 { get; set; }
        //public DbSet<AM74> DMAM74 { get; set; }
        //public DbSet<AM76> DMAM76 { get; set; }
        //public DbSet<AM84> DMAM84 { get; set; }
        //public DbSet<AM85> DMAM85 { get; set; }
        //public DbSet<AM86> DMAM86 { get; set; }
        //public DbSet<AM91> DMAM91 { get; set; }
        //public DbSet<AM92> DMAM92 { get; set; }
        //public DbSet<ARA00> DMARA00 { get; set; }
        //public DbSet<ARBCGT> DMARBCGT { get; set; }
        //public DbSet<ARctgs> DMARctgs { get; set; }
        //public DbSet<ARctgs01> DMARctgs01 { get; set; }
        //public DbSet<ARI70> DMARI70 { get; set; }
        //public DbSet<ARS20> DMARS20 { get; set; }
        //public DbSet<ARS21> DMARS21 { get; set; }
        //public DbSet<ARS30> DMARS30 { get; set; }
        //public DbSet<ARS31> DMARS31 { get; set; }
        //public DbSet<ARS90> DMARS90 { get; set; }
        //public DbSet<ARV20> DMARV20 { get; set; }
        //public DbSet<ARV30> DMARV30 { get; set; }
        //public DbSet<CorpLan> DMCorpLan { get; set; }
        //public DbSet<CorpLan1> DMCorpLan1 { get; set; }
        //public DbSet<CorpLan2> DMCorpLan2 { get; set; }
        //public DbSet<CorpLang> DMCorpLang { get; set; }
        //public DbSet<Corpuser> DMCorpuser { get; set; }
        //public DbSet<D_AD11> DMD_AD11 { get; set; }
        //public DbSet<D_AD21> DMD_AD21 { get; set; }
        //public DbSet<D_AD29> DMD_AD29 { get; set; }
        //public DbSet<D_AD31> DMD_AD31 { get; set; }
        //public DbSet<D_AD32> DMD_AD32 { get; set; }
        //public DbSet<D_AD39> DMD_AD39 { get; set; }
        //public DbSet<D_AD41> DMD_AD41 { get; set; }
        //public DbSet<D_AD42> DMD_AD42 { get; set; }
        //public DbSet<D_AD46> DMD_AD46 { get; set; }
        //public DbSet<D_AD47> DMD_AD47 { get; set; }
        //public DbSet<D_AD51> DMD_AD51 { get; set; }
        //public DbSet<D_AD52> DMD_AD52 { get; set; }
        //public DbSet<D_AD56> DMD_AD56 { get; set; }
        //public DbSet<D_AD57> DMD_AD57 { get; set; }
        //public DbSet<D_AD71> DMD_AD71 { get; set; }
        //public DbSet<D_AD72> DMD_AD72 { get; set; }
        //public DbSet<D_AD73> DMD_AD73 { get; set; }
        //public DbSet<D_AD74> DMD_AD74 { get; set; }
        //public DbSet<D_AD76> DMD_AD76 { get; set; }
        //public DbSet<D_AD81> DMD_AD81 { get; set; }
        //public DbSet<D_AD84> DMD_AD84 { get; set; }
        //public DbSet<D_AD85> DMD_AD85 { get; set; }
        //public DbSet<D_AD86> DMD_AD86 { get; set; }
        //public DbSet<D_AD91> DMD_AD91 { get; set; }
        //public DbSet<D_AD92> DMD_AD92 { get; set; }
        //public DbSet<D_AM11> DMD_AM11 { get; set; }
        //public DbSet<D_AM21> DMD_AM21 { get; set; }
        //public DbSet<D_AM29> DMD_AM29 { get; set; }
        //public DbSet<D_AM31> DMD_AM31 { get; set; }
        //public DbSet<D_AM32> DMD_AM32 { get; set; }
        //public DbSet<D_AM39> DMD_AM39 { get; set; }
        //public DbSet<D_AM41> DMD_AM41 { get; set; }
        //public DbSet<D_AM42> DMD_AM42 { get; set; }
        //public DbSet<D_AM46> DMD_AM46 { get; set; }
        //public DbSet<D_AM47> DMD_AM47 { get; set; }
        //public DbSet<D_AM51> DMD_AM51 { get; set; }
        //public DbSet<D_AM52> DMD_AM52 { get; set; }
        //public DbSet<D_AM56> DMD_AM56 { get; set; }
        //public DbSet<D_AM57> DMD_AM57 { get; set; }
        //public DbSet<D_AM71> DMD_AM71 { get; set; }
        //public DbSet<D_AM72> DMD_AM72 { get; set; }
        //public DbSet<D_AM73> DMD_AM73 { get; set; }
        //public DbSet<D_AM74> DMD_AM74 { get; set; }
        //public DbSet<D_AM76> DMD_AM76 { get; set; }
        //public DbSet<D_AM81> DMD_AM81 { get; set; }
        //public DbSet<D_AM84> DMD_AM84 { get; set; }
        //public DbSet<D_AM85> DMD_AM85 { get; set; }
        //public DbSet<D_AM86> DMD_AM86 { get; set; }
        //public DbSet<D_AM91> DMD_AM91 { get; set; }
        //public DbSet<D_AM92> DMD_AM92 { get; set; }
        //public DbSet<dtproperties> DMdtproperties { get; set; }
        //public DbSet<v6bak> DMv6bak { get; set; }
        //public DbSet<V6funny> DMV6funny { get; set; }
        //public DbSet<V6Lookup> DMV6Lookup { get; set; }
        //public DbSet<V6Menu> DMV6Menu { get; set; }
        //public DbSet<V6option> DMV6option { get; set; }
        //public DbSet<V6rights> DMV6rights { get; set; }
        //public DbSet<V6User> DMV6user { get; set; }
        //public DbSet<VCOMMENT> DMVCOMMENT { get; set; }
        #endregion
    }
}
