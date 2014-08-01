using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaryRiskCalculator
{
    /// <summary>
    /// QRISK2-2011 Open source calculator.
    /// A combined library developed for .NET from the open source project available at
    /// http://svn.clinrisk.co.uk/opensource/qrisk2/
    /// </summary>
    public class QRISK2 : QRISK
    {
        /// <summary>
        /// The QRISK version number
        /// </summary>
        public const string VERSION = "QRISK2-2011";
        /// <summary>
        /// Survivor function for Males
        /// </summary>
        private double[] survivor_M = {
		0,
		0.997251808643341,
		0.994411587715149,
		0.991577506065369,
		0.988431155681610,
		0.985084950923920,
		0.981717884540558,
		0.978166103363037,
		0.974576294422150,
		0.970824301242828,
		0.966860234737396,
		0.962876856327057,
		0.958815157413483,
		0.954597651958466,
		0.950800955295563,
		0.946760058403015
	    };
        /// <summary>
        /// Survivor function for Females
        /// </summary>
        private double[] survivor_F = {
		    0,
		    0.998361468315125,
		    0.996678650379181,
		    0.994997859001160,
		    0.993098974227905,
		    0.991098940372467,
		    0.989080786705017,
		    0.986959576606750,
		    0.984819591045380,
		    0.982504785060883,
		    0.980139017105103,
		    0.977676749229431,
		    0.975248038768768,
		    0.972871840000153,
		    0.970530509948730,
		    0.968057155609131

	    };

        /// <summary>
        /// Female Smoking co-efficients
        /// </summary>
        private double[] iSmoke_F = {
		0,                                                 //Non smoker
		0.2327702810549802900000000,        //Ex smoker
		0.4875584615635800100000000,        //Light smokers < 10
		0.6277834520129398400000000,        //Moderate smokers 10 - 19
		0.7659309359835262400000000         //Heavy smokers > 20
	};
        /// <summary>
        /// Male Smoking co-efficients
        /// </summary>
        private double[] iSmoke_M = {
		0,                                                 //Non smoker
		0.2682688391611277600000000,        //Ex smoker
		0.5042185439341627700000000,        //Light smokers < 10
		0.6186559642078698400000000,        //Moderate smokers 10 - 19
		0.7584809371310703400000000         //Heavy smokers > 20
	};

        /// <summary>
        /// Ethnicity transform Male
        /// </summary>
        private double[] ethRisk_M = {
		    0,                                                  //Not recorded
		    0,                                                  //White
		    0.2951201722226509100000000,        //Indian
		    0.5774536084141955700000000,        //Pakistani
		    0.5856789051039356100000000,        //Bangladeshi
		    0.1557364124244510200000000,        //Other Asian
		    -0.4239684807026608000000000,       //Black Caribbean
		    -0.5298535279109863900000000,       //Black African
		    -0.3409560555106792600000000,       //Chinese
		    -0.2547523360677878000000000        //Other ethnic group
	    };

        /// <summary>
        /// Ethnicity transform Female
        /// </summary>
        private double[] ethRisk_F = {
		    0,                                                  //Not recorded
		    0,                                                  //White
		    0.2701997485015847400000000,        //Indian
		    0.5849925816222197900000000,        //Pakistani
		    0.2928293437254873600000000,        //Bangladeshi
		    0.0457169598790452620000000,        //Other Asian
		    -0.0941087619983587700000000,       //Black Caribbean
		    -0.5515362108414120200000000,       //Black African
		    -0.3276371733521620800000000,       //Chinese
		    -0.1332541744745928700000000        //Other ethnic group
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

            age_1 = age_1 - 0.044995188713074;
            age_2 = age_2 - 0.069769531488419;
            bmi_1 = bmi_1 - 0.967867195606232;
            ratio = ratio - 4.458122253417969;
            sysBP = sysBP - 133.248199462890620;
            townsend = townsend - -0.164980158209801;

            /* Start of Sum */
            double a = 0;

            /* The conditional sums */

            a += ethRisk_M[ethnicity];
            a += iSmoke_M[smoker];

            /* Sum from continuous values */

            a += age_1 * 47.4409231432571520000000000;
            a += age_2 * -103.9694778548133500000000000;
            a += bmi_1 * 0.4454212658879678200000000;
            a += ratio * 0.1531415988987079000000000;
            a += sysBP * 0.0086481101655897820000000;
            a += townsend * 0.0326616854962242380000000;

            /* Sum from boolean values */

            a += af * 0.6724975345708518200000000;
            a += ra * 0.2844675803629553900000000;
            a += ckd * 0.7934428384714343800000000;
            a += hyp * 0.5501928673194242900000000;
            a += type2 * 0.8102736062476685300000000;
            a += fh * 0.7509625254916093600000000;

            /* Sum from interaction terms */
            switch (smoker)
            {
                case 1:
                    a += age_1 * -1.8114868938741493000000000;
                    break;
                case 2:
                    a += age_1 * -14.0178925355489450000000000;
                    break;
                case 3:
                    a += age_1 * -12.2971507354086000000000000;
                    break;
                case 4:
                    a += age_1 * -9.5995358645940403000000000;
                    break;
            }
            a += age_1 * af * -29.9229283009898380000000000;
            a += age_1 * ckd * -55.5547457814475080000000000;
            a += age_1 * hyp * 31.0186452463243260000000000;
            a += age_1 * type2 * -19.8796087944702440000000000;
            a += age_1 * bmi_1 * 16.3437082809787420000000000;
            a += age_1 * fh * -26.3175914239316240000000000;
            a += age_1 * sysBP * -0.2288663778369489900000000;
            a += age_1 * townsend * -2.8512030583797006000000000;
            switch (smoker)
            {
                case 1:
                    a += age_2 * 6.3639267384563913000000000;
                    break;
                case 2:
                    a += age_2 * 19.3756175012057240000000000;
                    break;
                case 3:
                    a += age_2 * 18.8415645049194790000000000;
                    break;
                case 4:
                    a += age_2 * 19.7059409243283880000000000;
                    break;
            }

            a += age_2 * af * 31.1557572626584510000000000;
            a += age_2 * ckd * 59.0028081909139600000000000;
            a += age_2 * hyp * -14.6191239386960210000000000;
            a += age_2 * type2 * 29.2015783029565630000000000;
            a += age_2 * bmi_1 * -2.9965983324865419000000000;
            a += age_2 * fh * 35.6829323968598970000000000;
            a += age_2 * sysBP * 0.3647340147196024300000000;
            a += age_2 * townsend * 3.3173634478389573000000000;

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

            age_1 = age_1 - 2.212557792663574;
            age_2 = age_2 - 23.965053558349609;
            bmi_1 = bmi_1 - 1.605302810668945;
            ratio = ratio - 3.710259437561035;
            sysBP = sysBP - 129.842712402343750;
            townsend = townsend - -0.301369071006775;

            /* Start of Sum */
            double a = 0;

            /* The conditional sums */

            a += ethRisk_F[ethnicity];
            a += iSmoke_F[smoker];

            /* Sum from continuous values */

            a += age_1 * 5.3451763070030678000000000;
            a += age_2 * -0.0141269980665285430000000;
            a += bmi_1 * 0.4265163581739610500000000;
            a += ratio * 0.1426273862880608500000000;
            a += sysBP * 0.0116526450551129520000000;
            a += townsend * 0.0596311153363571910000000;

            /* Sum from boolean values */

            a += af * 1.2229035784229223000000000;
            a += ra * 0.3270074608538595700000000;
            a += ckd * 0.7820359629541151500000000;
            a += hyp * 0.5481734836311560300000000;
            a += type2 * 0.8590918982324876600000000;
            a += fh * 0.6438314617246578800000000;
            //a += smoker * 0.5408334311058870000000000;

            /* Sum from interaction terms */
            switch (smoker)
            {
                case 1:
                    a += age_1 * 0.5715389084782670500000000;
                    break;
                case 2:
                    a += age_1 * -0.5974287351563502000000000;
                    break;
                case 3:
                    a += age_1 * -1.2334443621176749000000000;
                    break;
                case 4:
                    a += age_1 * -1.7283153973048535000000000;
                    break;
            }
            a += age_1 * af * -3.8277394672781142000000000;
            a += age_1 * ckd * -2.9025720400215884000000000;
            a += age_1 * hyp * -1.9524720689086561000000000;
            a += age_1 * type2 * -1.6599035893675749000000000;
            a += age_1 * bmi_1 * -4.2607595722174354000000000;
            a += age_1 * fh * -0.0843592219007630460000000;
            a += age_1 * sysBP * -0.0187441144599378300000000;
            a += age_1 * townsend * 0.0158368450820470140000000;
            switch (smoker)
            {
                case 1:
                    a += age_2 * -0.0121908132876918270000000;
                    break;
                case 2:
                    a += age_2 * 0.0045599556763008383000000;
                    break;
                case 3:
                    a += age_2 * 0.0127355214979385580000000;
                    break;
                case 4:
                    a += age_2 * 0.0183719223512536080000000;
                    break;
            }
            a += age_2 * af * 0.0411383240996853960000000;
            a += age_2 * ckd * 0.0357111956715128740000000;
            a += age_2 * hyp * 0.0200535596241913360000000;
            a += age_2 * type2 * 0.0163787650971418050000000;
            a += age_2 * bmi_1 * 0.0553349091629203870000000;
            a += age_2 * fh * -0.0100707528328853530000000;
            a += age_2 * sysBP * 0.0000486558504939835630000;
            a += age_2 * townsend * -0.0019506535670581145000000;

            /* Calculate the score itself */
            double score = 100.0 * (1 - Math.Pow(survivor_F[survivor], Math.Exp(a)));
            return score;
        }
    }

}
