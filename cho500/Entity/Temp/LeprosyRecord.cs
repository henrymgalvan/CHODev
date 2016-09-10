using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cho500.Entity
{
    public class LeprosyRecord
    {
        public enum Bacillary
        {
            PB, MB
        }
        public enum CaseType
        {
            New, Release, RAD, TransIn, ReClassified
        }
        public enum EHFscore
        {
            UponDx, UponTx
        }
        public enum ReactionNoOfEpisodes
        {
            DuringTx, PostTx
        }
        public enum TreatmentOutcomeEnum
        {
            TxCcured, TransOut, Defaulted, Died, Reclassified
        }
        public int Id { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public int CaseNumber { get; set; }
        public int AgeInYears { get; set; }
        public Bacillary Classification { get; set; }
        public DateTime DateStartedTx { get; set; }
        public EHFscore EHFScore { get; set; }
        public DateTime DateForSupervisedDose01 { get; set; }
        public DateTime DateForSupervisedDose02 { get; set; }
        public DateTime DateForSupervisedDose03 { get; set; }
        public DateTime DateForSupervisedDose04 { get; set; }
        public DateTime DateForSupervisedDose05 { get; set; }
        public DateTime DateForSupervisedDose06 { get; set; }
        public DateTime DateForSupervisedDose07 { get; set; }
        public DateTime DateForSupervisedDose08 { get; set; }
        public DateTime DateForSupervisedDose09 { get; set; }
        public DateTime DateForSupervisedDose10 { get; set; }
        public DateTime DateForSupervisedDose11 { get; set; }
        public DateTime DateForSupervisedDose12 { get; set; }
        public DateTime DateForSupervisedDose13 { get; set; }
        public DateTime DateForSupervisedDose14 { get; set; }
        public DateTime DateForSupervisedDose15 { get; set; }
        public DateTime DateForSupervisedDose16 { get; set; }
        public DateTime DateForSupervisedDose17 { get; set; }
        public DateTime DateForSupervisedDose18 { get; set; }
        public DateTime DateForSupervisedDose1 { get; set; }
        public ReactionNoOfEpisodes Reaction { get; set; }
        public TreatmentOutcomeEnum TreatmentOutcome { get; set; }
        public string Remarks { get; set; }

        public int PersonId { get; set; }
    }
}