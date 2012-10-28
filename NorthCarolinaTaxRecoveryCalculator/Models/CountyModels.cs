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
	    public static readonly int ALAMANCE = 1;
	    public static readonly int ALEXANDER = 2;
	    public static readonly int ALLEGHANY = 3;
	    public static readonly int ANSON = 4;
	    public static readonly int ASHE = 5;
	    public static readonly int AVERY = 6;
	    public static readonly int BEAUFORT = 7;
	    public static readonly int BERTIE = 8;
	    public static readonly int BLADEN = 9;
	    public static readonly int BRUNSWICK = 10;
	    public static readonly int BUNCOMBE = 11;
	    public static readonly int BURKE = 12;
	    public static readonly int CARBARRUS = 13;
	    public static readonly int CALDWELL = 14;
	    public static readonly int CAMDEN = 15;
	    public static readonly int CARTERET = 16;
	    public static readonly int CASWELL = 17;
	    public static readonly int CATAWBA = 18;
	    public static readonly int CHATHAM = 19;
	    public static readonly int CHEROKEE = 20;
	    public static readonly int CHOWAN = 21;
	    public static readonly int CLAY = 22;
	    public static readonly int CLEVELAND = 23;
	    public static readonly int COLUMBUS = 24;
	    public static readonly int CRAVEN = 25;
	    public static readonly int CUMBERLAND = 26;
	    public static readonly int CURRITUCK = 27;
	    public static readonly int DARE = 28;
	    public static readonly int DAVIDSON = 29;
	    public static readonly int DAVIE = 30;
	    public static readonly int DUPLIN = 31;
	    public static readonly int DURHAM = 32;
	    public static readonly int EDGECOMBE = 33;
	    public static readonly int FORSYTH = 34;
	    public static readonly int FRANKLIN = 35;
	    public static readonly int GASTON = 36;
	    public static readonly int GATES = 37;
	    public static readonly int GRAHAM = 38;
	    public static readonly int GRANVILLE = 39;
	    public static readonly int GREENE = 40;
	    public static readonly int GUILFORD = 41;
	    public static readonly int HALIFAX = 42;
	    public static readonly int HARNETT = 43;
	    public static readonly int HAYWOOD = 44;
	    public static readonly int HENDERSON = 45;
	    public static readonly int HERTFORD = 46;
	    public static readonly int HOKE = 47;
	    public static readonly int HYDE = 48;
	    public static readonly int IREDELL = 49;
	    public static readonly int JACKSON = 50;
	    public static readonly int JOHNSTON = 51;
	    public static readonly int JONES = 52;
	    public static readonly int LEE = 53;
	    public static readonly int LENOIR = 54;
	    public static readonly int LINCOLN = 55;
	    public static readonly int MACON = 56;
	    public static readonly int MADISON = 57;
	    public static readonly int MARTIN = 58;
	    public static readonly int MCDOWELL = 59;
	    public static readonly int MECKLENBURG = 60;
	    public static readonly int MITCHELL = 61;
	    public static readonly int MONTGOMERY = 62;
	    public static readonly int MOORE = 63;
	    public static readonly int NASH = 64;
	    public static readonly int NEW_HANOVER = 65;
	    public static readonly int NORTHHAMPTON = 66;
	    public static readonly int ONSLOW = 67;
	    public static readonly int ORANGE = 68;
	    public static readonly int PAMLICO = 69;
	    public static readonly int PASQUOTANK = 70;
	    public static readonly int PENDER = 71;
	    public static readonly int PERQUIMANS = 72;
	    public static readonly int PERSON = 73;
	    public static readonly int PITT = 74;
	    public static readonly int POLK = 75;
	    public static readonly int RANDOLPH = 76;
	    public static readonly int RICHMOND = 77;
	    public static readonly int ROBESON = 78;
	    public static readonly int ROCKINGHAM = 79;
	    public static readonly int ROWAN = 80;
	    public static readonly int RUTHERFORD = 81;
	    public static readonly int SAMPSON = 82;
	    public static readonly int SCOTLAND = 83;
	    public static readonly int STANLEY = 84;
	    public static readonly int STOKES = 85;
	    public static readonly int SURRY = 86;
	    public static readonly int SWAIN = 87;
	    public static readonly int TRANSYLVANIA = 88;
	    public static readonly int TYRRELL = 89;
	    public static readonly int UNION = 90;
	    public static readonly int VANCE = 91;
	    public static readonly int WAKE = 92;
	    public static readonly int WARREN = 93;
	    public static readonly int WASHINGTON = 94;
	    public static readonly int WATAUGA = 95;
	    public static readonly int WAYNE = 96;
	    public static readonly int WILKES = 97;
	    public static readonly int WILSON = 98;
	    public static readonly int YADKIN = 99;
	    public static readonly int YANCEY = 100;
	    public static readonly int NON_TAXABLE = 101;
    }
}
