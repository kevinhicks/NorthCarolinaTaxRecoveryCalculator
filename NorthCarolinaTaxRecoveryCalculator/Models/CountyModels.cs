using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

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
    }
}
