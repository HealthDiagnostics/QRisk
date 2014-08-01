using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaryRiskCalculator
{
   public static class QRisk2_2013
    {

       public const string VERSION = "QRISK2-2013";


       public static double calcQRISK_F(int age, int b_AF, int b_ra, int b_renal, int b_treatedhyp, int b_type1, int b_type2, double bmi,
           int ethrisk, int fh_cvd, double rati, double sbp, int smoke_cat, int surv, double town)
        {
	         var survivor = new double[16]{
		        0,
		        0,
		        0,
		        0,
		        0,
		        0,
		        0,
		        0,
		        0,
		        0,
		        0.988779306411743,
		        0,
		        0,
		        0,
		        0,
		        0
	        };

	        /* The conditional arrays */

	        var Iethrisk = new double[10]  {
		        0,
		        0,
		        0.3247649848349589700000000,
		        0.6490919016600146300000000,
		        0.3225960666395069100000000,
		        -0.0698843828816220060000000,
		        -0.0526174237973003890000000,
		        -0.3583620628676293400000000,
		        -0.4631969844038496000000000,
		        -0.0934620942542618430000000
	        };
	        var Ismoke = new double[5]{
		        0,
		        0.2325777856801529700000000,
		        0.5540189636820945800000000,
		        0.6903408593375299800000000,
		        0.8853278749891940700000000
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

	        age_1 = age_1 - 2.111304044723511;
	        age_2 = age_2 - 4.457604408264160;
	        bmi_1 = bmi_1 - 0.153318107128143;
	        bmi_2 = bmi_2 - 0.143754154443741;
	        rati = rati - 3.597785472869873;
	        sbp = sbp - 126.525978088378910;
	        town = town - -0.099030286073685;

	        /* Start of Sum */
	        double a=0;

	        /* The conditional sums */

	        a += Iethrisk[ethrisk];
	        a += Ismoke[smoke_cat];

	        /* Sum from continuous values */

	        a += age_1 * 4.1924277678057837000000000;
	        a += age_2 * 0.0727365264473135150000000;
	        a += bmi_1 * -0.4914322358663353900000000;
	        a += bmi_2 * -2.9736893891126503000000000;
	        a += rati * 0.1456741893144524700000000;
	        a += sbp * 0.0124301977948376370000000;
	        a += town * 0.0638839291971372570000000;

	        /* Sum from boolean values */

	        a += b_AF * 1.2548823570386274000000000;
	        a += b_ra * 0.3660166445401525400000000;
	        a += b_renal * 0.8000369779764396900000000;
	        a += b_treatedhyp * 0.6183822524814552900000000;
	        a += b_type1 * 1.7623106103850039000000000;
	        a += b_type2 * 1.0714795465634313000000000;
	        a += fh_cvd * 0.6138914873273221300000000;

	        /* Sum from interaction terms */         
            //a += age_1 * (smoke_cat == 1) * 1.5288149154981097000000000;
            //a += age_1 * (smoke_cat == 2) * 1.8932677924373249000000000;
            //a += age_1 * (smoke_cat == 3) * -0.2120377720167573300000000;
            //a += age_1 * (smoke_cat == 4) * -1.8022626575074350000000000;

            if ((smoke_cat == 1))
                a += age_1 * 1 * 1.5288149154981097000000000;
            if ((smoke_cat == 2))
                a += age_1 * 1 * 1.8932677924373249000000000;
            if ((smoke_cat == 3))
                a += age_1 * 1 * -0.2120377720167573300000000;
            if ((smoke_cat == 4))
                a += age_1 * 1 * -1.8022626575074350000000000;

	        a += age_1 * b_AF * -2.0489636167088636000000000;
	        a += age_1 * b_renal * 2.1378812069259072000000000;
	        a += age_1 * b_treatedhyp * -3.9467205554873872000000000;
	        a += age_1 * b_type1 * 5.0295952006040174000000000;
	        a += age_1 * b_type2 * -4.1006039491910871000000000;
	        a += age_1 * bmi_1 * 15.6869018580842020000000000;
	        a += age_1 * bmi_2 * 10.5172051502483440000000000;
	        a += age_1 * fh_cvd * 0.1788021490217178200000000;
	        a += age_1 * sbp * 0.0035841283715529319000000;
	        a += age_1 * town * 0.2643549142624118100000000;

            if ((smoke_cat == 1))
                a += age_2 * 1 * -0.3515526357562532800000000;
            if ((smoke_cat == 2))
                a += age_2 * 1 * -0.4673806143887411200000000;
            if ((smoke_cat == 3))
                a += age_2 * 1 * -0.0520627514443069360000000;
            if ((smoke_cat == 4))
                a += age_2 * 1 * 0.2408148624618006200000000;

	        a += age_2 * b_AF * 0.1989800041198926100000000;
	        a += age_2 * b_renal * -0.5666471358116902400000000;
	        a += age_2 * b_treatedhyp * 0.6659359807539360100000000;
	        a += age_2 * b_type1 * -1.3790306447153591000000000;
	        a += age_2 * b_type2 * 0.6477002677213917800000000;
	        a += age_2 * bmi_1 * -3.0654336861574962000000000;
	        a += age_2 * bmi_2 * -1.0265341871793443000000000;
	        a += age_2 * fh_cvd * -0.1598294041246048900000000;
	        a += age_2 * sbp * -0.0037836254753488216000000;
	        a += age_2 * town * -0.0731237907079357600000000;

	        /* Calculate the score itself */
            double score = 100.0 * (1 - Math.Pow(survivor[surv], Math.Exp(a)));
	        return score;
        }


        public static double calcQRISK_M(int age, int b_AF, int b_ra, int b_renal, int b_treatedhyp, int b_type1, int b_type2, double bmi,
           int ethrisk,int fh_cvd,double rati,double sbp,int smoke_cat,int surv,double town)
        {
	        var survivor = new double[16] {
		        0,
		        0,
		        0,
		        0,
		        0,
		        0,
		        0,
		        0,
		        0,
		        0,
		        0.978206992149353,
		        0,
		        0,
		        0,
		        0,
		        0
	        };

	        /* The conditional arrays */

	        var Iethrisk = new double[10]{
		        0,
		        0,
		        0.2455190496467173600000000,
		        0.5660101891908698700000000,
		        0.5071774786941407600000000,
		        0.1389394355409972500000000,
		        -0.3741460040971635900000000,
		        -0.4569877668572746000000000,
		        -0.3980300277796868800000000,
		        -0.2458314247118184900000000
	        };
	        var Ismoke = new double[5]  {
		        0,
		        0.2733747359324866800000000,
		        0.5613178495903169400000000,
		        0.6862483380221766600000000,
		        0.8089648612880009400000000
	        };

	        /* Applying the fractional polynomial transforms */
	        /* (which includes scaling)                      */

	        double dage = age;
	        dage=dage/10;
	        double age_1 = Math.Pow(dage,-1);
            double age_2 = Math.Pow(dage, 2);
	        double dbmi = bmi;
	        dbmi=dbmi/10;
            double bmi_1 = Math.Pow(dbmi, -2);
            double bmi_2 = Math.Pow(dbmi, -2) * Math.Log(dbmi);


	        /* Centring the continuous variables */

	        age_1 = age_1 - 0.229260012507439;
	        age_2 = age_2 - 19.025821685791016;
	        bmi_1 = bmi_1 - 0.146091341972351;
	        bmi_2 = bmi_2 - 0.140505045652390;
	        rati = rati - 4.400725841522217;
	        sbp = sbp - 131.575469970703120;
	        town = town - 0.002737425966188;

	        /* Start of Sum */
	        double a=0;

	        /* The conditional sums */

	        a += Iethrisk[ethrisk];
	        a += Ismoke[smoke_cat];

	        /* Sum from continuous values */

	        a += age_1 * -19.4666173334122840000000000;
	        a += age_2 * 0.0201364267507625730000000;
	        a += bmi_1 * 1.3830867611940247000000000;
	        a += bmi_2 * -7.1627340311445842000000000;
	        a += rati * 0.1533097330217813300000000;
	        a += sbp * 0.0099543428482831708000000;
	        a += town * 0.0320608645752168620000000;

	        /* Sum from boolean values */

	        a += b_AF * 0.8561770660317954400000000;
	        a += b_ra * 0.3295853885133138700000000;
	        a += b_renal * 0.7661782832063270800000000;
	        a += b_treatedhyp * 0.6570994445804946300000000;
	        a += b_type1 * 1.2235901893205057000000000;
	        a += b_type2 * 0.8201160328780074900000000;
	        a += fh_cvd * 0.6962022338056947900000000;

	        /* Sum from interaction terms */
            //a += age_1 * (smoke_cat == 1) * 0.7675246688050250100000000;
            //a += age_1 * (smoke_cat == 2) * 0.7287918369962067500000000;
            //a += age_1 * (smoke_cat == 3) * 3.2938032311654148000000000;
            //a += age_1 * (smoke_cat == 4) * 3.6962686629926185000000000;

            if ((smoke_cat == 1))
                a += age_1 * 1 * 0.7675246688050250100000000;
            if ((smoke_cat == 2))
                a += age_1 * 1 * 0.7287918369962067500000000;
            if ((smoke_cat == 3))
                a += age_1 * 1 * 3.2938032311654148000000000;
            if ((smoke_cat == 4))
                a += age_1 * 1 * 3.6962686629926185000000000;
            
	        a += age_1 * b_AF * 6.8279483425030065000000000;
	        a += age_1 * b_renal * -1.2499862850572443000000000;
	        a += age_1 * b_treatedhyp * 6.8793023059229004000000000;
	        a += age_1 * b_type1 * 2.5566900730730104000000000;
	        a += age_1 * b_type2 * 2.5043645216873411000000000;
	        a += age_1 * bmi_1 * 46.1982485629838000000000000;
	        a += age_1 * bmi_2 * -169.4442164713507000000000000;
	        a += age_1 * fh_cvd * 2.5891954185540058000000000;
	        a += age_1 * sbp * 0.0328765577798371750000000;
	        a += age_1 * town * -0.0039873285944521386000000;


            if ((smoke_cat == 1))
                a += age_2 * 1 * -0.0028190973036285923000000;
            if ((smoke_cat == 2))
                a += age_2 * 1 * -0.0074290549312194289000000;
            if ((smoke_cat == 3))
                a += age_2 * 1 * 0.0006623943592407952700000;
            if ((smoke_cat == 4))
                a += age_2 * 1 * -0.0009539165087934006100000;


	        a += age_2 * b_AF * 0.0051545540015502968000000;
	        a += age_2 * b_renal * -0.0190484751944149380000000;
	        a += age_2 * b_treatedhyp * 0.0042514995185120022000000;
	        a += age_2 * b_type1 * -0.0029641180896136064000000;
	        a += age_2 * b_type2 * -0.0049052623533052224000000;
	        a += age_2 * bmi_1 * 0.1174445082068387000000000;
	        a += age_2 * bmi_2 * -0.3493064079001478300000000;
	        a += age_2 * fh_cvd * -0.0059689380584026352000000;
	        a += age_2 * sbp * -0.0001231469495691592500000;
	        a += age_2 * town * -0.0008493525147140088800000;

	        /* Calculate the score itself */
	        double score = 100.0 * (1 - Math.Pow(survivor[surv], Math.Exp(a)) );
	        return score;
        }

    }
}
