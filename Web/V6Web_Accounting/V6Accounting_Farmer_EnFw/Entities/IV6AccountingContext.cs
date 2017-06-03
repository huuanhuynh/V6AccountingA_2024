using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    /// <summary>
    ///     Has API from DbContext and V6AccountingContext. This interface is for unit testing.
    /// </summary>
    public interface IV6AccountingContext : IDisposable
    {
        #region Propeties

        //DbSet<Abbpkh> DMAbbpkh { get; set; }
        //DbSet<Abhd> DMAbhd { get; set; }
        //DbSet<Abhdkh> DMAbhdkh { get; set; }
        //DbSet<ABkh> DMABkh { get; set; }
        //DbSet<Abkhovvkh> DMAbkhovvkh { get; set; }
        //DbSet<Abku> DMAbku { get; set; }
        //DbSet<ABlkct> DMABlkct { get; set; }
        //DbSet<ABlo> DMABlo { get; set; }
        //DbSet<ABntxt> DMABntxt { get; set; }
        //DbSet<Abphi> DMAbphi { get; set; }
        //DbSet<ABSPDD> DMABSPDD { get; set; }
        //DbSet<AbTD> DMAbTD { get; set; }
        //DbSet<AbTD2> DMAbTD2 { get; set; }
        //DbSet<AbTD3> DMAbTD3 { get; set; }
        //DbSet<ABtk> DMABtk { get; set; }
        //DbSet<ABtknt> DMABtknt { get; set; }
        //DbSet<ABvitri> DMABvitri { get; set; }
        //DbSet<ABvt> DMABvt { get; set; }
        //DbSet<ABvt13> DMABvt13 { get; set; }
        //DbSet<ABvv> DMABvv { get; set; }
        //DbSet<Abvvkh> DMAbvvkh { get; set; }
        //DbSet<ACKU> DMACKU { get; set; }
        //DbSet<ACvv> DMACvv { get; set; }
        //DbSet<AD11> DMAD11 { get; set; }
        //DbSet<AD21> DMAD21 { get; set; }
        //DbSet<AD29> DMAD29 { get; set; }
        //DbSet<AD31> DMAD31 { get; set; }
        //DbSet<AD32> DMAD32 { get; set; }
        //DbSet<AD39> DMAD39 { get; set; }
        //DbSet<AD41> DMAD41 { get; set; }
        //DbSet<AD42> DMAD42 { get; set; }
        //DbSet<AD46> DMAD46 { get; set; }
        //DbSet<AD47> DMAD47 { get; set; }
        //DbSet<AD51> DMAD51 { get; set; }
        //DbSet<AD52> DMAD52 { get; set; }
        //DbSet<AD56> DMAD56 { get; set; }
        //DbSet<AD57> DMAD57 { get; set; }
        //DbSet<AD71> DMAD71 { get; set; }
        //DbSet<AD72> DMAD72 { get; set; }
        //DbSet<AD73> DMAD73 { get; set; }
        //DbSet<AD74> DMAD74 { get; set; }
        //DbSet<AD76> DMAD76 { get; set; }
        DbSet<AD81> AD81Set { get; set; }
        //DbSet<AD81CT> DMAD81CT { get; set; }
        //DbSet<AD81CT0> DMAD81CT0 { get; set; }
        //DbSet<AD84> DMAD84 { get; set; }
        //DbSet<AD85> DMAD85 { get; set; }
        //DbSet<AD86> DMAD86 { get; set; }
        //DbSet<AD91> DMAD91 { get; set; }
        //DbSet<AD92> DMAD92 { get; set; }
        //DbSet<ADALCC> DMADALCC { get; set; }
        //DbSet<ADBPCC> DMADBPCC { get; set; }
        //DbSet<ADCTCC> DMADCTCC { get; set; }
        //DbSet<ADCTCCBP> DMADCTCCBP { get; set; }
        //DbSet<ADCTTSBP> DMADCTTSBP { get; set; }
        //DbSet<ADHSCC> DMADHSCC { get; set; }
        //DbSet<ADPBCC> DMADPBCC { get; set; }
        //DbSet<ADSLCC> DMADSLCC { get; set; }
        //DbSet<ADThue43> DMADThue43 { get; set; }
        //DbSet<Agltc1> DMAgltc1 { get; set; }
        //DbSet<Agltc2> DMAgltc2 { get; set; }
        //DbSet<Agltc3> DMAgltc3 { get; set; }
        //DbSet<Agltc4> DMAgltc4 { get; set; }
        //DbSet<Agltc5> DMAgltc5 { get; set; }
        //DbSet<Agltc6> DMAgltc6 { get; set; }
        //DbSet<Agltc8> DMAgltc8 { get; set; }
        //DbSet<AKhungCK> DMAKhungCK { get; set; }
        //DbSet<AKhungCkCt> DMAKhungCkCt { get; set; }
        DbSet<Albp> DMAlbp { get; set; }
        //DbSet<Albpcc> DMAlbpcc { get; set; }
        //DbSet<Albpht> DMAlbpht { get; set; }
        //DbSet<Alcc> DMAlcc { get; set; }
        //DbSet<Alck> DMAlck { get; set; }
        //DbSet<ALck2> DMALck2 { get; set; }
        //DbSet<ALCKM> DMALCKM { get; set; }
        //DbSet<ALCKMCt> DMALCKMCt { get; set; }
        //DbSet<Alcltg> DMAlcltg { get; set; }
        DbSet<Alct> DMAlct { get; set; }
        //DbSet<Alctct> DMAlctct { get; set; }
        //DbSet<Alcthd> DMAlcthd { get; set; }
        //DbSet<Aldmpbct> DMAldmpbct { get; set; }
        //DbSet<Aldmpbph> DMAldmpbph { get; set; }
        //DbSet<Aldmvt> DMAldmvt { get; set; }
        //DbSet<Aldmvtct> DMAldmvtct { get; set; }
        //DbSet<Aldvcs> DMAldvcs { get; set; }
        //DbSet<Aldvt> DMAldvt { get; set; }
        //DbSet<ALgia> DMALgia { get; set; }
        //DbSet<ALgia2> DMALgia2 { get; set; }
        //DbSet<ALGIA200> DMALGIA200 { get; set; }
        //DbSet<ALgiavon> DMALgiavon { get; set; }
        //DbSet<ALgiavon3> DMALgiavon3 { get; set; }
        //DbSet<ALgiavv> DMALgiavv { get; set; }
        //DbSet<Alhd> DMAlhd { get; set; }
        //DbSet<Alhttt> DMAlhttt { get; set; }
        //DbSet<Alhtvc> DMAlhtvc { get; set; }
        //DbSet<ALkc> DMALkc { get; set; }
        DbSet<Alkh> AlkhSet { get; set; }
        DbSet<V6option> V6OptionSet { get; set; }
        //DbSet<Alkho> DMAlkho { get; set; }
        //DbSet<AlkhTG> DMAlkhTG { get; set; }
        //DbSet<ALKMB> DMALKMB { get; set; }
        //DbSet<ALKMBCt> DMALKMBCt { get; set; }
        //DbSet<ALKMM> DMALKMM { get; set; }
        //DbSet<ALKMMCt> DMALKMMCt { get; set; }
        //DbSet<Alku> DMAlku { get; set; }
        //DbSet<Allnx> DMAllnx { get; set; }
        //DbSet<Allo> DMAllo { get; set; }
        //DbSet<ALloaicc> DMALloaicc { get; set; }
        //DbSet<Alloaick> DMAlloaick { get; set; }
        //DbSet<Alloaivc> DMAlloaivc { get; set; }
        //DbSet<ALloaivt> DMALloaivt { get; set; }
        //DbSet<Almagd> DMAlmagd { get; set; }
        DbSet<Almagia> DMAlmagia { get; set; }
        //DbSet<ALMAUHD> DMALMAUHD { get; set; }
        //DbSet<ALnhCC> DMALnhCC { get; set; }
        //DbSet<ALnhhd> DMALnhhd { get; set; }
        //DbSet<ALnhkh> DMALnhkh { get; set; }
        //DbSet<Alnhkh2> DMAlnhkh2 { get; set; }
        //DbSet<Alnhku> DMAlnhku { get; set; }
        //DbSet<Alnhphi> DMAlnhphi { get; set; }
        //DbSet<ALnhtk> DMALnhtk { get; set; }
        //DbSet<ALnhtk0> DMALnhtk0 { get; set; }
        //DbSet<ALnhvt> DMALnhvt { get; set; }
        //DbSet<Alnhvt2> DMAlnhvt2 { get; set; }
        //DbSet<Alnhvv> DMAlnhvv { get; set; }
        //DbSet<ALnhytcp> DMALnhytcp { get; set; }
        //DbSet<ALnk> DMALnk { get; set; }
        DbSet<Alnt> DMAlnt { get; set; }
        //DbSet<Alnv> DMAlnv { get; set; }
        //DbSet<Alnvien> DMAlnvien { get; set; }
        //DbSet<ALpb> DMALpb { get; set; }
        //DbSet<ALpb1> DMALpb1 { get; set; }
        //DbSet<Alphi> DMAlphi { get; set; }
        //DbSet<Alphuong> DMAlphuong { get; set; }
        //DbSet<ALplcc> DMALplcc { get; set; }
        //DbSet<ALpost> DMALpost { get; set; }
        //DbSet<ALqddvt> DMALqddvt { get; set; }
        //DbSet<Alqg> DMAlqg { get; set; }
        //DbSet<Alql> DMAlql { get; set; }
        //DbSet<Alquan> DMAlquan { get; set; }
        //DbSet<ALstt> DMALstt { get; set; }
        //DbSet<Altd> DMAltd { get; set; }
        //DbSet<Altd2> DMAltd2 { get; set; }
        //DbSet<Altd3> DMAltd3 { get; set; }
        //DbSet<ALtgcc> DMALtgcc { get; set; }
        //DbSet<ALtgnt> DMALtgnt { get; set; }
        //DbSet<ALTHAU> DMALTHAU { get; set; }
        //DbSet<ALTHAUCT> DMALTHAUCT { get; set; }
        //DbSet<Althue> DMAlthue { get; set; }
        //DbSet<Altinh> DMAltinh { get; set; }
        //DbSet<Altk0> DMAltk0 { get; set; }
        //DbSet<Altk1> DMAltk1 { get; set; }
        //DbSet<Altk2> DMAltk2 { get; set; }
        //DbSet<ALtklkKU> DMALtklkKU { get; set; }
        //DbSet<ALtklkvv> DMALtklkvv { get; set; }
        //DbSet<ALtknh> DMALtknh { get; set; }
        //DbSet<Altt> DMAltt { get; set; }
        //DbSet<ALttvt> DMALttvt { get; set; }
        //DbSet<Alvc> DMAlvc { get; set; }
        //DbSet<ALvitri> DMALvitri { get; set; }
        DbSet<ALvt> ALvtSet { get; set; }
        //DbSet<ALvttg> DMALvttg { get; set; }
        //DbSet<Alvv> DMAlvv { get; set; }
        //DbSet<Alytcp> DMAlytcp { get; set; }
        //DbSet<AM11> DMAM11 { get; set; }
        //DbSet<AM21> DMAM21 { get; set; }
        //DbSet<AM29> DMAM29 { get; set; }
        //DbSet<AM31> DMAM31 { get; set; }
        //DbSet<AM32> DMAM32 { get; set; }
        //DbSet<AM39> DMAM39 { get; set; }
        //DbSet<AM41> DMAM41 { get; set; }
        //DbSet<AM42> DMAM42 { get; set; }
        //DbSet<AM46> DMAM46 { get; set; }
        //DbSet<AM47> DMAM47 { get; set; }
        //DbSet<AM51> DMAM51 { get; set; }
        //DbSet<AM52> DMAM52 { get; set; }
        //DbSet<AM56> DMAM56 { get; set; }
        //DbSet<AM57> DMAM57 { get; set; }
        //DbSet<AM71> DMAM71 { get; set; }
        //DbSet<AM72> DMAM72 { get; set; }
        //DbSet<AM73> DMAM73 { get; set; }
        //DbSet<AM74> DMAM74 { get; set; }
        //DbSet<AM76> DMAM76 { get; set; }
        DbSet<AM81> AM81Set { get; set; }
        //DbSet<AM84> DMAM84 { get; set; }
        //DbSet<AM85> DMAM85 { get; set; }
        //DbSet<AM86> DMAM86 { get; set; }
        //DbSet<AM91> DMAM91 { get; set; }
        //DbSet<AM92> DMAM92 { get; set; }
        //DbSet<ARA00> DMARA00 { get; set; }
        //DbSet<ARBCGT> DMARBCGT { get; set; }
        //DbSet<ARctgs01> DMARctgs01 { get; set; }
        //DbSet<ARI70> DMARI70 { get; set; }
        //DbSet<ARS20> DMARS20 { get; set; }
        //DbSet<ARS21> DMARS21 { get; set; }
        //DbSet<ARS30> DMARS30 { get; set; }
        //DbSet<ARS31> DMARS31 { get; set; }
        //DbSet<ARS90> DMARS90 { get; set; }
        //DbSet<ARV20> DMARV20 { get; set; }
        //DbSet<ARV30> DMARV30 { get; set; }
        //DbSet<CorpLan> DMCorpLan { get; set; }
        //DbSet<CorpLan1> DMCorpLan1 { get; set; }
        //DbSet<CorpLan2> DMCorpLan2 { get; set; }
        //DbSet<CorpLang> DMCorpLang { get; set; }
        //DbSet<Corpuser> DMCorpuser { get; set; }
        //DbSet<D_AD11> DMD_AD11 { get; set; }
        //DbSet<D_AD21> DMD_AD21 { get; set; }
        //DbSet<D_AD29> DMD_AD29 { get; set; }
        //DbSet<D_AD31> DMD_AD31 { get; set; }
        //DbSet<D_AD32> DMD_AD32 { get; set; }
        //DbSet<D_AD39> DMD_AD39 { get; set; }
        //DbSet<D_AD41> DMD_AD41 { get; set; }
        //DbSet<D_AD46> DMD_AD46 { get; set; }
        //DbSet<D_AD51> DMD_AD51 { get; set; }
        //DbSet<D_AD56> DMD_AD56 { get; set; }
        //DbSet<D_AD71> DMD_AD71 { get; set; }
        //DbSet<D_AD72> DMD_AD72 { get; set; }
        //DbSet<D_AD73> DMD_AD73 { get; set; }
        //DbSet<D_AD74> DMD_AD74 { get; set; }
        //DbSet<D_AD76> DMD_AD76 { get; set; }
        //DbSet<D_AD81> DMD_AD81 { get; set; }
        //DbSet<D_AD84> DMD_AD84 { get; set; }
        //DbSet<D_AD85> DMD_AD85 { get; set; }
        //DbSet<D_AD86> DMD_AD86 { get; set; }
        //DbSet<D_AM76> DMD_AM76 { get; set; }
        //DbSet<D_AM81> DMD_AM81 { get; set; }
        //DbSet<v6bak> DMv6bak { get; set; }
        //DbSet<V6Lookup> DMV6Lookup { get; set; }
        //DbSet<V6Menu> DMV6Menu { get; set; }
        //DbSet<V6option> DMV6option { get; set; }
        //DbSet<V6rights> DMV6rights { get; set; }
        //DbSet<V6User> DMV6user { get; set; }
        //DbSet<VCOMMENT> DMVCOMMENT { get; set; }

        #endregion
        
            /// <summary>
        ///     See <see cref="System.Data.Entity.DbContext.SaveChanges()"/>
        /// </summary>
        int SaveChanges();
        
        /// <summary>
        ///     See <see cref="System.Data.Entity.DbContext.SaveChangesAsync()"/>
        /// </summary>
        Task<int> SaveChangesAsync();

        /// <summary>
        ///     See <see cref="System.Data.Entity.DbContext.SaveChangesAsync()"/>
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        ///     See <see cref="System.Data.Entity.DbContext.Set()"/>
        /// </summary>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        ///     See <see cref="System.Data.Entity.DbContext.Set()"/>
        /// </summary>
        DbSet Set(Type entityType);
        
        /// <summary>
        ///     See <see cref="System.Data.Entity.DbContext.Entry()"/>
        /// </summary>
        DbEntityEntry Entry(object entity);

        /// <summary>
        ///     See <see cref="System.Data.Entity.DbContext.Entry()"/>
        /// </summary>
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        Database Database { get; }
    }
}
