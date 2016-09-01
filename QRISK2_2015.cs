using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoronaryRiskCalculator
{
    public class QRISK2_2015
    {

        public static double calcQRISK_F(
int age,int b_AF,int b_ra,int b_renal,int b_treatedhyp,int b_type1,int b_type2,double bmi,int ethrisk,int fh_cvd,double rati,double sbp,int smoke_cat,int surv,double town
)
{
	double[] survivor =  new double[16]{
		0,
		0.999128758907318,
		0.998222172260284,
		0.997292637825012,
		0.996305286884308,
		0.995250642299652,
		0.994234919548035,
		0.993183135986328,
		0.992080569267273,
		0.990928232669830,
		0.989747583866119,
		0.988448619842529,
		0.987112879753113,
		0.985681176185608,
		0.984169244766235,
		0.982512056827545
	};

	/* The conditional arrays */

	double[] Iethrisk = new double[10] {
		0,
		0,
		0.2574099349831925900000000,
		0.6129795430571779400000000,
		0.3362159841669621300000000,
		0.1512517303224336400000000,
		-0.1794156259657768100000000,
		-0.3503423610057745400000000,
		-0.2778372483233216800000000,
		-0.1592734122665366000000000
	};
    double[] Ismoke = new double[5]{
		0,
		0.2119377108760385200000000,
		0.6618634379685941500000000,
		0.7570714587132305600000000,
		0.9496298251457036000000000
	};

	/* Applying the fractional polynomial transforms */
	/* (which includes scaling)                      */

	double dage = age;
	dage=dage/10;
	double age_1 = Math.Pow(dage,.5);
	double age_2 = dage;
	double dbmi = bmi;
	dbmi=dbmi/10;
    double bmi_1 = Math.Pow(dbmi, -2);
    double bmi_2 = Math.Pow(dbmi, -2) * Math.Log(dbmi);

	/* Centring the continuous variables */

    age_1 = age_1 - 2.086397409439087;
    age_2 = age_2 - 4.353054523468018;
    bmi_1 = bmi_1 - 0.152244374155998;
    bmi_2 = bmi_2 - 0.143282383680344;
    rati = rati - 3.506655454635620;
    sbp = sbp - 125.040039062500000;
    town = town - 0.416743695735931;

	/* Start of Sum */
	double a=0;

	/* The conditional sums */

	a += Iethrisk[ethrisk];
	a += Ismoke[smoke_cat];

	/* Sum from continuous values */

    a += age_1 * 4.4417863976316578000000000;
    a += age_2 * 0.0281637210672999180000000;
    a += bmi_1 * 0.8942365304710663300000000;
    a += bmi_2 * -6.5748047596104335000000000;
    a += rati * 0.1433900561621420900000000;
    a += sbp * 0.0128971795843613720000000;
    a += town * 0.0664772630011438850000000;

	/* Sum from boolean values */

    a += b_AF * 1.6284780236484424000000000;
    a += b_ra * 0.2901233104088770700000000;
    a += b_renal * 1.0043796680368302000000000;
    a += b_treatedhyp * 0.6180430562788129500000000;
    a += b_type1 * 1.8400348250874599000000000;
    a += b_type2 * 1.1711626412196512000000000;
    a += fh_cvd * 0.5147261203665195500000000;

	/* Sum from interaction terms */

    a += age_1 * (smoke_cat == 1 ? 1 : 0) * 0.7464406144391666500000000;
    a += age_1 * (smoke_cat == 2 ? 1 : 0) * 0.2568541711879666600000000;
    a += age_1 * (smoke_cat == 3 ? 1 : 0) * -1.5452226707866523000000000;
    a += age_1 * (smoke_cat == 4 ? 1 : 0) * -1.7113013709043405000000000;
    a += age_1 * b_AF * -7.0177986441269269000000000;
    a += age_1 * b_renal * -2.9684019256454390000000000;
    a += age_1 * b_treatedhyp * -4.2219906452967848000000000;
    a += age_1 * b_type1 * 1.6835769546040080000000000;
    a += age_1 * b_type2 * -2.9371798540034648000000000;
    a += age_1 * bmi_1 * 0.1797196207044682300000000;
    a += age_1 * bmi_2 * 40.2428166760658140000000000;
    a += age_1 * fh_cvd * 0.1439979240753906700000000;
    a += age_1 * sbp * -0.0362575233899774460000000;
    a += age_1 * town * 0.3735138031433442600000000;
    a += age_2 * (smoke_cat == 1 ? 1 : 0) * -0.1927057741748231000000000;
    a += age_2 * (smoke_cat == 2 ? 1 : 0) * -0.1526965063458932700000000;
    a += age_2 * (smoke_cat == 3 ? 1 : 0) * 0.2313563976521429400000000;
    a += age_2 * (smoke_cat == 4 ? 1 : 0) * 0.2307165013868296700000000;
    a += age_2 * b_AF * 1.1395776028337732000000000;
    a += age_2 * b_renal * 0.4356963208330940600000000;
    a += age_2 * b_treatedhyp * 0.7265947108887239600000000;
    a += age_2 * b_type1 * -0.6320977766275653900000000;
    a += age_2 * b_type2 * 0.4023270434871086800000000;
    a += age_2 * bmi_1 * 0.1319276622711877700000000;
    a += age_2 * bmi_2 * -7.3211322435546409000000000;
    a += age_2 * fh_cvd * -0.1330260018273720400000000;
    a += age_2 * sbp * 0.0045842850495397955000000;
    a += age_2 * town * -0.0952370300845990780000000;

	/* Calculate the score itself */
	double score = 100.0 * (1 - Math.Pow(survivor[surv], Math.Exp(a)) );
	return score;
}

       public static double calcQRISK_M(
int age,int b_AF,int b_ra,int b_renal,int b_treatedhyp,int b_type1,int b_type2,double bmi,int ethrisk,int fh_cvd,double rati,double sbp,int smoke_cat,int surv,double town
)
{
	double[] survivor = new double[16]{
		0,
		0.998205721378326,
		0.996380507946014,
		0.994511783123016,
		0.992520809173584,
		0.990338504314423,
		0.988223373889923,
		0.986053407192230,
		0.983751118183136,
		0.981297850608826,
		0.978794217109680,
		0.976175725460052,
		0.973336458206177,
		0.970354199409485,
		0.967230498790741,
		0.963918387889862
	};

	/* The conditional arrays */

	double[] Iethrisk = new double[10]{
		0,
		0,
		0.3173321430481919100000000,
		0.4738590786081115500000000,
		0.5171314655968145500000000,
		0.1370301157366419200000000,
		-0.3885522304972663900000000,
		-0.3812495485312194500000000,
		-0.4064461381650994500000000,
		-0.2285715521377336100000000
	};
    double[] Ismoke = new double[5]{
		0,
		0.2684479158158020200000000,
		0.6307674973877591700000000,
		0.7178078883378695700000000,
		0.8704172533465485100000000
	};

	/* Applying the fractional polynomial transforms */
	/* (which includes scaling)                      */

	double dage = age;
	dage=dage/10;
	double age_1 = Math.Pow(dage,-1);
	double age_2 = Math.Pow(dage,2);
	double dbmi = bmi;
	dbmi=dbmi/10;
	double bmi_1 = Math.Pow(dbmi,-2);
	double bmi_2 = Math.Pow(dbmi,-2)*Math.Log(dbmi);

	/* Centring the continuous variables */

    age_1 = age_1 - 0.233734160661697;
    age_2 = age_2 - 18.304403305053711;
    bmi_1 = bmi_1 - 0.146269768476486;
    bmi_2 = bmi_2 - 0.140587374567986;
    rati = rati - 4.321151256561279;
    sbp = sbp - 130.589752197265620;
    town = town - 0.551009356975555;

	/* Start of Sum */
	double a=0;

	/* The conditional sums */

	a += Iethrisk[ethrisk];
	a += Ismoke[smoke_cat];

	/* Sum from continuous values */

    a += age_1 * -18.0437312550377270000000000;
    a += age_2 * 0.0236486454254306940000000;
    a += bmi_1 * 2.5388084343581578000000000;
    a += bmi_2 * -9.1034725871528597000000000;
    a += rati * 0.1684397636136909500000000;
    a += sbp * 0.0105003089380754820000000;
    a += town * 0.0323801637634487590000000;

	/* Sum from boolean values */

    a += b_AF * 1.0363048000259454000000000;
    a += b_ra * 0.2519953134791012600000000;
    a += b_renal * 0.8359352886995286000000000;
    a += b_treatedhyp * 0.6603459695917862600000000;
    a += b_type1 * 1.3309170433446138000000000;
    a += b_type2 * 0.9454348892774417900000000;
    a += fh_cvd * 0.5986037897136281500000000;

	/* Sum from interaction terms */

    a += age_1 * (smoke_cat == 1 ? 1 : 0) * 0.6186864699379683900000000;
    a += age_1 * (smoke_cat == 2 ? 1 : 0) * 1.5522017055600055000000000;
    a += age_1 * (smoke_cat == 3 ? 1 : 0) * 2.4407210657517648000000000;
    a += age_1 * (smoke_cat == 4 ? 1 : 0) * 3.5140494491884624000000000;
    a += age_1 * b_AF * 8.0382925558108482000000000;
    a += age_1 * b_renal * -1.6389521229064483000000000;
    a += age_1 * b_treatedhyp * 8.4621771382346651000000000;
    a += age_1 * b_type1 * 5.4977016563835504000000000;
    a += age_1 * b_type2 * 3.3974747488766690000000000;
    a += age_1 * bmi_1 * 33.8489881012767600000000000;
    a += age_1 * bmi_2 * -140.6707025404897100000000000;
    a += age_1 * fh_cvd * 2.0858333154353321000000000;
    a += age_1 * sbp * 0.0501283668830720540000000;
    a += age_1 * town * -0.1988268217186850700000000;
    a += age_2 * (smoke_cat == 1 ? 1 : 0) * -0.0040893975066796338000000;
    a += age_2 * (smoke_cat == 2 ? 1 : 0) * -0.0056065852346001768000000;
    a += age_2 * (smoke_cat == 3 ? 1 : 0) * -0.0018261006189440492000000;
    a += age_2 * (smoke_cat == 4 ? 1 : 0) * -0.0014997157296173290000000;
    a += age_2 * b_AF * 0.0052471594895864343000000;
    a += age_2 * b_renal * -0.0179663586193546390000000;
    a += age_2 * b_treatedhyp * 0.0092088445323379176000000;
    a += age_2 * b_type1 * 0.0047493510223424558000000;
    a += age_2 * b_type2 * -0.0048113775783491563000000;
    a += age_2 * bmi_1 * 0.0627410757513945650000000;
    a += age_2 * bmi_2 * -0.2382914909385732100000000;
    a += age_2 * fh_cvd * -0.0049971149213281010000000;
    a += age_2 * sbp * -0.0000523700987951435090000;
    a += age_2 * town * -0.0012518116569283104000000;

	/* Calculate the score itself */
	double score = 100.0 * (1 - Math.Pow(survivor[surv], Math.Exp(a)) );
	return score;
}

    }
}
