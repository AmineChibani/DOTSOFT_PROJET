using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{

    [Table("PRODUIT_FAMILLE")]
    public class DbProduitFamille
    {
        [Key]
        [Column("ID_FAMILLE")]
        public int IdFamille { get; set; }

        [Column("NOM")]
        public string? Nom { get; set; }

        [Column("ID_FAMILLE_PARENT")]
        public int? IdFamilleParent { get; set; } = 0;

        [Column("ID_TYPE_GARANTIE")]
        public int IdTypeGarantie { get; set; }

        [Column("ID_TYPE_ETIQUETTE")]
        public int? IdTypeEtiquette { get; set; } = 1;

        [Column("ID_COULEUR")]
        public int? IdCouleur { get; set; } = 3;

        [Column("EXTENSION_GARANTIE")]
        public bool ExtensionGarantie { get; set; }

        [Column("REDEVANCE")]
        public bool? Redevance { get; set; }

        [Column("ORDRE")]
        public long? Ordre { get; set; } = 0;

        [Column("NIVEAU")]
        public long? Niveau { get; set; } = 0;

        [Column("CONTROL_STOCK")]
        public bool? ControlStock { get; set; } = true;

        [Column("TROC_AXE1")]
        public string? TrocAxe1 { get; set; }

        [Column("TROC_AXE2")]
        public string? TrocAxe2 { get; set; }

        [Column("TROC_AXE3")]
        public string? TrocAxe3 { get; set; }

        [Column("TROC_AXE4")]
        public string? TrocAxe4 { get; set; }

        [Column("TYPE_TRI")]
        public int? TypeTri { get; set; } = 1;

        [Column("LOGO")]
        public string? Logo { get; set; }

        [Column("ID_TYPE_AXE1")]
        public long? IdTypeAxe1 { get; set; }

        [Column("ID_TYPE_AXE2")]
        public long? IdTypeAxe2 { get; set; }

        [Column("ID_TYPE_AXE3")]
        public long? IdTypeAxe3 { get; set; }

        [Column("ID_TYPE_AXE4")]
        public long? IdTypeAxe4 { get; set; }

        [Column("ID_QUARTILE")]
        public long? IdQuartile { get; set; }

        [Column("ABANDONNE")]
        public bool? Abandonne { get; set; } = false;

        [Column("SAISIE_SERIE")]
        public bool? SaisieSerie { get; set; } = false;

        [Column("SAISIE_DATELIMITE")]
        public bool? SaisieDateLimite { get; set; } = false;

        [Column("COMPTE_TVA")]
        public string? CompteTVA { get; set; }

        [Column("CODE")]
        public char? Code { get; set; } = ' ';

        [Column("ID_STOCK_DEFAUT")]
        public int? IdStockDefaut { get; set; }

        [Column("TAUX_SERENITYPASS")]
        public float? TauxSerenityPass { get; set; } = 0;

        [Column("ID_TRANSPORTEUR")]
        public int? IdTransporteur { get; set; }

        [Column("PUBLIEE")]
        public bool? Publiee { get; set; } = true;

        [Column("COEF_MULT_FRAIS_PORT")]
        public float? CoefMultFraisPort { get; set; } = 1;

        [Column("MONTANT_LOGISTIQUE")]
        public decimal? MontantLogistique { get; set; }

        [Column("REGLE_APPLICATION")]
        public int? RegleApplication { get; set; }

        [Column("ID_TRANSPORTEUR_BO")]
        public int? IdTransporteurBO { get; set; } = 2;

        [Column("IMAGE")]
        public string? Image { get; set; }

        [Column("PAGE_MOU")]
        public string? PageMou { get; set; }

        [Column("FIDELITE_TYPE")]
        public string? FideliteType { get; set; }

        [Column("FIDELITE_NB_PTS")]
        public float? FideliteNbPts { get; set; }

        [Column("CODE_EXTERNE")]
        public string? CodeExterne { get; set; }

        [Column("ID_FAMILLE_LOGISTIQUE")]
        public int? IdFamilleLogistique { get; set; }

        [Column("ECOTAXE")]
        public decimal? EcoTaxe { get; set; }

        [Column("ECOTAXE_TVA")]
        public decimal? EcoTaxeTVA { get; set; }

        [Column("URL_SPECIFIQUE")]
        public string? UrlSpecifique { get; set; }

        [Column("TEXTE_SPECIAL")]
        public string? TexteSpecial { get; set; }

        [Column("VIGNETTE")]
        public byte[]? Vignette { get; set; }

        [Column("VIGNETTE_TYPEMIME")]
        public string? VignetteTypeMime { get; set; }

        [Column("DESC_COURT")]
        public string? DescCourt { get; set; }

        [Column("ID_TRADEDOUBLER_CATEGORIE")]
        public long? IdTradedoublerCategorie { get; set; } = 0;

        [Column("TRI_FAMILLE")]
        public int? TriFamille { get; set; }

        [Column("ID_TYPE_RETOUR_INFO")]
        public long? IdTypeRetourInfo { get; set; }

        [Column("TRI_FRONT")]
        public int? TriFront { get; set; }

        [Column("AFFICHAGE_CROSS")]
        public int? AffichageCross { get; set; }

        [Column("PHOTO_NONPRODUIT_1")]
        public byte[]? PhotoNonProduit1 { get; set; }

        [Column("PHOTO_NONPRODUIT_1_TYPEMIME")]
        public string? PhotoNonProduit1TypeMime { get; set; }

        [Column("PHOTO_NONPRODUIT_2")]
        public byte[]? PhotoNonProduit2 { get; set; }

        [Column("PHOTO_NONPRODUIT_2_TYPEMIME")]
        public string? PhotoNonProduit2TypeMime { get; set; }

        [Column("PHOTO_NONPRODUIT_3")]
        public byte[]? PhotoNonProduit3 { get; set; }

        [Column("PHOTO_NONPRODUIT_3_TYPEMIME")]
        public string? PhotoNonProduit3TypeMime { get; set; }

        [Column("TRI_PRODUITS_TYPE")]
        public string? TriProduitsType { get; set; }

        [Column("TRI_PRODUITS_ORDRE")]
        public string? TriProduitsOrdre { get; set; }

        [Column("TYPE_NAVIGATION")]
        public bool? TypeNavigation { get; set; } = false;

        [Column("GARANTIE_PAR_DEFAUT")]
        public int? GarantieParDefaut { get; set; } = 0;

        [Column("ID_TYPE_AFFICHAGE")]
        public long? IdTypeAffichage { get; set; }

        [Column("MASQUER_EXPORT")]
        public int? MasquerExport { get; set; } = 0;

        [Column("TXT_IDEN")]
        public string? TxtIden { get; set; }

        [Column("TYPE_AFFICHAGE_PRODUITS")]
        public int? TypeAffichageProduits { get; set; }

        [Column("FAMILLE_AFFECTATION")]
        public int? FamilleAffectation { get; set; }

        [Column("CRITERE_AFFECTATION")]
        public int? CritereAffectation { get; set; }

        [Column("PRODUITS_A_AFFECTATION")]
        public int? ProduitsAAffectation { get; set; }
    }
}
}
