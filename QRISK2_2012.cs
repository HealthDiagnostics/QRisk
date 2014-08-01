using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaryRiskCalculator
{
    /// <summary>
    /// QRISK2-2012 Open source calculator.
    /// A combined library developed for .NET from the open source project available at
    /// http://svn.clinrisk.co.uk/opensource/qrisk2/
    /// </summary>
    public class QRISK2_2012 : QRISK
    {/// <summary>
        /// The QRISK version number
        /// </summary>
        public const string VERSION = "QRISK2-2012";
        /// <summary>
        /// Survivor function for Males
        /// </summary>
        private double[] survivor_M = {
		    0,
		    0.996994316577911,
		    0.993941843509674,
		    0.990756928920746,
		    0.987367391586304,
		    0.983686089515686,
		    0.979941785335541,
		    0.975925624370575,
		    0.971652328968048,
		    0.967043697834015,
		    0.962060630321503,
		    0.956596851348877,
		    0.951202630996704,
		    0.945618212223053,
		    0.938979923725128,
		    0.932796955108643
	    };
        /// <summary>
        /// Survivor function for Females
        /// </summary>
        private double[] survivor_F = {
			0,
	        0.998272597789764,
	        0.996482193470001,
	        0.994584262371063,
	        0.992607414722443,
	        0.990407526493073,
	        0.988208174705505,
	        0.985817015171051,
	        0.983284771442413,
	        0.980540335178375,
	        0.977537572383881,
	        0.974311947822571,
	        0.971061646938324,
	        0.967655360698700,
	        0.963695406913757,
	        0.959843873977661
	    };

        /// <summary>
        /// Female Smoking co-efficients
        /// </summary>
        private double[] iSmoke_F = {
     	0,                                  //Non smoker
		0.2033232689326185900000000,        //Ex smoker
		0.4820416413827907100000000,        //Light smokers < 10
		0.6126017502669054400000000,        //Moderate smokers 10 - 19
		0.7481362046722807000000000         //Heavy smokers > 20
	};
        /// <summary>
        /// Male Smoking co-efficients
        /// </summary>
        private double[] iSmoke_M = {
		0,                                  //Non smoker
		0.2684290047224652300000000,        //Ex smoker
		0.5004920802983664500000000,        //Light smokers < 10
		0.6374758655070897900000000,        //Moderate smokers 10 - 19
		0.7423631397556198500000000         //Heavy smokers > 20
	};

        /// <summary>
        /// Ethnicity transform Male
        /// </summary>
        private double[] ethRisk_M = {
            0,                              //Not recorded
            0,                              //White
            0.3163117410944710200000000,    //Indian
            0.6091879104882792500000000,    //Pakistani
            0.5958436006239369300000000,    //Bangladeshi
            0.1142285841316822300000000,    //Other Asian
            -0.3489215674318830200000000,   //Black Caribbean
            -0.3603571055094617600000000,   //Black African
            -0.2666173260881414300000000,   //Chinese
            -0.1208018847857646500000000    //Other ethnic group      
	    };

        /// <summary>
        /// Ethnicity transform Female
        /// </summary>
        private double[] ethRisk_F = {
            0,                                 //Not recorded
		    0,                                 //White
		    0.2163282093750823500000000,       //Indian
		    0.6904687508186858600000000,       //Pakistani
		    0.3422685053345692600000000,       //Bangladeshi
		    0.0731008288350808310000000,       //Other Asian
		    -0.0989396610852535660000000,      //Black Caribbean
		    -0.2352321178062382600000000,      //Black African
		    -0.2956316192158425800000000,      //Chinese
		    -0.1010123741730201800000000       //Other ethnic group
	    };

        /// <summary>
        /// QRISK2 calculator for Males
        /// </summary>
        /// <param name="age">Patient age</param>
        /// <param name="bmi">Patient BMI</param>
        /// <param name="townsend">Patient Townsend score</param>
        /// <param name="sysBP">Pateint Systolic BP</param>
        /// <param name="ratio">Patient TC/HDL ratio</param>
        /// <param name="fh">Whether patient has FH of CHD in ist degree relative under 60</param>
        /// <param name="hist_cvd">Whether patirnt has a history of CVD</param>
        /// <param name="smoker">Patient smoking status</param>
        /// <param name="hyp">Whether patient is being treated for hypertension</param>
        /// <param name="type2">Whether patient has type2 diabetes</param>
        /// <param name="af">Whether patient has been diagnosed with Atrial Fibulation</param>
        /// <param name="ra">Whether patient has Rhumatoid Arthritis</param>
        /// <param name="ckd">Whether patient has Chronic Kidney disease</param>
        /// <param name="ethnicity">Patient ethnicity</param>
        /// <returns>QRISK score. Needs converting to a percentage</returns>
        public override double calcQRISK_M(int age, double bmi, double townsend, double sysBP, double ratio, int fh, int hist_cvd, int smoker, int hyp, int type2, int af, int ra, int ckd, int ethnicity, int survivor)
        {

            /* Applying the fractional polynomial transforms */
            /* (which includes scaling)    */
            double dage = (double)age;
            dage = dage / 10;
            double age_1 = Math.Pow(dage, -2);
            double age_2 = Math.Pow(dage, -2) * Math.Log(dage);
            double dbmi = bmi;
            dbmi = dbmi / 10;
            double bmi_1 = Math.Log(dbmi);

            /* Centring the continuous variables */

            age_1 = age_1 - 0.044809196144342;
            age_2 = age_2 - 0.069573938846588;
            bmi_1 = bmi_1 - 0.970408320426941;
            ratio = ratio - 4.462830543518066;
            sysBP = sysBP - 133.357818603515620;
            townsend = townsend - -0.356812149286270;

            /* Start of Sum */
            double a = 0;

            /* The conditional sums */

            a += ethRisk_M[ethnicity];
            a += iSmoke_M[smoker];

            /* Sum from continuous values */

            a += age_1 * 47.3163921673270660000000000;
            a += age_2 * -101.2362163621992300000000000;
            a += bmi_1 * 0.5424702715527407500000000;
            a += ratio * 0.1442545616564490900000000;
            a += sysBP * 0.0080703627452139003000000;
            a += townsend * 0.0364817567442078170000000;

            /* Sum from boolean values */

            a += af * 0.7546727404172400800000000;
            a += ra * 0.3089197610059550800000000;
            a += ckd * 0.7441080499101914200000000;
            a += hyp * 0.4978222402431560800000000;
            a += type2 * 0.7776278619904274400000000;
            a += fh * 0.6965155800196054500000000;

            /* Sum from interaction terms */
            switch (smoker)
            {
                case 1:
                    a += age_1 * -3.8805330901643544000000000;
                    break;
                case 2:
                    a += age_1 * -16.7031934095781480000000000;
                    break;
                case 3:
                    a += age_1 * -15.3737691868177200000000000;
                    break;
                case 4:
                    a += age_1 * -17.6452952709561190000000000;
                    break;
            }
            a += age_1 * af * -7.0282038602850179000000000;
            a += age_1 * ckd * -17.0146402956784510000000000;
            a += age_1 * hyp * 33.9625265583895200000000000;
            a += age_1 * type2 * 12.7885598573212390000000000;
            a += age_1 * bmi_1 * 3.2680340443282705000000000;
            a += age_1 * fh * -17.9219200079209140000000000;
            a += age_1 * sysBP * -0.1511050142259400800000000;
            a += age_1 * townsend * -2.5502442789058666000000000;
            switch (smoker)
            {
                case 1:
                    a += age_2 * 7.9708926855593845000000000;
                    break;
                case 2:
                    a += age_2 * 23.6859391977921480000000000;
                    break;
                case 3:
                    a += age_2 * 23.1370772208804420000000000;
                    break;
                case 4:
                    a += age_2 * 26.8673734647379380000000000;
                    break;
            }

            a += age_2 * af * 14.4518354362729100000000000;
            a += age_2 * ckd * 28.2702331053646850000000000;
            a += age_2 * hyp * -18.8166910013564140000000000;
            a += age_2 * type2 * 0.9629882124640055700000000;
            a += age_2 * bmi_1 * 10.5513753399433020000000000;
            a += age_2 * fh * 26.6046853897055510000000000;
            a += age_2 * sysBP * 0.2910870792157880200000000;
            a += age_2 * townsend * 3.0069917093360048000000000;

            /* Calculate the score itself */
            double score = 100.0 * (1 - Math.Pow(survivor_M[survivor], Math.Exp(a)));
            return score;
        }

        /// <summary>
        /// QRISK2 Calculation for Females
        /// </summary>
        /// <param name="age">Patient age</param>
        /// <param name="bmi">Patient BMI</param>
        /// <param name="townsend">Patient Townsend score</param>
        /// <param name="sysBP">Pateint Systolic BP</param>
        /// <param name="ratio">Patient TC/HDL ratio</param>
        /// <param name="fh">Whether patient has FH of CHD in ist degree relative under 60</param>
        /// <param name="hist_cvd">Whether patirnt has a history of CVD</param>
        /// <param name="smoker">Patient smoking status</param>
        /// <param name="hyp">Whether patient is being treated for hypertension</param>
        /// <param name="type2">Whether patient has type2 diabetes</param>
        /// <param name="af">Whether patient has been diagnosed with Atrial Fibulation</param>
        /// <param name="ra">Whether patient has Rhumatoid Arthritis</param>
        /// <param name="ckd">Whether patient has Chronic Kidney disease</param>
        /// <param name="ethnicity">Patient ethnicity</param>
        /// <param name="survivor"></param>
        /// <returns>QRISK score. Needs converting to a percentage</returns>
        public override double calcQRISK_F(int age, double bmi, double townsend, double sysBP, double ratio, int fh, int hist_cvd, int smoker, int hyp, int type2, int af, int ra, int ckd, int ethnicity, int survivor)
        {
            /* Applying the fractional polynomial transforms */
            /* (which includes scaling)                      */

            double dage = age;
            dage = dage / 10;
            double age_1 = Math.Pow(dage, 0.5);
            double age_2 = Math.Pow(dage, 2);
            double dbmi = bmi;
            dbmi = dbmi / 10;
            double bmi_1 = Math.Pow(dbmi, .5);

            /* Centring the continuous variables */

            age_1 = age_1 - 2.214729070663452;
            age_2 = age_2 - 24.059265136718750;
            bmi_1 = bmi_1 - 1.606468081474304;
            ratio = ratio - 3.708646535873413;
            sysBP = sysBP - 129.990249633789060;
            townsend = townsend - -0.485970288515091;

            /* Start of Sum */
            double a = 0;

            /* The conditional sums */

            a += ethRisk_F[ethnicity];
            a += iSmoke_F[smoker];

            /* Sum from continuous values */

            a += age_1 * 5.0327322737460403000000000;
            a += age_2 * -0.0108190285560098470000000;
            a += bmi_1 * 0.4723685118804857300000000;
            a += ratio * 0.1325846978117864200000000;
            a += sysBP * 0.0105865933893311400000000;
            a += townsend * 0.0597458366155125960000000;

            /* Sum from boolean values */

            a += af * 1.3260722293533254000000000;
            a += ra * 0.3626145113296154700000000;
            a += ckd * 0.7636191853466424200000000;
            a += hyp * 0.5420886736363689200000000;
            a += type2 * 0.8939864249454413400000000;
            a += fh * 0.5997157809513348200000000;

            /* Sum from interaction terms */
            switch (smoker)
            {
                case 1:
                    a += age_1 * 0.1773773580667799300000000;
                    break;     
                case 2:        
                    a += age_1 * -0.3277435393269309500000000;
                    break;     
                case 3:        
                    a += age_1 * -1.1532771847454322000000000;
                    break;     
                case 4:        
                    a += age_1 * -1.5396928677197224000000000;
                    break;
            }
            a += age_1 * af * -4.6084283441445892000000000;
            a += age_1 * ckd * -2.6400602815968979000000000;
            a += age_1 * hyp * -2.2479789428920025000000000;
            a += age_1 * type2 * -1.8452128842162694000000000;
            a += age_1 * bmi_1 * -3.0850905786159868000000000;
            a += age_1 * fh * -0.2480779144692392100000000;
            a += age_1 * sysBP * -0.0132453692890105240000000;
            a += age_1 * townsend * -0.0369396566313616930000000;
            switch (smoker)
            {
                case 1:
                    a += age_2 * -0.0051475082686792311000000;
                    break;     
                case 2:        
                    a += age_2 * -0.0005319773104753907200000;
                    break;     
                case 3:        
                    a += age_2 * 0.0105091708474237890000000;
                    break;     
                case 4:        
                    a += age_2 * 0.0154782037050560930000000;
                    break;
            }
            a += age_2 * af * 0.0507454178844450110000000;
            a += age_2 * ckd * 0.0343259130322268740000000;
            a += age_2 * hyp * 0.0257783591608558020000000;
            a += age_2 * type2 * 0.0179772109650001910000000;
            a += age_2 * bmi_1 * 0.0345004560510129770000000;
            a += age_2 * fh * -0.0062437412993034047000000;
            a += age_2 * sysBP * -0.0000295094907709919760000;
            a += age_2 * townsend * -0.0010621576948374419000000;

            /* Calculate the score itself */
            double score = 100.0 * (1 - Math.Pow(survivor_F[survivor], Math.Exp(a)));
            return score;
        }
    }
}
