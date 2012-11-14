using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Script.Serialization;

namespace NorthCarolinaTaxRecoveryCalculator.Models
{
    public class County
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public County() { }
        public County(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        public static string AsJsonArray()
        {
            string json = "[";            
            for (int i = 0; i < Counties.Length; i++)
            {
                if (i > 0)
                    json += ",";
                json += "'" + Counties[i].Name + "' ";
            }

            return json + "]";
        }

        public static County[] Counties = {
            new County(),
            new County(1, "Alamance"),
            new County(2, "Alexander"),
            new County(3, "Alleghany"),
            new County(4, "Anson"),
            new County(5, "Ashe"),
            new County(6, "Avery"),
            new County(7, "Beaufort"),
            new County(8, "Bertie"),
            new County(9, "Bladen"),
            new County(10, "Brunswick"),
            new County(11, "Buncombe"),
            new County(12, "Burke"),
            new County(13, "Carbarrus"),
            new County(14, "Caldwell"),
            new County(15, "Camden"),
            new County(16, "Carteret"),
            new County(17, "Caswell"),
            new County(18, "Catawba"),
            new County(19, "Chatham"),
            new County(20, "Cherokee"),
            new County(21, "Chowan"),
            new County(22, "Clay"),
            new County(23, "Cleveland"),
            new County(24, "Columbus"),
            new County(25, "Craven"),
            new County(26, "Cumberland"),
            new County(27, "Currituck"),
            new County(28, "Dare"),
            new County(29, "Davidson"),
            new County(30, "Davie"),
            new County(31, "Duplin"),
            new County(32, "Durham"),
            new County(33, "Edgecombe"),
            new County(34, "Forsyth"),
            new County(35, "Franklin"),
            new County(36, "Gaston"),
            new County(37, "Gates"),
            new County(38, "Graham"),
            new County(39, "Granville"),
            new County(40, "Greene"),
            new County(41, "Guilford"),
            new County(42, "Halifax"),
            new County(43, "Harnett"),
            new County(44, "Haywood"),
            new County(45, "Henderson"),
            new County(46, "Hertford"),
            new County(47, "Hoke"),
            new County(48, "Hyde"),
            new County(49, "Iredell"),
            new County(50, "Jackson"),
            new County(51, "Johnston"),
            new County(52, "Jones"),
            new County(53, "Lee"),
            new County(54, "Lenoir"),
            new County(55, "Lincoln"),
            new County(56, "Macon"),
            new County(57, "Madison"),
            new County(58, "Martin"),
            new County(59, "McDowell"),
            new County(60, "Mecklenburg"),
            new County(61, "Mitchell"),
            new County(62, "Montgomery"),
            new County(63, "Moore"),
            new County(64, "Nash"),
            new County(65, "New Hanover"),
            new County(66, "Northhampton"),
            new County(67, "Onslow"),
            new County(68, "Orange"),
            new County(69, "Pamlico"),
            new County(70, "Pasquotank"),
            new County(71, "Pender"),
            new County(72, "Perquimans"),
            new County(73, "Person"),
            new County(74, "Pitt"),
            new County(75, "Polk"),
            new County(76, "Randolph"),
            new County(77, "Richmond"),
            new County(78, "Robeson"),
            new County(79, "Rockingham"),
            new County(80, "Rowan"),
            new County(81, "Rutherford"),
            new County(82, "Sampson"),
            new County(83, "Scotland"),
            new County(84, "Stanley"),
            new County(85, "Stokes"),
            new County(86, "Surry"),
            new County(87, "Swain"),
            new County(88, "Transylvania"),
            new County(89, "Tyrrell"),
            new County(90, "Union"),
            new County(91, "Vance"),
            new County(92, "Wake"),
            new County(93, "Warren"),
            new County(94, "Washington"),
            new County(95, "Watauga"),
            new County(96, "Wayne"),
            new County(97, "Wilkes"),
            new County(98, "Wilson"),
            new County(99, "Yadkin"),
            new County(100, "Yancey"),
            new County(101, "Non-Taxable")
        };

        /** County ids. */
	    public const int ALAMANCE = 1;
	    public const int ALEXANDER = 2;
	    public const int ALLEGHANY = 3;
	    public const int ANSON = 4;
	    public const int ASHE = 5;
	    public const int AVERY = 6;
	    public const int BEAUFORT = 7;
	    public const int BERTIE = 8;
	    public const int BLADEN = 9;
	    public const int BRUNSWICK = 10;
	    public const int BUNCOMBE = 11;
	    public const int BURKE = 12;
	    public const int CARBARRUS = 13;
	    public const int CALDWELL = 14;
	    public const int CAMDEN = 15;
	    public const int CARTERET = 16;
	    public const int CASWELL = 17;
	    public const int CATAWBA = 18;
	    public const int CHATHAM = 19;
	    public const int CHEROKEE = 20;
	    public const int CHOWAN = 21;
	    public const int CLAY = 22;
	    public const int CLEVELAND = 23;
	    public const int COLUMBUS = 24;
	    public const int CRAVEN = 25;
	    public const int CUMBERLAND = 26;
	    public const int CURRITUCK = 27;
	    public const int DARE = 28;
	    public const int DAVIDSON = 29;
	    public const int DAVIE = 30;
	    public const int DUPLIN = 31;
	    public const int DURHAM = 32;
	    public const int EDGECOMBE = 33;
	    public const int FORSYTH = 34;
	    public const int FRANKLIN = 35;
	    public const int GASTON = 36;
	    public const int GATES = 37;
	    public const int GRAHAM = 38;
	    public const int GRANVILLE = 39;
	    public const int GREENE = 40;
	    public const int GUILFORD = 41;
	    public const int HALIFAX = 42;
	    public const int HARNETT = 43;
	    public const int HAYWOOD = 44;
	    public const int HENDERSON = 45;
	    public const int HERTFORD = 46;
	    public const int HOKE = 47;
	    public const int HYDE = 48;
	    public const int IREDELL = 49;
	    public const int JACKSON = 50;
	    public const int JOHNSTON = 51;
	    public const int JONES = 52;
	    public const int LEE = 53;
	    public const int LENOIR = 54;
	    public const int LINCOLN = 55;
	    public const int MACON = 56;
	    public const int MADISON = 57;
	    public const int MARTIN = 58;
	    public const int MCDOWELL = 59;
	    public const int MECKLENBURG = 60;
	    public const int MITCHELL = 61;
	    public const int MONTGOMERY = 62;
	    public const int MOORE = 63;
	    public const int NASH = 64;
	    public const int NEW_HANOVER = 65;
	    public const int NORTHHAMPTON = 66;
	    public const int ONSLOW = 67;
	    public const int ORANGE = 68;
	    public const int PAMLICO = 69;
	    public const int PASQUOTANK = 70;
	    public const int PENDER = 71;
	    public const int PERQUIMANS = 72;
	    public const int PERSON = 73;
	    public const int PITT = 74;
	    public const int POLK = 75;
	    public const int RANDOLPH = 76;
	    public const int RICHMOND = 77;
	    public const int ROBESON = 78;
	    public const int ROCKINGHAM = 79;
	    public const int ROWAN = 80;
	    public const int RUTHERFORD = 81;
	    public const int SAMPSON = 82;
	    public const int SCOTLAND = 83;
	    public const int STANLEY = 84;
	    public const int STOKES = 85;
	    public const int SURRY = 86;
	    public const int SWAIN = 87;
	    public const int TRANSYLVANIA = 88;
	    public const int TYRRELL = 89;
	    public const int UNION = 90;
	    public const int VANCE = 91;
	    public const int WAKE = 92;
	    public const int WARREN = 93;
	    public const int WASHINGTON = 94;
	    public const int WATAUGA = 95;
	    public const int WAYNE = 96;
	    public const int WILKES = 97;
	    public const int WILSON = 98;
	    public const int YADKIN = 99;
	    public const int YANCEY = 100;
	    public const int NON_TAXABLE = 101;
    }
}
