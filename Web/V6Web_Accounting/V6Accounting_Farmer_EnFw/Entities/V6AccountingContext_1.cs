namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using global::System;
    using global::System.Data.Entity;
    using global::System.ComponentModel.DataAnnotations.Schema;
    using global::System.Linq;

    public partial class V6AccountingContext_1 : DbContext
    {
        public V6AccountingContext_1()
            : base("name=V6AccountingContext")
        {
        }

        public virtual DbSet<Abbpkh> Abbpkhs { get; set; }
        public virtual DbSet<Abhd> Abhds { get; set; }
        public virtual DbSet<Abhdkh> Abhdkhs { get; set; }
        public virtual DbSet<Abkhovvkh> Abkhovvkhs { get; set; }
        public virtual DbSet<Abku> Abkus { get; set; }
        public virtual DbSet<Abphi> Abphis { get; set; }
        public virtual DbSet<AbTD> AbTDs { get; set; }
        public virtual DbSet<AbTD2> AbTD2 { get; set; }
        public virtual DbSet<AbTD3> AbTD3 { get; set; }
        public virtual DbSet<ABvitri> ABvitris { get; set; }
        public virtual DbSet<Abvvkh> Abvvkhs { get; set; }
        public virtual DbSet<ADBPCC> ADBPCCs { get; set; }
        public virtual DbSet<ADBPT> ADBPTS { get; set; }
        public virtual DbSet<ADCTCC> ADCTCCs { get; set; }
        public virtual DbSet<ADCTT> ADCTTS { get; set; }
        public virtual DbSet<ADSLCC> ADSLCCs { get; set; }
        public virtual DbSet<ADSLT> ADSLTS { get; set; }
        public virtual DbSet<AKhungCK> AKhungCKs { get; set; }
        public virtual DbSet<Albp> Albps { get; set; }
        public virtual DbSet<Albpcc> Albpccs { get; set; }
        public virtual DbSet<Albpht> Albphts { get; set; }
        public virtual DbSet<Albpt> Albpts { get; set; }
        public virtual DbSet<Alcc> Alccs { get; set; }
        public virtual DbSet<Alck> Alcks { get; set; }
        public virtual DbSet<ALck2> ALck2 { get; set; }
        public virtual DbSet<ALCKM> ALCKMs { get; set; }
        public virtual DbSet<Alct> Alcts { get; set; }
        public virtual DbSet<Aldmpbct> Aldmpbcts { get; set; }
        public virtual DbSet<Aldmpbph> Aldmpbphs { get; set; }
        public virtual DbSet<Aldmvt> Aldmvts { get; set; }
        public virtual DbSet<Aldvc> Aldvcs { get; set; }
        public virtual DbSet<Aldvt> Aldvts { get; set; }
        public virtual DbSet<Alhd> Alhds { get; set; }
        public virtual DbSet<Alhttt> Alhttts { get; set; }
        public virtual DbSet<Alhtvc> Alhtvcs { get; set; }
        public virtual DbSet<Alkh> Alkhs { get; set; }
        public virtual DbSet<Alkho> Alkhoes { get; set; }
        public virtual DbSet<AlkhTG> AlkhTGs { get; set; }
        public virtual DbSet<ALKMB> ALKMBs { get; set; }
        public virtual DbSet<ALKMM> ALKMMs { get; set; }
        public virtual DbSet<Alku> Alkus { get; set; }
        public virtual DbSet<Allnx> Allnxes { get; set; }
        public virtual DbSet<Allo> Alloes { get; set; }
        public virtual DbSet<ALloaicc> ALloaiccs { get; set; }
        public virtual DbSet<Alloaick> Alloaicks { get; set; }
        public virtual DbSet<Alloaivc> Alloaivcs { get; set; }
        public virtual DbSet<ALloaivt> ALloaivts { get; set; }
        public virtual DbSet<Almagia> Almagias { get; set; }
        public virtual DbSet<ALMAUHD> ALMAUHDs { get; set; }
        public virtual DbSet<ALnhCC> ALnhCCs { get; set; }
        public virtual DbSet<ALnhhd> ALnhhds { get; set; }
        public virtual DbSet<Alnhkh2> Alnhkh2 { get; set; }
        public virtual DbSet<Alnhphi> Alnhphis { get; set; }
        public virtual DbSet<ALnhtk0> ALnhtk0 { get; set; }
        public virtual DbSet<ALnht> ALnhts { get; set; }
        public virtual DbSet<Alnhvt2> Alnhvt2 { get; set; }
        public virtual DbSet<ALnhytcp> ALnhytcps { get; set; }
        public virtual DbSet<ALnk> ALnks { get; set; }
        public virtual DbSet<Alnt> Alnts { get; set; }
        public virtual DbSet<Alnv> Alnvs { get; set; }
        public virtual DbSet<Alnvien> Alnviens { get; set; }
        public virtual DbSet<ALpb> ALpbs { get; set; }
        public virtual DbSet<Alphi> Alphis { get; set; }
        public virtual DbSet<Alphuong> Alphuongs { get; set; }
        public virtual DbSet<ALplcc> ALplccs { get; set; }
        public virtual DbSet<ALplt> ALplts { get; set; }
        public virtual DbSet<ALpost> ALposts { get; set; }
        public virtual DbSet<ALqddvt> ALqddvts { get; set; }
        public virtual DbSet<Alqg> Alqgs { get; set; }
        public virtual DbSet<Alquan> Alquans { get; set; }
        public virtual DbSet<Altd> Altds { get; set; }
        public virtual DbSet<Altd2> Altd2 { get; set; }
        public virtual DbSet<Altd3> Altd3 { get; set; }
        public virtual DbSet<ALtgcc> ALtgccs { get; set; }
        public virtual DbSet<ALtgt> ALtgts { get; set; }
        public virtual DbSet<ALTHAU> ALTHAUs { get; set; }
        public virtual DbSet<Althue> Althues { get; set; }
        public virtual DbSet<Altinh> Altinhs { get; set; }
        public virtual DbSet<Altk0> Altk0 { get; set; }
        public virtual DbSet<Altk1> Altk1 { get; set; }
        public virtual DbSet<ALtklkKU> ALtklkKUs { get; set; }
        public virtual DbSet<ALtklkvv> ALtklkvvs { get; set; }
        public virtual DbSet<ALtknh> ALtknhs { get; set; }
        public virtual DbSet<Alt> Alts { get; set; }
        public virtual DbSet<Altt> Altts { get; set; }
        public virtual DbSet<ALttvt> ALttvts { get; set; }
        public virtual DbSet<Alvc> Alvcs { get; set; }
        public virtual DbSet<ALvitri> ALvitris { get; set; }
        public virtual DbSet<ALvt> ALvts { get; set; }
        public virtual DbSet<ALvttg> ALvttgs { get; set; }
        public virtual DbSet<Alvv> Alvvs { get; set; }
        public virtual DbSet<Alytcp> Alytcps { get; set; }
        public virtual DbSet<AM11> AM11 { get; set; }
        public virtual DbSet<AM21> AM21 { get; set; }
        public virtual DbSet<AM29> AM29 { get; set; }
        public virtual DbSet<AM31> AM31 { get; set; }
        public virtual DbSet<AM32> AM32 { get; set; }
        public virtual DbSet<AM39> AM39 { get; set; }
        public virtual DbSet<AM41> AM41 { get; set; }
        public virtual DbSet<AM42> AM42 { get; set; }
        public virtual DbSet<AM46> AM46 { get; set; }
        public virtual DbSet<AM47> AM47 { get; set; }
        public virtual DbSet<AM51> AM51 { get; set; }
        public virtual DbSet<AM52> AM52 { get; set; }
        public virtual DbSet<AM56> AM56 { get; set; }
        public virtual DbSet<AM57> AM57 { get; set; }
        public virtual DbSet<AM71> AM71 { get; set; }
        public virtual DbSet<AM72> AM72 { get; set; }
        public virtual DbSet<AM73> AM73 { get; set; }
        public virtual DbSet<AM74> AM74 { get; set; }
        public virtual DbSet<AM76> AM76 { get; set; }
        public virtual DbSet<AM81> AM81 { get; set; }
        public virtual DbSet<AM84> AM84 { get; set; }
        public virtual DbSet<AM85> AM85 { get; set; }
        public virtual DbSet<AM86> AM86 { get; set; }
        public virtual DbSet<AM91> AM91 { get; set; }
        public virtual DbSet<AM92> AM92 { get; set; }
        public virtual DbSet<CorpLan> CorpLans { get; set; }
        public virtual DbSet<CorpLan1> CorpLan1 { get; set; }
        public virtual DbSet<CorpLan2> CorpLan2 { get; set; }
        public virtual DbSet<CorpLang> CorpLangs { get; set; }
        public virtual DbSet<Corpuser> Corpusers { get; set; }
        public virtual DbSet<dtproperty> dtproperties { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<ModelDefinition> ModelDefinitions { get; set; }
        public virtual DbSet<V6Lookup> V6Lookup { get; set; }
        public virtual DbSet<V6option> V6option { get; set; }
        public virtual DbSet<V6rights> V6rights { get; set; }
        public virtual DbSet<V6user> V6user { get; set; }
        public virtual DbSet<ABkh> ABkhs { get; set; }
        public virtual DbSet<ABlkct> ABlkcts { get; set; }
        public virtual DbSet<ABlo> ABloes { get; set; }
        public virtual DbSet<ABntxt> ABntxts { get; set; }
        public virtual DbSet<ABSPDD> ABSPDDs { get; set; }
        public virtual DbSet<ABtk> ABtks { get; set; }
        public virtual DbSet<ABtknt> ABtknts { get; set; }
        public virtual DbSet<ABvt> ABvts { get; set; }
        public virtual DbSet<ABvt13> ABvt13 { get; set; }
        public virtual DbSet<ABvv> ABvvs { get; set; }
        public virtual DbSet<ACKU> ACKUs { get; set; }
        public virtual DbSet<ACvv> ACvvs { get; set; }
        public virtual DbSet<AD11> AD11 { get; set; }
        public virtual DbSet<AD21> AD21 { get; set; }
        public virtual DbSet<AD29> AD29 { get; set; }
        public virtual DbSet<AD31> AD31 { get; set; }
        public virtual DbSet<AD32> AD32 { get; set; }
        public virtual DbSet<AD39> AD39 { get; set; }
        public virtual DbSet<AD41> AD41 { get; set; }
        public virtual DbSet<AD42> AD42 { get; set; }
        public virtual DbSet<AD46> AD46 { get; set; }
        public virtual DbSet<AD47> AD47 { get; set; }
        public virtual DbSet<AD51> AD51 { get; set; }
        public virtual DbSet<AD52> AD52 { get; set; }
        public virtual DbSet<AD56> AD56 { get; set; }
        public virtual DbSet<AD57> AD57 { get; set; }
        public virtual DbSet<AD71> AD71 { get; set; }
        public virtual DbSet<AD72> AD72 { get; set; }
        public virtual DbSet<AD73> AD73 { get; set; }
        public virtual DbSet<AD74> AD74 { get; set; }
        public virtual DbSet<AD76> AD76 { get; set; }
        public virtual DbSet<AD81> AD81 { get; set; }
        public virtual DbSet<AD81CT> AD81CT { get; set; }
        public virtual DbSet<AD81CT0> AD81CT0 { get; set; }
        public virtual DbSet<AD84> AD84 { get; set; }
        public virtual DbSet<AD85> AD85 { get; set; }
        public virtual DbSet<AD86> AD86 { get; set; }
        public virtual DbSet<AD91> AD91 { get; set; }
        public virtual DbSet<AD92> AD92 { get; set; }
        public virtual DbSet<ADALCC> ADALCCs { get; set; }
        public virtual DbSet<ADALT> ADALTS { get; set; }
        public virtual DbSet<ADCTCCBP> ADCTCCBPs { get; set; }
        public virtual DbSet<ADCTTSBP> ADCTTSBPs { get; set; }
        public virtual DbSet<ADHSCC> ADHSCCs { get; set; }
        public virtual DbSet<ADHST> ADHSTS { get; set; }
        public virtual DbSet<ADKHT> ADKHTS { get; set; }
        public virtual DbSet<ADPBCC> ADPBCCs { get; set; }
        public virtual DbSet<ADThue43> ADThue43 { get; set; }
        public virtual DbSet<Agltc1> Agltc1 { get; set; }
        public virtual DbSet<Agltc2> Agltc2 { get; set; }
        public virtual DbSet<Agltc3> Agltc3 { get; set; }
        public virtual DbSet<Agltc4> Agltc4 { get; set; }
        public virtual DbSet<Agltc5> Agltc5 { get; set; }
        public virtual DbSet<Agltc6> Agltc6 { get; set; }
        public virtual DbSet<Agltc8> Agltc8 { get; set; }
        public virtual DbSet<AKhungCkCt> AKhungCkCts { get; set; }
        public virtual DbSet<ALCKMCt> ALCKMCts { get; set; }
        public virtual DbSet<Alcltg> Alcltgs { get; set; }
        public virtual DbSet<Alctct> Alctcts { get; set; }
        public virtual DbSet<Alcthd> Alcthds { get; set; }
        public virtual DbSet<Aldmvtct> Aldmvtcts { get; set; }
        public virtual DbSet<ALgia> ALgias { get; set; }
        public virtual DbSet<ALgia2> ALgia2 { get; set; }
        public virtual DbSet<ALGIA200> ALGIA200 { get; set; }
        public virtual DbSet<ALgiavon> ALgiavons { get; set; }
        public virtual DbSet<ALgiavon3> ALgiavon3 { get; set; }
        public virtual DbSet<ALgiavv> ALgiavvs { get; set; }
        public virtual DbSet<ALkc> ALkcs { get; set; }
        public virtual DbSet<ALKMBCt> ALKMBCts { get; set; }
        public virtual DbSet<ALKMMCt> ALKMMCts { get; set; }
        public virtual DbSet<Almagd> Almagds { get; set; }
        public virtual DbSet<ALnhdvc> ALnhdvcs { get; set; }
        public virtual DbSet<ALnhkh> ALnhkhs { get; set; }
        public virtual DbSet<Alnhku> Alnhkus { get; set; }
        public virtual DbSet<ALnhtk> ALnhtks { get; set; }
        public virtual DbSet<ALnhvt> ALnhvts { get; set; }
        public virtual DbSet<Alnhvv> Alnhvvs { get; set; }
        public virtual DbSet<ALpb1> ALpb1 { get; set; }
        public virtual DbSet<Alql> Alqls { get; set; }
        public virtual DbSet<ALstt> ALstts { get; set; }
        public virtual DbSet<ALtgnt> ALtgnts { get; set; }
        public virtual DbSet<ALTHAUCT> ALTHAUCTs { get; set; }
        public virtual DbSet<Altk2> Altk2 { get; set; }
        public virtual DbSet<ARA00> ARA00 { get; set; }
        public virtual DbSet<ARBCGT> ARBCGTs { get; set; }
        public virtual DbSet<ARctg> ARctgs { get; set; }
        public virtual DbSet<ARctgs01> ARctgs01 { get; set; }
        public virtual DbSet<ARI70> ARI70 { get; set; }
        public virtual DbSet<ARS20> ARS20 { get; set; }
        public virtual DbSet<ARS21> ARS21 { get; set; }
        public virtual DbSet<ARS30> ARS30 { get; set; }
        public virtual DbSet<ARS31> ARS31 { get; set; }
        public virtual DbSet<ARS90> ARS90 { get; set; }
        public virtual DbSet<ARV20> ARV20 { get; set; }
        public virtual DbSet<ARV30> ARV30 { get; set; }
        public virtual DbSet<D_AD11> D_AD11 { get; set; }
        public virtual DbSet<D_AD21> D_AD21 { get; set; }
        public virtual DbSet<D_AD29> D_AD29 { get; set; }
        public virtual DbSet<D_AD31> D_AD31 { get; set; }
        public virtual DbSet<D_AD32> D_AD32 { get; set; }
        public virtual DbSet<D_AD39> D_AD39 { get; set; }
        public virtual DbSet<D_AD41> D_AD41 { get; set; }
        public virtual DbSet<D_AD46> D_AD46 { get; set; }
        public virtual DbSet<D_AD51> D_AD51 { get; set; }
        public virtual DbSet<D_AD56> D_AD56 { get; set; }
        public virtual DbSet<D_AD71> D_AD71 { get; set; }
        public virtual DbSet<D_AD72> D_AD72 { get; set; }
        public virtual DbSet<D_AD73> D_AD73 { get; set; }
        public virtual DbSet<D_AD74> D_AD74 { get; set; }
        public virtual DbSet<D_AD76> D_AD76 { get; set; }
        public virtual DbSet<D_AD81> D_AD81 { get; set; }
        public virtual DbSet<D_AD84> D_AD84 { get; set; }
        public virtual DbSet<D_AD85> D_AD85 { get; set; }
        public virtual DbSet<D_AD86> D_AD86 { get; set; }
        public virtual DbSet<D_AM76> D_AM76 { get; set; }
        public virtual DbSet<D_AM81> D_AM81 { get; set; }
        public virtual DbSet<v6bak> v6bak { get; set; }
        public virtual DbSet<V6Menu> V6Menu { get; set; }
        public virtual DbSet<VCOMMENT> VCOMMENTs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.du_no00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.du_co00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.ty_gia)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abbpkh>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.du_no00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.du_co00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<Abhd>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.ty_gia)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abhd>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.du_no00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.du_co00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.ty_gia)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abhdkh>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.du_no00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.du_co00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.ty_gia)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<Abkhovvkh>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abku>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Abku>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abku>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abku>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abku>()
                .Property(e => e.du_no00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abku>()
                .Property(e => e.du_co00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abku>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abku>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abku>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<Abku>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abku>()
                .Property(e => e.ty_gia)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Abku>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abku>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Abku>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abku>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<Abku>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.du_no00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.du_co00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<Abphi>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.ty_gia)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abphi>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.ma_TD)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.du_no00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.du_co00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<AbTD>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.ty_gia)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.ma_TD2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.du_no00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.du_co00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.ty_gia)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD2>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.ma_TD3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.du_no00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.du_co00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.ty_gia)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AbTD3>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ABvitri>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ABvitri>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvitri>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvitri>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvitri>()
                .Property(e => e.ton00)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABvitri>()
                .Property(e => e.du00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ABvitri>()
                .Property(e => e.du_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ABvitri>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<ABvitri>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvitri>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ABvitri>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvitri>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ABvitri>()
                .Property(e => e.Ma_Dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.du_no00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.du_co00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.ty_gia)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<Abvvkh>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.CC0)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.line_nbr)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.so_the_CC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.tk_CC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.tk_PB)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.tk_cp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADBPCC>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.Ts0)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.line_nbr)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.so_the_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.tk_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.tk_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.tk_cp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADBPT>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.CC0)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.so_the_CC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.ten_ptkt)
                .IsFixedLength();

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.dvt)
                .IsFixedLength();

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.gia_tri)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.line_nbr)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADCTCC>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.Ts0)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.so_the_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.ten_ptkt)
                .IsFixedLength();

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.dvt)
                .IsFixedLength();

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.so_luong)
                .HasPrecision(14, 3);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.gia_tri)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.line_nbr)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADCTT>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.so_the_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.sl_pb)
                .HasPrecision(14, 3);

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADSLCC>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.so_the_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.sl_kh)
                .HasPrecision(14, 3);

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADSLT>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<AKhungCK>()
                .Property(e => e.khung_id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AKhungCK>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AKhungCK>()
                .Property(e => e.t_sl1)
                .HasPrecision(13, 3);

            modelBuilder.Entity<AKhungCK>()
                .Property(e => e.t_sl2)
                .HasPrecision(13, 3);

            modelBuilder.Entity<AKhungCK>()
                .Property(e => e.t_tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AKhungCK>()
                .Property(e => e.t_tien2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AKhungCK>()
                .Property(e => e.ghi_chu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AKhungCK>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AKhungCK>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AKhungCK>()
                .Property(e => e.Ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albp>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albp>()
                .Property(e => e.ten_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albp>()
                .Property(e => e.ten_bp2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albp>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<Albp>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albp>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albp>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albp>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Albp>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Albp>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Albp>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Albp>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Albp>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Albp>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.ten_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.ten_bp2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Albpcc>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Albpht>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.ten_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.ten_bpht2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.tk621)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.tk622)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.tk623)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.tk627)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.tk154)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Albpht>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Albpht>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Albpht>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Albpht>()
                .Property(e => e.STT_TINH)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Albpt>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpt>()
                .Property(e => e.ten_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpt>()
                .Property(e => e.ten_bp2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpt>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpt>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpt>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Albpt>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Albpt>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Albpt>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Albpt>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Albpt>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Albpt>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Alcc>()
                .Property(e => e.so_the_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ten_cc)
                .IsFixedLength();

            modelBuilder.Entity<Alcc>()
                .Property(e => e.so_hieu_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ten_cc2)
                .IsFixedLength();

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.nuoc_sx)
                .IsFixedLength();

            modelBuilder.Entity<Alcc>()
                .Property(e => e.nam_sx)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.nh_cc1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.nh_cc2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.nh_cc3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.tinh_pb)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ma_tg_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.so_ky_pb)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.tien_cl)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.kieu_pb)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ty_le_pb)
                .HasPrecision(6, 4);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.tong_sl)
                .HasPrecision(20, 6);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.loai_pb)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.tk_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.tk_pb)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.tk_cp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.cong_suat)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.loai_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ts_kt)
                .IsFixedLength();

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ly_do_dc)
                .IsFixedLength();

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ma_giam_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ly_do_giam)
                .IsFixedLength();

            modelBuilder.Entity<Alcc>()
                .Property(e => e.so_luong)
                .HasPrecision(14, 3);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ghi_chu)
                .IsFixedLength();

            modelBuilder.Entity<Alcc>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Alcc>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Alcc>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Alcc>()
                .Property(e => e.ma_qg)
                .IsFixedLength();

            modelBuilder.Entity<Alcc>()
                .Property(e => e.loai_cc0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.thuoc_nhom)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcc>()
                .Property(e => e.trang_thai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.ma_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.ten_ck)
                .IsFixedLength();

            modelBuilder.Entity<Alck>()
                .Property(e => e.ten_ck2)
                .IsFixedLength();

            modelBuilder.Entity<Alck>()
                .Property(e => e.loai_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.muc_do)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Alck>()
                .Property(e => e.tien_yn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.tienh_yn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.cong_yn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.con_lai_yn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alck>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alck>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alck>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alck>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.ma_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.nh_kh9)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.nh_vt9)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.dvt)
                .IsFixedLength();

            modelBuilder.Entity<ALck2>()
                .Property(e => e.he_so)
                .HasPrecision(14, 3);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.so_luong)
                .HasPrecision(14, 3);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.tl_ck)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.tien_ck)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.ma_gia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALck2>()
                .Property(e => e.Tien)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.Ma_CK)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.Ten_CK)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.Ten_CK2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.Ma_CK0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.Ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.t_sl1)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.t_sl2)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.t_tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.t_tien2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.ghi_chu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKM>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.Module_id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.ma_phan_he)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.ten_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.ten_ct2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.ma_ct_me)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.so_ct)
                .HasPrecision(6, 0);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.tieu_de_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.tieu_de2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.so_lien)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Alct>()
                .Property(e => e.ma_ct_in)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.form)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_phdbf)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ctdbf)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_trung_so)
                .HasPrecision(2, 0);

            modelBuilder.Entity<Alct>()
                .Property(e => e.procedur)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ngay_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_sl_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_sl_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_sl_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ngay_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ngay_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.dk_ctgs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.ma_ct_old)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ph_old)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.m_k_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.Tk_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.Tk_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.M_MA_LNX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.M_HSD)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.SIZE_CT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.THEM_IN)
                .HasPrecision(2, 0);

            modelBuilder.Entity<Alct>()
                .Property(e => e.phandau)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.phancuoi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.dinhdang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.M_Ma_KMB)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.M_Ma_KMM)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.M_SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.M_MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alct>()
                .Property(e => e.F6BARCODE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.Stt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.TK_CO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.TK_NO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.he_so)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Aldmpbct>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Aldmpbph>()
                .Property(e => e.tag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbph>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbph>()
                .Property(e => e.tk_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbph>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbph>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbph>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbph>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmpbph>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldmpbph>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldmpbph>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldmpbph>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Aldmpbph>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Aldmpbph>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Aldmvt>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmvt>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmvt>()
                .Property(e => e.sl_sp)
                .HasPrecision(20, 6);

            modelBuilder.Entity<Aldmvt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmvt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmvt>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldmvt>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldmvt>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldmvt>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Aldmvt>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Aldmvt>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Aldvc>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvc>()
                .Property(e => e.ten_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvc>()
                .Property(e => e.ten_dvcs2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvc>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvc>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvc>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvc>()
                .Property(e => e.DIA_CHI)
                .IsFixedLength();

            modelBuilder.Entity<Aldvc>()
                .Property(e => e.DIA_CHI2)
                .IsFixedLength();

            modelBuilder.Entity<Aldvc>()
                .Property(e => e.DIEN_THOAI)
                .IsFixedLength();

            modelBuilder.Entity<Aldvc>()
                .Property(e => e.NH_DVCS1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvc>()
                .Property(e => e.NH_DVCS2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvc>()
                .Property(e => e.NH_DVCS3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.dvt)
                .IsFixedLength();

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.ten_dvt)
                .IsFixedLength();

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.ten_dvt2)
                .IsFixedLength();

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldvt>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.ten_hd)
                .IsFixedLength();

            modelBuilder.Entity<Alhd>()
                .Property(e => e.ten_hd2)
                .IsFixedLength();

            modelBuilder.Entity<Alhd>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.ma_nvbh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.loai_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.nh_hd1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.nh_hd2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.nh_hd3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.so_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.tien_gt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.tien_gt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.kl_kh)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.kl_th)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhd>()
                .Property(e => e.CT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.ten_httt)
                .IsFixedLength();

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.ten_httt2)
                .IsFixedLength();

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.han_tt)
                .HasPrecision(5, 0);

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Alhttt>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.ma_htvc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.ten_htvc)
                .IsFixedLength();

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.ten_htvc2)
                .IsFixedLength();

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alhtvc>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.ma_kh)
                .IsFixedLength();

            modelBuilder.Entity<Alkh>()
                .Property(e => e.ten_kh)
                .IsFixedLength();

            modelBuilder.Entity<Alkh>()
                .Property(e => e.ten_kh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.ma_so_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.dien_thoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.fax)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.e_mail)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.home_page)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.doi_tac)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.ten_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.ngan_hang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.nh_kh1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.nh_kh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.nh_kh3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.du_nt13)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.du13)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.t_tien_cn)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.t_tien_hd)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.Ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.Nh_kh9)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.Ma_snvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.TK_NH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.DT_DD)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.TT_SONHA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.MA_PHUONG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.MA_TINH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.MA_QUAN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkh>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.ten_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.ten_kho2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.tk_dl)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.thu_kho)
                .IsFixedLength();

            modelBuilder.Entity<Alkho>()
                .Property(e => e.dia_chi)
                .IsFixedLength();

            modelBuilder.Entity<Alkho>()
                .Property(e => e.dien_thoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.fax)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.email)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.ma_lotrinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.ma_vc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.ghi_chu)
                .IsFixedLength();

            modelBuilder.Entity<Alkho>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.date_yn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.lo_yn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alkho>()
                .Property(e => e.NH_DVCS1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.ma_KHTG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.ten_khtg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.ten_khtg2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.ma_so_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.dien_thoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.fax)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.e_mail)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.home_page)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.doi_tac)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.ten_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.ngan_hang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.nh_kh1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.nh_kh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.nh_kh3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.du_nt13)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.du13)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.t_tien_cn)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.t_tien_hd)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.Ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.Nh_kh9)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.Ma_snvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.Bar_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AlkhTG>()
                .Property(e => e.TK_NH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.Ma_km)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.Ten_km)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.Ten_km2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.Ma_km0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.Ma_kmm)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.Ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.t_sl1)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.t_sl2)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.t_tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.t_tien2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.ghi_chu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMB>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.Ma_km)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.Ten_km)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.Ten_km2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.Ma_km0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.Ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.t_sl1)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.t_sl2)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.t_tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.t_tien2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.ghi_chu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMM>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.ten_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.ten_ku2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.tien0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alku>()
                .Property(e => e.tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alku>()
                .Property(e => e.lai_suat1)
                .HasPrecision(7, 4);

            modelBuilder.Entity<Alku>()
                .Property(e => e.lai_suat2)
                .HasPrecision(7, 4);

            modelBuilder.Entity<Alku>()
                .Property(e => e.nh_ku1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.nh_ku2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.nh_ku3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alku>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alku>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alku>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.so_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alku>()
                .Property(e => e.Tk_kc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.ma_lnx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.ten_loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.ten_loai2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allnx>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allo>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allo>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allo>()
                .Property(e => e.ten_lo)
                .IsFixedLength();

            modelBuilder.Entity<Allo>()
                .Property(e => e.ten_lo2)
                .IsFixedLength();

            modelBuilder.Entity<Allo>()
                .Property(e => e.ma_vt2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allo>()
                .Property(e => e.sl_nhap)
                .HasPrecision(20, 6);

            modelBuilder.Entity<Allo>()
                .Property(e => e.sl_xuat)
                .HasPrecision(20, 6);

            modelBuilder.Entity<Allo>()
                .Property(e => e.ghi_chu)
                .IsFixedLength();

            modelBuilder.Entity<Allo>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allo>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allo>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allo>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Allo>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Allo>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Allo>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Allo>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Allo>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Allo>()
                .Property(e => e.SO_LOSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allo>()
                .Property(e => e.SO_LODK)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Allo>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.loai_cc0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.ten)
                .IsFixedLength();

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.ten2)
                .IsFixedLength();

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.ghi_chu)
                .IsFixedLength();

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaicc>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.ma_loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.ten_loai)
                .IsFixedLength();

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.ten_loai2)
                .IsFixedLength();

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.xtype)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.ghi_chu)
                .IsFixedLength();

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaick>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.ma_loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.ten_loai)
                .IsFixedLength();

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.ten_loai2)
                .IsFixedLength();

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.ghi_chu)
                .IsFixedLength();

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alloaivc>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.loai_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.ten_loai)
                .IsFixedLength();

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.ten_loai2)
                .IsFixedLength();

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.ghi_chu)
                .IsFixedLength();

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALloaivt>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagia>()
                .Property(e => e.ma_gia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagia>()
                .Property(e => e.ten_gia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagia>()
                .Property(e => e.ten_gia2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagia>()
                .Property(e => e.Loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagia>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagia>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagia>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALMAUHD>()
                .Property(e => e.ma_mauhd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALMAUHD>()
                .Property(e => e.ten_mauhd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALMAUHD>()
                .Property(e => e.ten_mauhd2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALMAUHD>()
                .Property(e => e.loai_mau)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALMAUHD>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALMAUHD>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALMAUHD>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALMAUHD>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALMAUHD>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALMAUHD>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALMAUHD>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ALMAUHD>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ALMAUHD>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ALnhCC>()
                .Property(e => e.loai_nh)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ALnhCC>()
                .Property(e => e.ma_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhCC>()
                .Property(e => e.ten_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhCC>()
                .Property(e => e.ten_nh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhCC>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhCC>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ALnhCC>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhCC>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhCC>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.ma_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.ten_nh)
                .IsFixedLength();

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.ten_nh2)
                .IsFixedLength();

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhhd>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.ma_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.ten_nh)
                .IsFixedLength();

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.ten_nh2)
                .IsFixedLength();

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhkh2>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.ma_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.ten_nh)
                .IsFixedLength();

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.ten_nh2)
                .IsFixedLength();

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhphi>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhtk0>()
                .Property(e => e.ma_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhtk0>()
                .Property(e => e.ten_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhtk0>()
                .Property(e => e.ten_nh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnht>()
                .Property(e => e.loai_nh)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ALnht>()
                .Property(e => e.ma_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnht>()
                .Property(e => e.ten_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnht>()
                .Property(e => e.ten_nh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnht>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnht>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ALnht>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnht>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnht>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.ma_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.ten_nh)
                .IsFixedLength();

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.ten_nh2)
                .IsFixedLength();

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvt2>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhytcp>()
                .Property(e => e.nhom)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhytcp>()
                .Property(e => e.ten_nhom)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhytcp>()
                .Property(e => e.ten_nhom2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhytcp>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhytcp>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ALnhytcp>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhytcp>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhytcp>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ALnk>()
                .Property(e => e.Ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnk>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnk>()
                .Property(e => e.ten_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnk>()
                .Property(e => e.ten_nk2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnk>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnk>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnk>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnt>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnt>()
                .Property(e => e.ten_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnt>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Alnt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnt>()
                .Property(e => e.ten_nt2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnt>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<Alnt>()
                .Property(e => e.tk_pscl_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnt>()
                .Property(e => e.tk_pscl_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnt>()
                .Property(e => e.tk_dgcl_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnt>()
                .Property(e => e.tk_dgcl_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnv>()
                .Property(e => e.ma_nv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnv>()
                .Property(e => e.ten_nv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnv>()
                .Property(e => e.ten_nv2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnv>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnv>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Alnv>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnv>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnv>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.ten_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.ten_nvien2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.Loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.han_tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Alnvien>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ALpb>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.tag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.ten_bt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.ty_gia)
                .HasPrecision(13, 4);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt_rec01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt_rec02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt_rec03)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt_rec04)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt_rec05)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt_rec06)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt_rec07)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt_rec08)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt_rec09)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt_rec10)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt_rec11)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.stt_rec12)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.so_ct01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.so_ct02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.so_ct03)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.so_ct04)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.so_ct05)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.so_ct06)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.so_ct07)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.so_ct08)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.so_ct09)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.so_ct10)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.so_ct11)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.so_ct12)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.Ten_loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.MA_BPHTPH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.AUTO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb>()
                .Property(e => e.LOAI_PBCP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.ten_phi)
                .IsFixedLength();

            modelBuilder.Entity<Alphi>()
                .Property(e => e.ten_phi2)
                .IsFixedLength();

            modelBuilder.Entity<Alphi>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.ma_nvbh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.nh_phi1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.nh_phi2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.nh_phi3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.so_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.tien_gt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.tien_gt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.kl_kh)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.kl_th)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphi>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.ma_phuong)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.ten_ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.ten_ph2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Alphuong>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.ma_loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.ten_loai)
                .IsFixedLength();

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.ten_loai2)
                .IsFixedLength();

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ALplcc>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ALplt>()
                .Property(e => e.ma_loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplt>()
                .Property(e => e.ten_loai)
                .IsFixedLength();

            modelBuilder.Entity<ALplt>()
                .Property(e => e.ten_loai2)
                .IsFixedLength();

            modelBuilder.Entity<ALplt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplt>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplt>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplt>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALplt>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALplt>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALplt>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALplt>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ALplt>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ALplt>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ALpost>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.ma_post)
                .IsUnicode(false);

            modelBuilder.Entity<ALpost>()
                .Property(e => e._default)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.ARI70)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.ARA00)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.time)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.user_id)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.ma_phan_he)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.Itemid)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.stt_in)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ALpost>()
                .Property(e => e.search)
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.dvtqd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.he_so)
                .HasPrecision(16, 6);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.xtype)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALqddvt>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alqg>()
                .Property(e => e.ma_qg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alqg>()
                .Property(e => e.ten_qg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alqg>()
                .Property(e => e.ten_qg2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alqg>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<Alqg>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alqg>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alqg>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alqg>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alqg>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alqg>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alqg>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Alqg>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Alqg>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Alquan>()
                .Property(e => e.ma_quan)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alquan>()
                .Property(e => e.ten_quan)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alquan>()
                .Property(e => e.ten_quan2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alquan>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<Alquan>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alquan>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alquan>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alquan>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alquan>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alquan>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alquan>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Alquan>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Alquan>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Alquan>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd>()
                .Property(e => e.ma_td)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd>()
                .Property(e => e.ten_td)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd>()
                .Property(e => e.ten_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd2>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd2>()
                .Property(e => e.ten_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd2>()
                .Property(e => e.ten_td22)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd2>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd2>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd2>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd2>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd2>()
                .Property(e => e.GC_TD1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd2>()
                .Property(e => e.GC_TD2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd2>()
                .Property(e => e.GC_TD3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd3>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd3>()
                .Property(e => e.ten_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd3>()
                .Property(e => e.ten_td32)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd3>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd3>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd3>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd3>()
                .Property(e => e.LOAI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd3>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd3>()
                .Property(e => e.GC_TD1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd3>()
                .Property(e => e.GC_TD2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altd3>()
                .Property(e => e.GC_TD3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.ma_tg_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.ten_tg_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.ten_tg_cc2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.loai_tg_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ALtgcc>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.ma_tg_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.ten_tg_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.ten_tg_ts2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.loai_tg_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ALtgt>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.Ma_THAU)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.Ten_THAU)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.Ten_THAU2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.Ma_THAU0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.Ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.t_sl1)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.t_sl2)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.t_tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.t_tien2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.ghi_chu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAU>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Althue>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Althue>()
                .Property(e => e.ten_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Althue>()
                .Property(e => e.ten_thue2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Althue>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Althue>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Althue>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Althue>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Althue>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Althue>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altinh>()
                .Property(e => e.ma_tinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altinh>()
                .Property(e => e.ten_tinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altinh>()
                .Property(e => e.ten_tinh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altinh>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<Altinh>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altinh>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altinh>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altinh>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Altinh>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Altinh>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Altinh>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Altinh>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Altinh>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Altinh>()
                .Property(e => e.LOAI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altinh>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.ten_tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.ten_tk2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.ten_ngan)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.ten_ngan2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.tk_me)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.nh_tk0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.nh_tk2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.loai_cl_no)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.loai_cl_co)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Altk0>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk1>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk1>()
                .Property(e => e.ten_tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk1>()
                .Property(e => e.ds_tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk1>()
                .Property(e => e.ten_tk2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk1>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk1>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtklkKU>()
                .Property(e => e.tk_lkKU)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtklkKU>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtklkKU>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtklkKU>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtklkvv>()
                .Property(e => e.tk_lkvv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtklkvv>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtklkvv>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtklkvv>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtknh>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtknh>()
                .Property(e => e.tknh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtknh>()
                .Property(e => e.ten_tknh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtknh>()
                .Property(e => e.ten_tknh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtknh>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtknh>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtknh>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtknh>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.so_the_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.ten_ts)
                .IsFixedLength();

            modelBuilder.Entity<Alt>()
                .Property(e => e.so_hieu_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.ten_ts2)
                .IsFixedLength();

            modelBuilder.Entity<Alt>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.nuoc_sx)
                .IsFixedLength();

            modelBuilder.Entity<Alt>()
                .Property(e => e.nam_sx)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Alt>()
                .Property(e => e.nh_ts1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.nh_ts2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.nh_ts3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.tinh_kh)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Alt>()
                .Property(e => e.ma_tg_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.so_ky_kh)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Alt>()
                .Property(e => e.tien_cl)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alt>()
                .Property(e => e.kieu_kh)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Alt>()
                .Property(e => e.ty_le_kh)
                .HasPrecision(6, 4);

            modelBuilder.Entity<Alt>()
                .Property(e => e.tong_sl)
                .HasPrecision(20, 6);

            modelBuilder.Entity<Alt>()
                .Property(e => e.loai_pb)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Alt>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.tk_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.tk_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.tk_cp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.cong_suat)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.loai_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.ts_kt)
                .IsFixedLength();

            modelBuilder.Entity<Alt>()
                .Property(e => e.ly_do_dc)
                .IsFixedLength();

            modelBuilder.Entity<Alt>()
                .Property(e => e.ma_giam_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.ly_do_giam)
                .IsFixedLength();

            modelBuilder.Entity<Alt>()
                .Property(e => e.so_luong)
                .HasPrecision(14, 3);

            modelBuilder.Entity<Alt>()
                .Property(e => e.ghi_chu)
                .IsFixedLength();

            modelBuilder.Entity<Alt>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alt>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alt>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alt>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Alt>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Alt>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<Alt>()
                .Property(e => e.ma_qg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.thuoc_nhom)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alt>()
                .Property(e => e.trang_thai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altt>()
                .Property(e => e.ma_dm)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altt>()
                .Property(e => e.m_ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altt>()
                .Property(e => e.m_ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altt>()
                .Property(e => e.m_ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altt>()
                .Property(e => e.m_ngay_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altt>()
                .Property(e => e.m_ngay_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altt>()
                .Property(e => e.m_ngay_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altt>()
                .Property(e => e.m_sl_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altt>()
                .Property(e => e.m_sl_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altt>()
                .Property(e => e.m_sl_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altt>()
                .Property(e => e.m_gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altt>()
                .Property(e => e.m_gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altt>()
                .Property(e => e.m_gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.Tt_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.ten_tt)
                .IsFixedLength();

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.ten_tt2)
                .IsFixedLength();

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.ghi_chu)
                .IsFixedLength();

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALttvt>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.ma_vc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.ten_vc)
                .IsFixedLength();

            modelBuilder.Entity<Alvc>()
                .Property(e => e.ten_vc2)
                .IsFixedLength();

            modelBuilder.Entity<Alvc>()
                .Property(e => e.loai_vc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.height)
                .HasPrecision(13, 3);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.length)
                .HasPrecision(13, 3);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.volume)
                .HasPrecision(13, 3);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.weight)
                .HasPrecision(13, 3);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.width)
                .HasPrecision(13, 3);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.dvtheight)
                .IsFixedLength();

            modelBuilder.Entity<Alvc>()
                .Property(e => e.dvtlength)
                .IsFixedLength();

            modelBuilder.Entity<Alvc>()
                .Property(e => e.dvtvolume)
                .IsFixedLength();

            modelBuilder.Entity<Alvc>()
                .Property(e => e.dvtweight)
                .IsFixedLength();

            modelBuilder.Entity<Alvc>()
                .Property(e => e.dvtwidth)
                .IsFixedLength();

            modelBuilder.Entity<Alvc>()
                .Property(e => e.tg_xephang)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.tg_dohang)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.dvt_xep)
                .IsFixedLength();

            modelBuilder.Entity<Alvc>()
                .Property(e => e.dvt_do)
                .IsFixedLength();

            modelBuilder.Entity<Alvc>()
                .Property(e => e.bien_so)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvc>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.ten_vitri)
                .IsFixedLength();

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.ten_vitri2)
                .IsFixedLength();

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.ma_loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.kieu_nhap)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.kieu_xuat)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.kieu_ban)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.ghi_chu)
                .IsFixedLength();

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvitri>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.part_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.ten_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.ten_vt2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.he_so1)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.tk_cl_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.tk_gv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.tk_dt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.tk_tl)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.tk_spdd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.nh_vt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.nh_vt2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.nh_vt3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.sl_min)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.sl_max)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Short_name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Bar_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Loai_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Tt_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Nhieu_dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Lo_yn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Kk_yn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Weight)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.DvtWeight)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Weight0)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.DvtWeight0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Length)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Width)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Height)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Diamet)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.DvtLength)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.DvtWidth)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.DvtHeight)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.DvtDiamet)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Size)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Color)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Style)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Ma_qg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Packs)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Packs1)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.abc_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Dvtpacks)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Cycle_kk)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Han_sd)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Han_bh)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Kieu_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Cach_xuat)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Lma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.LdatePur)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.LdateQc)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Lso_qty)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Lso_qtymin)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Lso_qtymax)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.LCycle)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Lpolicy)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Pma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Pma_khc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Pma_khp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Pma_khl)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Prating)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Pquality)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Pquanlity)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Pdeliver)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.PFlex)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Ptech)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.nh_vt9)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.ma_thueNk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.tk_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.date_yn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.TK_CP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.MA_BPHT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.VITRI_YN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.MA_VTTG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.MA_KHTG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.TEN_KHTG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.TEN_QG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.Thue_suat)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.NH_VT4)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.NH_VT5)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.NH_VT6)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.NH_VT7)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.NH_VT8)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.MODEL)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.MA_VV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvt>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.ma_vttg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.part_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.ten_vttg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.ten_vttg2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.he_so1)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.tk_cl_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.tk_gv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.tk_dt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.tk_tl)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.tk_spdd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.nh_vt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.nh_vt2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.nh_vt3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.sl_min)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.sl_max)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Short_name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Bar_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Loai_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Tt_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Nhieu_dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Lo_yn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Kk_yn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Weight)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.DvtWeight)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Weight0)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.DvtWeight0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Length)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Width)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Height)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Diamet)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.DvtLength)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.DvtWidth)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.DvtHeight)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.DvtDiamet)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Size)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Color)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Style)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Ma_qg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Packs)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Packs1)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.abc_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Dvtpacks)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Cycle_kk)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Han_sd)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Han_bh)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Kieu_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Cach_xuat)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Lma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.LdatePur)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.LdateQc)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Lso_qty)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Lso_qtymin)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Lso_qtymax)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.LCycle)
                .HasPrecision(7, 0);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Lpolicy)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Pma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Pma_khc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Pma_khp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Pma_khl)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Prating)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Pquality)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Pquanlity)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Pdeliver)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.PFlex)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.Ptech)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.nh_vt9)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.ma_thueNk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.tk_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.date_yn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.TK_CP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.MA_BPHT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.VITRI_YN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALvttg>()
                .Property(e => e.MA_KHTG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.ten_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.ten_vv2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.nh_vv1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.nh_vv2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.nh_vv3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.tien_nt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.tien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.tk_kc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alvv>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.ma_ytcp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.ten_ytcp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.ten_ytcp2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.tk_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.tk_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.nhom)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.ten_ngan)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.ten_ngan2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Alytcp>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<AM11>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM11>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM11>()
                .Property(e => e.t_tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM11>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM11>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM11>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM11>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.Dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM11>()
                .HasMany(e => e.AD11)
                .WithRequired(e => e.AM11)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.so_seri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM21>()
                .Property(e => e.t_tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM21>()
                .Property(e => e.t_tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AM21>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM21>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM21>()
                .Property(e => e.sua_thue)
                .HasPrecision(1, 0);

            modelBuilder.Entity<AM21>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.sua_tkthue)
                .HasPrecision(1, 0);

            modelBuilder.Entity<AM21>()
                .Property(e => e.tinh_ck)
                .HasPrecision(1, 0);

            modelBuilder.Entity<AM21>()
                .Property(e => e.tk_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.t_ck_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM21>()
                .Property(e => e.t_ck)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM21>()
                .Property(e => e.han_tt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AM21>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM21>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM21>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ten_vtthue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM21>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM21>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM21>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.pt_ck)
                .HasPrecision(6, 2);

            modelBuilder.Entity<AM21>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM21>()
                .Property(e => e.ma_mauhd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM29>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM29>()
                .Property(e => e.t_tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM29>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM29>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM29>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM29>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM29>()
                .HasMany(e => e.AD29)
                .WithRequired(e => e.AM29)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM31>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM31>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM31>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AM31>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM31>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM31>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.so_hd_gtgt)
                .HasPrecision(2, 0);

            modelBuilder.Entity<AM31>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM31>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM31>()
                .Property(e => e.han_tt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AM31>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AM31>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_kh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM31>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM31>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM31>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM31>()
                .HasMany(e => e.AD31)
                .WithRequired(e => e.AM31)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM32>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM32>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM32>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AM32>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM32>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM32>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.so_hd_gtgt)
                .HasPrecision(2, 0);

            modelBuilder.Entity<AM32>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM32>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM32>()
                .Property(e => e.han_tt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AM32>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AM32>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_kh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.so_ct_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM32>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM32>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM32>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM32>()
                .HasMany(e => e.AD32)
                .WithRequired(e => e.AM32)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM39>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM39>()
                .Property(e => e.t_tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM39>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM39>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM39>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM39>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM39>()
                .HasMany(e => e.AD39)
                .WithRequired(e => e.AM39)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM41>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM41>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM41>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM41>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM41>()
                .Property(e => e.so_ct_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM41>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM41>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM41>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.loai_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ty_gia_ht)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .Property(e => e.stt_rec_kt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM41>()
                .HasMany(e => e.AD41)
                .WithRequired(e => e.AM41)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ma_td)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM42>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM42>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM42>()
                .Property(e => e.tk_du)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ty_gia_ht)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM42>()
                .Property(e => e.t_tien_ht)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM42>()
                .Property(e => e.t_cltg)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM42>()
                .Property(e => e.tk_cltg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AM42>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM42>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM42>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM42>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM42>()
                .HasMany(e => e.AD42)
                .WithRequired(e => e.AM42)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM46>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM46>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM46>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM46>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM46>()
                .Property(e => e.so_ct_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM46>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM46>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM46>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.loai_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ty_gia_ht)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .Property(e => e.stt_rec_kt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM46>()
                .HasMany(e => e.AD46)
                .WithRequired(e => e.AM46)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ma_td)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM47>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM47>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM47>()
                .Property(e => e.tk_du)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ty_gia_ht)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM47>()
                .Property(e => e.t_tien_ht)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM47>()
                .Property(e => e.t_cltg)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM47>()
                .Property(e => e.tk_cltg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AM47>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM47>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM47>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM47>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM47>()
                .HasMany(e => e.AD47)
                .WithRequired(e => e.AM47)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM51>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM51>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM51>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM51>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM51>()
                .Property(e => e.so_ct_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.so_hd_gtgt)
                .HasPrecision(2, 0);

            modelBuilder.Entity<AM51>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM51>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM51>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.t_tt_qd)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM51>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM51>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM51>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.loai_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ty_gia_ht)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .Property(e => e.stt_rec_kt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM51>()
                .HasMany(e => e.AD51)
                .WithRequired(e => e.AM51)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ma_td)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM52>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM52>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM52>()
                .Property(e => e.tk_du)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ty_gia_ht)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM52>()
                .Property(e => e.t_tien_ht)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM52>()
                .Property(e => e.t_cltg)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM52>()
                .Property(e => e.tk_cltg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AM52>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM52>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM52>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM52>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM52>()
                .HasMany(e => e.AD52)
                .WithRequired(e => e.AM52)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM56>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM56>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM56>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM56>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM56>()
                .Property(e => e.so_ct_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.so_hd_gtgt)
                .HasPrecision(2, 0);

            modelBuilder.Entity<AM56>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM56>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM56>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.t_tt_qd)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM56>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM56>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM56>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.loai_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ty_gia_ht)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .Property(e => e.stt_rec_kt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM56>()
                .HasMany(e => e.AD56)
                .WithRequired(e => e.AM56)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ma_td)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM57>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM57>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM57>()
                .Property(e => e.tk_du)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ty_gia_ht)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM57>()
                .Property(e => e.t_tien_ht)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM57>()
                .Property(e => e.t_cltg)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM57>()
                .Property(e => e.tk_cltg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AM57>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM57>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM57>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM57>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM57>()
                .HasMany(e => e.AD57)
                .WithRequired(e => e.AM57)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.t_so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.t_tien_nt0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.t_tien0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.t_cp_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.t_cp)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.thue_suat)
                .HasPrecision(6, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_kh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.so_ct_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM71>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM71>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM71>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.K_V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.tso_luong1)
                .HasPrecision(12, 3);

            modelBuilder.Entity<AM71>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.T_CK_NT)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.T_CK)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.Ck_vat)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.T_Gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.T_Gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM71>()
                .Property(e => e.Tk_gg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .Property(e => e.LOAI_CT0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM71>()
                .HasMany(e => e.AD71)
                .WithRequired(e => e.AM71)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.t_so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.t_tien_nt0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.t_tien0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.t_cp_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.t_cp)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.t_nk_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.t_nk)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.tk_thue_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.thue_suat)
                .HasPrecision(6, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM72>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ms_kh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM72>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM72>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM72>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.K_V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.Tso_luong1)
                .HasPrecision(12, 3);

            modelBuilder.Entity<AM72>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM72>()
                .HasMany(e => e.AD72)
                .WithRequired(e => e.AM72)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.stt_rec_pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.so_pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.t_so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM73>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM73>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM73>()
                .Property(e => e.t_tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM73>()
                .Property(e => e.t_tien0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM73>()
                .Property(e => e.t_cp_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM73>()
                .Property(e => e.t_cp)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM73>()
                .Property(e => e.thue_suat)
                .HasPrecision(6, 2);

            modelBuilder.Entity<AM73>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM73>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM73>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM73>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM73>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_kh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM73>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM73>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM73>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.Tso_luong1)
                .HasPrecision(12, 3);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM73>()
                .HasMany(e => e.AD73)
                .WithRequired(e => e.AM73)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.t_so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM74>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM74>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM74>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM74>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM74>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM74>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM74>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.K_V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.Tso_luong1)
                .HasPrecision(12, 3);

            modelBuilder.Entity<AM74>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM74>()
                .HasMany(e => e.AD74)
                .WithRequired(e => e.AM74)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_so_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.t_so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.sua_tkthue)
                .HasPrecision(1, 0);

            modelBuilder.Entity<AM76>()
                .Property(e => e.t_tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.t_tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.han_tt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AM76>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ghi_chu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ten_vtthue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM76>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM76>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM76>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.K_V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.Tso_luong1)
                .HasPrecision(12, 3);

            modelBuilder.Entity<AM76>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.T_Ck_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.T_Ck)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.T_Gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.T_Gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.T_Tien1_Nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.T_Tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.MA_NT01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.MA_NT02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.MA_NT03)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.MA_NT04)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TY_GIA01)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TY_GIA02)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TY_GIA03)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TY_GIA04)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TTIEN_NT01)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TTIEN_NT02)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TTIEN_NT03)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TTIEN_NT04)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TTIEN01)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TTIEN02)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TTIEN03)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TTIEN04)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.t_tien_nt4)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.t_tien4)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM76>()
                .Property(e => e.GHI_CHU01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_spph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_td2ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_td3ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .Property(e => e.ma_mauhd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM76>()
                .HasMany(e => e.AD76)
                .WithRequired(e => e.AM76)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.so_seri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_so_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.t_so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.sua_tkthue)
                .HasPrecision(1, 0);

            modelBuilder.Entity<AM81>()
                .Property(e => e.t_tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.t_tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.tk_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.t_ck_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.t_ck)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.han_tt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AM81>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ten_vtthue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM81>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM81>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM81>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.K_V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.Tso_luong1)
                .HasPrecision(12, 3);

            modelBuilder.Entity<AM81>()
                .Property(e => e.loai_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.pt_ck)
                .HasPrecision(6, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.T_Tien1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM81>()
                .Property(e => e.T_Tien1_nt)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM81>()
                .Property(e => e.Ma_SONB)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.T_gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.T_gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TK_GG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.LOAI_HD)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.T_TIEN_NT4)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.T_TIEN4)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.GHI_CHU01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.LOAI_CT0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_LCT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_LOAI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_NGHE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_SPPH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_TD2PH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_TD3PH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_SOXE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_NAMNU)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_TUOI)
                .HasPrecision(4, 0);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_KMDI)
                .HasPrecision(10, 0);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_LANKT)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_NOIMUA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.DIEN_THOAI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.DT_DD)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.SO_IMAGE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_SONHA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_PHUONG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_TINH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_QUAN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_NVSC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_LISTNV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_BP1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_THE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.LOAI_THE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_GIOVAO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_GIORA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.SO_CMND)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.NOI_CMND)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.NAM_SINH)
                .HasPrecision(4, 0);

            modelBuilder.Entity<AM81>()
                .Property(e => e.GHI_CHU02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_NT01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_NT02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_NT03)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_NT04)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TY_GIA01)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TY_GIA02)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TY_GIA03)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TY_GIA04)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TTIEN_NT01)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TTIEN_NT02)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TTIEN_NT03)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TTIEN_NT04)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TTIEN01)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TTIEN02)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TTIEN03)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TTIEN04)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.T_TIEN_NT5)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.T_TIEN5)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM81>()
                .Property(e => e.MA_KH3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.TT_SOLSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .Property(e => e.ma_mauhd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM81>()
                .HasMany(e => e.AD81)
                .WithRequired(e => e.AM81)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.t_so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM84>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM84>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM84>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM84>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM84>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM84>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM84>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.K_V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.tso_luong1)
                .HasPrecision(12, 3);

            modelBuilder.Entity<AM84>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.MA_SP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .Property(e => e.SL_SP)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM84>()
                .Property(e => e.STT_RECPN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM84>()
                .HasMany(e => e.AD84)
                .WithRequired(e => e.AM84)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_khon)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.t_so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM85>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM85>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM85>()
                .Property(e => e.stt_rec_dc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM85>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM85>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM85>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.K_V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.tso_luong1)
                .HasPrecision(12, 3);

            modelBuilder.Entity<AM85>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM85>()
                .HasMany(e => e.AD85)
                .WithRequired(e => e.AM85)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.t_so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.so_seri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.t_thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.t_tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ghi_chu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_kh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM86>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM86>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM86>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.TSo_luong1)
                .HasPrecision(12, 3);

            modelBuilder.Entity<AM86>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .Property(e => e.T_Ck_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.T_Ck)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.T_Gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.T_Gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.T_Tien1_Nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.T_Tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AM86>()
                .Property(e => e.ma_mauhd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM86>()
                .HasMany(e => e.AD86)
                .WithRequired(e => e.AM86)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.so_seri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_so_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.t_so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM91>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM91>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AM91>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM91>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM91>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.sua_tkthue)
                .HasPrecision(1, 0);

            modelBuilder.Entity<AM91>()
                .Property(e => e.t_tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM91>()
                .Property(e => e.t_tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM91>()
                .Property(e => e.tk_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.t_ck_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM91>()
                .Property(e => e.t_ck)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM91>()
                .Property(e => e.han_tt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<AM91>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM91>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM91>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ten_vtthue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM91>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM91>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM91>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.K_V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.T_Tien1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM91>()
                .Property(e => e.T_Tien1_nt)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM91>()
                .Property(e => e.CHIA_NHOM)
                .HasPrecision(4, 0);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.Tso_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM91>()
                .Property(e => e.loai_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.pt_ck)
                .HasPrecision(7, 2);

            modelBuilder.Entity<AM91>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM91>()
                .HasMany(e => e.AD91)
                .WithRequired(e => e.AM91)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.t_so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AM92>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM92>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM92>()
                .Property(e => e.t_tien_nt0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM92>()
                .Property(e => e.t_tien0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM92>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM92>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AM92>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_kh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.so_ct_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM92>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM92>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AM92>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.K_V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.tso_luong1)
                .HasPrecision(12, 3);

            modelBuilder.Entity<AM92>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AM92>()
                .HasMany(e => e.AD92)
                .WithRequired(e => e.AM92)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.SFile)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.SField)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.Id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.ctype)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.Sname)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.D)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.E)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.F)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.C)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.R)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.J)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.K)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.Ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan>()
                .Property(e => e.M_or_D)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan1>()
                .Property(e => e.SFile)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan1>()
                .Property(e => e.Id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan1>()
                .Property(e => e.Nline)
                .HasPrecision(4, 0);

            modelBuilder.Entity<CorpLan1>()
                .Property(e => e.D)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan1>()
                .Property(e => e.V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan1>()
                .Property(e => e.E)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan1>()
                .Property(e => e.F)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan1>()
                .Property(e => e.C)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan1>()
                .Property(e => e.R)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan1>()
                .Property(e => e.J)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan1>()
                .Property(e => e.K)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan2>()
                .Property(e => e.SFile)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan2>()
                .Property(e => e.SField)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan2>()
                .Property(e => e.Id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan2>()
                .Property(e => e.Sname)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan2>()
                .Property(e => e.D)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan2>()
                .Property(e => e.V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan2>()
                .Property(e => e.E)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan2>()
                .Property(e => e.F)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan2>()
                .Property(e => e.C)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan2>()
                .Property(e => e.R)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan2>()
                .Property(e => e.J)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLan2>()
                .Property(e => e.K)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLang>()
                .Property(e => e.Lan_Id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLang>()
                .Property(e => e.Lan_name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CorpLang>()
                .Property(e => e.Lan_name2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Corpuser>()
                .Property(e => e.user_id)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Corpuser>()
                .Property(e => e.user_name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Corpuser>()
                .Property(e => e.user_pre)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Corpuser>()
                .Property(e => e.password)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Corpuser>()
                .Property(e => e.comment)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Corpuser>()
                .Property(e => e.is_Madmin)
                .IsUnicode(false);

            modelBuilder.Entity<Corpuser>()
                .Property(e => e.del_yn)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Corpuser>()
                .Property(e => e.Rmodule)
                .IsUnicode(false);

            modelBuilder.Entity<Corpuser>()
                .Property(e => e.Inherit_id)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Corpuser>()
                .Property(e => e.inherit_ch)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Corpuser>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Corpuser>()
                .Property(e => e.LEVEL)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<dtproperty>()
                .Property(e => e.property)
                .IsUnicode(false);

            modelBuilder.Entity<dtproperty>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.Menu1)
                .WithOptional(e => e.Menu2)
                .HasForeignKey(e => e.ParentOID);

            modelBuilder.Entity<ModelDefinition>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.vVar)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.vMa_file)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.vOrder)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.vValue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.vLfScatter)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.vWidths)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.vFields)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.eFields)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.vHeaders)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.eHeaders)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.vUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.vTitle)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.eTitle)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.VTitlenew)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.ETitlenew)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.e1Title)
                .IsUnicode(false);

            modelBuilder.Entity<V6Lookup>()
                .Property(e => e.V_Search)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6option>()
                .Property(e => e.ma_phan_he)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6option>()
                .Property(e => e.stt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6option>()
                .Property(e => e.name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6option>()
                .Property(e => e.type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6option>()
                .Property(e => e.descript)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6option>()
                .Property(e => e.descript2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6option>()
                .Property(e => e.val)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6option>()
                .Property(e => e.defaul)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6option>()
                .Property(e => e.formattype)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6option>()
                .Property(e => e.inputmask)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6rights>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6rights>()
                .Property(e => e.Sfield)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6rights>()
                .Property(e => e.D)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6rights>()
                .Property(e => e.V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6rights>()
                .Property(e => e.E)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6rights>()
                .Property(e => e.Rread)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6rights>()
                .Property(e => e.Rhide)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6rights>()
                .Property(e => e.MD)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6rights>()
                .Property(e => e.sfile)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6rights>()
                .Property(e => e.loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.Module_id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.user_id)
                .HasPrecision(3, 0);

            modelBuilder.Entity<V6user>()
                .Property(e => e.user_name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.user_pre)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.password)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.comment)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.rights)
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.del_yn)
                .HasPrecision(18, 0);

            modelBuilder.Entity<V6user>()
                .Property(e => e.r_del)
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.r_edit)
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.r_add)
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<V6user>()
                .Property(e => e.r_kho)
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.r_dvcs)
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.USER_OTHER)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.Inherit_id)
                .HasPrecision(3, 0);

            modelBuilder.Entity<V6user>()
                .Property(e => e.inherit_ch)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.LEVEL)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6user>()
                .Property(e => e.web_password)
                .IsUnicode(false);

            modelBuilder.Entity<ABkh>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ABkh>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABkh>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABkh>()
                .Property(e => e.du_no00)
                .HasPrecision(15, 0);

            modelBuilder.Entity<ABkh>()
                .Property(e => e.du_co00)
                .HasPrecision(15, 0);

            modelBuilder.Entity<ABkh>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ABkh>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ABkh>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABkh>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABkh>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABkh>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABlkct>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ABlkct>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABlkct>()
                .Property(e => e.z_lk)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABlkct>()
                .Property(e => e.z_lk_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABlkct>()
                .Property(e => e.dt_lk)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABlkct>()
                .Property(e => e.dt_lk_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABlkct>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABlkct>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ABlkct>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABlkct>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABlkct>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ABlo>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ABlo>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABlo>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABlo>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABlo>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABlo>()
                .Property(e => e.ton00)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABlo>()
                .Property(e => e.du00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ABlo>()
                .Property(e => e.du_nt00)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ABlo>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<ABlo>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABlo>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ABlo>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABlo>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ABlo>()
                .Property(e => e.Ma_Dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.stt_rec_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ngay)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.stt_ctntxt)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_luong)
                .HasPrecision(12, 3);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.tien_nt)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.tien_cp)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.tien_cp_nt)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ton_kho01)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du01)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du_nt01)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ton_kho02)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du02)
                .HasPrecision(16, 0);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du_nt02)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ton_kho03)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du03)
                .HasPrecision(16, 0);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du_nt03)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ton_kho04)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du04)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du_nt04)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ton_kho05)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du05)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du_nt05)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ton_kho06)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du06)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du_nt06)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ton_kho07)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du07)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du_nt07)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ton_kho08)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du08)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du_nt08)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ton_kho09)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du09)
                .HasPrecision(16, 0);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du_nt09)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ton_kho10)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du10)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du_nt10)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ton_kho11)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du11)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du_nt11)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ton_kho12)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du12)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du_nt12)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.ton_kho13)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du13)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.so_du_nt13)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABntxt>()
                .Property(e => e.Ma_Dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.Thang)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.so_lsx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.sl_dd)
                .HasPrecision(12, 3);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.tl_ht)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.sl_qd)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.sl_nk)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.sl_sx)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.tien_dd_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.tien_dd)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ABSPDD>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ABtk>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.du_no00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.du_co00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.du_no1)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.du_co1)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.du_no_nt1)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.du_co_nt1)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABtk>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.du_no00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.du_co00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.du_no1)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.du_co1)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.du_no_nt1)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.du_co_nt1)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABtknt>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ABvt>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ABvt>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvt>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvt>()
                .Property(e => e.ton00)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABvt>()
                .Property(e => e.du00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABvt>()
                .Property(e => e.du_nt00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABvt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvt>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ABvt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvt>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ABvt>()
                .Property(e => e.Ma_Dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvt13>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvt13>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvt13>()
                .Property(e => e.ton13)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ABvt13>()
                .Property(e => e.du13)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABvt13>()
                .Property(e => e.du_nt13)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABvt13>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvt13>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvt13>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvv>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ABvv>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvv>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvv>()
                .Property(e => e.du_no00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABvv>()
                .Property(e => e.du_co00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABvv>()
                .Property(e => e.du_no_nt00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABvv>()
                .Property(e => e.du_co_nt00)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ABvv>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvv>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ABvv>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvv>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvv>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ABvv>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ACKU>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ACKU>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACKU>()
                .Property(e => e.ma_KU)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACKU>()
                .Property(e => e.lk_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ACKU>()
                .Property(e => e.lk_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ACKU>()
                .Property(e => e.lk_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ACKU>()
                .Property(e => e.lk_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ACKU>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACKU>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ACKU>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACKU>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACKU>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACKU>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ACvv>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ACvv>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACvv>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACvv>()
                .Property(e => e.lk_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ACvv>()
                .Property(e => e.lk_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ACvv>()
                .Property(e => e.lk_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ACvv>()
                .Property(e => e.lk_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ACvv>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACvv>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ACvv>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACvv>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACvv>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACvv>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<AD11>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ps_no)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ps_co)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD11>()
                .Property(e => e.nh_dk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD11>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD11>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD11>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD11>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.tk_dt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD21>()
                .Property(e => e.tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD21>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AD21>()
                .Property(e => e.thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD21>()
                .Property(e => e.thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ck_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ck)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD21>()
                .Property(e => e.tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD21>()
                .Property(e => e.tt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.thue_suati)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AD21>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_kh2_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD21>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD21>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD21>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_spph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_td2ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.ma_td3ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.tk_gv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.Tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD21>()
                .Property(e => e.Tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD21>()
                .Property(e => e.Ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD21>()
                .Property(e => e.STT_REC_TT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ps_no)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ps_co)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD29>()
                .Property(e => e.nh_dk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD29>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD29>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD29>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD29>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD31>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD31>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD31>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD31>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD31>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD31>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD31>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD32>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD32>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AD32>()
                .Property(e => e.tt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD32>()
                .Property(e => e.tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD32>()
                .Property(e => e.thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD32>()
                .Property(e => e.thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD32>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD32>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD32>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD32>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD32>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD32>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ps_no)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ps_co)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD39>()
                .Property(e => e.nh_dk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD39>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD39>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD39>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD39>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ps_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ps_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.tt_qd)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD41>()
                .Property(e => e.stt_rec_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AD41>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD41>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD41>()
                .Property(e => e.tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD41>()
                .Property(e => e.tt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD41>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD41>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD41>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD41>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ty_gia_ht2)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.Tien)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD41>()
                .Property(e => e.Tien_nt)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD41>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.TIEN_QD)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD41>()
                .Property(e => e.TY_GIAQD)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AD41>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_spph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_td2ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.ma_td3ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD41>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD42>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD42>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD42>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD42>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD42>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD42>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD42>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD42>()
                .Property(e => e.ps_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD42>()
                .Property(e => e.ps_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD42>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD42>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD42>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD42>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD42>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD42>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD42>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD42>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD42>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD42>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ps_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ps_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.tt_qd)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD46>()
                .Property(e => e.stt_rec_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AD46>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD46>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD46>()
                .Property(e => e.tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD46>()
                .Property(e => e.tt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD46>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD46>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD46>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD46>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ty_gia_ht2)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.Tien)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD46>()
                .Property(e => e.Tien_nt)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD46>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_spph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_td2ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.ma_td3ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD46>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD47>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD47>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD47>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD47>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD47>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD47>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD47>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD47>()
                .Property(e => e.ps_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD47>()
                .Property(e => e.ps_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD47>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD47>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD47>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD47>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD47>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD47>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD47>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD47>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD47>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ps_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ps_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.tt_qd)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD51>()
                .Property(e => e.stt_rec_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD51>()
                .Property(e => e.thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD51>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD51>()
                .Property(e => e.tt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD51>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD51>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD51>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD51>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ty_gia_ht2)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.Tien)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD51>()
                .Property(e => e.Tien_nt)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD51>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_spph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_td2ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.ma_td3ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD51>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD52>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD52>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD52>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD52>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD52>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD52>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD52>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD52>()
                .Property(e => e.ps_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD52>()
                .Property(e => e.ps_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD52>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD52>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD52>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD52>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD52>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD52>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD52>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD52>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD52>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD52>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ps_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ps_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.tt_qd)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD56>()
                .Property(e => e.stt_rec_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AD56>()
                .Property(e => e.thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD56>()
                .Property(e => e.thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD56>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD56>()
                .Property(e => e.tt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD56>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD56>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD56>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD56>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ty_gia_ht2)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.Tien)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD56>()
                .Property(e => e.Tien_nt)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD56>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_spph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_td2ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.ma_td3ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD56>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD57>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD57>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD57>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD57>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD57>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD57>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD57>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD57>()
                .Property(e => e.ps_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD57>()
                .Property(e => e.ps_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD57>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD57>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD57>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD57>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD57>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD57>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD57>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD57>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD57>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD57>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<AD71>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD71>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD71>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD71>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD71>()
                .Property(e => e.tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.gia_nt0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD71>()
                .Property(e => e.gia0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD71>()
                .Property(e => e.tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.tien0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.tien_hg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.tien_hg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.cp_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.cp)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD71>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD71>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD71>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.pn_gia_tbi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD71>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD71>()
                .Property(e => e.gia_nt01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD71>()
                .Property(e => e.gia01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.CK_NT)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.CK)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.PT_CKI)
                .HasPrecision(6, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.Ck_vat_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.Thue_suat)
                .HasPrecision(7, 4);

            modelBuilder.Entity<AD71>()
                .Property(e => e.Gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.Gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD71>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.so_image)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD71>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<AD72>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD72>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD72>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD72>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD72>()
                .Property(e => e.tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD72>()
                .Property(e => e.tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD72>()
                .Property(e => e.gia_nt0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD72>()
                .Property(e => e.gia0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD72>()
                .Property(e => e.tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD72>()
                .Property(e => e.tien0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD72>()
                .Property(e => e.tien_hg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD72>()
                .Property(e => e.tien_hg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD72>()
                .Property(e => e.cp_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD72>()
                .Property(e => e.cp)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD72>()
                .Property(e => e.nk_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD72>()
                .Property(e => e.nk)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD72>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD72>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD72>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD72>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD72>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.pn_gia_tbi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD72>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD72>()
                .Property(e => e.gia_nt01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD72>()
                .Property(e => e.gia01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD72>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<AD73>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD73>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD73>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD73>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD73>()
                .Property(e => e.gia_nt0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD73>()
                .Property(e => e.gia0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD73>()
                .Property(e => e.tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD73>()
                .Property(e => e.tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD73>()
                .Property(e => e.tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD73>()
                .Property(e => e.tien0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD73>()
                .Property(e => e.tien_hg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD73>()
                .Property(e => e.tien_hg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD73>()
                .Property(e => e.cp_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD73>()
                .Property(e => e.cp)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD73>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD73>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD73>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD73>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD73>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.pn_gia_tbi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD73>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD73>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.STT_REC_PN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.STT_REC0PN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD73>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<AD74>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD74>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD74>()
                .Property(e => e.gia_nt0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD74>()
                .Property(e => e.tien_nt0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD74>()
                .Property(e => e.gia0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD74>()
                .Property(e => e.tien0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD74>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD74>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD74>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD74>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD74>()
                .Property(e => e.stt_rec_px)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.stt_rec0px)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD74>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD74>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD74>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.pn_gia_tbi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD74>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD74>()
                .Property(e => e.gia_nt01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD74>()
                .Property(e => e.gia01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.so_image)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD74>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<AD76>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD76>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD76>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD76>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD76>()
                .Property(e => e.gia_nt2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.gia2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD76>()
                .Property(e => e.tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD76>()
                .Property(e => e.thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD76>()
                .Property(e => e.thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD76>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.tk_gv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.tk_tl)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.stt_rec_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.stt_rec0hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.pn_gia_tbi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.gia_nt21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.gia21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.Tien0)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AD76>()
                .Property(e => e.Tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD76>()
                .Property(e => e.gia0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.gia_nt0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.Pt_cki)
                .HasPrecision(7, 4);

            modelBuilder.Entity<AD76>()
                .Property(e => e.Ck_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD76>()
                .Property(e => e.Ck)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD76>()
                .Property(e => e.Gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD76>()
                .Property(e => e.Gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD76>()
                .Property(e => e.Tien1_Nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD76>()
                .Property(e => e.Tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD76>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD76>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<AD81>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD81>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD81>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gia_nt2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gia2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD81>()
                .Property(e => e.tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD81>()
                .Property(e => e.thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD81>()
                .Property(e => e.thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ck)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ck_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD81>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.tk_gv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.tk_dt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.stt_rec_pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.stt_rec0pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.px_gia_ddi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.tang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gia_nt21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gia21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gia_ban_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gia_ban)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.tk_cki)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.PT_CKI)
                .HasPrecision(7, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.TIEN1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD81>()
                .Property(e => e.TIEN1_NT)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD81>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD81>()
                .Property(e => e.gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD81>()
                .Property(e => e.Ma_gia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.GIA_NT4)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.GIA4)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AD81>()
                .Property(e => e.TIEN_NT4)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD81>()
                .Property(e => e.TIEN4)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD81>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.so_image)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.Status_DPI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.ma_loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.tt_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.dg_stat)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.char_01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.char_02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.char_03)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.num_01)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.num_02)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.num_03)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.NGUOI_SD1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.DIENTHOAI1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.DT_DD1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.TT_SOXE1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.TT_NAMNU1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.MA_TD2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT>()
                .Property(e => e.MA_TD3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.dg_stat)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.char_01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.char_02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.char_03)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.num_01)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.num_02)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.num_03)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.NGUOI_SD1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.DIENTHOAI1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.DT_DD1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.TT_SOXE1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.TT_NAMNU1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.MA_TD2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD81CT0>()
                .Property(e => e.MA_TD3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<AD84>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD84>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD84>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD84>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD84>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD84>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD84>()
                .Property(e => e.stt_rec_pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.stt_rec0pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD84>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD84>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD84>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.px_gia_ddi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD84>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.so_image)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD84>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<AD85>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD85>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD85>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD85>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD85>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD85>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD85>()
                .Property(e => e.stt_rec_pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.stt_rec0pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD85>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD85>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD85>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.px_gia_ddi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD85>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.ma_vitrin)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.MA_LNXN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.so_image)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD85>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<AD86>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD86>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD86>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD86>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD86>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD86>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD86>()
                .Property(e => e.stt_rec_pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.stt_rec0pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD86>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD86>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD86>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD86>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.px_gia_ddi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD86>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.Pt_cki)
                .HasPrecision(7, 4);

            modelBuilder.Entity<AD86>()
                .Property(e => e.Ck_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD86>()
                .Property(e => e.Ck)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD86>()
                .Property(e => e.Gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD86>()
                .Property(e => e.Gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD86>()
                .Property(e => e.Tien1_Nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD86>()
                .Property(e => e.Tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD86>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD86>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.he_so1)
                .HasPrecision(13, 3);

            modelBuilder.Entity<AD91>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD91>()
                .Property(e => e.so_luong)
                .HasPrecision(12, 3);

            modelBuilder.Entity<AD91>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD91>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD91>()
                .Property(e => e.gia_nt2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.gia2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD91>()
                .Property(e => e.tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD91>()
                .Property(e => e.thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD91>()
                .Property(e => e.thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD91>()
                .Property(e => e.ck)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD91>()
                .Property(e => e.ck_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<AD91>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.tk_gv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.tk_dt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.stt_rec_pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.stt_rec0pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD91>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.PT_CKI)
                .HasPrecision(7, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.TIEN1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD91>()
                .Property(e => e.TIEN1_NT)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD91>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD91>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.gia_nt21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.gia21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.gia_ban_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.gia_ban)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD91>()
                .Property(e => e.tk_cki)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<AD92>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<AD92>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.so_luong)
                .HasPrecision(12, 3);

            modelBuilder.Entity<AD92>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD92>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD92>()
                .Property(e => e.tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD92>()
                .Property(e => e.tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD92>()
                .Property(e => e.gia_nt0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD92>()
                .Property(e => e.gia0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD92>()
                .Property(e => e.tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD92>()
                .Property(e => e.tien0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD92>()
                .Property(e => e.tien_hg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD92>()
                .Property(e => e.tien_hg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD92>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD92>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD92>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.pn_gia_tbi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD92>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD92>()
                .Property(e => e.gia_nt01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD92>()
                .Property(e => e.gia01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AD92>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.Cc0)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.so_the_Cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.ma_nv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.Tang_giam)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.ma_tg_Cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.nguyen_gia)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.gt_da_pb)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.gt_tang)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.gt_giam)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.gt_cl)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.gt_pb_ky)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.so_ky)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.line_nbr)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADALCC>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ADALT>()
                .Property(e => e.Ts0)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.so_the_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.ma_nv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.Tang_giam)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.ma_tg_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.nguyen_gia)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.gt_da_kh)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.gt_tang)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.gt_giam)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.gt_cl)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.gt_kh_ky)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<ADALT>()
                .Property(e => e.so_ky)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.line_nbr)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADALT>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADALT>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADALT>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.CC0)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.line_nbr)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.so_the_CC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ma_bp_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.tk_pb_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.tk_cp_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.T_he_so)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.He_so)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ma_bpht_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ma_td2_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ma_td3_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTCCBP>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.Ts0)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.line_nbr)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.so_the_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.ma_bp_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.tk_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.tk_cp_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.T_he_so)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.He_so)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.ma_bpht_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.ma_td2_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.ma_td3_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADCTTSBP>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.cc0)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.so_the_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_nv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.tk_pb)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.tk_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.tk_cp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.he_so)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.nguyen_gia)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.gt_da_pb)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.gt_tang)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.gt_giam)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.gt_cl)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.gt_pb_ky)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.t_he_so)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.sua_pb)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.T_gt_pb_ky)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_bpht_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_td2_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_td3_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHSCC>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.Ts0)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.so_the_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ma_nv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.tk_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.tk_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.tk_cp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.he_so)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.nguyen_gia)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.gt_da_kh)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.gt_tang)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.gt_giam)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.gt_cl)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.gt_kh_ky)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<ADHST>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADHST>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADHST>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ADHST>()
                .Property(e => e.t_he_so)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.sua_kh)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.T_gt_kh_ky)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ma_bpht_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ma_td2_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ma_td3_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADHST>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.so_the_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.sua_kh)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.ma_nv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.tk_ts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.tk_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.tk_cp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.nguyen_gia)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.gt_da_kh)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.gt_tang)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.gt_giam)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.gt_cl)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.gt_kh_ky)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADKHT>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.so_the_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.sua_pb)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.ma_nv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.tk_cc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.tk_pb)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.tk_cp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.nguyen_gia)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.gt_da_pb)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.gt_tang)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.gt_giam)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.gt_cl)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.gt_pb_ky)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.dien_giai)
                .IsFixedLength();

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<ADPBCC>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.form)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.stt)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.stt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.ma_so0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.ma_so)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.loai_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.cach_tinh0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.cach_tinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.chi_tieu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.chi_tieu2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.dbf)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.thue_suat)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.tk_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.tk_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.thue)
                .HasPrecision(20, 2);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.thue_nt)
                .HasPrecision(20, 2);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.in_ck)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.bold)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.ids)
                .HasPrecision(20, 0);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.auto)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.tag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.ma_so01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.ma_so02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADThue43>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.stt)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.bold)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.in_ck)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.ts_nv)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.cong_no)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.ngoai_bang)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.cach_tinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.ma_so)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.chi_tieu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.chi_tieu2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.tien0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc1>()
                .Property(e => e.ids)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.stt)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.bold)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.in_ck)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.ma_so)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.chi_tieu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.chi_tieu2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.cach_tinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.tk_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.tk_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.giam_tru)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.ky_truoc)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.ky_nay)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.ky_truocnt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.ky_nay_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc2>()
                .Property(e => e.ids)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.stt)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.bold)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.in_ck)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.ma_so)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.cach_tinh0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.cach_tinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.chi_tieu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.chi_tieu2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.tk_du)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.du_dau)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.ps_no)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.ps_co)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.ps_no0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.ps_co0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.du_cuoi)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.du_dau_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.ps_no_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.ps_co_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.du_cuoi_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc3>()
                .Property(e => e.ids)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.stt)
                .HasPrecision(2, 0);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.bold)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.in_ck)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.loai_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.ma_so)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.cach_tinh0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.cach_tinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.chi_tieu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.chi_tieu2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.tk_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.tk_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.ky_nay)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.ky_nay_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc4>()
                .Property(e => e.ids)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.stt)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.bold)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.in_ck)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.chi_tieu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.chi_tieu2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.ma_so)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.cach_tinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.tk_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.tk_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.dau)
                .HasPrecision(2, 0);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.ky_truoc)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.ky_nay)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.ky_truocnt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.ky_naynt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc5>()
                .Property(e => e.ids)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.stt)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.bold)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.in_ck)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.chi_tieu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.chi_tieu2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.ma_so)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.cach_tinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.loai_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.ky_truoc)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.ky_nay)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.ky_truocnt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.ky_naynt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Agltc6>()
                .Property(e => e.ids)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.in_ck)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.ma_so)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.cach_tinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.stt)
                .HasPrecision(2, 0);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.he_so)
                .HasPrecision(7, 3);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.dvt2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.tb)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.ty_le)
                .HasPrecision(10, 3);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.chi_tieu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.chi_tieu2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.ten1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.ten12)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.ma_so1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.ts_kd1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.he_so1)
                .HasPrecision(8, 3);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.tb1)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.tien1)
                .HasPrecision(14, 0);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.ten2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.ten22)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.ma_so2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.ts_kd2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.he_so2)
                .HasPrecision(8, 3);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.tb2)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.tien2)
                .HasPrecision(14, 0);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.ids)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Agltc8>()
                .Property(e => e.iscal)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AKhungCkCt>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AKhungCkCt>()
                .Property(e => e.Khung_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AKhungCkCt>()
                .Property(e => e.t_sl1)
                .HasPrecision(13, 3);

            modelBuilder.Entity<AKhungCkCt>()
                .Property(e => e.t_sl2)
                .HasPrecision(13, 3);

            modelBuilder.Entity<AKhungCkCt>()
                .Property(e => e.t_tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AKhungCkCt>()
                .Property(e => e.t_tien2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<AKhungCkCt>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AKhungCkCt>()
                .Property(e => e.pt_ck)
                .HasPrecision(10, 4);

            modelBuilder.Entity<AKhungCkCt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AKhungCkCt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AKhungCkCt>()
                .Property(e => e.Ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.Ma_CK)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.Ma_CK0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.Ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.t_sl1)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.t_sl2)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.t_tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.t_tien2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.pt_ck)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.tien_ck)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALCKMCt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.tag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.stt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.ten_bt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.stt_rec01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.stt_rec02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.stt_rec03)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.stt_rec04)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.stt_rec05)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.stt_rec06)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.stt_rec07)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.stt_rec08)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.stt_rec09)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.stt_rec10)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.stt_rec11)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.stt_rec12)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.so_ct01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.so_ct02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.so_ct03)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.so_ct04)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.so_ct05)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.so_ct06)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.so_ct07)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.so_ct08)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.so_ct09)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.so_ct10)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.so_ct11)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcltg>()
                .Property(e => e.so_ct12)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alctct>()
                .Property(e => e.Module_id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alctct>()
                .Property(e => e.ma_phan_he)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alctct>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alctct>()
                .Property(e => e.ma_ct_me)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alctct>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alctct>()
                .Property(e => e.so_ct)
                .HasPrecision(6, 0);

            modelBuilder.Entity<Alctct>()
                .Property(e => e.m_trung_so)
                .HasPrecision(2, 0);

            modelBuilder.Entity<Alctct>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alctct>()
                .Property(e => e.chuan)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alctct>()
                .Property(e => e.loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.so_luong)
                .HasPrecision(16, 3);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alcthd>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.sl_SP)
                .HasPrecision(20, 6);

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.sl_dm_dh)
                .HasPrecision(20, 6);

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.sl_dm_kh)
                .HasPrecision(20, 6);

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.sl_tt)
                .HasPrecision(20, 6);

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.gc_td1)
                .IsFixedLength();

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.gc_td2)
                .IsFixedLength();

            modelBuilder.Entity<Aldmvtct>()
                .Property(e => e.gc_td3)
                .IsFixedLength();

            modelBuilder.Entity<ALgia>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia01)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia_nt01)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia02)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia_nt02)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia03)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia_nt03)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia04)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia_nt04)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia05)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia_nt05)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia06)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia_nt06)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia07)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia_nt07)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia08)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia_nt08)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia09)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia_nt09)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia10)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia_nt10)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia11)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia_nt11)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia12)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.gia_nt12)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgia>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ALgia2>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgia2>()
                .Property(e => e.gia_nt2)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia2>()
                .Property(e => e.gia2)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgia2>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgia2>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgia2>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgia2>()
                .Property(e => e.ma_gia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgia2>()
                .Property(e => e.Dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgia2>()
                .Property(e => e.Ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgia2>()
                .Property(e => e.STATUS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgia2>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALGIA200>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALGIA200>()
                .Property(e => e.gia_nt2)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALGIA200>()
                .Property(e => e.gia2)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALGIA200>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALGIA200>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALGIA200>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALGIA200>()
                .Property(e => e.ma_gia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALGIA200>()
                .Property(e => e.Dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALGIA200>()
                .Property(e => e.Ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALGIA200>()
                .Property(e => e.STATUS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALGIA200>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgiavon>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgiavon>()
                .Property(e => e.loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgiavon>()
                .Property(e => e.giavon)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon>()
                .Property(e => e.giavonnt)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia01)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia_nt01)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia02)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia_nt02)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia03)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia_nt03)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia04)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia_nt04)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia05)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia_nt05)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia06)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia_nt06)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia07)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia_nt07)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia08)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia_nt08)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia09)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia_nt09)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia10)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia_nt10)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia11)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia_nt11)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia12)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.gia_nt12)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.user_id0)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgiavon3>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ALgiavv>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgiavv>()
                .Property(e => e.gia_nt2)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavv>()
                .Property(e => e.gia2)
                .HasPrecision(14, 4);

            modelBuilder.Entity<ALgiavv>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgiavv>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALgiavv>()
                .Property(e => e.nh_vt2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.tag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.stt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.ten_bt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.tk_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.tk_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.loai_kc)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.group_kc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.stt_rec01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.stt_rec02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.stt_rec03)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.stt_rec04)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.stt_rec05)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.stt_rec06)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.stt_rec07)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.stt_rec08)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.stt_rec09)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.stt_rec10)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.stt_rec11)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.stt_rec12)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.so_ct01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.so_ct02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.so_ct03)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.so_ct04)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.so_ct05)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.so_ct06)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.so_ct07)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.so_ct08)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.so_ct09)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.so_ct10)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.so_ct11)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.so_ct12)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALkc>()
                .Property(e => e.AUTO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.Ma_km)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.Ma_km0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.Ma_kmm)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.Ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.t_sl1)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.t_sl2)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.t_tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.t_tien2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.pt_ck)
                .HasPrecision(10, 4);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.sl_km)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.tien_km)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.Ghi_chukm)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.Ghi_chuck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMBCt>()
                .Property(e => e.T_SLKM)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.Ma_km)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.Ma_km0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.Ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.t_sl1)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.t_sl2)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.t_tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.t_tien2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.pt_ck)
                .HasPrecision(10, 4);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.sl_km)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.tien_km)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALKMMCt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagd>()
                .Property(e => e.ma_ct_me)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagd>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagd>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagd>()
                .Property(e => e.ten_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagd>()
                .Property(e => e.ten_gd2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagd>()
                .Property(e => e.form)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Almagd>()
                .Property(e => e.loai_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhdvc>()
                .Property(e => e.ma_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhdvc>()
                .Property(e => e.ten_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhdvc>()
                .Property(e => e.ten_nh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhdvc>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhdvc>()
                .Property(e => e.dia_chi2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhdvc>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhdvc>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhdvc>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhdvc>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhkh>()
                .Property(e => e.ma_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhkh>()
                .Property(e => e.ten_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhkh>()
                .Property(e => e.ten_nh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhkh>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhkh>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhkh>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhkh>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhku>()
                .Property(e => e.ma_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhku>()
                .Property(e => e.ten_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhku>()
                .Property(e => e.ten_nh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhku>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhku>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhku>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhtk>()
                .Property(e => e.ma_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhtk>()
                .Property(e => e.ten_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhtk>()
                .Property(e => e.ten_nh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhtk>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhtk>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhtk>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhvt>()
                .Property(e => e.ma_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhvt>()
                .Property(e => e.ten_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhvt>()
                .Property(e => e.ten_nh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhvt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhvt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhvt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALnhvt>()
                .Property(e => e.CHECK_SYNC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvv>()
                .Property(e => e.ma_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvv>()
                .Property(e => e.ten_nh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvv>()
                .Property(e => e.ten_nh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvv>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvv>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alnhvv>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.tk_he_so)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.heso01)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.heso02)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.heso03)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.heso04)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.heso05)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.heso06)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.heso07)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.heso08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.heso09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.heso10)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.heso11)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.heso12)
                .HasPrecision(15, 3);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.ma_td)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALpb1>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alql>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Alql>()
                .Property(e => e.thang)
                .HasPrecision(2, 0);

            modelBuilder.Entity<Alql>()
                .Property(e => e.ql)
                .HasPrecision(15, 2);

            modelBuilder.Entity<Alql>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Alql>()
                .Property(e => e.user_id2)
                .HasPrecision(9, 0);

            modelBuilder.Entity<ALstt>()
                .Property(e => e.stt_rec)
                .HasPrecision(10, 0);

            modelBuilder.Entity<ALstt>()
                .Property(e => e.nam_bd)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ALtgnt>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgnt>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<ALtgnt>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgnt>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALtgnt>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.Ma_THAU)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.Ma_THAU0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.Ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.t_sl1)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.t_sl2)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.t_tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.t_tien2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.sl_km)
                .HasPrecision(13, 3);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.tien_km)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.Ghi_chukm)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.Ghi_chuck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALTHAUCT>()
                .Property(e => e.T_SLKM)
                .HasPrecision(20, 6);

            modelBuilder.Entity<Altk2>()
                .Property(e => e.type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk2>()
                .Property(e => e.tk2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk2>()
                .Property(e => e.nh_tk2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk2>()
                .Property(e => e.ten_tk2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk2>()
                .Property(e => e.ten_tk22)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk2>()
                .Property(e => e.dau)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk2>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Altk2>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.dien_giaih)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.nh_dk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.tk_du)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ty_gia)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ps_no)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ps_co)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_td)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.loai_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.Ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.So_lsx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.Ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.Ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.Ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.Ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.LOAI_CTGS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.KIEU_CTGS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.SO_LO0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_spph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_td2ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.ma_td3ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARA00>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.thang)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.stt)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.bold)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.in_ck)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.ma_so)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.chi_tieu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.chi_tieu2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.cach_tinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.tk_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.tk_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.giam_tru)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.ky_truoc)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.ky_nay)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.luy_ke)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.ky_truocnt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.ky_nay_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.luy_ke_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.ma_ytcp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.sl_tp_nk)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.tl_ht)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.sl_dd_ck)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.dd_dk)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.ps_no)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARBCGT>()
                .Property(e => e.dd_ck)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.no_co)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.TK_DU)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.KHOA_CTGS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.LOAI_CTGS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.KIEU_CTGS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.NHOM_CTGS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.SO_LO0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.MA_DVCS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctg>()
                .Property(e => e.THANG)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.no_co)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.STT)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.phandau)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.phancuoi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.dinhdang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.phanthang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.tag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.nhom_user)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.TK_DU0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.TK_DU)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.KHOA_CTGS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.LOAI_CTGS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.KIEU_CTGS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.NHOM_CTGS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.SO_LO0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.MA_DVCS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARctgs01>()
                .Property(e => e.THANG)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.so_seri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_khon)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.sl_nhap1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.sl_xuat1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gia01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gia_nt01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gia21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gia_nt21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tk_gv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tk_dt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.sl_nhap)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.sl_xuat)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tien_nt_n)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tien_nt_x)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tien_nhap)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tien_xuat)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gia_nt0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gia0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tien0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.cp_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.cp)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.nk_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.nk)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tk_thue_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gia_nt2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gia2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tien_nt2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tien2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ck_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ck)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tk_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.stt_rec_pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.stt_rec_dc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_td)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.loai_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ma_vitrin)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.So_lsx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.tang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.PT_CKI)
                .HasPrecision(7, 4);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TIEN1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TIEN1_NT)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.STT_RECDH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.STT_REC0DH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.MA_LNXN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.MA_LNX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ma_kmm)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ma_kmb)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TK_GG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Ma_gia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.MA_LCT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_LOAI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_NGHE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.MA_SPPH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.MA_TD2PH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.MA_TD3PH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_SOXE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_NAMNU)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_TUOI)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_KMDI)
                .HasPrecision(10, 0);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_LANKT)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_NOIMUA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.DIEN_THOAI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.DT_DD)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.SO_IMAGE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.DIA_CHI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_SONHA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.MA_PHUONG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.MA_TINH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.MA_QUAN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_NVSC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_LISTNV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.MA_BP1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.MA_THE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.LOAI_THE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_GIOVAO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_GIORA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.TT_SOLSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARI70>()
                .Property(e => e.Tien_tb)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.so_seri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.t_tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.t_tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.t_ck_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.t_ck)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.t_tt_nt0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.t_tt0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.han_tt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.t_tt_qd)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.tat_toan)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.tt_qd)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.stt_rec_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.t_tt1)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.t_tt_nt1)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.tt_cn)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ty_gia_dg)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.tien_cl_no)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.tien_cl_co)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.MA_KHO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.T_TIEN_NT5)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.T_TIEN5)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.MA_KH3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_spph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_td2ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_td3ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.ma_lct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS20>()
                .Property(e => e.TT_SOLSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.stt_rec_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.ty_gia)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.ty_gia_dg)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.tien_cl_no)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.tien_cl_co)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS21>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ma_tt)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.tk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_tien_nt0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_tien0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_cp_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_cp)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_nk_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_nk)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_tt_nt0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_tt0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.han_tt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_tt_qd)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.tat_toan)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.tt_qd)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.stt_rec_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_tt1)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.t_tt_nt1)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.tt_cn)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ty_gia_dg)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.tien_cl_no)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.tien_cl_co)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.MA_KHO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ma_spph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ma_td2ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ma_td3ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.ONG_BA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.TT_SOLSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.MA_BP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS30>()
                .Property(e => e.MA_NVIEN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.stt_rec_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.ky)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.nam)
                .HasPrecision(4, 0);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.ty_gia)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.ty_gia_dg)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.tien_cl_no)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.tien_cl_co)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.ma_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS31>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.so_seri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_khon)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.sl_nhap1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.sl_xuat1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.he_so1)
                .HasPrecision(8, 3);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gia01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gia_nt01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gia21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gia_nt21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tk_gv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tk_dt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.sl_nhap)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.sl_xuat)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tien_nt_n)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tien_nt_x)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tien_nhap)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tien_xuat)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gia_nt0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gia0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tien0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.cp_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.cp)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.nk_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.nk)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tk_thue_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gia_nt2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gia2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tien_nt2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tien2)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ck_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ck)
                .HasPrecision(16, 2);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.tk_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.stt_rec_pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.stt_rec_dc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_td)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.loai_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.Ma_vitrin)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.Ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.Ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.Ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.So_lsx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.Ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.Ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.Ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.Ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.Ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.Ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.Ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.Tang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.PT_CKI)
                .HasPrecision(7, 4);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.TIEN1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.TIEN1_NT)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.STT_RECDH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.STT_REC0DH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARS90>()
                .Property(e => e.Loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.so_seri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ten_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_so_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ten_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.t_tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.tk_du)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ghi_chu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.t_tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ty_gia)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.Ma_kh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV20>()
                .Property(e => e.ma_mauhd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.mau_bc)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ten_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ma_so_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ten_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.tk_du)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ghi_chu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ma_kho)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ma_vv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.Ma_kh2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<ARV30>()
                .Property(e => e.ma_mauhd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ps_no)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ps_co)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.nh_dk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD11>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.tk_dt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ck_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ck)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.tt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.thue_suati)
                .HasPrecision(5, 2);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_kh2_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_spph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_td2ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.ma_td3ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.tk_gv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.Tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.Tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.Ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD21>()
                .Property(e => e.STT_REC_TT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ps_no)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ps_co)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.nh_dk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD29>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD31>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.tt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD32>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ps_no)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ps_co)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.nh_dk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD39>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ps_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ps_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.tt_qd)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.stt_rec_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.tt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ty_gia_ht2)
                .HasPrecision(9, 2);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.Tien)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.Tien_nt)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.TIEN_QD)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.TY_GIAQD)
                .HasPrecision(9, 2);

            modelBuilder.Entity<D_AD41>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ps_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ps_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.tt_qd)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.stt_rec_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.tt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ty_gia_ht2)
                .HasPrecision(9, 2);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.Tien)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.Tien_nt)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD46>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ps_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ps_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.tt_qd)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.stt_rec_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.tt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ty_gia_ht2)
                .HasPrecision(9, 2);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.Tien)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.Tien_nt)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD51>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.dien_giaii)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.tk_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ps_no_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ps_co_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ps_no)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ps_co)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ma_kh_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.tt_qd)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.stt_rec_tt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.tt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.tt_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ty_gia_ht2)
                .HasPrecision(9, 2);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.Tien)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.Tien_nt)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD56>()
                .Property(e => e.MA_KHO2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.gia_nt0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.gia0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.tien0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.tien_hg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.tien_hg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.cp_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.cp)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.pn_gia_tbi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.gia_nt01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.gia01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.CK_NT)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.CK)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.PT_CKI)
                .HasPrecision(6, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.Ck_vat_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.Thue_suat)
                .HasPrecision(7, 4);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.Gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.Gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.so_image)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD71>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.gia_nt0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.gia0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.tien0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.tien_hg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.tien_hg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.cp_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.cp)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.nk_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.nk)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.pn_gia_tbi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.gia_nt01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.gia01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD72>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.gia_nt0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.gia0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.tien_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.tien)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.tien0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.tien_hg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.tien_hg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.cp_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.cp)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.pn_gia_tbi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.STT_REC_PN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.STT_REC0PN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD73>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.gia_nt0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.tien_nt0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.gia0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.tien0)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.stt_rec_px)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.stt_rec0px)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.pn_gia_tbi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.gia_nt01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.gia01)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.so_image)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD74>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.gia_nt2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.gia2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.tk_gv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.tk_tl)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.stt_rec_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.stt_rec0hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.pn_gia_tbi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.gia_nt21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.gia21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.Tien0)
                .HasPrecision(18, 0);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.Tien_nt0)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.gia0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.gia_nt0)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.Pt_cki)
                .HasPrecision(7, 4);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.Ck_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.Ck)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.Gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.Gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.Tien1_Nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.Tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD76>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gia_nt2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gia2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ck)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ck_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.tk_gv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.tk_dt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.stt_rec_pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.stt_rec0pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.px_gia_ddi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.tang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gia_nt21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gia21)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gia_ban_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gia_ban)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.tk_cki)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.PT_CKI)
                .HasPrecision(7, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.TIEN1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.TIEN1_NT)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.Ma_gia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.GIA_NT4)
                .HasPrecision(18, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.GIA4)
                .HasPrecision(18, 4);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.TIEN_NT4)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.TIEN4)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.so_image)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.Status_DPI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD81>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.stt_rec_pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.stt_rec0pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.px_gia_ddi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.so_image)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD84>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.stt_rec_pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.stt_rec0pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.px_gia_ddi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.ma_vitrin)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.MA_LNXN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.so_image)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD85>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.stt_rec0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.dvt1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.he_so1)
                .HasPrecision(16, 6);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.so_luong1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.tk_vt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_kho_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_nx_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_vv_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_td_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.gia_nt)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.gia)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.stt_rec_pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.stt_rec0pn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.thue)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.thue_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ln)
                .HasPrecision(5, 0);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.sl_td1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.sl_td2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.sl_td3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.gc_td1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.gc_td2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.gc_td3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.tk_thue_i)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.px_gia_ddi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.stt_recdh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.stt_rec0dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_bpht)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_sp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_hd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_ku)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_phi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.dvt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.gia_nt1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.gia1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_vitri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.ma_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.MA_LNX_I)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.Pt_cki)
                .HasPrecision(7, 4);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.Ck_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.Ck)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.Gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.Gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.Tien1_Nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.Tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.SO_KHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.SO_MAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AD86>()
                .Property(e => e.SO_LSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.so_seri0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.so_ct0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_so_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.t_so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.sua_tkthue)
                .HasPrecision(1, 0);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.t_tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.t_tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.han_tt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ghi_chu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ten_vtthue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.K_V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.Tso_luong1)
                .HasPrecision(12, 3);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.T_Ck_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.T_Ck)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.T_Gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.T_Gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.T_Tien1_Nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.T_Tien1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.MA_NT01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.MA_NT02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.MA_NT03)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.MA_NT04)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TY_GIA01)
                .HasPrecision(8, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TY_GIA02)
                .HasPrecision(8, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TY_GIA03)
                .HasPrecision(8, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TY_GIA04)
                .HasPrecision(8, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TTIEN_NT01)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TTIEN_NT02)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TTIEN_NT03)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TTIEN_NT04)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TTIEN01)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TTIEN02)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TTIEN03)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TTIEN04)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.t_tien_nt4)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.t_tien4)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.GHI_CHU01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_spph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_td2ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM76>()
                .Property(e => e.ma_td3ph)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_nk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_gd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.so_seri)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.so_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.so_lo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.so_lo1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_kh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ong_ba)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.dia_chi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_so_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.dien_giai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_bp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_nx)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.t_so_luong)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_nt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ty_gia)
                .HasPrecision(8, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.t_tien_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.t_tien)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_thue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.thue_suat)
                .HasPrecision(5, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.t_thue_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.t_thue)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.tk_thue_co)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.tk_thue_no)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.sua_tkthue)
                .HasPrecision(1, 0);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.t_tien_nt2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.t_tien2)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.tk_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.t_ck_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.t_ck)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.han_tt)
                .HasPrecision(3, 0);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.t_tt_nt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.t_tt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_dvcs)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.so_dh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ten_vtthue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.sl_ud1)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.sl_ud2)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.sl_ud3)
                .HasPrecision(16, 4);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.gc_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.gc_ud2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.gc_ud3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_ud1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.K_V)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_httt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.ma_nvien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.Tso_luong1)
                .HasPrecision(12, 3);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.loai_ck)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.pt_ck)
                .HasPrecision(6, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.kieu_post)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.xtag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.T_Tien1)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.T_Tien1_nt)
                .HasPrecision(20, 6);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.Ma_SONB)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.T_gg_nt)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.T_gg)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TK_GG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.LOAI_HD)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.T_TIEN_NT4)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.T_TIEN4)
                .HasPrecision(16, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.GHI_CHU01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.LOAI_CT0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_LCT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_LOAI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_NGHE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_SPPH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_TD2PH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_TD3PH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_SOXE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_SOKHUNG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_SOMAY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_NAMNU)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_TUOI)
                .HasPrecision(4, 0);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_KMDI)
                .HasPrecision(10, 0);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_LANKT)
                .HasPrecision(3, 0);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_NOIMUA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.DIEN_THOAI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.DT_DD)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.SO_IMAGE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_SONHA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_PHUONG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_TINH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_QUAN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_NVSC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_LISTNV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_BP1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_THE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.LOAI_THE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_GIOVAO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_GIORA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.SO_CMND)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.NOI_CMND)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.NAM_SINH)
                .HasPrecision(4, 0);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.GHI_CHU02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_NT01)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_NT02)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_NT03)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_NT04)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TY_GIA01)
                .HasPrecision(8, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TY_GIA02)
                .HasPrecision(8, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TY_GIA03)
                .HasPrecision(8, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TY_GIA04)
                .HasPrecision(8, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TTIEN_NT01)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TTIEN_NT02)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TTIEN_NT03)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TTIEN_NT04)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TTIEN01)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TTIEN02)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TTIEN03)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TTIEN04)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.T_TIEN_NT5)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.T_TIEN5)
                .HasPrecision(15, 2);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.MA_KH3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_AM81>()
                .Property(e => e.TT_SOLSX)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<v6bak>()
                .Property(e => e.max_file)
                .HasPrecision(4, 0);

            modelBuilder.Entity<v6bak>()
                .Property(e => e.file_name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<v6bak>()
                .Property(e => e.file_zip)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Menu>()
                .Property(e => e.module_id)
                .IsFixedLength();

            modelBuilder.Entity<V6Menu>()
                .Property(e => e.v2id)
                .IsUnicode(false);

            modelBuilder.Entity<V6Menu>()
                .Property(e => e.jobid)
                .IsUnicode(false);

            modelBuilder.Entity<V6Menu>()
                .Property(e => e.itemid)
                .IsUnicode(false);

            modelBuilder.Entity<V6Menu>()
                .Property(e => e.hotkey)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<V6Menu>()
                .Property(e => e.vbar2)
                .IsUnicode(false);

            modelBuilder.Entity<V6Menu>()
                .Property(e => e.page)
                .IsUnicode(false);

            modelBuilder.Entity<V6Menu>()
                .Property(e => e.ma_ct)
                .IsUnicode(false);

            modelBuilder.Entity<V6Menu>()
                .Property(e => e.loai_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.stt_rec)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.ma_ct)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.KHOA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.Level)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.Level_name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.ten_user)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.Lock)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.comment1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.comment2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.time0)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.time2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.SO_CT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.DIEN_GIAI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.MA_DVCS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.MA_KHO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VCOMMENT>()
                .Property(e => e.STATUS)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
